namespace FinalProject_PapaJohns.ViewModels.Admin.PizzaPicturesVM
{
    public class CreatePizzaPicturesVM
    {

        public IFormFile Photo { get; set; }
        public DateTime CreatedAt { get; set; }

        public string AddedBy { get; set; }



    }
}
