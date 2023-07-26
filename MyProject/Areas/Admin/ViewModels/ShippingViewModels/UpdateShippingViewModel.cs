namespace MyProject.Areas.Admin.ViewModels.ShippingViewModels;

public class UpdateShippingViewModel
{
    public int Id { get; set; }
    public IFormFile Image { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}