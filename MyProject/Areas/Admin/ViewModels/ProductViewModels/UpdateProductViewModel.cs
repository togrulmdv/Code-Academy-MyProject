using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Areas.Admin.ViewModels.ProductViewModels;

public class UpdateProductViewModel
{
    [Required, MaxLength(120)]
    public string Name { get; set; }
    [Column(TypeName = "decimal(6, 2)")]
    public decimal Price { get; set; }
    public IFormFile Image { get; set; }
    [Required, MaxLength(500)]
    public string Description { get; set; }
    [Required, Range(1, 5)]
    public int Rating { get; set; }
    public int CategoryId { get; set; }
}
