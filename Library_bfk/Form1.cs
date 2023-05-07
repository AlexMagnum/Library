using Guna.UI2.WinForms;
using Library_bfk.User_Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library_bfk
{
    public partial class Library : Form
    {
        public Library()
        {
            InitializeComponent();
            Dashboard uc = new Dashboard();
            AddUserControl(uc);
        }

        private void MoveImageBox(object sender)
        {
            Guna2Button b = (Guna2Button)sender;
            pictureBox2.Location = new Point(b.Location.X + 175, b.Location.Y + 1);
            pictureBox2.SendToBack();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button1_CheckedChanged(object sender, EventArgs e)
        {
            MoveImageBox(sender); 
        }

        private void AddUserControl(Control c)
        {
            c.Dock = DockStyle.Fill;
            panel2.Controls.Clear();
            panel2.Controls.Add(c);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Dashboard uc = new Dashboard();
            AddUserControl(uc);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Books uc = new Books();
            AddUserControl(uc);
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            Departments uc = new Departments();
            AddUserControl(uc);
        }
    }
}
