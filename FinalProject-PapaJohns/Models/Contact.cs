using FinalProject_PapaJohns.Models;

namespace FinalProject_PapaJohns.Models
{
    public class Contact:BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }


    }

}
