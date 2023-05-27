namespace ExampleRazorPizzariaProject.Data
{
    public interface IDataRepository
    {
        Task<T> GetAsync<T>(string sqlCommand);
        Task<List<T>> ListAsync<T>(string sqlCommand);
        Task<T> SaveAsync<T>(string sqlCommand);
    }
}
