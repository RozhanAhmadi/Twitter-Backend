using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterServer.Data;
using TwitterServer.Models.Dto.Account;
using TwitterServer.Models.Dto.UserDto;
using Microsoft.EntityFrameworkCore;
using TwitterServer.Models.Entity;
using System.Security.Claims;
using TwitterServer.Utilities;
using CommonObjects.Utilities;
using TwitterServer.Exceptions;

namespace TwitterServer.Commands.UserCommands
{
    public class SignInUserCommand : ISignInUserCommand
    {
        private readonly AppDbContext _dbContext;
        private readonly IJsonWebTokenService _jsonWebTokenService;
        private readonly JsonWebTokenSettings _jsonWebTokenSettings;
        public SignInUserCommand(AppDbContext dbContext, 
                                 IJsonWebTokenService jsonWebTokenService,
                                 JsonWebTokenSettings jsonWebTokenSettings)
        {
            _dbContext = dbContext;
            _jsonWebTokenService = jsonWebTokenService;
            _jsonWebTokenSettings = jsonWebTokenSettings;
        }

        public async Task<TokenResponse> SignInUserHandler(SignInUserDto request)
        {
            var user = await _dbContext.Users.Where(p => p.Username == request.UserEmail.ToLower()).Select(p =>
                    new UserEntity()
                    {
                        Id = p.Id,
                        Username = p.Username,
                        Password = p.Password,
                        Email = p.Email,

                    }).SingleOrDefaultAsync();

            if (user == null)
            {
                user = await _dbContext.Users.Where(p => p.Email == request.UserEmail.ToLower()).Select(p =>
                    new UserEntity()
                    {
                        Id = p.Id,
                        Username = p.Username,
                        Password = p.Password,
                        Email = p.Email,

                    }).SingleOrDefaultAsync();

                if (user == null)
                {
                    throw new TwitterApiException(400,"Invalid useremail");
                }

            }

            if(user.Password != request.Password.ToLower())
                throw new TwitterApiException(400,"Incorrect password");


            
            var claims = new List<Claim>();
            claims.AddSub(user.Id.ToString());
            claims.AddName(user.Username);

            var token = _jsonWebTokenService.Encode(claims);
            var tokenResponse = new TokenResponse(token, Convert.ToInt32(_jsonWebTokenSettings.Expires.TotalSeconds));
            
            return tokenResponse;
        }
    }
}
