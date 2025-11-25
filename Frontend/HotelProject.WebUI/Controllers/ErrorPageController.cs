using Microsoft.AspNetCore.Mvc;

namespace HotelProject.WebUI.Controllers
{
    public class ErrorPageControlle : Controller
    {
        public IActionResult ErrorPage()
        {
            return View();
        }
    }
}
