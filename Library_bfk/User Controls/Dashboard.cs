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
    public partial class Dashboard : UserControl
    {
        public Dashboard()
        {
            InitializeComponent();

            using(library_bfkEntities context = new library_bfkEntities())
            {
                var faculty = context.faculties.ToList();
                var unit = context.units.ToList();
                var group = context.groups.ToList();

                foreach (var item in faculty)
                {
                    treeView1.Nodes.Add(item.name.ToString());
                }

                for (int i = 0; i < treeView1.Nodes.Count; i++)
                {
                    foreach (var item in unit)
                    {   
                        if(context.faculties.Where(x => x.id == item.id_faculty).FirstOrDefault().name == treeView1.Nodes[i].Text)
                            treeView1.Nodes[i].Nodes.Add(item.name);
                    }
                }

                for (int i = 0; i < treeView1.Nodes.Count; i++)
                {
                    for (int j = 0; j < treeView1.Nodes[i].Nodes.Count; j++)
                        foreach (var item in group)
                        {
                            if (context.units.Where(x => x.id == item.id_unit).FirstOrDefault().name == treeView1.Nodes[i].Nodes[j].Text)
                                treeView1.Nodes[i].Nodes[j].Nodes.Add(item.name);
                        }
                }

                var booksTotal = context.books.ToList();
                var booksInBiblio = context.books.Where(x => x.status == "У наявності").ToList();
                var booksInStudent = context.books.Where(x => x.status == "Видано").ToList();

                label4.Text = booksTotal.Count.ToString();
                label6.Text = booksInBiblio.Count.ToString();
                label7.Text = booksInStudent.Count.ToString();

                chart1.Series["Series2"].Points.AddXY("У наявності", booksInBiblio.Count);
                chart1.Series["Series2"].Points.AddXY("Видано", booksInStudent.Count);

                var studentsTotal = context.studs.ToList();
                var studentsTec = context.studs.Where(x => x.faculty_id == 1).ToList();
                var studentsEconom = context.studs.Where(x => x.faculty_id == 2).ToList();  
                var studentsNature = context.studs.Where(x => x.faculty_id == 3).ToList();

                label8.Text = studentsTotal.Count.ToString();
                label12.Text = studentsEconom.Count.ToString();
                label10.Text = studentsTec.Count.ToString();
                label14.Text = studentsNature.Count.ToString();

                chart2.Series["Series2"].Points.AddXY("Технічне", studentsTec.Count);
                chart2.Series["Series2"].Points.AddXY("Економічне", studentsEconom.Count);
                chart2.Series["Series2"].Points.AddXY("Пр. рес.", studentsNature.Count);
            }
        }
    }
}
