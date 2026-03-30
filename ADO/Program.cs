using System;
using System.Data.SqlClient;
using System.IO;
namespace PV_522_ADO
{
	internal static class Program
	{		
		static void Main(string[] args)
		{
			string contnection_string = "Data Source=DESKTOP-FHF0PU1\\SQLEXPRESS;Initial Catalog=Movies_PV_522;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
			Console.WriteLine(contnection_string);
			var connection = new SqlConnection(contnection_string);

			string cmd = "SELECT * FROM Directors";
			GetInfoDatabase.Select(cmd, connection);
			Console.WriteLine($"Количество записей: {GetInfoDatabase.Scalar("SELECT COUNT(*) FROM Directors", connection)}");
			GetInfoDatabase.Select("SELECT * FROM Movies", connection);
		}
	}
}
