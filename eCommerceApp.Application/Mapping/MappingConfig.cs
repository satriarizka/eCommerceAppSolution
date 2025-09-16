using AutoMapper;
using eCommerceApp.Application.DTOs.Cart;
using eCommerceApp.Application.DTOs.Category;
using eCommerceApp.Application.DTOs.Identity;
using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Entities.Cart;
using eCommerceApp.Domain.Entities.Identity;

namespace eCommerceApp.Application.Mapping
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            // Product → GetProduct
            CreateMap<Product, GetProduct>()
                .ForMember(dest => dest.Base64Image, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null));

            // GetProduct → Product (untuk Create/Update)
            CreateMap<CreateProduct, Product>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Base64Image));
            CreateMap<UpdateProduct, Product>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Base64Image));

            // Category → GetCategory
            CreateMap<Category, GetCategory>();

            // DTO → Category
            CreateMap<CreateCategory, Category>();
            CreateMap<UpdateCategory, Category>();

            // Identity
            CreateMap<CreateUser, AppUser>();
            CreateMap<LoginUser, AppUser>();

            // Cart / Payment
            CreateMap<PaymentMethod, GetPaymentMethod>();
            CreateMap<CreateAchieve, Achieve>();
        }
    }
}
