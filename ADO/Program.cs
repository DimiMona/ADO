using ADO;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
namespace PV_522_ADO
{

	internal class Program
	{
		
		static void Main(string[] args)
		{
			string contnection_string = "Data Source=DESKTOP-FHF0PU1\\SQLEXPRESS;Initial Catalog=Movies_PV_522;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
			Connector connector = new Connector(contnection_string);

			connector.Select("SELECT * FROM Directors");
			connector.Select("title, first_name,last_name","Movies,Directors", "director=dorector_id");
			//Console.WriteLine(contnection_string);
			//connection = new SqlConnection(contnection_string);

			//string cmd = "SELECT last_name,first_name FROM Directors";
			//Select(cmd);
			//Console.WriteLine($"Количество записей: {Scalar("SELECT COUNT(*) FROM Directors")}");
			//Select("title,realise_date,first_name,last_name", "Movies,Directors" , "director=director_id");
		}

		
	}
}
