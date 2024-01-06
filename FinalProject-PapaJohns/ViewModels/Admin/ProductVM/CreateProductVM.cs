using FinalProject_PapaJohns.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject_PapaJohns.ViewModels.Admin.ProductVM
{
    public class CreateProductVM
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number")]
        public double Price { get; set; }

        [Required(ErrorMessage = "CategoryId is required")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Photo is required")]
        public IFormFile Photo { get; set; }

        [Required(ErrorMessage = "AddedBy is required")]
        public string AddedBy { get; set; }
        public string? Desc { get; set; }





    }
}
