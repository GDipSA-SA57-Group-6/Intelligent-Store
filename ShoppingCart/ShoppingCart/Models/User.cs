using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Models
{
    public class User
    {
        public User()
        {
            Id = new Guid();
        }
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(20)]

        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        public string Password { get; set; }


        // 用户和订单是一对多的关系
        [Required]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
