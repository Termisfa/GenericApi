namespace GenericApi.ApiRestHandler
{
    public interface IApiRestHandler
    {
        Task<string> Get(string schema, string table, string? parameters = default);

        Task<int> Post(HttpObject obj);

        Task<int> Put(HttpObject obj, string parameters);

        Task<int> Delete(string schema, string table, string parameters);

        Task<int> DeleteWithoutWhere(string schema, string table);

        void DeleteConnectionStrings();
    }
}
