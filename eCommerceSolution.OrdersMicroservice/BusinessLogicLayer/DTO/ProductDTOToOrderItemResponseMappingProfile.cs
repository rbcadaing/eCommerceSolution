using AutoMapper;

namespace BusinessLogicLayer.DTO;

public class ProductDTOToOrderItemResponseMappingProfile : Profile
{
    public ProductDTOToOrderItemResponseMappingProfile()
    {
        CreateMap<ProductDTO, OrderItemResponse>()
          .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
          .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));
    }
}
