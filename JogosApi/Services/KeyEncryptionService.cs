using System.Security.Cryptography;
using System.Text;

namespace JogosApi.Services;

public class KeyEncryptionService
{
    private readonly string _chave;

    public KeyEncryptionService(string chave)
    {
        _chave = chave;
    }

    public string Criptografar(string texto)
    {
        using var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(_chave);
        aes.GenerateIV();

        var iv = aes.IV;
        using var encryptor = aes.CreateEncryptor();

        var inputBytes = Encoding.UTF8.GetBytes(texto);
        var encrypted = encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);

        // Junta o IV com o texto criptografado
        var result = new byte[iv.Length + encrypted.Length];
        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
        Buffer.BlockCopy(encrypted, 0, result, iv.Length, encrypted.Length);

        return Convert.ToBase64String(result);
    }

    public string Descriptografar(string textoCriptografado)
    {
        var fullBytes = Convert.FromBase64String(textoCriptografado);

        using var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(_chave);

        // Extrai o IV (16 bytes)
        var iv = new byte[16];
        var ciphertext = new byte[fullBytes.Length - 16];
        Buffer.BlockCopy(fullBytes, 0, iv, 0, iv.Length);
        Buffer.BlockCopy(fullBytes, iv.Length, ciphertext, 0, ciphertext.Length);

        aes.IV = iv;

        using var decryptor = aes.CreateDecryptor();
        var decryptedBytes = decryptor.TransformFinalBlock(ciphertext, 0, ciphertext.Length);

        return Encoding.UTF8.GetString(decryptedBytes);
    }
}
