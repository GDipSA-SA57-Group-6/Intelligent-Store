using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;
using ShoppingCart.Models;
using System.Reflection.Metadata.Ecma335;

namespace ShoppingCart.Data
{
    public class UserData
    {
        /// 以下方法使用SQLClient写的
        /// <summary>
        /// 这里避免了SQL注入的问题
        /// 
        /// 调用这个函数会返回能不能在数据库里查到用户名和密码
        /// 原来的函数返回调用SQLClient返回的是查找的个数 我希望返回的是用户的GUID
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static Guid AuthenticateUsernamePassword(string username, string password)
        {
            Guid userId  = Guid.Empty;
            string connectionString = @"Server=SHUAIHAO; Database=ShoppingCartData; Integrated Security=true; Encrypt=false ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = @"SELECT Id FROM Users WHERE Name=@user AND Password=@pwd";

                SqlParameter param1 = new SqlParameter
                {
                    ParameterName = @"user",
                    Value = username
                };
                SqlParameter param2 = new SqlParameter
                {
                    ParameterName = @"pwd",
                    Value = password
                };

                SqlCommand cmd = new SqlCommand(sql, connection);
                
                cmd.Parameters.Add(param1);
                cmd.Parameters.Add(param2);
                
                SqlDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    userId = (Guid)reader["Id"];
                }
                connection.Close();


                return userId; 
            }
        }

        public static bool IsUsernameOkay(string username)
        {
			string connectionString = @"Server=SHUAIHAO; Database=ShoppingCartData; Integrated Security=true; Encrypt=false ";
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
				string sql = @"SELECT Id FROM Users WHERE Name=@Username";
                SqlParameter parameter1 = new SqlParameter
                {
                    ParameterName = @"Username",
                    Value = username
                };
                SqlCommand cmd = new SqlCommand (sql, connection);
                cmd.Parameters.Add(parameter1);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    return true;
                }
                return false;
			}
        }
        /*
        public static bool AuthenticateUsernamePassword(string username, string password)
        {
            int result = 0;
            // bool flag  = false;
            string connectionString = @"Server=SHUAIHAO; Database=ShoppingCartData; Integrated Security=true; Encrypt=false ";
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = @"SELECT Count(*) FROM Users WHERE Name=@user AND Password=@pwd";

                SqlParameter param1 = new SqlParameter
                {
                    ParameterName = @"user",
                    Value = username
                };
                SqlParameter param2 = new SqlParameter
                {
                    ParameterName = @"pwd",
                    Value = password
                };

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add(param1);
                cmd.Parameters.Add(param2);
                
                result = (int)cmd.ExecuteScalar(); // 对于Count(*) 方法的读取
            }
            return result == 1;
        }*/


        /*
        /// <summary>
        /// 从数据库中，读取该用户之前加入购物车的商品
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static List<Good> GetPreviousRecords(string  username)
        {
            List<Good> goods = new List<Good>();

            string connectionString = @"Server=SHUAIHAO; Database=ShoppingCart; Integrated Security=true; Encrypt=false ";
            using(SqlConnection connection = new SqlConnection( connectionString))
            {
                connection.Open ();
                
                string sql = @"SELECT * FROM GoodsList g1  Inner Join goods_picked_by_user  g2 on  g1.ID=g2.ID WHERE g2.username=@user";
                SqlParameter parameter1 = new SqlParameter
                {
                    ParameterName = @"user",
                    Value = username
                };
                
                SqlCommand cmd  = new SqlCommand(sql, connection);
                cmd.Parameters.Add(parameter1);
               
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    goods.Add(new Good()
                    {
                        ID = (int)reader["ID"],
                        Name = (string)reader["name"],
                        Description = (string)reader["description"],
                        Amount = (int)reader["quantity"]
                    });
                }

            }
            return goods;
        }*/

    }
}
