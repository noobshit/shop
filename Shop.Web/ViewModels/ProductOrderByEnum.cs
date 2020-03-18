using System.ComponentModel.DataAnnotations;

namespace Shop.Web.ViewModels
{
    public enum ProductOrderByEnum
    {
        [Display(Name = "Default order")]
        Default,
        [Display(Name ="Low price first")]
        PriceLow,
        [Display(Name ="High price first")]
        PriceHigh,
        [Display(Name ="Alphabetical order")]
        NameLow,
        [Display(Name ="Reverse alphabetical order")]
        NameHigh,
    }
}
