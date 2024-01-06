namespace FinalProject_PapaJohns.ViewModels.Admin.PizzaPicturesVM
{
    public class UpdatePizzaPicturesVM
    {
        public string? ImageUrl { get; set; }
        public IFormFile Photo { get; set; }

        public DateTime UpdatedAt { get; set; }
        public string AddedBy { get; set; }

    }
}
