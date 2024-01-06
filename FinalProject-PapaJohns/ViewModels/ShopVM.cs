using FinalProject_PapaJohns.Helpers;
using FinalProject_PapaJohns.Models;

namespace FinalProject_PapaJohns.ViewModels
{
    public class ShopVM
    {
        public List<Product> Products { get; set; }

        public Pagination <Product> pagination { get; set; }


    }
}
