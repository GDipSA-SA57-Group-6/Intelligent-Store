﻿namespace ShoppingCart.Models
{
	/// <summary>
	/// 用于对历史记录分组的数据结构
	/// </summary>
	public class HistoryItemGrouped
	{
		/// <summary>
		/// 商品ID
		/// </summary>
		public int GoodId { get; set; }
		public List<Guid> SerialNumbers { get; set; }
		public DateTime LPDate { get; set; }
		public HistoryItemGrouped(int Id, DateTime Date)
		{
			this.GoodId = Id;
			SerialNumbers = new List<Guid>();
			LPDate = Date;
		}
	}
}
