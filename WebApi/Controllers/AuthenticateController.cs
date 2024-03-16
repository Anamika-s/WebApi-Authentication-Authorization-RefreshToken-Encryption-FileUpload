//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using WebApi.IRepo;
//using WebApi.Models;
//using WebApi.Repository;

//namespace WebApi_N_Tier.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthenticateController : ControllerBase
//    {
//        IAuthenticationManager _repo;
//        IConfiguration _config;
//        //ITokenRefresher _refreshTokenGeneratorrefreshTokenGenerator;
//        public AuthenticateController(IConfiguration config, IAuthenticationManager repo
//            /*ITokenRefresher refreshTokenGeneratorrefreshTokenGenetor*/)
//        {
//            _config = config;
//            _repo = repo;
//            //_refreshTokenGeneratorrefreshTokenGenerator= refreshTokenGeneratorrefreshTokenGenetor;  

//        }
//        [HttpPost]
//        public IActionResult Login(LoginModel user)
//        {

//            AuthentcationResponse token = _repo.AuthenticateUser(user.Email, user.Password);

//            if (token == null)
//                return Unauthorized();
//            return Ok(token);
//        }

//        //[HttpPost("refresh")]

//        //public IActionResult Refresh(RefreshTokenModel refreshTokenModel)
//        //{
//        //    var token =  _refreshTokenGeneratorrefreshTokenGenerator.Refresh(refreshTokenModel);
//        //    if(token==null) return Unauthorized();
//        //    return Ok(token);
//        //}
//        //private User Authenticate(User user)
//        //{
//        //    User obj = _context.UserList.FirstOrDefault(x => x.Email == user.Email && x.Password == user.Password);
//        //    return obj;
//        //}

//        //string GetRoleName(int roleId)
//        //{
//        //    string roleName = (from x in _context.Roles
//        //                       where x.RoleId == roleId
//        //                       select x.RoleName).FirstOrDefault();
//        //    return roleName;
//        //}
//        //private string GenerateJSONWebToken(User user)
//        //{
//        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
//        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
//        //    string roleName = GetRoleName(user.RoleId);
//        //    var claims = new Claim[]
//        //    {
//        //        new Claim(JwtRegisteredClaimNames.Jti, new Guid().ToString()),
//        //        new Claim(JwtRegisteredClaimNames.Sid,user.Id.ToString()),
//        //        new Claim(ClaimTypes.Email , user.Email),
//        //        new Claim(ClaimTypes.Role, roleName),
//        //        new Claim(type:"DateOnly", DateTime.Now.ToString())
//        //    };

//        //    var token = new JwtSecurityToken(_config["Jwt:Issuer"],
//        //      _config["Jwt:Audience"],
//        //      claims,
//        //      expires: DateTime.Now.AddMinutes(120),
//        //      signingCredentials: credentials);

//        //    return new JwtSecurityTokenHandler().WriteToken(token);
//        //}


//    }
//}

