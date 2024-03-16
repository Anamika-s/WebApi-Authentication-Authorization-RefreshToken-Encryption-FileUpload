using WebApi.Models;

namespace WebApi.Repository.Context
{
    public interface IUserRefreshTokenREpo
    {
        void SaveOrUpdateUserRefreshToken(UserRefreshToken refreshToken);
        bool CheckIfRefreshTokenValueIsValid(string email, string refreshToken);
    }
    public class UserRefreshTokenREpo : IUserRefreshTokenREpo
    {
        public static Dictionary<string, string> RefreshTokenStore;
        public UserRefreshTokenREpo()
        {
           RefreshTokenStore = new Dictionary<string, string>();
        }
        public bool CheckIfRefreshTokenValueIsValid(string email, string refreshToken)
        {
            string refToken = "";
            RefreshTokenStore.TryGetValue(email, out refToken);
            return refToken.Equals(refreshToken);
        }

        public void SaveOrUpdateUserRefreshToken(UserRefreshToken refreshToken)
        {
         if(RefreshTokenStore.ContainsKey(refreshToken.Email))
                RefreshTokenStore[refreshToken.Email] = refreshToken.RefreshToken; 
         else
                RefreshTokenStore.Add(refreshToken.Email, refreshToken.RefreshToken);
                    }
    }
}
