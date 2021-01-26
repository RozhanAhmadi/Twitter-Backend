using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TwitterServer.Utilities
{
    public static class ClaimExtensions
    {
        public static void AddSub(this ICollection<Claim> claims, string sub)
        {
            claims.Add(new Claim("sub", sub));
        }
        public static void AddName(this ICollection<Claim> claims, string userName)
        {
            claims.Add(new Claim(ClaimTypes.Name, userName));
        }
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
