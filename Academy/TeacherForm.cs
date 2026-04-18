using Academy.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Academy
{
	public partial class TeacherForm : HumanForm
	{
		Models.Teacher teacher;
		public TeacherForm()
		{
			InitializeComponent();			
		}

		public TeacherForm(int id) : this()
		{
			DataTable table = DataBase.Connector.Select($"SELECT * FROM Teachers WHERE teacher_id = {id}");
			teacher = new Models.Teacher(table.Rows[0].ItemArray);
			human = teacher;
			Extract();
		}

		protected override void Extract()
		{
			base.Extract();
			if (!string.IsNullOrEmpty(teacher.work_since))
			{
				dtpWorkSince.Value = Convert.ToDateTime(teacher.work_since);				
			}
			else
			{
				// Если даты нет, устанавливаем значение по умолчанию
				dtpWorkSince.Value = DateTime.Now;
			}
			textBoxRate.Text = teacher.rate.ToString();
		}
		protected override void buttonOk_Click(object sender, EventArgs e)
		{
			decimal rate = 0;
			base.buttonOk_Click(sender, e);
			teacher = new Models.Teacher(human, dtpWorkSince.Value.ToString("yyyy-MM-dd"),rate);
			if (teacher.id == 0) teacher.id =
						Convert.ToInt32(DataBase.Connector.Scalar
						($"INSERT Students({teacher.GetNames()}) VALUES ({teacher.GetValues()});SELECT SCOPE_IDENTITY();"
						));
			else DataBase.Connector.Update($"UPDATE Students SET {teacher.GetUpdateString()} WHERE stud_id={teacher.id}");
		}

	}
}
