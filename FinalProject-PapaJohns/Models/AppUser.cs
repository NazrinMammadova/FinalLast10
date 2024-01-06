using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject_PapaJohns.Models
{
    public class AppUser:IdentityUser
    {
        [NotMapped]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsBlocked { get; set; }
        public List<Sale> Sales { get; set; }

        //public List<Coment> Coments { get; set; }

        //public bool IsSubscribed { get; set; }

    }
}
