using ChatApi.vmModels;
using System;
using System.Threading.Tasks;

namespace ChatApi.ServiceContract
{
    public interface IOperatorRegistration
    {
        Task<int> CreateUpdate(OperatorRegistrationVM _vmOperator);
    }
}
