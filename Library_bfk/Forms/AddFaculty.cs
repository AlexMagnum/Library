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
    public partial class AddFaculty : Form
    {
        public string facultyName { get; set; }
        public string facultyLeadName { get; set; }
        public string facultyLeadSurname { get; set; }
        public string facultyLeadPhone { get; set; }
        public AddFaculty()
        {
            InitializeComponent();
        }

        public AddFaculty(string facultyName, string facultyLeadName, string facultyLeadSurname,
            string facultyLeadPhone)
        {
            InitializeComponent();
            guna2TextBox1.Text = facultyName;
            guna2TextBox2.Text = facultyLeadName;
            guna2TextBox3.Text = facultyLeadSurname;
            guna2TextBox4.Text = facultyLeadPhone;

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
                    facultyName = guna2TextBox1.Text;
                    facultyLeadName = guna2TextBox2.Text;
                    facultyLeadSurname = guna2TextBox3.Text;
                    facultyLeadPhone = guna2TextBox4.Text;
                  

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
