//using Azure;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Security.Cryptography;
//using System.Text;
//using WebApi.IRepo;
//using WebApi.Models;
//using WebApi.Repository.Context;


//public class AuthenticateManager1 : IAuthenticationManager1
//{
//    static IDictionary<string, string> UserRefreshTokens { get; set; }
//    IDictionary<string, string> IAuthenticationManager1.UserRefreshTokens { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

//    StudentDbContext _context;
//    IConfiguration _config;
//    IRefreshTokenGenerator _refreshTokenGenerator;



//    public AuthenticateManager1(StudentDbContext context, IConfiguration config,
//            IRefreshTokenGenerator refreshTokenGenerator)
//    {
//        _context = context;
//        _config = config;
//        _refreshTokenGenerator = refreshTokenGenerator;
//        //UserRefreshTokens = new Dictionary<string, string>();

//    }



//    private string GenerateAccessToken(int userId)
//    {
//        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
//        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

//        var tokenHandler = new JwtSecurityTokenHandler();
//        //string roleName = GetRole(email);

//        var tokenDescriptor = new SecurityTokenDescriptor
//        {
//            Subject = new ClaimsIdentity(new Claim[]
//            {
//                    new Claim(ClaimTypes.Name, userId.ToString()),
//                    //new Claim(ClaimTypes.Role, roleName),

//            }),
//            Expires = DateTime.Now.AddHours(1),
//            SigningCredentials = credentials
//        };
//        var token = tokenHandler.CreateToken(tokenDescriptor);
//        return tokenHandler.WriteToken(token);
//    }


//    public AuthentcationResponse AuthenticateUser(string email, string password)
//    {

//        var user = FindUser(email, password);

//        if (user == null)
//            return null;

//        AuthentcationResponse auth = new AuthentcationResponse();
//        string JwtToken = GenerateAccessToken(user.Id);
//        RefreshToken refreshToken = GenerateRefreshToken();
//        refreshToken.UserId = -1;
//        _context.RefreshTokens.Add(refreshToken);
//        _context.SaveChanges();

//        return new AuthentcationResponse
//        {
//            JwtToken = JwtToken,
//            RefreshToken = refreshToken.RToken
//};
//}

//    public RefreshToken GenerateRefreshToken()
//    {
//        RefreshToken refreshToksn = new RefreshToken();
//        var randomNumber = new byte[32];
//        using (var randomNumberGenerator = RandomNumberGenerator.Create())
//        {
//            randomNumberGenerator.GetBytes(randomNumber);
//            refreshToksn.RToken = Convert.ToBase64String(randomNumber);
//        }
//        refreshToksn.ExpiryDate = DateTime.Now.AddMonths(6);
//        return refreshToksn;
//    }
//    public AuthentcationResponse AuthenticateUser(string email, Claim[] claims)
//    {
//        throw new NotImplementedException();
//    }

//    public string GetRole(string email)
//    {
//        throw new NotImplementedException();
//    }
     
//    public RefreshToken ValidateRefreshToken(User1 user, string refreshToken)
//    {
//       RefreshToken obj = _context.RefreshTokens.Where(rt => rt.RToken == refreshToken)
//          .OrderByDescending(rt => rt.ExpiryDate)
//          .FirstOrDefault();
//        return obj;

//    }

////    public AuthentcationResponse AuthenticateUser(string email, Claim[] claims)
////{

////    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
////    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
////    var tokenHandler = new JwtSecurityTokenHandler();

////    var tokenDescriptor = new JwtSecurityToken(
////        claims: claims,
////        expires: DateTime.Now.AddHours(1),
////        signingCredentials: credentials
////    );
////    var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
////    string refreshToken = _refreshTokenGenerator.GenerateRefreshToken();
////    if (UserRefreshTokens.ContainsKey("email"))
////    {
////        UserRefreshTokens["email"] = refreshToken;
////    }
////    else
////        UserRefreshTokens.Add("email", refreshToken);
////    return new AuthentcationResponse
////    {
////        JwtToken = token,
////        RefreshToken = refreshToken
////    };
////}


//public string GenerateToken(string email, string password)
//{
//    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
//    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

//    var tokenHandler = new JwtSecurityTokenHandler();
//    //string roleName = GetRole(email);

//    var tokenDescriptor = new SecurityTokenDescriptor
//    {
//        Subject = new ClaimsIdentity(new Claim[]
//        {
//                    new Claim(ClaimTypes.Name, email),
//                    //new Claim(ClaimTypes.Role, roleName),

//        }),
//        Expires = DateTime.Now.AddHours(1),
//        SigningCredentials = credentials
//    };
//    var token = tokenHandler.CreateToken(tokenDescriptor);
//    return tokenHandler.WriteToken(token);

//}

//public string GenerateToken(string username, Claim[] claims)
//{
//    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
//    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
//    var tokenHandler = new JwtSecurityTokenHandler();

//    var jwtSecurityToken = new JwtSecurityToken(
//        claims: claims,
//        expires: DateTime.Now.AddHours(1),
//        signingCredentials: credentials);
//    //string roleName = GetRole(email);

//    return tokenHandler.WriteToken(jwtSecurityToken);
//    //var token = tokenHandler.CreateToken(tokenDescriptor);
//    //return tokenHandler.WriteToken(token);

//}

//  User1 FindUser(string email, string password)
//{
//    User1 obj = _context.UserList.FirstOrDefault(x => x.Email == email && x.Password == password);
//    return obj;
//}

//string GetRoleName(int roleId)
//{
//    string roleName = (from x in _context.Roles
//                       where x.RoleId == roleId
//                       select x.RoleName).FirstOrDefault();
//    return roleName;
//}

//private string GenerateJSONWebToken(string email, string password)
//{
//    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
//    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
//    string roleName = GetRole(email);
//    var claims = new Claim[]
//        {
//                new Claim(JwtRegisteredClaimNames.Jti, new Guid().ToString()),
//                //new Claim(JwtRegisteredClaimNames.Sid,user.Id.ToString()),
//                //new Claim(ClaimTypes.Email , user.Email),
//                new Claim(ClaimTypes.Role, roleName),
//                new Claim(type:"DateOnly", DateTime.Now.ToString())
//        };

//    var token = new JwtSecurityToken(_config["Jwt:Issuer"],
//_config["Jwt:Audience"],
//claims,
//expires: DateTime.Now.AddMinutes(120),
//signingCredentials: credentials);

//    return new JwtSecurityTokenHandler().WriteToken(token);
//}


////public string GetRole(string email)
////{
////    var roleId = _context.UserList.Where(x => x.Email == email).Select(x => x.RoleId).FirstOrDefault();

////    string roleName = GetRoleName(roleId);
////    return roleName;
////}


//}



