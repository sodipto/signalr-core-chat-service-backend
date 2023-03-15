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
        Task<Inbox> GetInbox(Guid senderID, Guid receiverID);
        Task<List<dynamic>> GetChatList(Guid userID, int offset = 0, int pageSize = 20);
        Task<List<dynamic>> GetChatMessages(string inboxID, Guid userID, int offset = 0, int pageSize = 20);

        Task<int> SaveInbox(Inbox inbox);
        Task<int> SaveMessage(Message message);
        Task<int> MessagesMarkAsRead(Guid inboxID, Guid userID);

        Task<int> DeleteMessage(Guid messageID, Guid userID);
        Task<int> DeleteInbox(Guid inboxID, Guid userID);
    }
}
