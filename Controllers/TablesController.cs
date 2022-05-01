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
        public virtual async Task<ActionResult<Response>> Get(string table, string? parameters = default)
        {
            return Ok(await _apiRestHandler.Get(Request.Headers["schema"], table, parameters));
        }

        [AuthorizeCustom]
        [HttpPost]
        public virtual async Task<ActionResult<Response>> Post(string table, HttpObject obj)
        {
            return Ok(await _apiRestHandler.Post(Request.Headers["schema"], table, obj));
        }

        [AuthorizeCustom]
        [HttpPost("BulkInsert")]
        public virtual async Task<ActionResult<Response>> BulkInsert(string table, BulkInsert obj)
        {
            return Ok(await _apiRestHandler.BulkInsert(Request.Headers["schema"], table, obj));
        }

        //Example format: table=constants&parameters=(id=5|name=serverId)
        [AuthorizeCustom]
        [HttpPut]
        public virtual async Task<ActionResult<Response>> Put(string table, string parameters, HttpObject obj)
        {
            return Ok(await _apiRestHandler.Put(Request.Headers["schema"], table, obj, parameters));
        }

        //Example format: table=constants&parameters=(id=5|name=serverId)
        [AuthorizeCustom]
        [HttpDelete]
        public virtual async Task<ActionResult<Response>> Delete(string table, string parameters)
        {
            return Ok(await _apiRestHandler.Delete(Request.Headers["schema"], table, parameters));
        }

        [AuthorizeCustom]
        [HttpDelete("/WithoutWhere")]
        public virtual async Task<ActionResult<Response>> DeleteWithoutWhere(string table)
        {
            return Ok(await _apiRestHandler.DeleteWithoutWhere(Request.Headers["schema"], table));
        }

        //Example format: ShowCreateTable/table=constants
        [AuthorizeCustom]
        [HttpGet("ShowCreateTable")]
        public virtual async Task<ActionResult<Response>> ShowCreateTable(string table)
        {
            return Ok(await _apiRestHandler.ShowCreateTable(Request.Headers["schema"], table));
        }
    }
}


