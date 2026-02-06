using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace OpenIddictCertificateGenerator
{
    public class CertificateGenerator(IConfiguration configuration)
    {
        private readonly IConfiguration _configuration = configuration;

        public void GenerateEncryptionCertificate()
        {
            var encryptionConfig = _configuration.GetSection("Certificates:Encryption");
            var fileName = encryptionConfig["FileName"];
            var password = encryptionConfig["Password"];
            var distinguishedName = encryptionConfig["DistinguishedName"];

            if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(distinguishedName))
            {
                throw new InvalidOperationException("Encryption certificate configuration is incomplete.");
            }

            using var algorithm = RSA.Create(keySizeInBits: 2048);
            var subject = new X500DistinguishedName(distinguishedName);
            var request = new CertificateRequest(
                subject, algorithm, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            request.CertificateExtensions.Add(
                new X509KeyUsageExtension(X509KeyUsageFlags.KeyEncipherment, critical: true));

            var certificate = request.CreateSelfSigned(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddYears(2));

            File.WriteAllBytes(fileName,
                certificate.Export(X509ContentType.Pfx, password));
        }

        public void GenerateSigningCertificate()
        {
            var signingConfig = _configuration.GetSection("Certificates:Signing");
            var fileName = signingConfig["FileName"];
            var password = signingConfig["Password"];
            var distinguishedName = signingConfig["DistinguishedName"];

            if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(distinguishedName))
            {
                throw new InvalidOperationException("Signing certificate configuration is incomplete.");
            }

            using var algorithm = RSA.Create(keySizeInBits: 2048);
            var subject = new X500DistinguishedName(distinguishedName);
            var request = new CertificateRequest(
                subject, algorithm, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            request.CertificateExtensions.Add(
                new X509KeyUsageExtension(X509KeyUsageFlags.DigitalSignature, critical: true));

            var certificate = request.CreateSelfSigned(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddYears(2));

            File.WriteAllBytes(fileName,
                certificate.Export(X509ContentType.Pfx, password));
        }
    }
}
