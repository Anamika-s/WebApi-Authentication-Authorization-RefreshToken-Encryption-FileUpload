using WebApi.Models;

namespace WebApi.IRepo
{
    public interface IStudentRepo
    {
        public List<Student> GetStudents();
        public Student GetStudentById(int id);
        public void AddStudent(Student student);
        public Student UpdateStudent(int id, Student student);
        public void DeleteStudent(int id);
    }
}
