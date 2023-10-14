namespace ShoppingCart.Models
{
	public class HistoryItem
	{
		public int GoodId { get; set; }
		public Guid SerialNumber { get; set; }
		public DateTime DateTime { get; set; }
		public HistoryItem(int goodId, Guid serialNumber, DateTime dateTime)
		{
			this.GoodId = goodId;
			this.SerialNumber = serialNumber;
			DateTime = dateTime;
		}
	}
}
