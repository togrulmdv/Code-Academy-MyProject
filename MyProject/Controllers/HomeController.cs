using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    public async Task<IActionResult> Index()
	{
        var sliders = await _context.Sliders.ToListAsync();
        var shippings = await _context.Shippings.ToListAsync();

        var homeViewModels = new HomeViewModel
        {
            Sliders = sliders,
            Shippings = shippings
        };

		return View(homeViewModels);
	}
}
