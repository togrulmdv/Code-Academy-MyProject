using AutoMapper;
using MyProject.Areas.Admin.ViewModels.ProductViewModels;
using MyProject.Models;

namespace MyProject.Mappers;

public class ProductMapperProfile : Profile
{
    public ProductMapperProfile()
    {
        CreateMap<CreateProductViewModel, Product>()
            .ReverseMap();
        CreateMap<UpdateProductViewModel, Product> ()
            .ReverseMap()
            .ForMember(upvm => upvm.Image, x => x.Ignore());
    }
}
