using System.Security.Cryptography;
using System.Text;

  public static class EncryptionHelper
    {

         static string EncryptAesManaged(string raw, string password, byte[] iv)
        {
            byte[] Key = Encoding.UTF8.GetBytes(password);
            AesManaged aes = new AesManaged();
            aes.Key = Key;
            aes.IV = iv;
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(),
                CryptoStreamMode.Write);
            byte[] inputBytes = Encoding.UTF8.GetBytes(raw);
            cs.Write(inputBytes, 0, inputBytes.Length);
            cs.FlushFinalBlock();
            byte[] encr = ms.ToArray();
            return Convert.ToBase64String(encr);
        }

        public static string Decrypt(string raw, string password, byte[] iv)
        {
            byte[] Key = Encoding.UTF8.GetBytes(password);
            AesManaged aes = new AesManaged();
            aes.Key = Key;
            aes.IV = iv;
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write);
            byte[] inputBytes = Convert.FromBase64String(raw);
            cs.Write(inputBytes, 0, inputBytes.Length);
            cs.FlushFinalBlock();
            byte[] encr = ms.ToArray();
            return UTF8Encoding.UTF8.GetString(encr, 0, encr.Length);
        }
    }
