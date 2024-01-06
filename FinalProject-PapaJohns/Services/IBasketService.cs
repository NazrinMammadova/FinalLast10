
using FinalProject_PapaJohns.ViewModels;

namespace FinalProject_PapaJohns.Services
{
    public interface IBasketService
    {
        int GetCount();
        void Increase(int id);
        void Decrease(int id);
        void Delete(int id);

        List<BasketVM> GetBasket();

        


    }
}