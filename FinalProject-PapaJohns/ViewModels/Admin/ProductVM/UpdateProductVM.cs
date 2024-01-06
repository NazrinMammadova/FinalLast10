using FinalProject_PapaJohns.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject_PapaJohns.ViewModels.Admin.ProductVM
{
    public class UpdateProductVM
    {
        public string Name { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number")]
        public double Price { get; set; }

        public int CategoryId { get; set; }


        public string AddedBy { get; set; }
        public string? Desc { get; set; }

        public int Id { get; set; }
        public DateTime UpdatedAt { get; set; }
        public IFormFile? newPhoto { get; set; }
        public Category category { get; set; }


    }
}
