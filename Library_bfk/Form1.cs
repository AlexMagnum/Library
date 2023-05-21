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

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Students uc = new Students();
            AddUserControl(uc);
        }
    }
}
