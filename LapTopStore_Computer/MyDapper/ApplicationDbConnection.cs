using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace LapTopStore_Computer.MyDapper
{
    public class ApplicationDbConnection : IApplicationDbConnection, IDisposable
    {
        private readonly IDbConnection connection;
        private readonly string Schema;

        public ApplicationDbConnection(IConfiguration configuration)
        {
            connection = new SqlConnection(configuration.GetConnectionString("LapTopStore"));

        }
        public IDbConnection GetConnection => this.connection;

        IDbConnection IApplicationDbConnection.GetConnection => throw new NotImplementedException();

        public void Dispose()
        {
            connection.Dispose();
        }
        public async Task<int> ExecuteAsync(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            return await connection.ExecuteAsync(sql, param, transaction, commandTimeout: 600, commandType: commandType);
        }
        public async Task<List<T>> QueryAsync<T>(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            return (await connection.QueryAsync<T>(sql, param, transaction, commandTimeout: 600, commandType: commandType))?.AsList();
        }
        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            return await connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction, commandType: commandType);
        }

        public async Task<T> QuerySingleAsync<T>(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            return await connection.QuerySingleAsync<T>(sql, param, transaction, commandType: commandType);
        }

    }

}
