using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library_bfk.User_Controls
{
    public partial class Departments : UserControl
    {
        public Departments()
        {
            InitializeComponent();
        }

        private void Departments_Load(object sender, EventArgs e)
        {
            LoadFaculty();
        }

        private void LoadFaculty()
        {

            using (library_bfkEntities context = new library_bfkEntities())
            {
                var faculty = context.faculties.ToList();
                
                foreach(var item in faculty)
                {
                    treeView1.Nodes.Add(item.name);
                }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            using (library_bfkEntities context = new library_bfkEntities())
            {
                var facultyLead = context.faculties.Where(b => b.name == treeView1.SelectedNode.Text).FirstOrDefault();
                label1.Text = facultyLead.head_name + " " + facultyLead.head_surname;
                label3.Text = facultyLead.phone;
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            LoadFaculty();
            label1.Text = "";
            label3.Text = "";
        }
    }
}
