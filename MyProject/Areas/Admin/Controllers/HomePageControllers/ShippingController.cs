using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.Contexts;
using MyProject.Models;

namespace MyProject.Areas.Admin.Controllers.HomePageControllers;

[Area("Admin")]

public class ShippingController : Controller
{

	private readonly AppDbContext _context;

	public ShippingController(AppDbContext context)
	{
		_context = context;
	}

	public async Task<IActionResult> Index()
	{

		var shippings = await _context.Shippings.ToListAsync();

		return View(shippings);
	}

	public IActionResult Add()
	{
		return View();
	}

	[HttpPost]

	public async Task<IActionResult> Add(Shipping shipping)
	{
		if (!ModelState.IsValid)
		{
			return View();
		}

		await _context.Shippings.AddAsync(shipping);
		await _context.SaveChangesAsync();

		return RedirectToAction("Index");
	}

	public async Task<IActionResult> Detail(int id)
	{
		Shipping? shipping = await _context.Shippings.FirstOrDefaultAsync(shp => shp.Id == id);
		if (shipping is null)
		{
			return NotFound();
		}

		return View(shipping);
	}

	public async Task<IActionResult> Delete(int id)
	{
		Shipping? shipping = await _context.Shippings.FirstOrDefaultAsync(shp => shp.Id == id);
		if (shipping is null)
		{
			return NotFound();
		}

		return View(shipping);
	}

	[HttpPost]
	[ActionName("Delete")]

	public async Task<IActionResult> DeleteItem(int id)
	{
		Shipping? shipping = await _context.Shippings.FirstOrDefaultAsync(shp => shp.Id == id);
		if (shipping is null)
		{
			return NotFound();
		}

		_context.Shippings.Remove(shipping);
		await _context.SaveChangesAsync();

		return RedirectToAction("Index");
	}

	public async Task<IActionResult> Update(int id)
	{
		Shipping? shipping = await _context.Shippings.FirstOrDefaultAsync(shp => shp.Id == id);
		if (shipping is null)
		{
			return NotFound();
		}

		return View(shipping);
	}

	[HttpPost]
	[ActionName("Update")]

	public async Task<IActionResult> UpdateItem(int id, Shipping shippingItem)
	{
		Shipping? shipping = await _context.Shippings.FirstOrDefaultAsync(shp => shp.Id == id);
		if (shipping is null)
		{
			return NotFound();
		}

		_context.Shippings.Update(shipping);
		await _context.SaveChangesAsync();

		return RedirectToAction("Index");
	}
}
