using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StudentApi.Models;
using System.Net.Http.Headers;
//using StudentApi.Repository.Context;
using System.Security.Cryptography;
using System.Text;
using WebApi;
using WebApi.IRepo;
using WebApi.Models;
using WebApi.Repository;
using WebApi.Security;
 namespace StudentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
     
    public class StudentUsingDapperController : ControllerBase
    {
        
        private readonly IConfiguration _configuration;
        
        
        StudentDapperRepo _repo; 
        AppSettings _app;
        public StudentUsingDapperController(StudentDapperRepo repo , IConfiguration configuration) //, StudentDbContext context, IDataProtectionProvider dataProtector)
        {
            

            
            _configuration = configuration;
             _repo= repo;
             }

      
        [HttpGet]
        public IActionResult Get()
        {

             var students = _repo.GetStudents();
                 return Ok(students);

        }

        //[Authorize(Roles ="Admin,Manager")]
        //[Authorize(Policy ="AdminAndManager")]
        [HttpGet("{id}")]
        public IActionResult GetStudentById(int id)
        {
             
            return Ok(_repo.GetStudentById(id)); 
             }

        [HttpPost]
        ////[Authorize(Roles = "Admin,Manager")]
        public IActionResult AddStudent([FromForm] Student student)
        {
            string sFolderPath;
            sFolderPath = "C:/Documents";
            var file = Request.Form.Files[0];
            StringBuilder result = new StringBuilder();
            var reader = new StreamReader(file.OpenReadStream());

            result.AppendLine(reader.ReadToEnd());

            Directory.CreateDirectory(sFolderPath + "\\user");
            var pathToSave = sFolderPath + "\\user";
            //var pathToSave = Path.Combine(Directory.GetCurrentDirectory() + folderName + "\\user");
            
            if (file.Length > 0)
            {
                //Stream str = file.OpenReadStream();

                 var encryptedBytes = Encoding.ASCII.GetBytes(result.ToString());
                
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);
                //var dbPath = Path.Combine(folderName, fileName); 
                student.AdharFilePath = fullPath;
                //System.IO.File.Create(fullPath);
                //string ff = CipherHelp.Encrypt(encryptedBytes, "tytytytytytytytytytytytytyty");
                System.IO.File.WriteAllText(fullPath, result.ToString());

                //CipherHelper.Encrypt(encryptedBytes)

                // using (FileStream stream = new(fullPath,FileMode.Create))
                //{

                //    file.CopyTo(new MemoryStream(encryptedBytes));
                //}
               _repo.AddStudent(student);
                return CreatedAtAction(nameof(AddStudent), student);
            }
            else
                return BadRequest();


           
        }

        //[HttpDelete("{id}")]
        //public void DeleteStudent(int id)
        //{
        //    var obj = _context.Students.SingleOrDefault(x => x.Id == id);
        //    if(obj != null)
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


        //[HttpPatch("{id}")]
        //public IActionResult EditPartialStudent(int id, JsonPatchDocument<Student> patchDocument)
        //{
        //    if (patchDocument == null || id < 1) return BadRequest();
        //    var obj = _context.Students.SingleOrDefault(x => x.Id == id);
        //    if (obj != null)
        //    {
        //        var temp = new Student
        //        {
        //            Id = obj.Id,
        //            Name = obj.Name,
        //            Address = obj.Address,
        //            Marks = obj.Marks
        //        };
        //        patchDocument.ApplyTo(temp, ModelState);
        //        if (!ModelState.IsValid) { return BadRequest(); }
        //        else
        //            obj.Name = temp.Name;
        //        obj.Address = temp.Address;
        //        obj.Marks = temp.Marks;
        //        _context.SaveChanges();
        //        return Ok();
        //    }
        //    else
        //        return NotFound();
        //}
    }
}
