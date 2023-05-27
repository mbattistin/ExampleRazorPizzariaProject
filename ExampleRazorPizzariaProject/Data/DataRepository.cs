using Dapper;
using MySql.Data.MySqlClient;

namespace ExampleRazorPizzariaProject.Data
{
    public class DataRepository : IDataRepository
    {
        public MySqlConnection Connection { get; set; }

        public DataRepository(MySqlConnection connection)
        {
            Connection = connection;
        }

        public async Task<List<T>> ListAsync<T>(string sqlCommand)
        {
            try
            {
                return (List<T>)await Connection.QueryAsync<T>(sqlCommand);

            }
            catch
            {
                return new List<T>();
            }
        }

        public async Task<T> SaveAsync<T>(string sqlCommand)
        {
            try
            {
                return  (T)await Connection.ExecuteScalarAsync(sqlCommand);
            }
            catch 
            {
                return default;
            }

        }

        public async Task<T> GetAsync<T>(string sqlCommand)
        {
            try
            {
                return await Connection.QueryFirstAsync<T>(sqlCommand);

            }
            catch
            {
                return default;
            }
        }
    }
}
