using Microsoft.AspNetCore.Mvc;
using ShoppingCart.DB;
using ShoppingCart.Models;
using System.Collections.Generic;

namespace ShoppingCart.Controllers
{
    /// <summary>
    /// This controller aims to manipulate database.
    /// </summary>
    public class DatabaseController : Controller
    {
        // dependency injection 注意数据库这种dependency 在middleware里不需要注入 可以直接访问
        private readonly LoggedUser _loggedUser;

        private readonly MyDbContext db; // 已经注册过了
        public DatabaseController(MyDbContext db, LoggedUser loggedUser)
        {
            this.db = db;
            this._loggedUser = loggedUser;
        }
       

        /// <summary>
        /// 这个Action是用来初始化的
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            
            
            AddUser("admin", "pwd");

			AddGood("Algorithm Design and Analysis", 76, "Advanced algorithms", "/img/1.jpg");
			AddGood("Data Structures and Algorithms", 138, "Problem-solving techniques", "/img/2.png");
			AddGood("Computer Networks", 57, "Network protocols", "/img/3.jpg");
			AddGood("Operating Systems", 112, "Memory management", "/img/4.png");
			AddGood("Software Engineering", 85, "Agile development", "/img/5.jpg");
			AddGood("Database Management Systems", 67, "SQL and NoSQL databases", "/img/6.png");
			AddGood("Web Development", 66, "Front-end and back-end technologies", "/img/7.jpg");
			AddGood("Cybersecurity Fundamentals", 109, "Threat detection and prevention", "/img/8.png");
			AddGood("Artificial Intelligence Basics", 83, "Machine learning concepts", "/img/9.jpg");
			AddGood("Cloud Computing Technologies", 122, "Virtualization and distributed computing", "/img/10.jpg");
			AddGood("Computer Graphics and Visualization", 117, "3D modeling and rendering", "/img/11.jpg");
			AddGood("Mobile App Development", 66, "iOS and Android programming", "/img/12.jpg");
			AddGood("Software Testing and Quality Assurance", 96, "Test automation techniques", "/img/13.png");
			AddGood("Machine Learning Applications", 51, "Natural language processing", "/img/14.jpg");
			AddGood("Information Security Management", 128, "Risk assessment and compliance", "/img/15.jpg");
			AddGood("Internet of Things (IoT) Concepts", 97, "Embedded systems programming", "/img/16.png");
			AddGood("Big Data Analytics", 56, "Data mining techniques", "/img/17.png");
			AddGood("Computer Architecture and Organization", 133, "CPU design principles", "/img/18.jpg");
			AddGood("Object-Oriented Programming", 69, "Design patterns and principles", "/img/19.png");
			AddGood("Human-Computer Interaction", 85, "User interface design", "/img/20.png");
			AddGood("Compiler Design and Construction", 114, "Lexical analysis and parsing", "/img/21.jpg");
			AddGood("Information Retrieval Systems", 62, "Search engine algorithms", "/img/22.jpg");
			AddGood("Computer Vision and Image Processing", 90, "Image recognition techniques", "/img/23.jpg");
			AddGood("Cryptography Fundamentals", 77, "Encryption and decryption methods", "/img/24.jpg");
			AddGood("Embedded Systems Programming", 57, "Real-time operating systems", "/img/25.jpg");
			AddGood("Calculus I", 90, "Limits and derivatives", "/img/26.png");
			AddGood("Linear Algebra", 110, "Vector spaces and linear transformations", "/img/27.jpg");
			AddGood("Probability Theory", 95, "Probability distributions and random variables", "/img/28.jpg");
			AddGood("Discrete Mathematics", 85, "Logic and set theory", "/img/29.jpg");
			AddGood("Differential Equations", 100, "Ordinary and partial differential equations", "/img/30.png");




			// 测试从数据库的查找是否成功 => 成功
			// return Ok(GetGood(1) );


			/*
            // 测试数据库订单更新是否成功 => 成功
            List<BasketItem> list = new List<BasketItem>();
            list.Add(new BasketItem { id = 1, item = 1 });
            list.Add(new BasketItem { id = 2, item = 2 });
            Guid userId = Guid.Parse("5F58E82B-A0A1-4792-F74D-08DBCAF49C62");
            AddOrder(userId, list);
            */

			return Ok("Successfully initialized database!");
        }



        /// <summary>
        /// 新增用户信息
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void AddUser(string username, string password)
        {
            db.Add(new User
            {
                Name = username,
                Password = password
            } );  
            db.SaveChanges();
        }

        /// <summary>
        /// 新增商品信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <param name="desc"></param>
        /// <param name="img"></param>
        public void AddGood(string name, int price, string desc, string img)
        {
            db.Add( new Good
            {
                Name = name,
                Price = price,
                Description = desc,
                Img = img
            } ); 
            db.SaveChanges();
        }



