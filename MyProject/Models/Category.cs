using MyProject.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Models;

public class Category : BaseEntity
{
	[Required, MaxLength(100)]
	public string Name { get; set; }
	public bool IsDeleted { get; set; }
	public ICollection<Product>? Products { get; set; }
}