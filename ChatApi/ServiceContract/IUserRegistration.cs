using ChatApi.vmModels;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ChatApi.ServiceContract
{
    public interface IUserRegistration
    {
        Task<IHttpActionResult> CreateUpdate(UserRegistrationVM objEmployee);
    }
}
