using System.ComponentModel.DataAnnotations;

namespace MyProject.Areas.Admin.ViewModels.ShippingViewModels;

public class AddShippingViewModel
{
    public string Image { get; set; }
    [Required, MaxLength(120)]
    public string Title { get; set; }
    [Required, MaxLength(255)]
    public string Description { get; set; }
}