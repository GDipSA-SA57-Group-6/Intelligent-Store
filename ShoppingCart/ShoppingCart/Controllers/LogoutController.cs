using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Models;

namespace ShoppingCart.Controllers
{
	public class LogoutController : Controller
	{
		// dependency injection
		private readonly LoggedUser loggedUser;
		public LogoutController(LoggedUser loggedUser)
		{
			this.loggedUser = loggedUser;
		}

		/// <summary>
		/// 进入这个界面一定要先登录 否则转到登陆界面 采取一定的保护措施
		/// </summary>
		/// <returns></returns>
		public IActionResult Index()
		{
			Guid? userId = GetUserId();

			if(userId != Guid.Empty && AuthenticateLoginStatus((Guid)userId))
			{
				Response.Cookies.Delete("UserId");
			}
			return RedirectToAction("Index", "Login");
		}

		public bool AuthenticateLoginStatus(Guid userId)
		{
			if(userId == Guid.Empty) return false;
			if(loggedUser.loggedDic.ContainsKey(userId))
			{
				return true;
			}
			else return false;
		}

		/// <summary>
		/// 返回当前HTTP请求的UserId，如果有的话
		/// </summary>
		/// <returns></returns>
		public Guid? GetUserId()
		{
			string? userId = Request.Cookies["UserId"];
			if(userId == null) { return null; }
			else return Guid.Parse(userId);
		} 
	}
}
