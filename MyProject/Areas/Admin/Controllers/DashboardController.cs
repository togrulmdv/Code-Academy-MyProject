using Microsoft.AspNetCore.Mvc;

namespace MyProject.Areas.Admin.Controllers;

[Area("Admin")]

public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
