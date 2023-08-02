using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using MyProject.Areas.Admin.ViewModels.ProductViewModels;
using MyProject.Contexts;
using MyProject.Exceptions;
using MyProject.Models;
using MyProject.Services.Interfaces;

namespace MyProject.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{

	private string _errorMessage;
	private readonly AppDbContext _context;
	private readonly IWebHostEnvironment _webHostEnvironment;
	private readonly IFileService _fileService;
	private readonly IMapper _mapper;

    public ProductController(AppDbContext context, IWebHostEnvironment webHostEnvironment, IFileService fileService, IMapper mapper)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
        _fileService = fileService;
        _mapper = mapper;
    }

    public async Task GetCategoriesAsync()
	{
		ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
	}

	public async Task<IActionResult> Index()
	{
		var products = await _context.Products.Include(p => p.Category).AsNoTracking().ToListAsync();

		//ProductViewModel productViewModel = new ProductViewModel()
		//{
		//	Name = product.Name,
		//};

		return View(products);
	}

	//public async Task<IActionResult> Detail()
	//{
	//	await GetCategoriesAsync();
	//}

	public async Task<IActionResult> Create()
	{
		await GetCategoriesAsync();


		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(CreateProductViewModel productCreateVM)
	{
		await GetCategoriesAsync();
		if (!ModelState.IsValid)
			return View();
		bool isEXist = _context.Products.Any(p => p.Name.ToLower().Trim() == productCreateVM.Name.ToLower().Trim());
		if (isEXist)
		{
			ModelState.AddModelError("Name", "Product is already exist");
			return View();

		}
		string fileName = string.Empty;
		try
		{
			fileName = await _fileService.CreateFileAsync(file: productCreateVM.Image, path: Path.Combine(_webHostEnvironment.WebRootPath, "assets", "images", "website-images"), maxFileSize: 100, fileType: "image/");
		}
		catch (FileSizeException ex)
		{
			ModelState.AddModelError("Image", ex.Message);
			return View();
		}
		catch (FileTypeException ex)
		{
			ModelState.AddModelError("Image", ex.Message);
			return View();
		}

		//Product product = new Product()
		//{
		//	Name = productCreateVM.Name,
		//	Price = productCreateVM.Price,
		//	Description = productCreateVM.Description,
		//	Rating = productCreateVM.Rating,
		//	CategoryId = productCreateVM.CategoryId,
		//	Image = fileName,
		//	CreatedBy = "Admin",
		//	CreatedDate = DateTime.UtcNow,
		//	UpdatedBy = "Admin",
		//	UpdatedDate = DateTime.UtcNow,
		//};

		var product = _mapper.Map<Product>(productCreateVM);

		await _context.AddAsync(product);
		await _context.SaveChangesAsync();
		return RedirectToAction(nameof(Index));
	}

    public async Task<IActionResult> Update(int id)
    {
        var product = await _context.Products/*Include(p => p.Category)*/.FirstOrDefaultAsync(f => f.Id == id);
        if (product is null)
            return NotFound();
		
		await GetCategoriesAsync();

		UpdateProductViewModel updateProductViewModel = _mapper.Map<UpdateProductViewModel>(product);

        return View(updateProductViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int id, UpdateProductViewModel updateProductViewModel)
    {
		await GetCategoriesAsync();
        
		if (!ModelState.IsValid)
            return View();

        var product = await _context.Products.FirstOrDefaultAsync(prdct => prdct.Id == id);
        
		if (product is null)
            return NotFound();

		string fileName = product.Image;

        if (updateProductViewModel.Image is not null)
        {
            try
            {
				string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "images", "website-images");

				_fileService.DeleteFile(Path.Combine(path, product.Image));


				fileName = await _fileService.CreateFileAsync(updateProductViewModel.Image, path, 100, "image/");
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError("Image", ex.Message);
                return View();
            }
            catch (FileTypeException ex)
            {
                ModelState.AddModelError("Image", ex.Message);
                return View();
            }
        }

        _mapper.Map(updateProductViewModel, product);
        product.Image = fileName;

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}