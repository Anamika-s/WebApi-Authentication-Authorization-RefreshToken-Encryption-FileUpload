using BookStoresWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Text;
using WebApi.Models;


namespace WebApi.Repository.Context
{
    public class StudentDbContext : DbContext
    {
        IConfiguration _configuration;
        
        public StudentDbContext(IConfiguration cofig,DbContextOptions<StudentDbContext> options) : base(options) {
            _configuration = cofig;
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<User1> UserList { get; set; }
        //public DbSet<User> User { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<User> Users { get; set; }
        //public DbSet<FileDetails> FileDetails { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                ConnectionStringProtector  p = new ConnectionStringProtector();
                //string str = p.Protect("server=ANAMIKA\\SQLSERVER;database=PracticeDatabase1;integrated security=true;TrustServerCertificate=true");
               string con= string.Empty;
              
                   //string str = EncryptionHelper.Encrypt("jhghjghjgjhghjghjghjghjghgh","aaa","aaa")
                string str = _configuration.GetConnectionString("MyConnection");
                byte[] iv = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                string Password = "1111111111111111";
                //using (AesManaged aes = new AesManaged())
                //{

                //}
                string con1 = EncryptionHelper.Decrypt(str, Password, iv);
                optionsBuilder.UseSqlServer(con1);
                //optionsBuilder.UseSqlServer("server=ANAMIKA\\SQLSERVER;database=PracticeDatabase1;integrated security=true;TrustServerCertificate=true");
            }
        }
        public int GetStudentsByCount()
=> throw new NotSupportedException();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDbFunction(typeof(StudentDbContext)
  .GetMethod(nameof(GetStudentsByCount)
            /* new[] { typeof(int) }*/))
  .HasName("GetStudentsByCount");

            modelBuilder.Entity<Role>().
                HasData(new Role
                {
                    RoleId = 1,
                    RoleName = "Admin"
                },
                new Role
                {
                    RoleId = 2,
                    RoleName = "Manager"
                },
                new Role
                {
                    RoleId = 3,
                    RoleName = "Employee"
                });
            modelBuilder.Entity<User>().
                HasData(new User() 
                {
                    UserId = 100,
                    FirstName = "admin1@gmail.com",
                    LastName = "a",
                    EmailAddress= "admin1@gmail.com",
                    Password = "password",
                    RoleId = 1,
                }, new User

                {
                    UserId = 101,
                    FirstName = "admin2@gmail.com",
                    LastName = "a",
                    EmailAddress = "admin1@gmail.com",
                    Password = "password",
                    RoleId = 2,
                }

                );


            //    }



            //}
        }

    }
}

class A
{
    public static string DecryptStringFromBytes_Aes(string Text)
    {
    byte[] Key = Encoding.ASCII.GetBytes(@"qwr{@^h`h&_`50/ja9!'dcmh3!uw<&=?");
    byte[] IV = Encoding.ASCII.GetBytes(@"9/\~V).A,lY&=t2b");
        if (Text == null || Text.Length <= 0)
            throw new ArgumentNullException("cipherText");
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException("Key");
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException("IV");

        string plaintext = null;
        byte[] cipherText = Convert.FromBase64String(Text.Replace(' ', '+'));

        using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Padding = PaddingMode.PKCS7;


            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }

        }

        return plaintext;
    }
}
//public class FileService  
//{
//    private readonly StudentDbContext dbContextClass;
//    public FileService(StudentDbContext dbContextClass)
//    {
//        this.dbContextClass = dbContextClass;
//    }
//    public async Task PostFileAsync(IFormFile fileData, FileType fileType)
//    {
//        try
//        {
//            var fileDetails = new FileDetails()
//            {
//                ID = 0,
//                FileName = fileData.FileName,
//                FileType = fileType,
//            };
//            using (var stream = new MemoryStream())
//            {
//                fileData.CopyTo(stream);
//                fileDetails.FileData = stream.ToArray();
//            }
//            var result = dbContextClass.FileDetails.Add(fileDetails);
//            await dbContextClass.SaveChangesAsync();
//        }
//        catch (Exception)
//        {
//            throw;
//        }
//    }
//    public async Task PostMultiFileAsync(List<FileUploadModel> fileData)
//    {
//        try
//        {
//            foreach (FileUploadModel file in fileData)
//            {
//                var fileDetails = new FileDetails()
//                {
//                    ID = 0,
//                    FileName = file.FileDetails.FileName,
//                    FileType = file.FileType,
//                };
//                using (var stream = new MemoryStream())
//                {
//                    file.FileDetails.CopyTo(stream);
//                    fileDetails.FileData = stream.ToArray();
//                }
//                var result = dbContextClass.FileDetails.Add(fileDetails);
//            }
//            await dbContextClass.SaveChangesAsync();
//        }
//        catch (Exception)
//        {
//            throw;
//        }
//    }
//    public async Task DownloadFileById(int Id)
//    {
//        try
//        {
//            var file = dbContextClass.FileDetails.Where(x => x.ID == Id).FirstOrDefaultAsync();
//            var content = new System.IO.MemoryStream(file.Result.FileData);
//            var path = Path.Combine(
//               Directory.GetCurrentDirectory(), "FileDownloaded",
//               file.Result.FileName);
//            await CopyStream(content, path);
//        }
//        catch (Exception)
//        {
//            throw;
//        }
//    }
//    public async Task CopyStream(Stream stream, string downloadPath)
//    {
//        using (var fileStream = new FileStream(downloadPath, FileMode.Create, FileAccess.Write))
//        {
//            await stream.CopyToAsync(fileStream);
//        }
//    }
//}