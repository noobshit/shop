using System.ComponentModel.DataAnnotations;

namespace Shop.Web.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }

        public string ImagePath { get; set; }
    }
}
