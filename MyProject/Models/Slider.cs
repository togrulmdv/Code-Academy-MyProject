using MyProject.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Models;

public class Slider : BaseEntity
{
	[Required, MaxLength(20)]
	public string Suptitle { get; set; }
	[Required, MaxLength(15)]
	public string Title { get; set; }
	[Required, MaxLength(35)]
	public string Description { get; set; }
	public string Image { get; set; }
	public bool IsDeleted { get; set; }
}