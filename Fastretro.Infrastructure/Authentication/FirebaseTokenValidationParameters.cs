using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;

namespace Fastretro.Infrastructure.Authentication
{
    public class FirebaseTokenValidationParameters : TokenValidationParameters
    {
        public FirebaseTokenValidationParameters(string projectId, IEnumerable<X509SecurityKey> issuerSigningKeys, string validIssuer)
        {
            ValidateIssuerSigningKey = true;
            IssuerSigningKeys = issuerSigningKeys;
            ValidateIssuer = true;
            ValidIssuer = validIssuer;
            ValidateAudience = true;
            ValidAudience = projectId;
            ValidateLifetime = true;
        }
    }
}
