//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.DataProtection;
//using Microsoft.AspNetCore.Http;
////using Microsoft.AspNetCore.JsonPatch;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using StudentApi.Models;
//using System.Net.Http.Headers;
////using StudentApi.Repository.Context;
//using System.Security.Cryptography;
//using System.Text;
//using WebApi;
//using WebApi.IRepo;
//using WebApi.Models;
//using WebApi.Security;
//namespace StudentApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]

//    public class StudentRefreshTokenController : ControllerBase
//    {
//        private readonly IConfiguration _configuration;

//        AuthenticateManager1 _manager;
//        IStudentRepo _repo;
//        public StudentRefreshTokenController(IStudentRepo repo,
//            IConfiguration configuration) //, StudentDbContext context, IDataProtectionProvider dataProtector)
//        {
//            _configuration = configuration;
//            _repo = repo;
//        }


//        //[HttpGet]
//        //public IActionResult Get()
//        //{


//        //     return _repo.GetStudents().ToList();

//        //}

//        //[Authorize(Roles ="Admin,Manager")]
//        [Authorize(Policy = "AdminAndManager")]
//        [HttpGet("{id}")]
//        public IActionResult GetStudentById(int id)
//        {

//            return Ok(_repo.GetStudentById(id));
//        }

//        //[HttpPost]
//        //[Authorize(Roles = "Admin,Manager")]
//        //public IActionResult AddStudent([FromForm] Student student)
//        //{
//        //    string sFolderPath;
//        //    sFolderPath = "C:/Documents";
//        //    var file = Request.Form.Files[0];
//        //    StringBuilder result = new StringBuilder();
//        //    var reader = new StreamReader(file.OpenReadStream());

//        //    result.AppendLine(reader.ReadToEnd());

//        //    Directory.CreateDirectory(sFolderPath + "\\user");
//        //    var pathToSave = sFolderPath + "\\user";
//        //    //var pathToSave = Path.Combine(Directory.GetCurrentDirectory() + folderName + "\\user");

//        //    if (file.Length > 0)
//        //    {
//        //        //Stream str = file.OpenReadStream();

//        //        var encryptedBytes = Encoding.ASCII.GetBytes(result.ToString());

//        //        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
//        //        var fullPath = Path.Combine(pathToSave, fileName);
//        //        //var dbPath = Path.Combine(folderName, fileName); 
//        //        student.AdharFilePath = fullPath;
//        //        //System.IO.File.Create(fullPath);
//        //        string ff = CipherHelper.Encrypt(encryptedBytes, "tytytytytytytytytytytytytyty");
//        //        System.IO.File.WriteAllText(fullPath, ff);

//        //        //CipherHelper.Encrypt(encryptedBytes)

//        //        // using (FileStream stream = new(fullPath,FileMode.Create))
//        //        //{

//        //        //    file.CopyTo(new MemoryStream(encryptedBytes));
//        //        //}
//        //        _repo.AddStudent(student);
//        //        return CreatedAtAction(nameof(AddStudent), student);
//        //    }
//        //    else
//        //        return BadRequest();



//        //}

//    }
//}
