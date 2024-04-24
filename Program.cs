using System.Security.Cryptography.X509Certificates;

string senha = "";
string base64 = "";

byte[] certificadoPFX = CertificadoPFX.GerarCertificadoPFX(base64, senha);
File.WriteAllBytes("certificado.pfx", certificadoPFX);

string base64Certificado = CertificadoPFX.GerarBase64DoCertificado("certificado.pfx", senha);

Console.WriteLine($"Deu certo?: {base64 == base64Certificado}");

public class CertificadoPFX
{
    public static byte[] GerarCertificadoPFX(string base64, string senha)
    {
        // Decodificar a string Base64 em um array de bytes
        byte[] bytesBase64 = Convert.FromBase64String(base64);

        // Carregar o certificado a partir do array de bytes
        X509Certificate2 certificado = new X509Certificate2(bytesBase64, senha);

        // Exportar o certificado para o formato PFX
        byte[] certificadoPFX = certificado.Export(X509ContentType.Pfx, senha);

        return certificadoPFX;
    }

    public static string GerarBase64DoCertificado(string caminhoCertificado, string senha)
    {
        // Carregar o certificado a partir do arquivo PFX e da senha
        X509Certificate2 certificado = new X509Certificate2(caminhoCertificado, senha);

        // Exportar o certificado como um array de bytes
        byte[] bytesCertificado = certificado.Export(X509ContentType.Cert);

        // Gerar a string Base64 do certificado
        string base64Certificado = Convert.ToBase64String(bytesCertificado);

        return base64Certificado;
    }
}
