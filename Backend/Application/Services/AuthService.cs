using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public static class AuthService
    {
        public static async Task<SecurityTokenDescriptor> CreateTokenInternalLoginAsync(
            User user,
            IList<string> roles,
            UserManager<User> userManager,
            string password)
        {
            // Load and validate environment variables
            var secret = Environment.GetEnvironmentVariable("secret") ?? string.Empty;
            var site = Environment.GetEnvironmentVariable("site") ?? string.Empty;
            var audience = Environment.GetEnvironmentVariable("audience") ?? string.Empty;

            // Check password asynchronously
            var isPasswordValid = await userManager.CheckPasswordAsync(user, password);
            if (!isPasswordValid)
                return null;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            string username = user.UserName ?? string.Empty;

            // Create claims
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, username),
                new Claim("logged_on", DateTime.UtcNow.ToString("o")) // ISO 8601 format
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            // Build token descriptor
            return new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                Issuer = site,
                Audience = audience,
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            };
        }
    }
}
