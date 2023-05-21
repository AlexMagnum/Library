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
    public partial class Search : Form
    {
        public string querySearch { get; set; } = "";
        public Search()
        {
            InitializeComponent();
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
                    querySearch += "publisher='" + guna2TextBox2.Text.Replace("'", "''") + "' AND ";
                if(guna2TextBox3.Text != "")
                    querySearch += "year=" + Convert.ToInt16(guna2TextBox3.Text) + " AND ";
                if(guna2TextBox4.Text != "")
                    querySearch += "author='" + guna2TextBox4.Text.Replace("'", "''") + "' AND ";
                if (guna2TextBox6.Text != "")
                    querySearch += "isbn='" + guna2TextBox6.Text.Replace("'", "''") + "' AND ";
                if (guna2TextBox7.Text != "")
                    querySearch += "inventory_number='" + guna2TextBox7.Text.Replace("'", "''") + "' AND ";
                if (guna2ComboBox1.SelectedIndex == 0 || guna2ComboBox1.SelectedIndex == 1)
                    querySearch += "status='" + guna2ComboBox1.Text.Replace("'", "''") + "' AND ";

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
