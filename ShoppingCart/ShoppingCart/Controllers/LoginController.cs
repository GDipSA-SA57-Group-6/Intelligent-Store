using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Models;
using System.Diagnostics;

namespace ShoppingCart.Controllers
{
    public class LoginController : Controller
    {
        /// <summary>
        /// 把已经在全局注册好的依赖注入到这个controller中, 目的在于访问其中的Dictionary
        /// </summary>
        private readonly LoggedUser _loggedUser;
        public LoginController(LoggedUser loggedUser)
        {
            this._loggedUser = loggedUser;
        }


		/// <summary>
		/// 用来持续跟踪的数据结构得使用依赖注入 不能在页面里定义 否则每次刷新页面也会刷新该数据结构
		/// 
		/// 在这里检查用户有无登录 如果登陆了 直接更新订单数据库 也就是说需要在cookie里传回购物车里的对象
		/// 
		/// 这里要注意 因为每次启动 dependency都会清空，所以可能存在用户有id，表里没有的情况 所以要两个判断条件都满足
		/// </summary>
		/// <returns></returns>
		public IActionResult Index()
        {
            string assignedUserId = RetrieveSessionId();
            if (assignedUserId != null && _loggedUser.loggedDic.ContainsKey(Guid.Parse(assignedUserId)))
            {
                // return Ok(assignedUserId);
                return RedirectToAction("Index", "UserHistory");
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// html界面里 会根据表单的action 找到这个方法
        /// 下方代码是系统发现用户没有登录才运行的, 只有经过Index的过滤 没有登陆过的才会运行Authenticate
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public IActionResult Authenticate(string username, string password)
        {
            //如果有空的 给一个状态
            if(username == null || password == null)
            {
                ViewBag.msg = "Can't be null.";
                return Ok(ViewBag.msg);
            }

            // 调用我们在Data里写好的方法 这个方法会调用数据库
            // 在这里 如果用户名和密码存在 返回分配给该用户的Guid
            Guid userId = Data.UserData.AuthenticateUsernamePassword(username, password); 
            if(userId != Guid.Empty)
            {
                ViewBag.msg = "Successful";
                LoginRegistrate(userId);
                
                // 测试注册有无成功 => 成功
                // Debug.WriteLine(_loggedUser.loggedDic[userId]);

                return RedirectToAction("Index", "Checkout");
            }
            else
            {
                // 登陆失败
                ViewBag.msg = "Unsuccessful";
                return RedirectToAction("Index", "Login");
            }
        }

        /// <summary>
        /// 用来向用户分配sessionId，并且更新系统注册表,这里按道理说键值应该是username，为了方便先设置为空，有精力再写
        /// </summary>
        public void LoginRegistrate(Guid userId)
        {
            if(!_loggedUser.loggedDic.ContainsKey(userId))
            {
                _loggedUser.loggedDic.Add(userId, "");

                // 注册成功以后要把用户专属的userId分配给该用户 通过设置sessionId
                AssignSessionId(userId);
            }
        }

        /// <summary>
        /// 通过Response.Cookies
        /// 给当前HTTP请求，分配上它专属的用户Id
        /// </summary>
        /// <param name="userId"></param>
        public void AssignSessionId(Guid userId)
        {
            // In servers, use Response.Cookies to create and send Session IDs to clients via cookies
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append("UserId", userId.ToString(), options);
        }


        public string RetrieveSessionId()
        {
            string? userId = Request.Cookies["UserId"];
            if (userId == null) return null;
            else return userId;
        }

    }
}
