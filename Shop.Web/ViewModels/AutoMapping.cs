using AutoMapper;
using Shop.Data.Models;

namespace Shop.Web.ViewModels
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
