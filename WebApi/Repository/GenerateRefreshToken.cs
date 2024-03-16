//using System.Security.Cryptography;
//using WebApi.IRepo;
//using WebApi.Models;

//namespace WebApi.Repository
//{
//    public class RefreshTokenGenerator : IRefreshTokenGenerator
//    {
//        RefreshToken refreshToksn = new RefreshToken();
//        public RefreshToken GenerateRefreshToken()
//        {
//            var randomNumber = new byte[32];
//            using(var randomNumberGenerator = RandomNumberGenerator.Create())
//            {
//                randomNumberGenerator.GetBytes(randomNumber);
//                refreshToksn.RToken  = Convert.ToBase64String(randomNumber);
//            }
//            refreshToksn.ExpiryDate = DateTime.Now.AddMonths(6);
//            return refreshToksn;
//        }
        
//    }
//}
