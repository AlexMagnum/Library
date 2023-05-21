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
    public partial class AddGroupe : Form
    {
        public string groupeName { get; set; }
        public string groupeLeadName { get; set; }
        public string groupeLeadSurname { get; set; }
        public string groupeLeadPhone { get; set; }
        public long groupeFacultyId { get; set; }
        public long groupeUnitId { get; set; }
        public AddGroupe()
        {
            InitializeComponent();
            using (library_bfkEntities context = new library_bfkEntities())
            {
                var facultyList = context.faculties.ToList();
                foreach(var item in facultyList)
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
            }
        }

        public AddGroupe(string groupeName, string groupeLeadName, string groupeLeadSurname,
            string groupeLeadPhone, long groupeFacultyId, long groupeUnitId)
        {
            InitializeComponent();
            guna2TextBox1.Text = groupeName;
            guna2TextBox2.Text = groupeLeadName;
            guna2TextBox3.Text = groupeLeadSurname;
            guna2TextBox4.Text = groupeLeadPhone;

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
            }

            using (library_bfkEntities context = new library_bfkEntities())
            {
                var UFI = context.faculties.Where(x => x.id == groupeFacultyId).FirstOrDefault();
                guna2ComboBox1.SelectedIndex = guna2ComboBox1.FindStringExact(UFI.name);

                var UUI = context.units.Where(x => x.id == groupeUnitId).FirstOrDefault();
                guna2ComboBox2.SelectedIndex = guna2ComboBox2.FindStringExact(UUI.name);
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
                if (guna2TextBox1.Text != "" && guna2TextBox2.Text != "" && guna2TextBox3.Text != ""
                    && guna2TextBox4.Text != "")
                {
                    groupeName = guna2TextBox1.Text;
                    groupeLeadName = guna2TextBox2.Text;
                    groupeLeadSurname = guna2TextBox3.Text;
                    groupeLeadPhone = guna2TextBox4.Text;
                    using (library_bfkEntities context = new library_bfkEntities()) {
                        var UFI = context.faculties.Where(x => x.name == guna2ComboBox1.SelectedItem).FirstOrDefault();
                        groupeFacultyId = UFI.id;

                        var UUI = context.units.Where(x => x.name == guna2ComboBox2.SelectedItem).FirstOrDefault();
                        groupeUnitId = UUI.id;
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
                MessageBox.Show("Помилка у введених даних чи їх форматі" + ex.Message, "Помилка даних",
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
