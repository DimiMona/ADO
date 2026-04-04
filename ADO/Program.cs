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


			Console.WriteLine(connector.GetPrimaryKeyColumnName("Movies"));
			Console.WriteLine(connector.GetNextPrimaryKey("Movies"));
			
			while(true)
			{
				Console.WriteLine("Введите Имя режисера");
				var firstName = Console.ReadLine();
				Console.WriteLine("Введите Фамилию режисера");
				var lastName = Console.ReadLine();
				if (!connector.Validation(firstName, lastName))
				{
					connector.Insert($"INSERT Directors(director_id,first_name,last_name)VALUES({connector.GetNextPrimaryKey("Directors")},N'{firstName}', N'{lastName}')");
					break;
				}
				else
				{
					Console.WriteLine($"Данный режисер уже есть!");
				}
			}

			//connector.Insert("INSERT Directors(director_id,first_name,last_name)VALUES(7,N'The', N'Wachowskis')");

			connector.Select("SELECT * FROM Directors");
			connector.Select("movie_id,title, first_name,last_name","Movies,Directors", "director=director_id");
			//Console.WriteLine(contnection_string);
			//connection = new SqlConnection(contnection_string);

			//string cmd = "SELECT last_name,first_name FROM Directors";
			//Select(cmd);
			//Console.WriteLine($"Количество записей: {Scalar("SELECT COUNT(*) FROM Directors")}");
			//Select("title,realise_date,first_name,last_name", "Movies,Directors" , "director=director_id");
		}

		
	}
}
