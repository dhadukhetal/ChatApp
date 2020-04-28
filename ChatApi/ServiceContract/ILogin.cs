using ChatApi.vmModels;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChatApi.ServiceContract
{
    public interface ILogin
    {
        Task<UserDetail> UserLogin(LoginVM objLoginModel);
        Task<HttpResponseMessage> CheckUpdateDownload(UserUpdateCheck obj);
        Task<UserUpdateRespCheck> CheckUpdate(UserUpdateCheck obj);
    }
}
