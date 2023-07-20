using Microsoft.AspNetCore.Mvc;
using MyProject.Contexts;
using MyProject.Models;
using MyProject.ViewModels;
using System.Drawing.Text;

namespace MyProject.Controllers;

public class HomeController : Controller
{
	private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
	{
        var sliders = _context.Sliders;
        var shippings = _context.Shippings;

        var homeViewModels = new HomeViewModel
        {
            Sliders = sliders,
            Shippings = shippings
        };

		return View(homeViewModels);
	}
}
