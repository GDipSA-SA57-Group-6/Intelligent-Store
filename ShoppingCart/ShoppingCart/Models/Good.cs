using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.Models
{
    /// <summary>
    /// In EF Core, a Model (C# class) is mapped to a database table
    /// </summary>
    public class Good
    {
        [Key]
        [Required]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        [MaxLength(50)]
        public string Description { get; set; }

        [Required]
        [MaxLength(50)]
        public string Img { get; set; }

        // [ForeignKey("GoodID")] 不需要设置了 我在所连接的文件里重新设置
        public virtual ICollection<OrderGoodQuantity> OrderGoodQuantities { get; set; }


        // 这里也不用了 因为按道理来说 我在OrderGoodQuantity设置了复合主键以后，应会自动把复合键作为Serials的外键
        // [ForeignKey("GoodID")]
        // public virtual ICollection<OrderProductSerial> OrderProductSerials { get; set; }

    }
}
