using Microsoft.Data.SqlClient;
using ShoppingCart.Models;

namespace ShoppingCart.Data
{
    public class GoodsData
    {


        /*
         * 以下方法是调用SQLClient写的，现在用Entity Framework改写
        public static List<Good> GetGoodsData(string search = null)
        {
            List<Good> ans = new List<Good>();
            List<Good> filter = new List<Good>();
            
            string connectionString = @"Server=SHUAIHAO; Database=ShoppingCart; Integrated Security=true; Encrypt=false ";
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT * FROM GoodsList";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    filter.Add(new Good()
                    {
                        ID = (int)reader["ID"],
                        Name = (string)reader["name"],
                        Description = (string)reader["description"]
                    });
                }
            }

            if(search == null)
            {
                ans = filter;
            }
            else
            {
                IEnumerable<Good> iter = from good in filter where good.Description.Contains(search) select good;
                foreach(Good good in iter)
                {
                    ans.Add(good);
                }
            }

            return ans;
        }
        */
    }
}
