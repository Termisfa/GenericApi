using GenericApi.ApiRestHandler;
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

        [HttpDelete]
        public virtual IActionResult Delete()
        {
            _apiRestHandler.DeleteConnectionStrings();
            return Ok();
        }

    }
}


