using Castle.Core.Internal;
using ChatApp.Core.Data;
using ChatApp.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Core.Service
{
    public class MessageService : IMessageService
    {
        private IDapperRepositiory _repository;
        public MessageService(IDapperRepositiory repository)
        {
            _repository = repository;
        }

        public async Task<List<dynamic>> GetChatList(Guid userID, int offset = 0, int pageSize = 20)
        {
            string sql = $"SELECT COUNT(*) over() AS \"TotalCount\",chat.\"InboxID\",CASE WHEN chat.\"OwnerID\"=@userID" +
                $" THEN chat.\"OwnerID\" ELSE chat.\"ReceiverID\" END \"SenderID\", CASE WHEN chat.\"ReceiverID\"=@userID" +
                $" THEN chat.\"OwnerID\" ELSE chat.\"ReceiverID\" END \"ReceiverID\",CASE WHEN chat.\"ReceiverID\"=@userID" +
                $" THEN chat.\"InboxOwnerFullName\" ELSE chat.\"InboxReceipentFullName\" END \"ReceiverName\",CASE WHEN chat.\"ReceiverID\"=@userID" +
                $" THEN chat.\"InboxOwnerProfileImageSrc\" ELSE chat.\"InboxReceipentProfileImageSrc\" END \"ReceiverProfileImage\",chat.\"SenderID\"" +
                $" AS \"LastMessageSenderID\",chat.\"Content\", chat.\"SeenStatus\",chat.\"DeliveredStatus\",chat.\"SentDateTime\" FROM (SELECT DISTINCT" +
                $" ON(i.\"ID\")*,inboxowner.\"FullName\" AS \"InboxOwnerFullName\",inboxowner.\"ProfileImageSrc\" AS \"InboxOwnerProfileImageSrc\"," +
                $"inboxreceipent.\"FullName\" AS \"InboxReceipentFullName\",inboxreceipent.\"ProfileImageSrc\" AS \"InboxReceipentProfileImageSrc\"," +
                $"i.\"CreatedAt\" AS \"SentDateTime\" FROM \"inbox\" i INNER JOIN \"messages\" m ON i.\"ID\"=m.\"InboxID\" INNER JOIN \"users\" inboxowner" +
                $" ON inboxowner.\"ID\"=i.\"OwnerID\" INNER JOIN \"users\" inboxreceipent ON inboxreceipent.\"ID\"=i.\"ReceiverID\" WHERE i.\"Deleted\"= FALSE AND m.\"Deleted\"= FALSE" +
                $" AND (i.\"OwnerID\"=@userID OR i.\"ReceiverID\"=@userID) AND ( CASE WHEN i.\"ReceiverID\"=@userID AND i.\"ReceiverDeleted\"=true THEN 1 " +
                $"WHEN i.\"OwnerID\"=@userID AND i.\"OwnerDeleted\"=true THEN 1 WHEN m.\"SenderID\"=@userID AND m.\"SenderDeleted\"=TRUE THEN 1 " +
                $"WHEN m.\"SenderID\"!=@userID AND m.\"ReceiverDeleted\"=TRUE THEN 1 ELSE 0 END) = 0 ORDER BY i.\"ID\",m.\"CreatedAt\" Desc) chat " +
                $"OFFSET @offset LIMIT @pageSize";

            var chats = await _repository.QueryAsync<dynamic>(sql, new { userID, offset, pageSize });

            return chats.ToList();
        }

        public async Task<List<dynamic>> GetChatMessages(string inboxID, Guid userID, int offset = 0, int pageSize = 20)
        {
            string sql = $"SELECT COUNT(*) over() AS \"TotalCount\",chat.\"ID\" AS \"MessageID\",chat.\"SenderID\"," +
                $"u.\"FullName\" AS \"SenderName\",u.\"ProfileImageSrc\" AS \"SenderProfileImageSrc\",chat.\"Content\"," +
                $"chat.\"SeenStatus\",chat.\"UpdatedAt\" AS \"SeenDateTime\",chat.\"DeliveredStatus\",chat.\"CreatedAt\"" +
                $" AS \"SentDateTime\" FROM messages chat INNER JOIN \"users\" u ON u.\"ID\"=chat.\"SenderID\" " +
                $"WHERE chat.\"InboxID\"=@inboxID AND chat.\"Deleted\"= FALSE AND (CASE WHEN chat.\"SenderID\"=@userID" +
                $" AND chat.\"SenderDeleted\"= TRUE THEN 1 WHEN chat.\"SenderID\"!=@userID AND chat.\"ReceiverDeleted\"= TRUE THEN 1" +
                $" ELSE 0 END) = 0 ORDER BY chat.\"CreatedAt\" DESC OFFSET @ OFFSET LIMIT @pageSize";

            var chatMessages = await _repository.QueryAsync<dynamic>(sql, new { inboxID, userID, offset, pageSize });

            return chatMessages.ToList();
        }

        public async Task<int> SaveInbox(Inbox inbox)
        {
            string sql = $"INSERT INTO inbox (\"ID\", \"OwnerID\", \"ReceiverID\", \"OwnerDeleted\"," +
                $" \"ReceiverDeleted\", \"CreatedAt\", \"UpdatedAt\", \"Deleted\") " +
                $"VALUES ('{inbox.ID}', '{inbox.OwnerID}', '{inbox.ReceiverID}', False, False, NOW(), NOW(), False)";

            int affectedRow = await _repository.ExecuteAsync(sql);

            return affectedRow;
        }

        public async Task<int> SaveMessage(Message message)
        {
            string sql = $"INSERT INTO messages (\"ID\", \"InboxID\", \"Content\", \"SenderID\", \"SenderDeleted\"," +
                $" \"ReceiverDeleted\", \"SeenStatus\", \"DeliveredStatus\", \"CreatedAt\", \"UpdatedAt\", \"Deleted\")" +
                $" VALUES ('{message.ID}', '{message.InboxID}', '{message.Content}', '{message.SenderID}', False, False, False, False, NOW(), NOW(), False)";

            int affectedRow = await _repository.ExecuteAsync(sql);

            return affectedRow;
        }

        public async Task<int> DeleteMessage(Guid messageID, Guid userID)
        {
            string sql = $"UPDATE messages SET \"SenderDeleted\"= CASE WHEN \"SenderID\"=@userID " +
                $"THEN TRUE ELSE \"SenderDeleted\" END, \"ReceiverDeleted\"= CASE WHEN \"SenderID\"!=@userID" +
                $" THEN TRUE ELSE \"ReceiverDeleted\" END,\"UpdatedAt\"=NOW() WHERE \"ID\"=@messageID AND " +
                $" \"Deleted\"= FALSE AND (\"SenderDeleted\"= FALSE OR \"ReceiverDeleted\"= FALSE)";

            int affectedRow = await _repository.ExecuteAsync(sql, new { messageID, userID });

            return affectedRow;
        }

        public async Task<int> DeleteInbox(Guid inboxID, Guid userID)
        {
            string sql = $"UPDATE inbox SET \"OwnerDeleted\"= CASE WHEN \"OwnerID\"=@userID THEN TRUE " +
                $"ELSE \"OwnerDeleted\" END,\"ReceiverDeleted\"= CASE WHEN \"ReceiverID\"=@userID THEN TRUE" +
                $" ELSE \"ReceiverDeleted\" END,\"UpdatedAt\"= NOW()\r\nWHERE \"ID\"=@inboxID AND \"Deleted\"= FALSE" +
                $" AND (\"OwnerDeleted\"= FALSE OR \"ReceiverDeleted\"= FALSE)";

            int affectedRow = await _repository.ExecuteAsync(sql, new { inboxID, userID });

            return affectedRow;
        }

        public async Task<int> MessagesMarkAsRead(Guid inboxID, Guid userID)
        {
            string sql = $"UPDATE messages SET \"SeenStatus\"=true,\"UpdatedAt\"=NOW() WHERE" +
                $" \"InboxID\"=@inboxID AND \"SenderID\"!=@userID AND \"SeenStatus\"=false AND \"Deleted\"=FALSE ";

            int affectedRow = await _repository.ExecuteAsync(sql, new { inboxID, userID });

            return affectedRow;
        }
    }
}
