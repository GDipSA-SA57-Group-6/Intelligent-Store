namespace ShoppingCart.Models
{
    /// <summary>
    /// 这个类用来dependency injection
    /// 全局维护用户登录信息
    /// </summary>
    public class LoggedUser
    {
        public Dictionary<Guid, string> loggedDic;
        public LoggedUser() 
        {
            loggedDic = new Dictionary<Guid, string>();
        }
    }
}
