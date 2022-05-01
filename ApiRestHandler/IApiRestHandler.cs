using GenericApi.Models;
using Microsoft.Extensions.Primitives;

namespace GenericApi.ApiRestHandler
{
    public interface IApiRestHandler
    {
        Task<Response> Get(string schema, string table, string? parameters = default);

        Task<Response> Post(string schema, string table, HttpObject obj);

        Task<Response> BulkInsert(string schema, string table, BulkInsert obj);

        Task<Response> Put(string schema, string table, HttpObject obj, string parameters);

        Task<Response> Delete(string schema, string table, string parameters);

        Task<Response> DeleteWithoutWhere(string schema, string table);

        Task<Response> ShowCreateTable(string schema, string table);

        Task<Response> ResetConnections();
    }
}
