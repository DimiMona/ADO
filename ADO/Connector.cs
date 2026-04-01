using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace ADO
{
	
	public class Connector
	{
		SqlConnection connection;
		public Connector(string connection_string) 
		{ 
			connection = new SqlConnection(connection_string);
			Console.WriteLine(connection.ConnectionString);
		}
		public void Select(string cmd)
		{
			SqlCommand command = new SqlCommand(cmd, connection);
			connection.Open();
			SqlDataReader reader = command.ExecuteReader();

			int[] string_size = new int[reader.FieldCount];
			var interval = 11;
			for (int i = 0; i < reader.FieldCount; i++)
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
			for (int i = 0; i < string_size.Sum(); i++) Console.Write("_"); Console.WriteLine();
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
		public void Select(string fields, string tables, string condition = "")
		{
			string cmd = $"SELECT {fields} FROM {tables}";
			if (condition != "") cmd += $" WHERE {condition}";
			Select(cmd);
		}
		public object Scalar(string cmd)
		{
			object value = null;
			SqlCommand command = new SqlCommand(cmd, connection);
			connection.Open();
			value = command.ExecuteScalar();
			connection.Close();
			return value;
		}
		public void Insert(string fields, string tableName, string values)
		{
			// Формируем SQL команду
			string cmd = $"INSERT INTO {tableName} ({fields}) VALUES ({values})";

			// Создаем объект команды
			SqlCommand command = new SqlCommand(cmd, connection);

			// Выполняем вставку
			connection.Open();
			command.ExecuteNonQuery();
			connection.Close();

			Console.WriteLine("Данные успешно добавлены!");
		}

	}
}
