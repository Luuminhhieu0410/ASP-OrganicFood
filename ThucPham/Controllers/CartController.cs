using Microsoft.AspNetCore.Mvc;

namespace ThucPham.Controllers
{
    public class CartController : Controller
    {
        public IActionResult AddToCart()
        {
            return View();
        }
    }
}
