using BookStoresWebAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class User1
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        [ForeignKey("RoleId")]
        public int RoleId { get; set; }
        public Role? Role { get; set; }
    }

    public class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public short RoleId { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
