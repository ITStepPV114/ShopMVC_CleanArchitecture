using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using ShopMVC.Helper;

namespace ShopMVC.Services
{
    public class CartService : ICartService
    {
        private readonly HttpContext? _httpContext;
        private readonly IProductsService _productsService;

        public CartService(IHttpContextAccessor httpContextAccessor, IProductsService productsService)
        {
            _httpContext = httpContextAccessor.HttpContext;
            _productsService = productsService;
        }
        public IEnumerable<ProductDto> GetProducts()
        {
            List<int> idList = _httpContext.Session.GetObject<List<int>>("mycart");
            if (idList == null) idList = new List<int>();
            List<ProductDto> products = idList.Select(id => _productsService.Get(id)).ToList();
            return products;
        }
        public void Add(int id)
        {
            if (_productsService.Get(id) == null) return ;
            List<int>? idList = _httpContext.Session.GetObject<List<int>>("mycart");
            if (idList == null) idList = new List<int>();
            idList.Add(id); //add id of product to cart
            _httpContext.Session.SetObject<List<int>>("mycart", idList);
        }

        public void Remove(int id)
        {
            if (_productsService.Get(id) == null) return ;
            List<int>? idList = _httpContext.Session.GetObject<List<int>>("mycart");
            if (idList == null) idList = new List<int>();
            idList.Remove(id); //delete id of product to cart
            _httpContext.Session.SetObject<List<int>>("mycart", idList);
        }
    }
}
