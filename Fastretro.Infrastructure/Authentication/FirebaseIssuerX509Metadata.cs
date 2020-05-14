using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.IdentityModel.Tokens;

namespace Fastretro.Infrastructure.Authentication
{
    public class FirebaseIssuerX509Metadata
    {
        private string _kId;
        private readonly string _rawCertificate;

        public FirebaseIssuerX509Metadata(string kid, string rawCertificate)
        {
            _kId = kid;
            _rawCertificate = rawCertificate;
        }

        public X509SecurityKey ToX509SecurityKey()
        {
            var lines = _rawCertificate.Split('\n');
            var selectedLines = lines.Skip(1).Take(lines.Length - 3);
            var base64CertificateKey = string.Join(Environment.NewLine, selectedLines);

            return new X509SecurityKey(new X509Certificate2(Convert.FromBase64String(base64CertificateKey)));
        }
    }
}
