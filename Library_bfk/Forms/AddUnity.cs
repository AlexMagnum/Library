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
    public partial class AddUnity : Form
    {
        public string unityName { get; set; }
        public string unityLeadName { get; set; }
        public string unityLeadSurname { get; set; }
        public string unityLeadPhone { get; set; }
        public long unityFacultyId { get; set; }
        public AddUnity()
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
            }
        }

        public AddUnity(string unityName, string unityLeadName, string unityLeadSurname,
            string unityLeadPhone, long unityFacultyId)
        {
            InitializeComponent();
            guna2TextBox1.Text = unityName;
            guna2TextBox2.Text = unityLeadName;
            guna2TextBox3.Text = unityLeadSurname;
            guna2TextBox4.Text = unityLeadPhone;

            using (library_bfkEntities context = new library_bfkEntities())
            {
                var facultyList = context.faculties.ToList();
                foreach (var item in facultyList)
                {
                    guna2ComboBox1.Items.Add(item.name);
                }
                guna2ComboBox1.SelectedIndex = 0;
            }

            using (library_bfkEntities context = new library_bfkEntities())
            {
                var UFI = context.faculties.Where(x => x.id == unityFacultyId).FirstOrDefault();
                guna2ComboBox1.SelectedIndex = guna2ComboBox1.FindStringExact(UFI.name);
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
                    unityName = guna2TextBox1.Text;
                    unityLeadName = guna2TextBox2.Text;
                    unityLeadSurname = guna2TextBox3.Text;
                    unityLeadPhone = guna2TextBox4.Text;
                    using (library_bfkEntities context = new library_bfkEntities()) {
                        var UFI = context.faculties.Where(x => x.name == guna2ComboBox1.SelectedItem).FirstOrDefault();
                        unityFacultyId = UFI.id;
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
