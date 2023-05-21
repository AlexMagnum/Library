using Library_bfk.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library_bfk.User_Controls
{
    public partial class Students : UserControl
    {
        int id = 0;
        public Students()
        {
            InitializeComponent();
        }

        private void Books_Load(object sender, EventArgs e)
        {
            LoadData();
            studentsGrid.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 222, 0);
            studentsGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 222, 0);
            studentsGrid.EnableHeadersVisualStyles = false;
        }

        private void LoadData()
        {
            using (library_bfkEntities context = new library_bfkEntities())
            {
                context.studs.Load();
                studentsGrid.DataSource = context.studs.Local.ToBindingList();
                studentsGrid.Columns[1].HeaderText = "Ім'я";
                studentsGrid.Columns[2].HeaderText = "Прізвище";
                studentsGrid.Columns[3].HeaderText = "Спеціальність";
                studentsGrid.Columns[4].HeaderText = "Група";
                studentsGrid.Columns[5].HeaderText = "Відділення";
                studentsGrid.Columns[6].HeaderText = "Циклова";
                studentsGrid.Columns[7].Visible = false;
                studentsGrid.Columns[8].Visible = false;
                studentsGrid.Columns[9].Visible = false;
                studentsGrid.Columns[10].Visible = false;
            }
        }

        private void ClearData()
        {
            id = 0;
            studName.Text = "";
            studSpeciality.Text = "";
            studSurname.Text = "";
            studFaculty.Text = "";
            studGroupe.Text = "";
            studUnit.Text = "";
            studGroupe.Text = "";
            studBooks.Text = "";
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            using (AddStudent f = new AddStudent())
            {
                DialogResult result = f.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    using (library_bfkEntities context = new library_bfkEntities())
                    {
                        stud s = new stud();

                        s.name = f.studName;
                        s.surname = f.studSurname;
                        s.specialty = f.studSpecialty;
                        s.faculty_id = f.studFaculty;
                        s.unit_id = f.studUnit;
                        s.group_id = f.studGroup;

                        context.studs.Add(s);
                        context.SaveChanges();

                        MessageBox.Show("Студента успішно додано до бази!", "Студента додано",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    LoadData();
                }
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            ClearData();
            LoadData();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (id != 0)
            {
                using (library_bfkEntities context = new library_bfkEntities())
                {
                    stud s = context.studs.Find(id);
                    using (AddStudent f = new AddStudent(s.name, s.surname, s.specialty, studFaculty.Text,
                         studUnit.Text, studGroupe.Text))
                    {
                        DialogResult result = f.ShowDialog(this);
                        if (result == DialogResult.OK)
                        {
                            s.name = f.studName;
                            s.surname = f.studSurname;
                            s.specialty = f.studSpecialty;
                            s.faculty_id = f.studFaculty;
                            s.unit_id = f.studUnit;
                            s.group_id = f.studGroup;

                            context.SaveChanges();

                            MessageBox.Show("Студент успішно оновлений у базі!", "Студента оновлено",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                ClearData();
                LoadData();
            }
            else
            {
                MessageBox.Show("Спочатку оберіть студента для редагування",
                   "Оберіть студента", MessageBoxButtons.OK,
                   MessageBoxIcon.Warning);
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (id != 0)
            {
                DialogResult question = MessageBox.Show("Ви дійсно бажаєте видалити цього " +
                        "студента?", "Видалити", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (question == DialogResult.Yes)
                {
                    using (library_bfkEntities context = new library_bfkEntities())
                    {
                        stud s = context.studs.Find(id);
                        context.studs.Remove(s);
                        context.SaveChanges();
                    }
                    MessageBox.Show("Студента було успішно видалено",
                  "Студента видалено", MessageBoxButtons.OK,
                  MessageBoxIcon.Information);
                    ClearData();
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Спочатку оберіть студента для видалення",
                   "Оберіть студента", MessageBoxButtons.OK,
                   MessageBoxIcon.Warning);
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            using (SearchStudent f = new SearchStudent())
            {
                DialogResult result = f.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    using (library_bfkEntities context = new library_bfkEntities())
                    {
                        if (f.querySearch != "")
                        {
                            string querySearch = "SELECT * FROM stud WHERE " + f.querySearch.Remove(f.querySearch.Length - 4, 4);
                            var students = context.studs.SqlQuery(querySearch).ToList();

                            if (students.Count > 0)
                            {
                                studentsGrid.DataSource = students;
                            }
                            else
                            {
                                MessageBox.Show("Пошук не дав результатів, спробуйте інші параметри пошуку",
                                    "Нічого не знайдено", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Не обрано жодного параметру пошуку",
                                "Нічого не знайдено", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        private void studentsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ClearData();
            try
            {
                id = Convert.ToInt32(studentsGrid.Rows[e.RowIndex].Cells[0].Value.ToString());
                studName.Text = studentsGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
                studSurname.Text = studentsGrid.Rows[e.RowIndex].Cells[2].Value.ToString();
                studSpeciality.Text = studentsGrid.Rows[e.RowIndex].Cells[3].Value.ToString();

                using (library_bfkEntities context = new library_bfkEntities())
                {
                    long stud_id = Convert.ToInt64(studentsGrid.Rows[e.RowIndex].Cells[4].Value);
                    var stud_groupe = context.groups.Where(x => x.id == stud_id).FirstOrDefault();
                    studGroupe.Text = stud_groupe.name;
                }

                using (library_bfkEntities context = new library_bfkEntities())
                {
                    long stud_id = Convert.ToInt64(studentsGrid.Rows[e.RowIndex].Cells[5].Value);
                    var stud_faculty = context.faculties.Where(x => x.id == stud_id).FirstOrDefault();
                    studFaculty.Text = stud_faculty.name;
                }

                using (library_bfkEntities context = new library_bfkEntities())
                {
                    long stud_id = Convert.ToInt64(studentsGrid.Rows[e.RowIndex].Cells[6].Value);
                    var stud_unit = context.units.Where(x => x.id == stud_id).FirstOrDefault();
                    studUnit.Text = stud_unit.name;
                }

                using (library_bfkEntities context = new library_bfkEntities())
                {
                    var bookCount = context.books_students.Where(x => x.student_id == id).ToList();
                    if (bookCount.Count > 0)
                    {
                        studBooks.Text = bookCount.Count.ToString();
                    }
                    else
                    {
                        studBooks.Text = "Студент ще не брав книг у бібліотеці";
                    }
                }

            }
            catch (Exception ex)
            {
            }
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            if (id != 0)
            {
                using (library_bfkEntities context = new library_bfkEntities())
                {
                    var bookCount = context.books_students.Where(x => x.student_id == id).ToList();
                    if (bookCount.Count > 0)
                    {
                        using (ViewBookStudent f = new ViewBookStudent(id))
                        {
                            DialogResult result = f.ShowDialog(this);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Студент не має книг",
                   "Книги відсутні", MessageBoxButtons.OK,
                   MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("Спочатку оберіть студента для перегляду його книг",
                   "Оберіть студента", MessageBoxButtons.OK,
                   MessageBoxIcon.Warning);
            }
        }
    }
}
