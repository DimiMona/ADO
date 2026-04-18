using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Academy
{
	public partial class HumanForm : Form
	{
		internal Models.Human human;
		public HumanForm()
		{
			InitializeComponent();
		}

		protected virtual void Compress()
		{
			human = new Models.Human
				(
				Convert.ToInt32(labelID.Text == "" ? "0" : labelID.Text.Split(':').Last()),
				textBoxLastName.Text,
				textBoxFirstName.Text,
				textBoxMiddleName.Text,
				dtpBirthDate.Value.ToString("yyyy-MM-dd"),
				textBoxEmail.Text,
				textBoxPhone.Text,
				pictureBoxPhoto.Image
				);
		}

		protected virtual void Extract()
		{
			labelID.Text = $"ID:{human.id}";
			textBoxLastName.Text = human.last_name;
			textBoxFirstName.Text = human.first_name;
			textBoxMiddleName.Text = human.middle_name;
			dtpBirthDate.Value =Convert.ToDateTime(human.birth_date);
			textBoxEmail.Text = human.email;
			textBoxPhone.Text = human.phone;
		}

		protected virtual void buttonOk_Click(object sender, EventArgs e)
		{
			Compress();
		}

		private void buttonBrowser_Click(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Filter = "JPG files (*.jpg)|*.jpg|PNG files (*.png)|*.png|ALL image files|*.png;*.jpg|All files(*.*)|*.*";
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				pictureBoxPhoto.Image = Image.FromFile(dialog.FileName);
			}
		}
	}
}
