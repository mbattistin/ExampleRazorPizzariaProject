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
    }
}
