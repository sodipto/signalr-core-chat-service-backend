using Dapper;
using System.Data;
using static Dapper.SqlMapper;

namespace ChatApp.Core.Data
{
    public class DapperRepositiory : IDapperRepositiory
    {
        #region Fields
        private readonly ApplicationDbContext _dbContext;
        #endregion

        #region Ctor
        public DapperRepositiory(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods

        public async Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null)
        {
            return (await _dbContext.Connection.QueryAsync<T>(sql, param, transaction)).AsList();
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null)
        {
            return await _dbContext.Connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction);
        }
        public async Task<T> QuerySingleAsync<T>(string sql, object param = null, IDbTransaction transaction = null)
        {
            return await _dbContext.Connection.QuerySingleAsync<T>(sql, param, transaction);
        }

        public async Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null)
        {
            return await _dbContext.Connection.ExecuteAsync(sql, param, transaction);
        }
        #endregion
    }
}
