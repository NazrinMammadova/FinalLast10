namespace FinalProject_PapaJohns.ViewModels.Admin.HomeSliderVM
{
    public class UpdateHomeSliderVM
    {
        public string? ImageUrl { get; set; }
        public IFormFile Photo { get; set; }

        public DateTime UpdatedAt { get; set; }
        public string AddedBy { get; set; }

    }
}
