using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.Contexts;
using MyProject.Models;

namespace MyProject.Areas.Admin.Controllers.HomePageControllers;

[Area("Admin")]

public class SliderController : Controller
{

	private readonly AppDbContext _context;

	public SliderController(AppDbContext context)
	{
		_context = context;
	}

	public async Task<IActionResult> Index()
	{

		var sliders = await _context.Sliders.ToListAsync();

		return View(sliders);
	}

	public IActionResult Add()
	{
		return View();
	}

	[HttpPost]

	public async Task<IActionResult> Add(Slider slider)
	{
		if (!ModelState.IsValid)
		{
			return View();
		}

		await _context.Sliders.AddAsync(slider);
		await _context.SaveChangesAsync();

		return RedirectToAction("Index");
	}

	public async Task<IActionResult> Detail(int id)
	{
		Slider? slider = await _context.Sliders.FirstOrDefaultAsync(shp => shp.Id == id);
		if (slider is null)
		{
			return NotFound();
		}

		return View(slider);
	}

	public async Task<IActionResult> Delete(int id)
	{
		Slider? slider = await _context.Sliders.FirstOrDefaultAsync(sld => sld.Id == id);
		if (slider is null)
		{
			return NotFound();
		}

		return View(slider);
	}

	[HttpPost]
	[ActionName("Delete")]

	public async Task<IActionResult> DeleteItem(int id)
	{
		Slider? slider = await _context.Sliders.FirstOrDefaultAsync(shp => shp.Id == id);
		if (slider is null)
		{
			return NotFound();
		}

		_context.Sliders.Remove(slider);
		await _context.SaveChangesAsync();

		return RedirectToAction("Index");
	}
}