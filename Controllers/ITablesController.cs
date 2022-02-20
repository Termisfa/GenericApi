using Microsoft.AspNetCore.Mvc;

namespace GenericApi.Controllers
{
    public interface ITablesController
    {

        ActionResult<string> Get(string schema, string table, string? parameters = default);

        IActionResult Post(HttpObject obj);
        
        IActionResult Put(HttpObject obj, string parameters);
        
        IActionResult Delete(string schema, string table, string parameters);
        
        IActionResult DeleteWithoutWhere(string schema, string table);
    }
}
