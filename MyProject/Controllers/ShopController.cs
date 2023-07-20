using Microsoft.AspNetCore.Mvc;

namespace MyProject.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
