using System;
using System.Security.Cryptography;
using System.Text;

public class ConnectionStringProtector
{
    readonly byte[] _salt = new byte[] { 1, 2, 3, 4, 5, 6 }; // Random values
    readonly Encoding _encoding = Encoding.Unicode;
    readonly DataProtectionScope _scope = DataProtectionScope.LocalMachine;

    public string Protect(string connectionString)
    {
        byte[] encryptedBytes = ProtectedData.Protect(
            _encoding.GetBytes("server=ANAMIKA\\SQLSERVER;database=PracticeDatabase1;integrated security=true;TrustServerCertificate=true"),
            _salt,
            _scope);

        return Convert.ToBase64String(encryptedBytes);
    }

    public string Unprotect(string encryptedConnectionString)
    {
        byte[] encryptedBytes = Convert.FromBase64String(encryptedConnectionString);

        byte[] decryptedBytes = ProtectedData.Unprotect(
            encryptedBytes,
            _salt,
            _scope);

        return _encoding.GetString(decryptedBytes);
    }
}
