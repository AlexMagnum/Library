using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library_bfk.Forms
{
    public partial class SearchStudent : Form
    {
        public string querySearch { get; set; } = "";
        public SearchStudent()
        {
            InitializeComponent();
            using (library_bfkEntities context = new library_bfkEntities())
            {
                var facultyList = context.faculties.ToList();
                foreach (var item in facultyList)
                {
                    guna2ComboBox1.Items.Add(item.name);
                }
                guna2ComboBox1.SelectedIndex = -1;

                var unitList = context.units.ToList();
                foreach (var item in unitList)
                {
                    guna2ComboBox2.Items.Add(item.name);
                }
                guna2ComboBox2.SelectedIndex = -1;

                var groupList = context.groups.ToList();
                foreach (var item in groupList)
                {
                    guna2ComboBox3.Items.Add(item.name);
                }
                guna2ComboBox3.SelectedIndex = -1;
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(guna2TextBox1.Text != "")
                    querySearch += "name='" + guna2TextBox1.Text.Replace("'", "''") + "' AND ";
                if(guna2TextBox2.Text != "")
                    querySearch += "surname='" + guna2TextBox2.Text.Replace("'", "''") + "' AND ";
                if(guna2TextBox3.Text != "")
                    querySearch += "specialty='" + guna2TextBox3.Text.Replace("'", "''") + "' AND ";
                if (guna2ComboBox1.SelectedIndex >= 0)
                {
                    using(library_bfkEntities context = new library_bfkEntities())
                    {
                        var studFacultyId = context.faculties.Where(x => x.name == guna2ComboBox1.SelectedItem).FirstOrDefault();
                        querySearch += "faculty_id=" + studFacultyId.id + " AND ";
                    }
                }
                if (guna2ComboBox2.SelectedIndex >= 0)
                {
                    using (library_bfkEntities context = new library_bfkEntities())
                    {
                        var studUnitId = context.units.Where(x => x.name == guna2ComboBox2.SelectedItem).FirstOrDefault();
                        querySearch += "unit_id=" + studUnitId.id + " AND ";
                    }
                }
                if (guna2ComboBox3.SelectedIndex >= 0)
                {
                    using (library_bfkEntities context = new library_bfkEntities())
                    {
                        var studGroupId = context.groups.Where(x => x.name == guna2ComboBox3.SelectedItem).FirstOrDefault();
                        querySearch += "group_id=" + studGroupId.id + " AND ";
                    }
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка у введених даних чи їх форматі", "Помилка даних",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }


    }
}
