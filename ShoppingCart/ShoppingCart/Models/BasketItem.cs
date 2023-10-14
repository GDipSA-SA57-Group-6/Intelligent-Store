namespace ShoppingCart.Models
{
    /// <summary>
    /// 这个类是用来接受订单数据的, 因为在js里的basket就是这样的格式，所以接口要对应上，才能解析
    /// </summary>
    public class BasketItem
    {
        public int id { get; set; }
        public int item { get; set; }
    }
}
