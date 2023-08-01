using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyProject.Models.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Models;

public class Product : BaseEntity
{
	[Required, MaxLength(120)]
	public string Name { get; set; }
	[Column(TypeName = "decimal(6, 2)")]
	public decimal Price { get; set; }
	public string Image { get; set; }
	[Required, MaxLength(500)]
	public string Description { get; set; }
	[Required, Range(1, 5)]
	public int Rating { get; set; }
	public int CategoryId { get; set; }
	public Category Category { get; set; }
	public bool IsDeleted { get; set; }
	public DateTime CreatedDate { get; set; }
	public string CreatedBy { get; set; }
	public DateTime UpdatedDate { get; set; }
	public string UpdatedBy { get; set; }
}