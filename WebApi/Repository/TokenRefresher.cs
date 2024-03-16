//using Microsoft.IdentityModel.Tokens;
//using Newtonsoft.Json.Linq;
//using System.IdentityModel.Tokens.Jwt;
//using System.Text;
//using WebApi.IRepo;
//using WebApi.Models;

//namespace WebApi.Repository
//{
//    public class TokenRefresher : ITokenRefresher
//    {
//        IConfiguration _config;
//        IAuthenticationManager1 _autIhenticationManager1;
//        public TokenRefresher(IConfiguration config, IAuthenticationManager1 authenticationManager)
//        {
//            _config = config;
//            _autIhenticationManager1 = authenticationManager;
//        }
//        //public AuthentcationResponse Refresh(RefreshTokenModel refreshToken)
//        //{
//        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

//        //    var tokenHandler = new JwtSecurityTokenHandler();
//        //    SecurityToken validatedToken;
//        //    var principal = tokenHandler.ValidateToken(refreshToken.JwtToken,
//        //        new Microsoft.IdentityModel.Tokens.TokenValidationParameters
//        //        {
//        //            ValidateIssuerSigningKey = true,
//        //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"])),

//        //    ValidateIssuer = false,
//        //            ValidateAudience = false
//        //        }, out validatedToken);
//        //    var jwtToken = validatedToken as JwtSecurityToken;
//        //    if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
//        //    {
//        //        throw new SecurityTokenException("Invalid token passed");
//        //    }
//        //    var email = principal.Identity.Name;
//        //    //var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
//        //    if (jwtToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
//        //        throw new SecurityTokenException("Invalid token");

//        //    //if (refreshToken.RefreshToken != _autIhenticationManager1.UserRefreshTokens["email"])
//        //    //    throw new SecurityTokenException("Invalid token passed");
//        //    //return _autIhenticationManager1.AuthenticateUser(email, principal.Claims.ToArray());
//        //}
//    }
//}
