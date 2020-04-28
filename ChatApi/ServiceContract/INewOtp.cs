using ChatApi.vmModels;
using System.Data;
using System.Threading.Tasks;

namespace ChatApi.ServiceContract
{
    public interface INewOtp
    {
        Task<DataTable> GetNewOTP(NewOtpVM newotpmodel);
    }
}
