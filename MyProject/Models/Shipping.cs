using MyProject.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Models;

public class Shipping : BaseEntity
{
	public string Image { get; set; }
	[Required, MaxLength(35)]
	public string Title { get; set; }
	[Required, MaxLength(45)]
	public string Description { get; set; }
	public bool IsDeleted { get; set; }
}