using GenericApi.ApiRestHandler;
using GenericApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace GenericApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : Controller
    {

        private readonly IApiRestHandler _apiRestHandler;

        public AdminController(IApiRestHandler apiRestHandler)
        {
            _apiRestHandler = apiRestHandler;
        }

        [HttpGet("/ResetConnections")]
        public virtual ActionResult<Response> ResetConnections()
        {
            return Ok(_apiRestHandler.ResetConnections().Result);
        }
    }
}


