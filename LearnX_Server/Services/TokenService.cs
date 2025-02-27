using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LearnX_Server
{
    public class TokenService : ITokenService
    {
        private readonly string _secretKey;

        public TokenService(IConfiguration configuration)
        {
            _secretKey = configuration["Jwt:SecretKey"] ?? throw new ArgumentNullException("SecretKey is not configured.");
        }

        public string CreateToken(string userId, string email, string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId), // Store GUID as string
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }



        // ✅ Verify JWT Token and Extract User ID (as GUID)
        public Guid VerifyTokenAndGetId(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_secretKey);

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero // Prevents extra allowed time for expired tokens
                };

             
                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

                if (validatedToken is not JwtSecurityToken jwtToken)
                {
                    throw new Exception("Invalid token format.");
                }

                var userIdClaim = principal?.FindFirst(ClaimTypes.NameIdentifier);

                if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
                {
                    throw new Exception("User ID claim not found in token.");
                }

          

                // ✅ Convert User ID from String to Guid Safely
                if (Guid.TryParse(userIdClaim.Value, out Guid userId))
                {
                    return userId;
                }
                else
                {
                    throw new Exception("Invalid GUID format in token.");
                }
            }
            catch (SecurityTokenExpiredException)
            {
                throw new Exception("Token has expired.");
            }
            catch (SecurityTokenInvalidSignatureException)
            {
                throw new Exception("Token signature is invalid.");
            }
            catch (Exception ex)
            {
                throw new Exception("Token validation failed: " + ex.Message);
            }
        }
    }
}
