using ChatApi.vmModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApi.ServiceContract
{
    public interface IChat
    {
        Task<int> ChatSend(ChatVM _chat);
        Task<RespRepoVM> ChatReceivedByTime(ChatVM _vm);
        Task<int> ChatSignOut(ChatVM _chat);
    }
}
