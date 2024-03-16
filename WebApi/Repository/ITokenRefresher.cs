using WebApi.Models;

namespace WebApi.Repository
{
    public interface ITokenRefresher
    {
        AuthentcationResponse Refresh(RefreshTokenModel refreshToken);
    }
}