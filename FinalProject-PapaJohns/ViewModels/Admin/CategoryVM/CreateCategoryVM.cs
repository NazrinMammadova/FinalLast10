using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FinalProject_PapaJohns.ViewModels.Admin.CategoryVM
{
    public class CreateCategoryVM
    {

        [Required]
        public string Name { get; set; }
        public string AddedBy { get; set; }
        public DateTime CreatedAt { get; set; }




    }
}
