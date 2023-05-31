using System.Data.SqlClient;

namespace L49Sql_Connection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string strConnect = @"Data Source=XUANPHUC\XUANPHUC;Initial Catalog=xtlab;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(strConnect);

            Console.WriteLine($"Status: {sqlConnection.State}");
            sqlConnection.Open();
            Console.WriteLine($"Status: {sqlConnection.State}");

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "select top(5) * from Sanpham";
            var dataReader = sqlCommand.ExecuteReader();
            while(dataReader.Read())
            {
                Console.WriteLine($"{dataReader["TenSanpham"]}");
            }

            sqlConnection.Close();
        }
    }
}