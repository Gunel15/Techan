using Microsoft.AspNetCore.Mvc;

namespace Techan.Areas.Admin.Controllers
{
    public class BrandController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
