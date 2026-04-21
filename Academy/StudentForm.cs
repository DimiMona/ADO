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
	public partial class StudentForm : HumanForm
	{
		Models.Student student;
		public StudentForm()
		{
			InitializeComponent();
			cbStudentsGroup.DataSource = DataBase.Connector.Select("SELECT * FROM Groups");
			cbStudentsGroup.DisplayMember = "group_name";
			cbStudentsGroup.ValueMember = "group_id";
		}

		public StudentForm(int id): this()
		{
			DataTable table = DataBase.Connector.Select($"SELECT * FROM Students WHERE Stud_id = {id}");
			student = new Models.Student(table.Rows[0].ItemArray);
			student.photo = DataBase.Connector.DownloadPhoto(id, "Students", "photo");
			human = student;
			Extract();
		}

		protected override void Extract()
		{
			base.Extract();
			cbStudentsGroup.SelectedValue = Convert.ToInt32(student.group);
		}

		protected override void buttonOk_Click(object sender, EventArgs e)
		{
			base.buttonOk_Click(sender, e);
			student = new Models.Student(human, Convert.ToInt32(cbStudentsGroup.SelectedValue));
			if (student.id == 0) student.id =
						Convert.ToInt32(DataBase.Connector.Scalar
						($"INSERT Students({student.GetNames()}) VALUES ({student.GetValues()});SELECT SCOPE_IDENTITY();"
						));
			else DataBase.Connector.Update($"UPDATE Students SET {student.GetUpdateString()} WHERE stud_id={student.id}");
			if (pictureBoxPhoto.Image != null)
				DataBase.Connector.UploadPhoto(student.SerializePhoto(), student.id, "photo", "Students");
		}
	}
}
