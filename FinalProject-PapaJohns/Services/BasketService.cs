using FinalProject_PapaJohns.Services;
using FinalProject_PapaJohns.DAL;
using FinalProject_PapaJohns.Services;
using FinalProject_PapaJohns.ViewModels;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FinalProject_PapaJohns.Services
{
    public class BasketService : IBasketService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly AppDbContext _context;
        public BasketService(IHttpContextAccessor contextAccessor, AppDbContext context)
        {
            _contextAccessor = contextAccessor;
            _context = context;
        }

        public void Decrease(int id)
        {
            string basket = _contextAccessor.HttpContext.Request.Cookies["basket"];
            var products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            var decreasedProduct = products.FirstOrDefault(p => p.Id == id);

            if (decreasedProduct != null)
            {
                if (decreasedProduct.BasketCount > 0)
                {
                    decreasedProduct.BasketCount--;

                    if (decreasedProduct.BasketCount == 0)
                    {
                        var productToRemove = products.FirstOrDefault(p => p.Id == id);
                        if (productToRemove != null)
                        {
                            products.Remove(productToRemove);
                        }
                    }


                    _contextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(products));
                }
            }
        }



        public void Delete(int id)
        {
            string basket = _contextAccessor.HttpContext.Request.Cookies["basket"];
            var products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            var deletedProduct = products.FirstOrDefault(p => p.Id == id);

            if (deletedProduct != null)
            {
                products.Remove(deletedProduct);
                _contextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(products));
            }
        }

        public List<BasketVM> GetBasket()
        {
            List<BasketVM> baskets = new();
            string basket = _contextAccessor.HttpContext.Request.Cookies["basket"];

            if (basket == null) return baskets;
            baskets = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            foreach (var basketProduct in baskets)
            {

                var dbProduct = _context.Products.FirstOrDefault(p => p.Id == basketProduct.Id);

                basketProduct.Name = dbProduct.Name;
                basketProduct.ImageUrl = dbProduct.ImageUrl;
            }

            return baskets;

        }

        public int GetCount()
        {

            string basket = _contextAccessor.HttpContext.Request.Cookies["basket"];
            if (basket == null) return 0;
            return JsonConvert.DeserializeObject<List<BasketVM>>(basket).Count;


        }

        public void Increase(int id)
        {
            string basket = _contextAccessor.HttpContext.Request.Cookies["basket"];
            var products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            var increasedProduct = products.FirstOrDefault(p => p.Id == id);

            if (increasedProduct != null)
            {
                increasedProduct.BasketCount++;
                _contextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(products));
            }
        }
    }
}