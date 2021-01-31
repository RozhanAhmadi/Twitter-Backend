using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterServer.Commands.UserCommands;
using Microsoft.EntityFrameworkCore;
using TwitterServer.Data;
using CommonObjects.Utilities;
using WebApi.Middlewares;
using Swashbuckle.AspNetCore.Filters;
using TwitterServer.Commands.UserCommands.Interfaces;
using TwitterServer.Commands.TweetCommands;
using TwitterServer.Commands;
using TwitterServer.Commands.InfoCommands;

namespace TwitterServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddCors();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TwitterServer", Version = "v1" });
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",

                });
                c.OperationFilter<SecurityRequirementsOperationFilter>();
            });
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
           // services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddTransient<IAddUserCommand, AddUserCommand>();
            services.AddScoped<ISignInUserCommand, SignInUserCommand>();
            services.AddScoped<IEditUserCommand, EditUserCommand>();
            services.AddScoped<IGetUserCommand, GetUserCommand>();
            services.AddScoped<IFollowUserCommand, FollowUserCommand>();
            services.AddScoped<ITweetActionCommand, TweetActionCommand>();
            services.AddScoped<IGetTweetCommand, GetTweetCommand>();
            services.AddScoped<IInfoCommand, InfoCommand>();

            services.AddJsonWebToken(
                key: Configuration.GetSection("JwtKey").Get<string>(),
                expires: TimeSpan.FromHours(5));

            services.AddAuthenticationJwtBearer();
            services.AddHttpContextAccessor();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseMiddleware<ApiExceptionHandlingMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TwitterServer v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
        }
    }
}
