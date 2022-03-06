using GenericApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace GenericApi.Controllers
{
    public interface ITablesController
    {

        ActionResult<Response> Get(string schema, string table, string? parameters = default);

        ActionResult<Response> Post(HttpObject obj);

        ActionResult<Response> Put(HttpObject obj, string parameters);

        ActionResult<Response> Delete(string schema, string table, string parameters);

        ActionResult<Response> DeleteWithoutWhere(string schema, string table);
    }
}
