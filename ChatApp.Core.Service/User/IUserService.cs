using ChatApp.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Core.Service
{
    public interface IUserService
    {
        Task<bool> IsEmailExist(string email);
        Task<User> GetUserById(Guid userID);
        Task<User> GetUser(string email, string password);

        Task<int> Save(User user);
        Task<int> Update(Guid userID, string fullName, string profileImageSrc);
        Task<bool> ChangePassword(Guid userID, string password);
        Task<bool> DeleteAccount(Guid userID);
    }
}
