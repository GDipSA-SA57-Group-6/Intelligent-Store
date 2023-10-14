using Microsoft.AspNetCore.Mvc;

namespace ShoppingCart.Controllers
{
    public class ShoppingCartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
