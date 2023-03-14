using ChatApp.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Core.Service
{
    public interface IMessageService
    {
        Task<List<dynamic>> GetChatList(Guid userID, int offset = 0, int pageSize = 20);
        Task<List<dynamic>> GetChatMessages(string inboxID, int offset = 0, int pageSize = 20);
        Task<int> SaveInbox(Inbox inbox);
        Task<int> SaveMessage(Message message);
    }
}
