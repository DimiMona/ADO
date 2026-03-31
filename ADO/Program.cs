using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
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

			string cmd = "SELECT last_name,first_name FROM Directors";
			Select(cmd);
			Console.WriteLine($"Количество записей: {Scalar("SELECT COUNT(*) FROM Directors")}");
			Select("title,realise_date,first_name,last_name", "Movies,Directors" , "director=director_id");
		}

		static void Select(string cmd)
		{
			SqlCommand command = new SqlCommand(cmd, connection);
			connection.Open();
			SqlDataReader reader = command.ExecuteReader();

			int[] string_size = new int[reader.FieldCount];
			var interval = 11;
			for(int i = 0; i <reader.FieldCount; i++)
				if (reader.GetName(i).ToString().Length > string_size[i]) string_size[i] = reader.GetName(i).ToString().Length + interval;
			while (reader.Read())
			{
				for (int i = 0; i < reader.FieldCount; i++)
				{
					if (reader[i].ToString().Length > string_size[i]) string_size[i] = reader[i].ToString().Length + 1;
				}
			}
			reader.Close();
			reader = command.ExecuteReader();
			for (int i = 0; i < reader.FieldCount; i++)
			{
				Console.Write($"{reader.GetName(i).PadRight(string_size[i])}");
				//if (reader.GetName(i).ToString().Length > string_size[i]) string_size[i]=reader.GetName(i).ToString().Length + 1;
			}
				
			Console.WriteLine();
			for(int i=0; i< string_size.Sum();i++ ) Console.Write("_"); Console.WriteLine();
			while (reader.Read())
			{
				//Console.WriteLine($"{reader[0]}\t{reader[1]}\t{reader[2]}");
				for (int i = 0; i < reader.FieldCount; i++)
					Console.Write(reader[i].ToString().PadRight(string_size[i]) + "\t");
				Console.WriteLine();
			}
			reader.Close();
			connection.Close();
		}
		static void Select(string fields, string tables, string condition = "")
		{
			string cmd = $"SELECT {fields} FROM {tables}";
			if (condition != "") cmd += $" WHERE {condition}";
			Select(cmd);
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
