namespace FinalProject_PapaJohns.ViewModels.Admin.HomeSliderVM
{
    public class CreateHomeSliderVM
    {
        public IFormFile Photo { get; set; }
        public DateTime CreatedAt { get; set; }

        public string AddedBy { get; set; }
    }
}
