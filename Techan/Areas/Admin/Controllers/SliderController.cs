using Microsoft.AspNetCore.Mvc;

namespace Techan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}