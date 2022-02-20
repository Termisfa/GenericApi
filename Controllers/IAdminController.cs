using Microsoft.AspNetCore.Mvc;

namespace GenericApi.Controllers
{
    public interface IAdminController
    {

        IActionResult Delete();
    }
}
