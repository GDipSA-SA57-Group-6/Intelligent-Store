using Azure.Core;
using ShoppingCart.Models;

namespace ShoppingCart.Middleware
{
	/// <summary>
	/// This is a middleware to check the right of accees to different controllers and pages.
	/// </summary>
	public class AccessCheck
	{
		private RequestDelegate next;
		private readonly LoggedUser loggedUser;
		public AccessCheck(RequestDelegate next, LoggedUser loggedUser)
		{
			this.next = next;
			this.loggedUser = loggedUser;
		}

		public async Task Invoke(HttpContext context)
		{
			if(!context.Request.Path.StartsWithSegments("/Login") &&
				!context.Request.Path.StartsWithSegments("/ShoppingCart") &&
				!context.Request.Path.StartsWithSegments("/Checkout") &&
				context.Request.Cookies["UserId"] == null &&
				!loggedUser.loggedDic.ContainsKey(Guid.Parse(context.Request.Cookies["UserId"])))
			{
				context.Response.Redirect("/Login/Index");
			}
			else
			{
				await next(context);
			}
		}

	}
}
