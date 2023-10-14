using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.Models
{

    /// <summary>
    /// 我在尝试使用entity framework 设计后台订单相关的数据库, 用户每次交易都会创建一笔新的订单，每一笔订单都和一个用户建立
    /// 一一对应的关系，每一笔订单都对应了一个或多个商品, 以及每个商品的数量
    /// </summary>
    public class Order
    {
        public Order()
        {
            OrderId = new Guid();
        }


        // 每一笔订单都有一个单独的编号
        [Required]
        [Key]
        public Guid OrderId { get; set; }


        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        

        // 订单创建日期
        [Required]
        public DateTime DateTime { get; set; }



        public virtual ICollection<OrderGoodQuantity> OrderGoodQuantities { get; set; }

    }
}
