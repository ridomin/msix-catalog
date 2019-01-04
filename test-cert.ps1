New-SelfSignedCertificate -Subject "CN=Ricardo Minguez, O=Ricardo Minguez, L=Redmond, S=Washington, C=US" `
-FriendlyName ridoFake `
-Type CodeSigning `
-KeyUsage DigitalSignature `
-KeyLength 2048 `
-KeyAlgorithm RSA `
-HashAlgorithm SHA256 `
-TextExtension @('2.5.29.37={text}1.3.6.1.5.5.7.3.3', '2.5.29.19={text}Subject Type:End Entity') `
-Provider "Microsoft Enhanced RSA and AES Cryptographic Provider" `
-CertStoreLocation "Cert:\CurrentUser\My" 