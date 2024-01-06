using FinalProject_PapaJohns.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinalProject_PapaJohns.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {


        }
        public DbSet<HomeSlider> HomeSliders { get; set; }
        public DbSet<PapaService> PapaServices { get; set; }
        public DbSet<PizzaPicture> pizzaPictures { get; set; }
        public DbSet<SloganPapaJohns> SloganPapaJohns { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<MenuPicture> MenuPictures{ get; set; }
        public DbSet<RuleRegulation> RuleRegulations{ get; set; }
        public DbSet<AboutPapaJohns> AboutPapaJohns{ get; set; }
        public DbSet<Contact> Contacts{ get; set; }
        public DbSet<Product> Products{ get; set; }
        public DbSet<Category> Categories{ get; set; }

        public DbSet<SaleProduct> SaleProducts { get; set; }
        public DbSet<Sale> Sales { get; set; }






    }
}
