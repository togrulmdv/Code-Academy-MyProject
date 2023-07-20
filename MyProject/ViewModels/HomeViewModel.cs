using MyProject.Models;

namespace MyProject.ViewModels;

public class HomeViewModel
{
	public IEnumerable<Slider> Sliders { get; set; }
	public IEnumerable<Shipping> Shippings { get; set; }
}
