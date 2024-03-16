namespace WebApi.Models
{
    public class AuthentcationResponse : User1
    {
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
        public AuthentcationResponse(User1 user)
        {
            this.Id = user.Id;
            this.Email = user.Email;
            

            this.RoleId = user.RoleId;
        }

    }
}
