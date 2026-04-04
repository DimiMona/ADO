using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.Xml.Schema;
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

		public string GetPrimaryKeyColumnName(string Table)
		{
			string cmd = $@"
SELECT	COLUMN_NAME FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE
WHERE	CONSTRAINT_NAME=
(
SELECT	CONSTRAINT_NAME 
FROM	INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
WHERE	TABLE_NAME=N'{Table}'
AND		CONSTRAINT_TYPE=N'PRIMARY KEY'
);
";
			return Scalar(cmd).ToString();
		}

		public int GetLastPrimayKey(string table)
		{
			return (int)Scalar($"SELECT MAX({GetPrimaryKeyColumnName(table)}) FROM {table}");
		}

		public int GetNextPrimaryKey(string table)
		{
			return GetLastPrimayKey(table) + 1;
		}

		public void Insert(string cmd)
		{
			SqlCommand command = new SqlCommand(cmd, connection);
			connection.Open();
			command.ExecuteNonQuery();
			connection.Close();
		}
		public bool Validation (string firstName, string lastName)
		{
			string valid = $"SELECT COUNT(*) FROM Directors WHERE first_name = @firstName AND last_name = @lastName";

			connection.Open();
			SqlCommand validCommand = new SqlCommand(valid, connection);
			validCommand.Parameters.AddWithValue("@firstName", firstName);
			validCommand.Parameters.AddWithValue("@lastName", lastName);
			int count = (int)validCommand.ExecuteScalar();
			connection.Close();
			return count > 0;
		}
	}
}
