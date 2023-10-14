using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Models
{
    public class OrderProductSerial
    {
        public OrderProductSerial()
        {
            SerialNumber = new Guid();
        }

        [Key]
        public Guid SerialNumber { get; set; }

        // OrderGoodQuantity已经建立了一对多的关系，但是要添加导航，才能直接添加对象
        public virtual OrderGoodQuantity OrderGoodQuantity { get; set; }
        // public virtual Good Good { get; set; }


    }
}

