using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.DB;
using ShoppingCart.Models;

namespace ShoppingCart.Controllers
{
    public class GoodsController : Controller
    {
        // dependency injection
        private readonly MyDbContext db;
        public GoodsController(MyDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 以JSON格式返回所有的货物
        /// 异步方式投影
        /// 
        /// 你可以使用投影来选择你想要返回的特定属性，而不是返回整个实体。这样可以避免加载关联实体的数据
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetAllGoods()
        {
            var result = await db.Goods.Select(g => new
            {
                g.ID,
                g.Name,
                g.Price,
                g.Description,
                g.Img
            }).ToListAsync();

            return Json(result);
        }




        /*
        /// <summary>
        /// 参数交给MVC去帮绑定
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public IActionResult GetGoods(string search=null)
        {
            List<Good> goods = new List<Good>();
              
            goods = Data.GoodsData.GetGoodsData(search);

            return Ok(goods);
        }

        public IActionResult GetPrevious(string username)
        {
            List<Good> ans = Data.UserData.GetPreviousRecords(username);
            return Ok(ans);
        }*/
    }
}
