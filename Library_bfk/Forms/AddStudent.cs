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
    public partial class AddStudent : Form
    {
        public string studName { get; set; }
        public string studSurname { get; set; }
        public string studSpecialty { get; set; }
        public long studFaculty { get; set; }
        public long studUnit { get; set; }
        public long studGroup { get; set; }
        public AddStudent()
        {
            InitializeComponent();
            using (library_bfkEntities context = new library_bfkEntities())
            {
                var facultyList = context.faculties.ToList();
                foreach (var item in facultyList)
                {
                    guna2ComboBox1.Items.Add(item.name);
                }
                guna2ComboBox1.SelectedIndex = 0;

                var unitList = context.units.ToList();
                foreach (var item in unitList)
                {
                    guna2ComboBox2.Items.Add(item.name);
                }
                guna2ComboBox2.SelectedIndex = 0;

                var groupList = context.groups.ToList();
                foreach (var item in groupList)
                {
                    guna2ComboBox3.Items.Add(item.name);
                }
                guna2ComboBox3.SelectedIndex = 0;
            }
        }

        public AddStudent(string studName, string studSurname, string studSpecialty, string studFaculty,
            string studUnit, string studGroup)
        {
            InitializeComponent();

            guna2TextBox1.Text = studName;
            guna2TextBox2.Text = studSurname;
            guna2TextBox3.Text = studSpecialty;

            using (library_bfkEntities context = new library_bfkEntities())
            {
                var facultyList = context.faculties.ToList();
                foreach (var item in facultyList)
                {
                    guna2ComboBox1.Items.Add(item.name);
                }
                var unitList = context.units.ToList();
                foreach (var item in unitList)
                {
                    guna2ComboBox2.Items.Add(item.name);
                }
                var groupList = context.groups.ToList();
                foreach (var item in groupList)
                {
                    guna2ComboBox3.Items.Add(item.name);
                }
            }

            guna2ComboBox1.SelectedIndex = guna2ComboBox1.FindStringExact(studFaculty);
            guna2ComboBox2.SelectedIndex = guna2ComboBox2.FindStringExact(studUnit);
            guna2ComboBox3.SelectedIndex = guna2ComboBox3.FindStringExact(studGroup);
        }
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (guna2TextBox1.Text != "" && guna2TextBox2.Text != "" && guna2TextBox3.Text != "")
                {
                    studName = guna2TextBox1.Text;
                    studSurname = guna2TextBox2.Text;
                    studSpecialty = guna2TextBox3.Text;
                    using (library_bfkEntities context = new library_bfkEntities())
                    {
                        var facultyId = context.faculties.Where(x => x.name == guna2ComboBox1.SelectedItem).FirstOrDefault();
                        studFaculty = facultyId.id;
                        var unitId = context.units.Where(x => x.name == guna2ComboBox2.SelectedItem).FirstOrDefault();
                        studUnit = unitId.id;
                        var groupeId = context.groups.Where(x => x.name == guna2ComboBox3.SelectedItem).FirstOrDefault();
                        studGroup = groupeId.id;
                    }

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Потрібно заповнити обов'язкові поля помічені зірочкою", "Не заповнені дані",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
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
