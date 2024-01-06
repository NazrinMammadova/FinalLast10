using System.ComponentModel.DataAnnotations;

namespace FinalProject_PapaJohns.ViewModels.Account
{
    public class ResetPasswordVM
    {
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmedPassword { get; set; }



    }
}
