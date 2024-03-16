using Microsoft.AspNetCore.DataProtection;
using WebApi.IRepo;
using WebApi.Models;
//using WebApi.Models;
using WebApi.Repository.Context;
using WebApi.Security;

namespace WebApi.Repository
{
    public class StudentRepo : IStudentRepo
    {

        private readonly IDataProtector _dataProtector;
        DataProtectionSecuritySettings dataProtectionSecuritySettings;


        StudentDbContext _context;
        public StudentRepo(StudentDbContext context, 
            IDataProtectionProvider dataProtectionProvider,
            DataProtectionSecuritySettings dataProtectionSecuritySettings)
        {
            _context = context;
            _dataProtector = dataProtectionProvider.CreateProtector(dataProtectionSecuritySettings.StudentIdRouteValue);
        }

        public void AddStudent(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();

        }

        public void DeleteStudent(int id)
        {
            var student = _context.Students.FirstOrDefault(x => x.Id == id);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
        }

        public Student GetStudentById(int id)
        {
            var student = _context.Students.FirstOrDefault(x => x.Id == id);

            return student;
        }

        public List<Student> GetStudents()
        {
          //int x =   _context.GetStudentsByCount();
            return _context.Students.ToList();
            //var list = (from x in _context.Students
            //                     select new {Id = _dataProtector.Protect(x.Id.ToString()),
            //                      Name = x.Name,
            //                     Marks = x.Marks}).ToList();

            
        }

        public Student UpdateStudent(int id, Student student)
        {
            var obj = _context.Students.FirstOrDefault(x => x.Id == id);
            if(obj != null)
            {
                obj.Address = student.Address;
                obj.AdharNumber= student.AdharNumber;
                obj.Marks = student.Marks;
                

            }
            _context.SaveChanges();
            return obj;
        }
    }
}
