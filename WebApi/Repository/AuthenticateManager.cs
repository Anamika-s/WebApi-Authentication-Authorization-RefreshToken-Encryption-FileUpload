using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Models;
using WebApi.Repository.Context;
 

    public class AuthenticateManager : WebApi.IRepo.IAuthenticationManager
    {
        StudentDbContext _context;
        IConfiguration _config;
        public AuthenticateManager(StudentDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
    public string AuthenticateUser(string email, string password)
    {
       
        var user = FindUser(email, password);

        if (user == null)
            return null;
        string token =  GenerateToken(email, password);
        return token;
    }
    public string GenerateToken(string email, string password)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenHandler = new JwtSecurityTokenHandler();
         string roleName = GetRole(email);
       
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, roleName),

            }),
            Expires = DateTime.Now.AddHours(1),
            SigningCredentials = credentials
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);

    }
    
        private User1 FindUser(string email, string password)
        {
            User1 obj = _context.UserList.FirstOrDefault(x => x.Email == email && x.Password == password);
            return obj;
        }

    string GetRoleName(int roleId)
    {
        string roleName = (from x in _context.Roles
                           where x.RoleId == roleId
                           select x.RoleName).FirstOrDefault();
        return roleName;
    }

    private string GenerateJSONWebToken(string email, string password)
{
    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        string roleName = GetRole(email);
        var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, new Guid().ToString()),
                //new Claim(JwtRegisteredClaimNames.Sid,user.Id.ToString()),
                //new Claim(ClaimTypes.Email , user.Email),
                new Claim(ClaimTypes.Role, roleName),
                new Claim(type:"DateOnly", DateTime.Now.ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
      _config["Jwt:Audience"],
      claims,
      expires: DateTime.Now.AddMinutes(120),
      signingCredentials: credentials);

    return new JwtSecurityTokenHandler().WriteToken(token);
}


    public string GetRole(string email)
    {
        var roleId  =  _context.UserList.Where(x=>x.Email == email).Select(x => x.RoleId).FirstOrDefault();

        string roleName = GetRoleName(roleId);
        return roleName;
    }


}

     

