# OpenIddictCertificateGenerator

Console tool that creates self-signed X.509 certificates (PFX files) for development and testing. It produces one encryption certificate (Key Encipherment) and one signing certificate (Digital Signature) using settings supplied in appsettings.json.

Key points
- Generates two PFX files: an encryption certificate and a signing certificate.
- Reads configuration from OpenIddictCertificateGenerator/appsettings.json at runtime.
- Intended for local development and testing only; self-signed certificates are not suitable for production.

Prerequisites
- .NET 8 SDK (or later compatible SDK installed).

Build
- Restore and build the project from the repository root:
  dotnet build OpenIddictCertificateGenerator/OpenIddictCertificateGenerator.csproj

Run
- Run the tool (it will create the configured PFX files in the project output or working directory):
  dotnet run --project OpenIddictCertificateGenerator/OpenIddictCertificateGenerator.csproj

Configuration
- The tool reads certificate settings from OpenIddictCertificateGenerator/appsettings.json. Provide values for FileName, Password and DistinguishedName for each certificate.

Example appsettings.json
{
  "Certificates": {
    "Encryption": {
      "FileName": "encryption-certificate.pfx",
      "Password": "P@ssw0rd",
      "DistinguishedName": "CN=Encryption Certificate"
    },
    "Signing": {
      "FileName": "signing-certificate.pfx",
      "Password": "P@ssw0rd",
      "DistinguishedName": "CN=Signing Certificate"
    }
  }
}

Notes
- The generated PFX files are self-signed and intended for development/testing only.
- Protect the PFX passwords; consider using environment variables or a secrets store for production workflows.
- Adjust key sizes, validity period and other certificate parameters in the source code if different properties are required.

Files of interest
- OpenIddictCertificateGenerator/Program.cs — loads configuration and runs the generator.
- OpenIddictCertificateGenerator/CertificateGenerator.cs — certificate creation logic.

License
- This project is licensed under the MIT License. See the LICENSE.txt file at the repository root for details.