using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using DBtools;

namespace DLLcheck
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Connector connector = new Connector
				(
				ConfigurationManager.ConnectionStrings["Movies_PV_522"].ConnectionString
				);
			connector.Select("*", "Directors");
			connector.Select("title,realise_date,first_name,last_name",
				"Movies,Directors",
				"director=director_id"
				);
			connector.Update("UPDATE Directors SET first_name = N'123', last_name = N'123' WHERE director_id = 1");
			connector.Select("*", "Directors");
			//Connector connectorAcademy = new Connector
			//	(
			//	ConfigurationManager.ConnectionStrings["PV_522_Import"].ConnectionString
			//	);
			//connectorAcademy.Select("*", "Disciplines");
			
			
		}
	}
}
