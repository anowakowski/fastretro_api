using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;


namespace Fastretro.Infrastructure.Authentication
{
    public class FirebaseIssuerKeyProvider
    {
        private readonly string _requestUri;

        public FirebaseIssuerKeyProvider(string requestUri)
        {
            _requestUri = requestUri;
        }

        public async Task<IEnumerable<X509SecurityKey>> GetSigningKeys()
        {
            var client = new HttpClient();
            var jsonResult = await client.GetStringAsync(_requestUri);

            var issuerSigningKeys = JObject
                .Parse(jsonResult)
                .Children()
                .Cast<JProperty>()
                .Select(i => new FirebaseIssuerX509Metadata(i.Path, i.Value.ToString()))
                .Select(m => m.ToX509SecurityKey());

            return issuerSigningKeys.ToList();
        }
    }
}
