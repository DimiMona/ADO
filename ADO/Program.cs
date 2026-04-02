using ADO;
using System;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Linq;

namespace PV_522_ADO
{

	internal class Program
	{
		
		static void Main(string[] args)
		{
			//string contnection_string = "Data Source=DESKTOP-FHF0PU1\\SQLEXPRESS;Initial Catalog=Movies_PV_522;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
			string contnection_string = ConfigurationManager.ConnectionStrings["Movies"].ConnectionString;
			Connector connector = new Connector(contnection_string);

			connector.Select("SELECT * FROM Directors");
			connector.Select("title, first_name,last_name","Movies,Directors", "director=director_id");

			// Получаем следующий ID
			int nextId = Convert.ToInt32(connector.Scalar("SELECT ISNULL(MAX(director_id), 0) + 1 FROM Directors"));
			//Console.WriteLine("\n=== ДОБАВЛЯЕМ НОВОГО РЕЖИССЕРА ===");
			//connector.Insert("director_id, first_name, last_name", "Directors",$"{nextId}, 'Gay', 'Ritchie'");
			//connector.Select("SELECT * FROM Directors");

			Console.WriteLine("\n=====================================================================================\n");
			nextId = Convert.ToInt32(connector.Scalar("SELECT ISNULL(MAX(movie_id), 0) + 1 FROM Movies"));
			connector.Insert("movie_id,title,realise_date, director", "Movies", $"{nextId}, 'The Matrix', '1991.03.31', 1" );
			connector.Select("title, first_name,last_name", "Movies,Directors", "director=director_id");
			Console.WriteLine("\n=== ДОБАВЛЯЕМ НОВОГО РЕЖИССЕРА ===");
			//Console.WriteLine(contnection_string);
			//connection = new SqlConnection(contnection_string);

			//string cmd = "SELECT last_name,first_name FROM Directors";
			//Select(cmd);
			//Console.WriteLine($"Количество записей: {Scalar("SELECT COUNT(*) FROM Directors")}");
			//Select("title,realise_date,first_name,last_name", "Movies,Directors" , "director=director_id");
		}


	}
}