        /// <summary>
        /// 新订单写入数据库，注意提供的接口是basketItem类型的，就是说需要解析才能写入数据库
        /// </summary>
        /// <param name="userId">提供用户ID,也就是系统给用户分配的GUID</param>
        /// <param name="list">提供购物车列表，商品根据BasketItem格式</param>
        public void AddOrder(Guid userId, List<BasketItem> list)
        {
            Guid orderId = new Guid();
            DateTime dateTime = DateTime.Now;
            User userToAdd = GetUser(userId);

            Order newOrder = new Order
            {
                OrderId = orderId,
                User = userToAdd,
                DateTime = dateTime
            };

            db.Add(newOrder);

            
            foreach (BasketItem item in list)
            {
                int goodId = item.id;
                int goodQuantity = item.item;
                Good goodToAdd = GetGood(goodId);
                // 记录每笔订单 每种商品的数量
                OrderGoodQuantity newOrderGoodQuantity = new OrderGoodQuantity
                {
                    Order = newOrder,
                    Good = goodToAdd,
                    Quantity = goodQuantity
                };
                db.Add(newOrderGoodQuantity);
                
                // 记录每笔订单 每个商品的序列号
                for (int i = 0; i < goodQuantity; i++)
                {
                    db.Add(new OrderProductSerial
                    {
                        OrderGoodQuantity = newOrderGoodQuantity,
                        SerialNumber = new Guid()
                    });
                }
            }
            db.SaveChanges();
        }




        /// <summary>
        /// 从数据库里返回当前ID的用户，如果没找到就返回一个新的
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User GetUser(Guid userId)
        {
            User? user = db.Users.FirstOrDefault(x => x.Id == userId);
            if (user == null) { return new Models.User(); }
            else return user;
        }




        /// <summary>
        /// 从数据库里返回当前ID的商品, 如果没找到的话就返回一个新的
        /// </summary>
        /// <param name="goodId"></param>
        /// <returns></returns>
        public Good GetGood(int goodId)
        {
            Good? good = db.Goods.FirstOrDefault(x => x.ID == goodId);

            if (good == null) return new Good();
            else return good;
        }




        /// <summary>
        /// 用来接收并验证客户端请求
        /// </summary>
        /// <param name="list">
        /// The attribute [FromBody] must be specified when reciving data in JSON format
        /// </param>
        /// <returns></returns>
        public IActionResult TryToCheckout([FromBody] BasketItem[] list)
        {
            string? userId = Request.Cookies["UserId"];

            if(userId != null && _loggedUser.loggedDic.ContainsKey(Guid.Parse(userId))) 
            {
                // 并且处理数据库更新的逻辑
                AddOrder(Guid.Parse(userId), list.ToList());

                return Json(new { status = "Successful" });
            }

            return Json(new { status = "Unsuccessful" });
        }




        /// <summary>
        /// 这个动作需要保护   !! 不能让身份不明的请求访问 ！！有空再写！！
        /// 
        /// 返回一个数组 数组里每个元素 都有两部分 商品ID和SerialNumber
        /// </summary>
        /// <returns>
        /// 通过JSON 让AJAX return 
        /// </returns>
        public IActionResult GetUserHistory()
        {
            
            if (Request.Cookies["UserId"] == null) return RedirectToAction("Index", "Login");
			// userId如果有的话会随着HTTP请求传过来
			Guid? userId = Guid.Parse(Request.Cookies["UserId"]);

            // 测试 => successful
            // Guid userId = Guid.Parse("5F58E82B-A0A1-4792-F74D-08DBCAF49C62");
            
            List<Order> orders = db.Orders.Where( x => x.UserId == userId).ToList();
            List<OrderProductSerial> orderProductSerials = new List<OrderProductSerial>();
            foreach(var order in orders)
            {
                List<OrderProductSerial> temp = db.OrderProductSerials.Where( x => 
                x.OrderGoodQuantity.OrderId == order.OrderId).ToList();

                foreach(var ttemp in temp) orderProductSerials.Add(ttemp);
			}

            List<HistoryItem> historyItems = new List<HistoryItem>();
            foreach(var item in orderProductSerials)
            {
                historyItems.Add(new HistoryItem(item.OrderGoodQuantity.GoodId, item.SerialNumber, item.OrderGoodQuantity.Order.DateTime));
            }

            // 现在historyItems are all the items we bought, then can group them via LINQ
            // So we need to build another data structure to group them

            // 尝试用LINQ分类 并把分好的数据放进新的数据结构
            List<HistoryItemGrouped> historyItemsGrouped = new List<HistoryItemGrouped>();
            var iter = from item in historyItems 
                       group item by item.GoodId; // 分类
            // 遍历类
            foreach(var grp in iter)
            {
                DateTime LPDate = new DateTime(1, 1, 1);
                foreach (var item in grp)
                {
                    if (LPDate >= item.DateTime) continue;
                    LPDate = item.DateTime;
                }
                // 分组以后.Key代表按照什么标准进行分组的 .Count表示数量
                HistoryItemGrouped historyItemGrouped = new HistoryItemGrouped(grp.Key, LPDate);
                foreach(var item in grp)
                {
                    historyItemGrouped.SerialNumbers.Add(item.SerialNumber);
                }
                historyItemsGrouped.Add(historyItemGrouped);
            }

            // 返回一个JSON对象 在Javascript里进行解析
            return Json(historyItemsGrouped);
        }
    }
}
