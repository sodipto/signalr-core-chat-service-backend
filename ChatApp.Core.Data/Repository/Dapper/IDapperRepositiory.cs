using System.Data;
using static Dapper.SqlMapper;

namespace ChatApp.Core.Data
{
    public interface IDapperRepositiory
    {
        Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null);
        Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null);
        Task<T> QuerySingleAsync<T>(string sql, object param = null, IDbTransaction transaction = null);
        Task<GridReader> QueryMultipleAsync(string sql, object param = null, IDbTransaction transaction = null);
        Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null);
    }
}
