using DigitalBankWCF.Request;
using DigitalBankWCF.Response;
using DigitalBankWCF.Utilities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace DigitalBankWCF.JWT
{
    public class GenerarToken
    {

        public static string GenerateToken(LoginJWT cedenciales)
        {

            string secretKey = Configuraciones.secretKeyJWT;

            byte[] keyBytes = Encoding.UTF8.GetBytes(secretKey);

            using (var hmac = new HMACSHA256(keyBytes))
            {
                byte[] key = hmac.Key;
                var securityKey = new SymmetricSecurityKey(key);
                var credentialss = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, cedenciales.Username),
                };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = credentialss
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                string accessToken = tokenHandler.WriteToken(token);

                return accessToken;
                
            }
            
        }

        public static AuthenticationResult Authenticate()
        {
            var httpRequest = (HttpRequestMessageProperty)OperationContext.Current.IncomingMessageProperties[HttpRequestMessageProperty.Name];
            string token = httpRequest.Headers["Authorization"];

            if (string.IsNullOrEmpty(token) || !token.StartsWith("Bearer "))
            {
                throw new SecurityTokenValidationException("Token is missing or invalid");
            }

            string jwtToken = token.Substring("Bearer ".Length).Trim();

            string secretKey = Configuraciones.secretKeyJWT;
            byte[] keyBytes = Encoding.UTF8.GetBytes(secretKey);
            var securityKey = new SymmetricSecurityKey(keyBytes);


            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,
                ValidateIssuer = false,
                ValidIssuer = "http://localhost:54677/",
                ValidateAudience = false,
                ValidAudience = "http://localhost:54677/",
                ValidateLifetime = true
            };

            try
            {
                var claimsPrincipal = tokenHandler.ValidateToken(jwtToken, validationParameters, out _);

                if (claimsPrincipal.Identity.IsAuthenticated)
                {
                    return new AuthenticationResult
                    {
                        IsAuthenticated = true,
                        Username = claimsPrincipal.Identity.Name,
                        Roles = claimsPrincipal.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList()
                    };
                }
                else
                {
                    return new AuthenticationResult
                    {
                        IsAuthenticated = false,
                        ErrorMessage = "Authentication failed"
                    };
                }
            }
            catch (SecurityTokenValidationException ex)
            {
                return new AuthenticationResult
                {
                    IsAuthenticated = false,
                    ErrorMessage = "Authentication failed"
                };
                throw new FaultException(ex.Message);
            }
        }
    }

}