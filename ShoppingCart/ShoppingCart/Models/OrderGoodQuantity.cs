using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.Models
{
    /// <summary>
    /// 注意 如果只是在Good里设置了外键，那么是没用的，还是只有一个主键
    /// 外键只是起到一个导航的作用，并不能作为主键使用
    /// 
    /// 
    /// 要解决这个bug 需要定义复合主键
    /// 
    /// https://www.cnblogs.com/JacZhu/p/5734852.html
    /// </summary>
    public class OrderGoodQuantity
    {
        // 当一个表自己没有主键 但是需要引用外键的时候 需要先导航到外表 然后在foreign key里设置
        // 使用OrderId作为外键
        [Key, ForeignKey("Order")]
        public Guid OrderId { get; set; }
        // 导航到外表
        public virtual Order Order { get; set; }


        // 要添加对外表的引用，才能直接在db里添加外表类的对象
        [Key, ForeignKey("Good")]
        public int GoodId;
        public virtual Good Good { get; set; }
        
        [Required]
        // 该商品的数量
        public int Quantity { get; set; }

        // can be null
        public virtual ICollection<OrderProductSerial> OrderProductSerials { get; set; }
    }
}

