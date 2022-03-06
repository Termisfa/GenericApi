using GenericApi.Models;

namespace GenericApi.ApiRestHandler
{
    public interface IApiRestHandler
    {
        Task<Response> Get(string schema, string table, string? parameters = default);

        Task<Response> Post(HttpObject obj);

        Task<Response> Put(HttpObject obj, string parameters);

        Task<Response> Delete(string schema, string table, string parameters);

        Task<Response> DeleteWithoutWhere(string schema, string table);

        Task<Response> ResetConnections();
    }
}
