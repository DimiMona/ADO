using System;
using System.Data.SqlClient;
using System.IO;
namespace PV_522_ADO
{
	internal class Program
	{
		static SqlConnection connection;
		static void Main(string[] args)
		{
			string contnection_string = "Data Source=DESKTOP-FHF0PU1\\SQLEXPRESS;Initial Catalog=Movies_PV_522;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
			Console.WriteLine(contnection_string);
			connection = new SqlConnection(contnection_string);

			string cmd = "SELECT * FROM Directors";
			Select(cmd);
			Console.WriteLine($"Количество записей: {Scalar("SELECT COUNT(*) FROM Directors")}");
			Select("SELECT * FROM Movies");
		}

		static void Select(string cmd)
		{
			SqlCommand command = new SqlCommand(cmd, connection);
			connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			for (int i=0; i< reader.FieldCount;i++)
				Console.Write($"{reader.GetName(i)}\t");
			Console.WriteLine();
			while (reader.Read())
			{
				Console.WriteLine($"{reader[0]}\t{reader[1]}\t{reader[2]}");
			}
			reader.Close();
			connection.Close();
		}

		static object Scalar(string cmd)
		{
			object value = null;
			SqlCommand command = new SqlCommand(cmd, connection);
			connection.Open();
			value = command.ExecuteScalar();
			connection.Close();
			return value;
		}
	}
}
