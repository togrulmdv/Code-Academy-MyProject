using System.ComponentModel.DataAnnotations;

namespace MyProject.Areas.Admin.ViewModels.ShippingViewModels;

public class UpdateShippingViewModel
{
    public int Id { get; set; }
    public IFormFile? Image { get; set; }
	[Required, MaxLength(120)]
	public string Title { get; set; }
	[Required, MaxLength(150)]
	public string Description { get; set; }
}