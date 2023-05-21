using Library_bfk.Forms;
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
        long id = 0;
        long idUnit = 0;
        long idGroupe = 0;
        public Departments()
        {
            InitializeComponent();
        }

        private void Departments_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadUnit();
            LoadGroup();
        }

        private void LoadData()
        {
            using (library_bfkEntities context = new library_bfkEntities())
            {
                var faculty = context.faculties.ToList();

                foreach (var item in faculty)
                {
                    treeView1.Nodes.Add(item.name);
                }
            }
          
        }

        private void LoadUnit()
        {
            using (library_bfkEntities context = new library_bfkEntities())
            {
                var faculty = context.units.ToList();

                foreach (var item in faculty)
                {
                    treeView2.Nodes.Add(item.name);
                }
            }
        }
        private void LoadGroup()
        {
            using (library_bfkEntities context = new library_bfkEntities())
            {
                var groupe = context.groups.ToList();

                foreach (var item in groupe)
                {
                    treeView3.Nodes.Add(item.name);
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
                id = facultyLead.id;
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            LoadData();
            label1.Text = "";
            label3.Text = "";
            id = 0;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            using (AddFaculty form = new AddFaculty())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    using (library_bfkEntities context = new library_bfkEntities())
                    {
                        faculty f = new faculty();

                        f.name = form.facultyName;
                        f.head_name = form.facultyLeadName;
                        f.head_surname = form.facultyLeadSurname;
                        f.phone = form.facultyLeadPhone;

                        context.faculties.Add(f);
                        context.SaveChanges();

                        treeView1.Nodes.Clear();
                        LoadData();
                        label1.Text = "";
                        label3.Text = "";
                    }
                }
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (id != 0)
            {

                using (library_bfkEntities context = new library_bfkEntities())
                {
                    var faculty = context.faculties.Where(b => b.id == id).FirstOrDefault();

                    using (AddFaculty form = new AddFaculty(faculty.name, faculty.head_name,
                        faculty.head_surname, faculty.phone))
                    {
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            faculty.name = form.facultyName;
                            faculty.head_name = form.facultyLeadName;
                            faculty.head_surname = form.facultyLeadSurname;
                            faculty.phone = form.facultyLeadPhone;

                            context.SaveChanges();

                            treeView1.Nodes.Clear();
                            LoadData();
                            label1.Text = "";
                            label3.Text = "";
                            id = 0;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Спочатку оберіть відділення у списку", "Оберіть відділення",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (id != 0)
            {

                using (library_bfkEntities context = new library_bfkEntities())
                {
                    var faculty = context.faculties.Where(b => b.id == id).FirstOrDefault();
                    
                    context.faculties.Remove(faculty);
                    context.SaveChanges();

                    treeView1.Nodes.Clear();
                    LoadData();
                    label1.Text = "";
                    label3.Text = "";
                    id = 0;
                }
            }
            else
            {
                MessageBox.Show("Спочатку оберіть відділення у списку", "Оберіть відділення",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            using (AddUnity form = new AddUnity())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    using (library_bfkEntities context = new library_bfkEntities())
                    {
                        unit u = new unit();

                        u.name = form.unityName;
                        u.head_name = form.unityLeadName;
                        u.head_surname = form.unityLeadSurname;
                        u.phone = form.unityLeadPhone;
                        u.id_faculty = form.unityFacultyId;

                        context.units.Add(u);
                        context.SaveChanges();

                        treeView2.Nodes.Clear();
                        LoadUnit();
                        label5.Text = "";
                        label8.Text = "";
                    }
                }
            }
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            treeView2.Nodes.Clear();
            LoadUnit();
            label5.Text = "";
            label8.Text = "";
            idUnit = 0;
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            if (idUnit != 0)
            {
                using (library_bfkEntities context = new library_bfkEntities())
                {
                    var unit = context.units.Where(b => b.id == idUnit).FirstOrDefault();

                    using (AddUnity form = new AddUnity(unit.name, unit.head_name,
                        unit.head_surname, unit.phone, unit.id_faculty))
                    {
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            unit.name = form.unityName;
                            unit.head_name = form.unityLeadName;
                            unit.head_surname = form.unityLeadSurname;
                            unit.phone = form.unityLeadPhone;
                            unit.id_faculty = form.unityFacultyId;

                            context.SaveChanges();
                            treeView2.Nodes.Clear();

                            LoadUnit();
                            label5.Text = "";
                            label8.Text = "";
                            idUnit = 0;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Спочатку оберіть циклову комісію у списку", "Оберіть циклову комісію",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void treeView2_AfterSelect(object sender, TreeViewEventArgs e)
        {
            using (library_bfkEntities context = new library_bfkEntities())
            {
                var unitLead = context.units.Where(b => b.name == treeView2.SelectedNode.Text).FirstOrDefault();
                label5.Text = unitLead.head_name + " " + unitLead.head_surname;
                label8.Text = unitLead.phone;
                idUnit = unitLead.id;
            }
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            if (idUnit != 0)
            {
                using (library_bfkEntities context = new library_bfkEntities())
                {
                    var unit = context.units.Where(b => b.id == idUnit).FirstOrDefault();

                    context.units.Remove(unit);
                    context.SaveChanges();

                    treeView2.Nodes.Clear();
                    LoadUnit();
                    label5.Text = "";
                    label8.Text = "";
                    idUnit = 0;
                }
            }
            else
            {
                MessageBox.Show("Спочатку оберіть циклову комісію у списку", "Оберіть циклову комісію",
                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void treeView3_AfterSelect(object sender, TreeViewEventArgs e)
        {
            using (library_bfkEntities context = new library_bfkEntities())
            {
                var groupeLead = context.groups.Where(b => b.name == treeView3.SelectedNode.Text).FirstOrDefault();
                label9.Text = groupeLead.head_name + " " + groupeLead.head_surname;
                label12.Text = groupeLead.phone;
                idGroupe = groupeLead.id;
            }
        }

        private void guna2Button12_Click(object sender, EventArgs e)
        {
            treeView3.Nodes.Clear();
            LoadGroup();
            label9.Text = "";
            label12.Text = "";
            idGroupe = 0;
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            using (AddGroupe form = new AddGroupe())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    using (library_bfkEntities context = new library_bfkEntities())
                    {
                        group g = new group();

                        g.name = form.groupeName;
                        g.head_name = form.groupeLeadName;
                        g.head_surname = form.groupeLeadSurname;
                        g.phone = form.groupeLeadPhone;
                        g.id_faculty = form.groupeFacultyId;
                        g.id_unit = form.groupeUnitId;

                        context.groups.Add(g);
                        context.SaveChanges();

                        treeView3.Nodes.Clear();
                        LoadGroup();
                        label9.Text = "";
                        label12.Text = "";
                    }
                }
            }
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            if (idGroupe != 0)
            {
                using (library_bfkEntities context = new library_bfkEntities())
                {
                    var groupe = context.groups.Where(b => b.id == idGroupe).FirstOrDefault();

                    using (AddGroupe form = new AddGroupe(groupe.name, groupe.head_name,
                        groupe.head_surname, groupe.phone, groupe.id_faculty, groupe.id_unit))
                    {
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            groupe.name = form.groupeName;
                            groupe.head_name = form.groupeLeadName;
                            groupe.head_surname = form.groupeLeadSurname;
                            groupe.phone = form.groupeLeadPhone;
                            groupe.id_faculty = form.groupeFacultyId;
                            groupe.id_unit = form.groupeUnitId;

                            context.SaveChanges();
                            treeView3.Nodes.Clear();

                            LoadGroup();
                            label9.Text = "";
                            label12.Text = "";
                            idGroupe = 0;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Спочатку оберіть навчальну групу у списку", "Оберіть навчальну групу",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {
            if (idGroupe != 0)
            {
                using (library_bfkEntities context = new library_bfkEntities())
                {
                    var groupe = context.groups.Where(b => b.id == idGroupe).FirstOrDefault();

                    context.groups.Remove(groupe);
                    context.SaveChanges();

                    treeView3.Nodes.Clear();
                    LoadGroup();
                    label9.Text = "";
                    label12.Text = "";
                    idGroupe = 0;
                }
            }
            else
            {
                MessageBox.Show("Спочатку оберіть навчальну групу у списку", "Оберіть навчальну групу",
                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
