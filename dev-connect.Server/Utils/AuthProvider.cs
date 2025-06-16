
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using dev_connect.Server.Data.Entities.AuthToken;
using dev_connect.Server.Services.Cache;
using static dev_connect.Server.Data.Exceptions.DominExceptions;

namespace dev_connect.Server.Utils
{
    public class AuthProvider
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            var cacheService = new CacheService(new MemoryCache(new MemoryCacheOptions()));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetValue<string>("JWTSecret"))),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context =>
                    {
                        var principal = context.Principal;
                        var userId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                        var jwtUniqueId = principal.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;
                        var expClaim = principal.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Exp)?.Value;

                        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(jwtUniqueId) || string.IsNullOrEmpty(expClaim))
                        {
                            throw new SecurityTokenException("Token does not contain required claims");
                        }

                        // Parse the expiration time once
                        var tokenExpiry = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expClaim)).UtcDateTime;

                        // Fetch cached token
                        var cachedToken = cacheService.GetCache<AuthToken>(userId);

                        if (cachedToken == null)
                        {
                            // If token not found in cache, add it
                            cachedToken = new AuthToken
                            {
                                UserName = userId,
                                JwtUniqueId = jwtUniqueId,
                                TokenExpiry = tokenExpiry
                            };
                            cacheService.SetCache(userId, cachedToken, CacheSetting.cacheForThreeHourAndExtendByHalfAndHour);
                        }
                        else if (cachedToken.JwtUniqueId != jwtUniqueId)
                        {
                            // If the cached JWT ID is different, compare expiry times
                            if (cachedToken.TokenExpiry > tokenExpiry)
                            {
                                throw new InvalidAuthException("Token is invalid or session has expired");
                            }
                            else
                            {
                                // Update cache with new token details
                                cachedToken.JwtUniqueId = jwtUniqueId;
                                cachedToken.TokenExpiry = tokenExpiry;
                                cacheService.SetCache(userId, cachedToken, CacheSetting.cacheForThreeHourAndExtendByHalfAndHour);
                            }
                        }
                    }
                };
            });

        }
    }
}
