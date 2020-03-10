using AutoMapper;
using Shop.Data.Models;

namespace Shop.ViewModels
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Product, ProductViewModel>();
            CreateMap<CartProduct, CartProductViewModel>();
        }
    }
}
