using System.Data.Common;
using Application.Abstractions;
using Npgsql;

namespace Persistence.Data
{
    public class DbConnectionFactory(string ConnectionString) : IDbConnectionFactory
    {
        public async Task<DbConnection> CreateSqlConnection()
        {
            var connection = new NpgsqlConnection(ConnectionString);
            await connection.OpenAsync();
            return connection;
        }
    }
}