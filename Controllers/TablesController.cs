using GenericApi.ApiRestHandler;
using GenericApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace GenericApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TablesController : Controller, ITablesController
    {
        private readonly IApiRestHandler _apiRestHandler;

        public TablesController(IApiRestHandler apiRestHandler)
        {
            _apiRestHandler = apiRestHandler;
        }

        //Example format: schema=testapi&table=constants
        //Example format: schema=testapi&table=constants&parameters=(id=5|name=serverId)
        [HttpGet]
        public virtual ActionResult<Response> Get(string schema, string table, string? parameters = default)
        {
            return Ok(_apiRestHandler.Get(schema, table, parameters).Result);
        }

        [HttpPost]
        public virtual ActionResult<Response> Post(HttpObject obj)
        {
            return Ok(_apiRestHandler.Post(obj).Result);
        }

        //Example format: schema=testapi&table=constants&parameters=(id=5|name=serverId)
        [HttpPut]
        public virtual ActionResult<Response> Put(HttpObject obj, string parameters)
        {
            return Ok(_apiRestHandler.Put(obj, parameters).Result);
        }

        //Example format: schema=testapi&table=constants&parameters=(id=5|name=serverId)
        [HttpDelete]
        public virtual ActionResult<Response> Delete(string schema, string table, string parameters)
        {
            return Ok(_apiRestHandler.Delete(schema, table, parameters).Result);
        }

        [HttpDelete("/WithoutWhere")]
        public virtual ActionResult<Response> DeleteWithoutWhere(string schema, string table)
        {
            return Ok(_apiRestHandler.DeleteWithoutWhere(schema, table).Result);
        }
    }
}


