namespace ExampleRazorPizzariaProject.Data
{
    public interface IDataRepository
    {
        Task<List<T>> ListAsync<T>(string sqlCommand);
    }
}
