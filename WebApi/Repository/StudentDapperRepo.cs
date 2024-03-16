using Dapper;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Data.SqlClient;
using WebApi.IRepo;
using WebApi.Models;
using WebApi.Repository.Context;
using WebApi.Security;

namespace WebApi.Repository
{
    public class StudentDapperRepo : IStudentRepo
    {

      
        StudentDbContext _context;
        private string connectionString = string.Empty;
        public StudentDapperRepo(StudentDbContext context, IConfiguration configuration)
        {    
            _context = context;
            connectionString = configuration.GetSection("ConnectionStrings1:DbConnection").Value;             }

        public void AddStudent(Student student)
        {
            var sqlQuery = "insert into students (name, address, marks) values (@name, @address, @marks)";
            using (var connection = new SqlConnection(connectionString))
            {
                  connection.Execute(sqlQuery, new { 
                student.Name,
                student.Address,
                student.Marks});
            }
         }

        public  void DeleteStudent(int id)
        {
            var sqlQuery = "delete * from students where id=@id";
            using (var connection = new SqlConnection(connectionString))
            {
                    connection.Execute(sqlQuery, new { id = id });
            }
        }

        public  Student GetStudentById(int id)
        {
            var sqlQuery = "select * from students where id=@id";
            using (var connection = new SqlConnection(connectionString))
            {
                return   connection.QueryFirstOrDefault <Student>(sqlQuery, new { id = id });
            }
        }

        public List<Student> GetStudents()
        {
            var sqlQuery = "select * from students";
            using(var connection = new SqlConnection(connectionString))
            {
                return   connection.Query<Student>(sqlQuery).ToList();
            }
             
        }

        public Student UpdateStudent(int id, Student student)
        {
            var sqlQuery = "update students set address =@address  where id=@id";
            using (var connection = new SqlConnection(connectionString))
            {
                   connection.Execute(sqlQuery, 
                    new { student.Id,
                    student.Name,
                    student.Address,
                    student.Marks});
            }
            return student;
        }
    }
}
