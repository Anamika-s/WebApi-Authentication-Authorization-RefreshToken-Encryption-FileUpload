using Aes_Example;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Utilities.Zlib;
using StudentApi.Models;
using System;
using System.IO;
using System.Net.Http.Headers;
//using StudentApi.Repository.Context;
using System.Security.Cryptography;
using System.Text;
using WebApi;
using WebApi.IRepo;
using WebApi.Models;
using WebApi.Security;
using static System.Net.WebRequestMethods;

class Test
{
    public static string Decrypt(string cipherText)
    {
        try
        {
            string EncryptionKey = "@Test";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        catch (Exception ex)
        {
            //Log.writeLog(ex);
            return null;
        }
    }
}
namespace StudentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class StudentController : ControllerBase
    {
        private readonly string _environmentName;
        private readonly IConfiguration _configuration;
        private readonly IDataProtector _dataProtector;
        DataProtectionSecuritySettings dataProtectionSecuritySettings;

        IStudentRepo _repo;

        AppSettings _app;
        public StudentController(IStudentRepo repo, IDataProtectionProvider dataProtectionProvider,
          AppSettings app, DataProtectionSecuritySettings dataProtectionSecuritySettings, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment, IConfiguration configuration) //, StudentDbContext context, IDataProtectionProvider dataProtector)
        {
            _app = app;
            _dataProtector = dataProtectionProvider.CreateProtector("testing");
            _repo = repo;
            _environmentName = hostingEnvironment.EnvironmentName;
            _configuration = configuration;
            string encrytedStr = _dataProtector.Protect(@"server=ANAMIKA\SQLSERVER;database=PracticeDatabase1;integrated security=true;TrustServerCertificate=true");
            _configuration["MySecret"] = encrytedStr;

            _dataProtector = dataProtectionProvider.CreateProtector(dataProtectionSecuritySettings.StudentIdRouteValue);
        }


        [HttpGet]
        public IActionResult Get()
        {

            //string encrytedStr = _dataProtector.Protect(@"server=ANAMIKA\SQLSERVER;database=PracticeDatabase1;integrated security=true;TrustServerCertificate=true");

            //string data = _environmentName == "Development" ? _configuration["MySercet"] : _dataProtector.Unprotect(_configuration["MySercet"]);

            //return _context.Students.ToList();
            var students = _repo.GetStudents();
            //var list = students.Select(x => new Student
            //{
            //    EncryptedId = _dataProtector.Protect(x.Id.ToString()),
            //    Name = x.Name,
            //    Marks = x.Marks,
            //    Address = x.Address,

            //    AdharFilePath = x.AdharFilePath,
            //    AdharNumber = x.AdharNumber
            //}); ;
            return Ok(students);

        }
        [Authorize(Policy = "EmployeeNumberofYears")]
        //[Authorize(Roles = "Admin,Manager")]
        //[Authorize(Policy = "AdminAndManager")]
        [HttpGet("{id}")]
        //public IActionResult GetStudentById(string id)
        public IActionResult GetStudentById(int id)
        {
            //int id1 = Convert.ToInt16(_dataProtector.Unprotect(id));
            return Ok(_repo.GetStudentById(id));
        }





        [HttpGet("getdocument/{id}")]


        public  async Task<IActionResult> DownloadFile(string fileName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                "Uploads\\", fileName);
            if (string.IsNullOrEmpty(fileName) || fileName == null)
            {
                return Content("File Name is Empty...");
            }

            FileStream fs = new FileStream(filePath, FileMode.Open);
            //var provider = new FileExtensionContentTypeProvider();
            //if(!provider.TryGetContentType(filePath, out var contentType))
            //{
            //    contentType = "application/octet-stream";
            //}
            //var bytes= System.IO.File.ReadAllText(filePath);
            //string str = CipherHelper.Decrypt(bytes, "tytytytytytytytytytytytytyty");

            //string keyString = "thisisasecuredtestkey123";
            //string str = CipherHelp.DecryptString(bytes,keyString);
 
