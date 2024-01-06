using System.ComponentModel.DataAnnotations;

namespace FinalProject_PapaJohns.ViewModels.Admin.CategoryVM
{
    public class UpdateCategoryVM
    {
        [Required]
        public string Name { get; set; }
        public string AddedBy { get; set; }
        public DateTime UpdatedAt { get; set; }




    }


    


}
