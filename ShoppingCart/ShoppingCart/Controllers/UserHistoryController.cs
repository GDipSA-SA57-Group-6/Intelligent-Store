using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Models;

namespace ShoppingCart.Controllers
{
    public class UserHistoryController : Controller
    {
        private readonly LoggedUser loggedUser;
        public UserHistoryController(LoggedUser loggedUser)
        {
            this.loggedUser = loggedUser;
        }

        /// <summary>
        /// 需要对这个控制器做保护，想要访问这个界面，必须先登录
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            if(!AuthenticateStatus()) return RedirectToAction("Index", "Login");
            else return View();
        }

        public bool AuthenticateStatus()
        {
            if (Request.Cookies["UserId"] == null) return false;

            Guid userId = Guid.Parse(Request.Cookies["UserId"]);

            if (!loggedUser.loggedDic.ContainsKey(userId)) return false;

            return true;
        }
    }
}
