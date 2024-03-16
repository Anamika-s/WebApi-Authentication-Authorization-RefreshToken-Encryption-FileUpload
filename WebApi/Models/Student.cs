using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Student
    {
        public int? Id { get; set; }
        [NotMapped]
        public string? EncryptedId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Marks { get; set; }
        public string? AdharNumber{ get; set; }
        [NotMapped]
        public IFormFile? AdharFile { get; set; }
       
        public string? AdharFilePath { get; set; }

    }
}
