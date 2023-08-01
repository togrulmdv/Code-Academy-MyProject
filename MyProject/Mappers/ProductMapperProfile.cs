using AutoMapper;
using MyProject.Areas.Admin.ViewModels.ProductViewModels;
using MyProject.Models;

namespace MyProject.Mappers;

public class ProductMapperProfile : Profile
{
    //private IWebHostEnvironment _WebHostEnvironment;
    public ProductMapperProfile()
    {
        //_WebHostEnvironment = webHostEnvironment;
        CreateMap<CreateProductViewModel, Product>().ReverseMap();
        CreateMap<UpdateProductViewModel, Product> ().ReverseMap()
            .ForMember(prdct => prdct.Image.FileName, opt => opt.MapFrom(upv => upv.Image));
    }
}
