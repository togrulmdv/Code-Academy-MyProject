using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.Areas.Admin.ViewModels.ShippingViewModels;
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

	public async Task<IActionResult> Detail(int id)
	{
		Shipping? shipping = await _context.Shippings.FirstOrDefaultAsync(shp => shp.Id == id);
		if (shipping is null)
		{
			return NotFound();
		}

		return View(shipping);
	}

	public async Task<IActionResult> Add()
	{
		if (await _context.Shippings.CountAsync() == 3)
			return BadRequest();

		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Add(AddShippingViewModel addShippingViewModel)
	{
		if (await _context.Shippings.CountAsync() == 3)
			return BadRequest();

		if (!ModelState.IsValid)
			return View();

		Shipping shipping = new Shipping
		{
			Title = addShippingViewModel.Title,
			Image = addShippingViewModel.Image,
			Description = addShippingViewModel.Description,
		};

		await _context.Shippings.AddAsync(shipping);
		await _context.SaveChangesAsync();

		return RedirectToAction(nameof(Index));
	}

	public async Task<IActionResult> Update(int id)
	{
		var shipping = await _context.Shippings.FirstOrDefaultAsync(shp => shp.Id == id);
		if (shipping is null)
			return NotFound();

		UpdateShippingViewModel updateShippingViewModel = new UpdateShippingViewModel
		{
			Id = shipping.Id,
			Image = shipping.Image,
			Title = shipping.Title,
			Description = shipping.Description,
		};

		return View(updateShippingViewModel);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Update(int id, UpdateShippingViewModel updateShippingViewModel)
	{
		if (!ModelState.IsValid)
			return View();

		var shipping = await _context.Shippings.FirstOrDefaultAsync(shp => shp.Id == id);
		if (shipping is null)
			return NotFound();

		shipping.Image = updateShippingViewModel.Image;
		shipping.Title = updateShippingViewModel.Title;
		shipping.Description = updateShippingViewModel.Description;

		_context.Shippings.Update(shipping);
		await _context.SaveChangesAsync();

		return RedirectToAction(nameof(Index));
	}

	public async Task<IActionResult> Delete(int id)
	{
		IQueryable<Shipping> query = _context.Shippings.AsQueryable();

		if (await query.CountAsync() == 1)
			return BadRequest();

		var shipping = await query.FirstOrDefaultAsync(shp => shp.Id == id);
		if (shipping is null)
			return NotFound();

		DeleteShippingViewModel deleteShippingViewModel = new()
		{
			Image = shipping.Image,
			Title = shipping.Title,
			Description = shipping.Description,
		};

		return View(deleteShippingViewModel);
	}

	[HttpPost]
	[ActionName("Delete")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DeleteShipping(int id)
	{
		if (await _context.Shippings.CountAsync() == 1)
			return BadRequest();

		var shipping = await _context.Shippings.FirstOrDefaultAsync(shp => shp.Id == id);
		if (shipping is null)
			return NotFound();

		shipping.IsDeleted = true;
		await _context.SaveChangesAsync();

		return RedirectToAction(nameof(Index));
	}
}
