namespace ShoppingCart.Models
{
    /// <summary>
    /// 这个类是放在购物车里的类，也就是之后会通过AJAX发过来的订单类
    /// </summary>
    public class BasketItem
    {
        public BasketItem(int goodId, int quantity)
        {
            this.GoodId = goodId;
            this.Quantity = quantity;
        }
        public int GoodId { get; set; }
        public int Quantity { get; set; }
    }
}
