using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Academy.Models
{
	class Teacher : Human
	{
		internal string work_since;
		internal decimal rate;

		public Teacher
			(
			int id,
			string last_name, string first_name, string middle_name, string birth_date,
			string email, string phone, Image photo,
			string work_since, decimal rate
			):base (id, last_name, first_name, middle_name, birth_date, email, phone, photo)
		{
			this.work_since = work_since;
			this.rate		= rate;
		}

		public Teacher(Human human, string work_since, decimal rate):base (human)
		{
			this.work_since = work_since; 
			this.rate = rate;
		}

		public Teacher(object[] values) : base(values)
		{
			// Проверяем на DBNull.Value
			this.work_since = values.Length > 8 && values[8] != DBNull.Value
				? values[8].ToString()
				: "";  // Пустая строка вместо null

			this.rate = values.Length > 9 && values[9] != DBNull.Value
				? Convert.ToDecimal(values[9])
				: 0;  // 0 вместо null

		}

		public override string GetNames()
		{
			return base.GetNames() + ",work_since,rate";
		}

		public override string GetValues()
		{
			return base.GetValues() + $",N'{work_since}',{rate}";
		}
		public override string GetUpdateString()
		{
			return base.GetUpdateString() + $",work_since = N'{work_since}',rate = {rate}";
		}
	}
}
