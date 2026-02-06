using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var certificateGenerator = new OpenIddictCertificateGenerator.CertificateGenerator(configuration);

certificateGenerator.GenerateEncryptionCertificate();
Console.WriteLine("Encryption certificate generated successfully.");

certificateGenerator.GenerateSigningCertificate();
Console.WriteLine("Signing certificate generated successfully.");
