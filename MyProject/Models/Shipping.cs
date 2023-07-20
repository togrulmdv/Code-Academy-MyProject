using System.ComponentModel.DataAnnotations;

namespace MyProject.Models;

public class Shipping
{
	public int Id { get; set; }
	public string Image { get; set; }
	[Required, MaxLength(35)]
	public string Title { get; set; }
	[Required, MaxLength(45)]
	public string Description { get; set; }
}
