using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Models
{
	internal static class DataBase
	{
		static internal DBtools.Connector Connector { get; set; } =
			new DBtools.Connector(ConfigurationManager.ConnectionStrings["PV_522_Import"].ConnectionString);
	}
}
