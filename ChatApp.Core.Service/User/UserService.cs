using ChatApp.Core.Data;
using ChatApp.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Core.Service
{
    public class UserService : IUserService
    {
        private IDapperRepositiory _repository;
        public UserService(IDapperRepositiory repository)
        {
            _repository = repository;
        }

        public async Task<bool> IsEmailExist(string email)
        {
            string sql = $"SELECT COUNT(*) FROM \"Users\" WHERE \"Email\"=@email AND \"Deleted\"=false";

            var row = await _repository.QueryFirstOrDefaultAsync<int>(sql, new { email });

            return row > 0;
        }

        public async Task<User> GetUserById(Guid userID)
        {
            string sql = $"SELECT *FROM \"Users\" WHERE \"ID\"=@userID AND \"Deleted\"=false";

            var user = await _repository.QueryFirstOrDefaultAsync<User>(sql, new { userID });

            return user;
        }

        public async Task<User> GetUser(string email, string password)
        {
            string sql = $"SELECT *FROM \"Users\" WHERE \"Email\"=@email AND \"Password\"='{password}' AND " +
                $"\"Deleted\"=false";

            var user = await _repository.QueryFirstOrDefaultAsync<User>(sql, new { email });

            return user;
        }

        public async Task<int> Save(User user)
        {
            string sql = $"INSERT INTO \"Users\" (\"ID\", \"FullName\", \"ProfileImageSrc\", \"Email\"," +
                $" \"Password\", \"CreatedAt\", \"UpdatedAt\", \"UpdatedByID\", \"Deleted\") " +
                $"VALUES ('{user.ID}', '{user.FullName}', '{user.ProfileImageSrc}', '{user.Email}'," +
                $" '{user.Password}', NOW(), NOW(), '{user.ID}', False)";

            int affectedRow = await _repository.ExecuteAsync(sql);

            return affectedRow;
        }

        public async Task<int> Update(Guid userID, string fullName, string profileImageSrc)
        {
            string sql = $"UPDATE \"Users\" SET \"FullName\"='{fullName}',\"ProfileImageSrc\"='{profileImageSrc}'," +
                $"\"UpdatedAt\"=NOW(),\"UpdatedByID\"=@userID WHERE \"ID\"=@userID AND \"Deleted\"=false";

            int affectedRow = await _repository.ExecuteAsync(sql, new { userID });

            return affectedRow;
        }

        public async Task<bool> ChangePassword(Guid userID, string password)
        {
            string sql = $"UPDATE \"Users\" SET \"Password\"='{password}',\"UpdatedAt\"=NOW()," +
                $"\"UpdatedByID\"=@userID WHERE \"ID\"=@userID AND \"Deleted\"=false";

            int affectedRow = await _repository.ExecuteAsync(sql, new { userID });

            return affectedRow > 0;
        }

        public async Task<bool> DeleteAccount(Guid userID)
        {
            string sql = $"UPDATE \"Users\" SET \"Deleted\"=True,\"UpdatedAt\"=NOW()," +
                $"\"UpdatedByID\"=@userID WHERE \"ID\"=@userID AND \"Deleted\"=false";

            int affectedRow = await _repository.ExecuteAsync(sql, new { userID });

            return affectedRow > 0;
        }
    }
}
