namespace WebApi.IRepo
{
    public interface IAuthenticationManager
    {
        public string AuthenticateUser(string username, string password);
        public string GetRole(string email);
    }
}

