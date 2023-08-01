using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.Areas.Admin.ViewModels.ShippingViewModels;
using MyProject.Contexts;
using MyProject.Models;
using MyProject.Utils;

namespace MyProject.Areas.Admin.Controllers.HomePageControllers;

[Area("Admin")]

public class ShippingController : Controller
{

	private readonly AppDbContext _context;
	private readonly IWebHostEnvironment _webHostEnvironment;

	public ShippingController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
	{
		_context = context;
		_webHostEnvironment = webHostEnvironment;
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

	public async Task<IActionResult> Create()
	{
		if (await _context.Shippings.CountAsync() == 3)
			return BadRequest();

		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(CreateShippingViewModel createShippingViewModel)
	{
		if (await _context.Shippings.CountAsync() == 3)
			return BadRequest();

		if (!ModelState.IsValid)
			return View();

		if(!createShippingViewModel.Image.CheckFileType("image/"))
		{
			ModelState.AddModelError("Image", "File type must be Image");
			return View();
		}

		if(!createShippingViewModel.Image.CheckFileSize(100))
		{
			ModelState.AddModelError("Image", "Image can not be larger than 100 KB");
			return View();
		}

		string fileName = $"{Guid.NewGuid()}-{createShippingViewModel.Image.FileName}";

		string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "images", "website-images", fileName);

		using(FileStream fileStream = new FileStream(path, FileMode.Create))
		{
			await createShippingViewModel.Image.CopyToAsync(fileStream);
		}

		Shipping shipping = new Shipping
		{
			Title = createShippingViewModel.Title,
			Image = fileName,
			Description = createShippingViewModel.Description,
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

		if (updateShippingViewModel.Image is not null)
		{
			if (!updateShippingViewModel.Image.CheckFileType("image/"))
			{
				ModelState.AddModelError("Image", "File type must be Image");
				return View();
			}

			if (!updateShippingViewModel.Image.CheckFileSize(100))
			{
				ModelState.AddModelError("Image", "Image can not be larger than 100 KB");
				return View();
			}

			string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "images", "website-images", shipping.Image);

			if (System.IO.File.Exists(path))
			{
				System.IO.File.Delete(path);
			}

			string fileName = $"{Guid.NewGuid()}-{updateShippingViewModel.Image.FileName}";

			string newPath = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "images", "website-images", fileName);

			using (FileStream fileStream = new FileStream(newPath, FileMode.Create))
			{
				await updateShippingViewModel.Image.CopyToAsync(fileStream);
			}

			shipping.Image = fileName;
		}

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

		string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "images", "website-images", shipping.Image);

		if(System.IO.File.Exists(path))
		{
			System.IO.File.Delete(path);
		}

		shipping.IsDeleted = true;
		await _context.SaveChangesAsync();

		return RedirectToAction(nameof(Index));
	}
}
