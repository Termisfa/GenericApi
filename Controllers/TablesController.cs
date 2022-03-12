using GenericApi.ApiRestHandler;
using GenericApi.Authorization;
using GenericApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace GenericApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TablesController : Controller
    {
        private readonly IApiRestHandler _apiRestHandler;

        public TablesController(IApiRestHandler apiRestHandler)
        {
            _apiRestHandler = apiRestHandler;
        }

        //Example format: table=constants
        //Example format: table=constants&parameters=(id=5|name=serverId)
        [AuthorizeCustom]
        [HttpGet]
        public virtual ActionResult<Response> Get(string table, string? parameters = default)
        {
            return Ok(_apiRestHandler.Get(Request.Headers["schema"], table, parameters).Result);
        }

        [AuthorizeCustom]
        [HttpPost]
        public virtual ActionResult<Response> Post(string table, HttpObject obj)
        {
            return Ok(_apiRestHandler.Post(Request.Headers["schema"], table, obj).Result);
        }

        //Example format: table=constants&parameters=(id=5|name=serverId)
        [AuthorizeCustom]
        [HttpPut]
        public virtual ActionResult<Response> Put(string table, string parameters, HttpObject obj)
        {
            return Ok(_apiRestHandler.Put(Request.Headers["schema"], table, obj, parameters).Result);
        }

        //Example format: table=constants&parameters=(id=5|name=serverId)
        [AuthorizeCustom]
        [HttpDelete]
        public virtual ActionResult<Response> Delete(string table, string parameters)
        {
            return Ok(_apiRestHandler.Delete(Request.Headers["schema"], table, parameters).Result);
        }

        [AuthorizeCustom]
        [HttpDelete("/WithoutWhere")]
        public virtual ActionResult<Response> DeleteWithoutWhere(string table)
        {
            return Ok(_apiRestHandler.DeleteWithoutWhere(Request.Headers["schema"], table).Result);
        }
    }
}


