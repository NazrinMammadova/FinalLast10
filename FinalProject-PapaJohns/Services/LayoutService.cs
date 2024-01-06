using FinalProject_PapaJohns.DAL;
using FinalProject_PapaJohns.Models;
using FinalProject_PapaJohns.Services;

namespace FinalProject_PapaJohns.Services
{
    public class LayoutService : ILayoutService
    {
        private readonly AppDbContext _appDbContext;

        public LayoutService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public List<Category> GetAllCategories()
        {
            var categories = _appDbContext.Categories.ToList();
            return categories;



        }



    }
}
