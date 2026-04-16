using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using DBtools;

namespace Academy
{
	public partial class MainForm : Form
	{
		Connector connector;
		DataGridView[] tables;
		public MainForm()
		{
			InitializeComponent();
			connector = new Connector(ConfigurationManager.ConnectionStrings["PV_522_Import"].ConnectionString);
			dgvDirections.DataSource = connector.Select("SELECT * FROM Directions");
			dgvStudents.DataSource = connector.Select("SELECT * FROM Students");
			dgvGroups.DataSource = connector.Select("SELECT * FROM Groups");
			dgvDisciplines.DataSource = connector.Select("SELECT * FROM Disciplines");
			dgvTeachers.DataSource = connector.Select("SELECT * FROM Teachers");
			
			tables = new DataGridView[] { dgvStudents, dgvGroups, dgvDirections, dgvDisciplines, dgvTeachers};
		}

		private void buttonAddStudents_Click(object sender, EventArgs e)
		{
			StudentForm student = new StudentForm();
			student.ShowDialog();
		}

		private void dgvStudents_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			int id = Convert.ToInt32(dgvStudents.Rows[e.RowIndex].Cells[0].Value);
			StudentForm form = new StudentForm(id);
			if (form.ShowDialog() == DialogResult.OK) tabControl_SelectedIndexChanged(tabControl, null);
		}

		private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
		{
			int i = (sender as TabControl).SelectedIndex;
			tables[i].DataSource = connector.Select($"SELECT * FROM {tabControl.SelectedTab.Text}");
		}
	}
}
