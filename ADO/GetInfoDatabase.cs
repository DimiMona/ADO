using System;
using System.Data.SqlClient;

namespace PV_522_ADO
{
	internal static class GetInfoDatabase
	{		
		public static object Scalar(string cmd, SqlConnection connection)
		{
			object value = null;
			SqlCommand command = new SqlCommand(cmd, connection);
			connection.Open();
			value = command.ExecuteScalar();
			connection.Close();
			return value;
		}

		public static void Select(string cmd, SqlConnection connection)
		{
			SqlCommand command = new SqlCommand(cmd, connection);
			connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			for (int i = 0; i < reader.FieldCount; i++)
				Console.Write($"{reader.GetName(i)}\t");
			Console.WriteLine();
			while (reader.Read())
			{
				Console.WriteLine($"{reader[0]}\t{reader[1]}\t{reader[2]}");
			}
			reader.Close();
			connection.Close();
		}
	}
}