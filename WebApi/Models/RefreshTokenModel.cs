using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class RefreshTokenModel : User1
    {
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
        public RefreshTokenModel(User1 user)
        {
            this.Id = user.Id;
            this.Email = user.Email;
             
            this.RoleId = user.RoleId;
        }


    }

}

