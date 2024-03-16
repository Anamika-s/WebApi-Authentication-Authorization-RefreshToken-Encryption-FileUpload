//using BookStoresWebAPI.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using Newtonsoft.Json.Linq;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Security.Principal;
//using System.Text;
//using WebApi.IRepo;
//using WebApi.Models;
//using WebApi.Repository;
//using WebApi.Repository.Context;

//namespace WebApi_N_Tier.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class Authenticate1Controller : ControllerBase
//    {
//        //IUserRefreshTokenREpo _x;
//        IAuthenticationManager1 _repo;
//        IConfiguration _config;
//        //ITokenRefresher _refreshTokenGeneratorrefreshTokenGenerator;
//        public Authenticate1Controller(IConfiguration config, 
//            IAuthenticationManager1 repo
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
//            //_x.SaveOrUpdateUserRefreshToken(new UserRefreshToken
//            //{
//            //    RefreshToken = token.RefreshToken,
//            //    Email = user.Email
//            //});
//            return Ok(token);
//        }


//        [HttpPost("refresh")]
//        public IActionResult Refresh(RefreshTokenModel refreshToken)
//        {

//            User user = GetUserFromAccessToken(refreshToken.JwtToken);
//            if (user != null && ValidateRefreshToken(user, refreshToken.RefreshToken))
//            {
//                AuthentcationResponse response = new AuthentcationResponse(user);
//                response.JwtToken = _repo.GenerateToken()

//            }
//            return null;
//        }
//            //AuthentcationResponse token = _repo.AuthenticateUser(user.Email, user.Password);

//            //if (token == null)
//            //    return Unauthorized();
//            //_x.SaveOrUpdateUserRefreshToken(new UserRefreshToken
//            //{
//            //    RefreshToken = token.RefreshToken,
//            //    Email = user.Email
//            //});
//        //    return Ok(token);
//        //}

//        private bool ValidateRefreshToken(User user, string refreshToken)
//        {
//            RefreshToken obj = _repo.ValidateRefreshToken(user, refreshToken); ;
//            if (obj != null && obj.UserId == user.Id && obj.ExpiryDate > DateTime.Now)
//                return true;
//            else

//                return false;
//        }

//        private User GetUserFromAccessToken(string jwtToken)
//        {
//            throw new NotImplementedException();
//        }


//        //[HttpPost("refresh")]

//        //public IActionResult Refresh(RefreshTokenModel refreshTokenModel)
//        //{
//        //    if (refreshTokenModel == null) return Unauthorized();
//        //    var handler = new JwtSecurityTokenHandler();
//        //    SecurityToken validatedToken;
//        //    IPrincipal principal = handler.ValidateToken(refreshTokenModel.JwtToken,
//        //        GetTokenValidationParameters(), out validatedToken);
//        //    var email = principal.Identity.Name;
//        //    if (_x.CheckIfRefreshTokenValueIsValid(email, refreshTokenModel))

//        //}
//        ////var token = _refreshTokenGeneratorrefreshTokenGenerator.Refresh(refreshTokenModel);


//        //private TokenValidationParameters GetTokenValidationParameters()
//        //{
//        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
//        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

//        //    return new TokenValidationParameters
//        //    {
//        //        ValidateIssuerSigningKey = true,
//        //        IssuerSigningKey = securityKey

//        //    };




//    }
//    //private User Authenticate(User user)
//    //{
//    //    User obj = _context.UserList.FirstOrDefault(x => x.Email == user.Email && x.Password == user.Password);
//    //    return obj;
//    //}

//    //string GetRoleName(int roleId)
//    //{
//    //    string roleName = (from x in _context.Roles
//    //                       where x.RoleId == roleId
//    //                       select x.RoleName).FirstOrDefault();
//    //    return roleName;
//    //}
//    //private string GenerateJSONWebToken(User user)
//    //{
//    //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
//    //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
//    //    string roleName = GetRoleName(user.RoleId);
//    //    var claims = new Claim[]
//    //    {
//    //        new Claim(JwtRegisteredClaimNames.Jti, new Guid().ToString()),
//    //        new Claim(JwtRegisteredClaimNames.Sid,user.Id.ToString()),
//    //        new Claim(ClaimTypes.Email , user.Email),
//    //        new Claim(ClaimTypes.Role, roleName),
//    //        new Claim(type:"DateOnly", DateTime.Now.ToString())
//    //    };

//    //    var token = new JwtSecurityToken(_config["Jwt:Issuer"],
//    //      _config["Jwt:Audience"],
//    //      claims,
//    //      expires: DateTime.Now.AddMinutes(120),
//    //      signingCredentials: credentials);

//    //    return new JwtSecurityTokenHandler().WriteToken(token);
//    //}


//}