            var filePath1 = Path.Combine(Directory.GetCurrentDirectory(),
                "Downloads\\Files");
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath1);
            }
            var exactPath = Path.Combine(Directory.GetCurrentDirectory()
            , "Downloads\\Files", fileName);


            // **************************
             
              string encryptionKey = "MAKV2SPBNI99212";
                //byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
                Aes encryptor = Aes.Create();
                
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    CryptoStream cs = new CryptoStream(fs, encryptor.CreateDecryptor(), CryptoStreamMode.Read);
            Response.Headers.Add(HeaderNames.ContentType, "application/octet-stream");
            Response.Headers.Add("Content-Disposition","attachment;filename="+ Path.GetFileName(exactPath) + Path.GetExtension(exactPath));
            int data;
            var outputStream = this.Response.Body;
            const int bufferSize = 1 << 10;

            var buffer = new byte[bufferSize];
            while (true) // (data = cs.ReadByte()) != -1)

            {
                var bytesRead = await cs.ReadAsync(buffer, 0, bufferSize);
                if (bytesRead == 0) break;
                await outputStream.WriteAsync(buffer, 0, bytesRead);
                //await cs.WriteAsync(data, 0 , data);
            }

            //var bytesRead = buffer.ReadByte(buffer, 0, bufferSize);
            outputStream.Flush();
            return Ok();
            //
         

           
            //StringBuilder result = new StringBuilder();
             ////  
            
             
            //System.IO.File.WriteAllText(exactPath, str);
            //return PhysicalFile(exactPath, contentType, Path.GetFileName(exactPath));
            //return  File(str,contentType,Path.GetFileName(fileName));
            // get the filePath

    //        var filePath = Path.Combine(Directory.GetCurrentDirectory(),
    //            "ServerFiles", fileName);


          
            
    //        // create a memorystream
    //        var memoryStream = new MemoryStream();

    //        using (var stream = new FileStream(filePath, FileMode.Open))
    //        {
    //            await stream.CopyToAsync(memoryStream);
    //        }

    //        // set the position to return the file from
    //        memoryStream.Position = 0;

    //        // Get the MIMEType for the File
    //        //var mimeType = (string file) =>
    //        //{
    //        //    //var mimeTypes = MimeTypes.GetMimeTypes();
    //        //    var extension = Path.GetExtension(file).ToLowerInvariant();
    //        //    //return mimeTypes[extension];
    //        //};
    //        if (memoryStream == null)
    //            return NotFound(); // returns a NotFoundResult with Status404NotFound response.
    //           return File(memoryStream, "application/octet-stream", Path.GetFileName(filePath)); // returns a FileStreamResult

    //        //return File(memoryStream, mimeType(filePath), Path.GetFileName(filePath));
       
    ////public IActionResult GetDocumentById(int id)
    ////    {
    ////        //int id1 = Convert.ToInt16(_dataProtector.Unprotect(id));
    ////        return Ok(_repo.GetStudentById(id));
    ////    }

}
        //string AddFile(IFormFile file)
        //{
        //    string file1 = file.FileName;
        //    long length = file1.Length;
        //    if (length > 0)
        //    {
        //        using var fs = file.OpenReadStream();
        //        byte[] bytes = new byte[length];
        //        fs.Read(bytes, 0, (int)bytes.Length);
                
        //    }
        //    string fileName = "";
        //         var extension = "." + file.FileName.Split(".")
        //            [file.FileName.Split('.').Length - 1];
        //        fileName = "Adhar" + DateTime.Now.Ticks.ToString() + extension;
        //        var filePath = Path.Combine(Directory.GetCurrentDirectory(),
        //            "Uploads\\Files");
        //        if (!Directory.Exists(filePath))
        //        {
        //            Directory.CreateDirectory(filePath);
        //        }
        //        var exactPath = Path.Combine(Directory.GetCurrentDirectory()
        //            , "Uploads\\Files", fileName);
        //        StringBuilder result = new StringBuilder();

        //        var reader = new StreamReader(file.OpenReadStream());

        //        result.AppendLine(reader.ReadToEnd());
        //        reader.Close();
        //        ////  
        //        //var encryptedBytes = Encoding.ASCII.GetBytes(result.ToString());
        //        byte[] keyString = Encoding.UTF8.GetBytes("thisisasecuredtestkey123");
        //        string output = Path.Combine(Directory.GetCurrentDirectory()
        //            , "Uploads\\Files" + fileName);
        //        FileStream fs1 = new FileStream(output, FileMode.Create);
        //        RijndaelManaged rijndaelManaged = new RijndaelManaged();
        //        CryptoStream cs = new CryptoStream(fs1, rijndaelManaged.CreateEncryptor(keyString, keyString), CryptoStreamMode.Write);
        //        foreach (var data in result)
        //        {
        //            cs.WriteByte(data);
        //        }
        //        cs.Close();
        //        fs1.Close();

        //        //    string ff = CipherHelp.EncryptString(result.ToString(), keyString);
        //        //    System.IO.File.WriteAllText(exactPath, result.ToString());
        //        //    //using (var stream = new FileStream(exactPath, FileMode.Create))
        //        //    //{
        //        //    //    Request.Form.Files[0].CopyTo(stream);
        //        //    //}

        //        //}
        //        //catch (Exception ex)
        //        //{
        //        //}
        //        return fileName;
        //    }

        //    }
        [HttpPost]
        //[Authorize(Roles = "Admin,Manager")]
        public IActionResult AddStudent([FromForm] Student student)
        {
            IFormFile file = student.AdharFile;
            long length = file.Length;
            if (length > 0)
            {
                using var fs = file.OpenReadStream();
                byte[] bytes = new byte[length];
                fs.Read(bytes, 0, (int)bytes.Length);
                fs.Close();
                string fileName = file.FileName;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "Uploads\\Files\\", file.FileName);

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                //-----------------

                //var numberOfBits = 256; // or 192 or 128, however using a larger bit size renders the encrypted data harder to decipher

                //var keyString = new byte[numberOfBits / 8]; // 8 bits per byte

                //new RNGCryptoServiceProvider().GetBytes(keyString);

                //var rijndaelManagedCipher = new RijndaelManaged();

                ////Don't forget to set the explicitly set the block size for the IV if you're not using the default of 128

                //rijndaelManagedCipher.BlockSize = 128;
                //rijndaelManagedCipher.Key= "@79878978787878978978";

                //rijndaelManagedCipher.IV = keyString;
                //// -----------------------
                //byte[] keyString = Convert.FromBase64String("AAECAwQFBgcICQoLDA0ODw==");
                string output = Path.Combine(Directory.GetCurrentDirectory()
                    , "Uploads\\Files\\" + file.FileName);
                FileStream fs1 = new FileStream(output, FileMode.Create);
                fs1.Write(bytes, 0, bytes.Length);
                fs1.Close();
                //------------------
                //string encryptionKey = "MAKV2SPBNI99212";
                ////byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
                //Aes encryptor = Aes.Create();
                
                //    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                //    encryptor.Key = pdb.GetBytes(32);
                //    encryptor.IV = pdb.GetBytes(16);
                //    CryptoStream cs = new CryptoStream(fs1, encryptor.CreateEncryptor(), CryptoStreamMode.Write);
                ////foreach (var b in bytes)
                ////{
                ////    cs.WriteByte((byte)b);

                ////}
                //byte[] inputBytes = Encoding.UTF8.GetBytes(raw);
                //cs.Write(inputBytes, 0, inputBytes.Length);
                //cs.FlushFinalBlock();
                //byte[] encr = ms.ToArray();
                //return Convert.ToBase64String(encr);

                ////cs.Write(bytes, 0, bytes.Length);
                //cs.Close();
                //    fs1.Close();

 
                    //---
                    //RijndaelManaged rijndaelManaged = new RijndaelManaged();
                    //CryptoStream cs = new CryptoStream(fs1, rijndaelManaged.CreateEncryptor(keyString, keyString), CryptoStreamMode.Write);
                    //foreach (var data in bytes)
                    //{
                    //    cs.WriteByte(data);
                    //}
                    //cs.Close();
                    


            }

            student.AdharFilePath = "a";
            //    string sFolderPath;
            //    sFolderPath = "C:/Documents";
            //    var file = Request.Form.Files[0];
            //    StringBuilder result = new StringBuilder();
            //    var reader = new StreamReader(file.OpenReadStream());

            //    result.AppendLine(reader.ReadToEnd());
            //    reader.Close();
            //    Directory.CreateDirectory(sFolderPath + "\\user");
            //    var pathToSave = sFolderPath + "\\user";
            //    //pathToSave = Path.Combine(Directory.GetCurrentDirectory() + folderName + "\\user");

            //    if (file.Length > 0)
            //    {
            //        Stream str = file.OpenReadStream();

            //        var encryptedBytes = Encoding.ASCII.GetBytes(result.ToString());

            //        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            //        var fullPath = Path.Combine(pathToSave, fileName);
            //        //var dbPath = Path.Combine(folderName, fileName);
            //        student.AdharFilePath = fullPath;
            //        //System.IO.File.Create(fullPath);
            //        string ff = CipherHelper.Encrypt(encryptedBytes, "tytytytytytytytytytytytytyty");
            //        System.IO.File.WriteAllText(fullPath, ff);
            //        str.Close();
            //        //CipherHelper.Encrypt(encryptedBytes)

            //        //using (FileStream stream = new(fullPath, FileMode.Create))
            //        //{

            //        //    file.CopyTo(new MemoryStream(encryptedBytes));
            //        //}
            _repo.AddStudent(student);
            return CreatedAtAction(nameof(AddStudent), student);
        
         }
        //}

        //[HttpDelete("{id}")]
        //public void DeleteStudent(int id)
        //{
        //    var obj = _context.Students.SingleOrDefault(x => x.Id == id);
        //    if (obj != null)
        //    {
        //        _context.Students.Remove(obj);
        //        _context.SaveChanges();
        //    }
        //}


        //[HttpPut("{id}")]
        //public bool EditStudent(int id, Student student)
        //{
        //    var obj = _context.Students.SingleOrDefault(x => x.Id == id);
        //    if (obj != null)
        //    {
        //        obj.Name = student.Name;
        //        obj.Marks = student.Marks;

        //        _context.SaveChanges();
        //        return true;
        //    }
        //    else
        //        return false;
        //}


        //    [HttpPatch("{id}")]
        //    public IActionResult EditPartialStudent(int id, JsonPatchDocument<Student> patchDocument)
        //    {
        //        if (patchDocument == null || id < 1) return BadRequest();
        //        var obj = _context.Students.SingleOrDefault(x => x.Id == id);
        //        if (obj != null)
        //        {
        //            var temp = new Student
        //            {
        //                Id = obj.Id,
        //                Name = obj.Name,
        //                Address = obj.Address,
        //                Marks = obj.Marks
        //            };
        //            patchDocument.ApplyTo(temp, ModelState);
        //            if (!ModelState.IsValid) { return BadRequest(); }
        //            else
        //                obj.Name = temp.Name;
        //            obj.Address = temp.Address;
        //            obj.Marks = temp.Marks;
        //            _context.SaveChanges();
        //            return Ok();
        //        }
        //        else
        //            return NotFound();
        //
        // }
        //}
    }

}