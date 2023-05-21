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
    public partial class GiveBook : Form
    {
        int bookId { get; set; }
        int studId { get; set; }
        public GiveBook(int id)
        {
            InitializeComponent();
            bookId = id;
            using (library_bfkEntities context = new library_bfkEntities())
            {
                var groupList = context.groups.ToList();
                foreach (var item in groupList)
                {
                    guna2ComboBox1.Items.Add(item.name);
                }
                guna2ComboBox1.SelectedIndex = -1;
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            using (library_bfkEntities context = new library_bfkEntities())
            {
                var book = context.books.Find(bookId);

                if (book.status == "У наявності")
                {
                    MessageBox.Show("Книга уже знаходиться у бібліотеці",
                          "Книга у наявності", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    book.status = "У наявності";
                    context.SaveChanges();

                    var bookstud = context.books_students.Where(x => x.book_id == bookId).FirstOrDefault();
                    try
                    {
                        context.books_students.Remove(bookstud);
                        context.SaveChanges();
                        this.DialogResult = DialogResult.OK;
                    }
                    catch (Exception ex)
                    {
                    }

                    MessageBox.Show("Книгу повернено до бібліотеки",
                          "Книгу повернено", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

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

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (library_bfkEntities context = new library_bfkEntities())
            {
                var studGroupId = context.groups.Where(x => x.name == guna2ComboBox1.SelectedItem).FirstOrDefault();

                string querySearch = "SELECT * FROM stud WHERE group_id=" + studGroupId.id;

                var students = context.studs.SqlQuery(querySearch).ToList();

                if (students.Count > 0)
                {
                    studentsGrid.DataSource = students;
                    studentsGrid.Columns[1].HeaderText = "Ім'я";
                    studentsGrid.Columns[2].HeaderText = "Прізвище";
                    studentsGrid.Columns[3].HeaderText = "Спеціальність";
                    studentsGrid.Columns[4].Visible = false;
                    studentsGrid.Columns[5].Visible = false;
                    studentsGrid.Columns[6].Visible = false;
                    studentsGrid.Columns[7].Visible = false;
                    studentsGrid.Columns[8].Visible = false;
                    studentsGrid.Columns[9].Visible = false;
                    studentsGrid.Columns[10].Visible = false;
                }
                else
                {
                    MessageBox.Show("Помилка у виборі групи",
                        "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void studentsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                studId = Convert.ToInt32(studentsGrid.Rows[e.RowIndex].Cells[0].Value.ToString());
                label2.Text = "ID: " + studentsGrid.Rows[e.RowIndex].Cells[0].Value.ToString() + " > " +
                studentsGrid.Rows[e.RowIndex].Cells[1].Value.ToString() + " " +
                studentsGrid.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
            catch (Exception ex)
            {
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (guna2ComboBox1.SelectedIndex >= 0)
            {
                if (studId != 0)
                {
                    using (library_bfkEntities context = new library_bfkEntities())
                    {
                        var isSet = context.books_students.Where(x => x.book_id == bookId).Where(x => x.student_id
                        == studId).FirstOrDefault();
                        var isSetBook = context.books.Find(bookId);
                        if (isSet != null || isSetBook.status == "Видано")
                        {
                            MessageBox.Show("Дану книгу студенту вже видано", "Книга вже у студента",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            books_students bookstudent = new books_students();
                            bookstudent.book_id = bookId;
                            bookstudent.student_id = studId;
                            bookstudent.date_issue = DateTime.Now;

                            context.books_students.Add(bookstudent);

                            var book = context.books.Find(bookId);
                            book.status = "Видано";
                            context.SaveChanges();

                            MessageBox.Show("Книгу успішно видано студенту", "Книгу видано",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Студента не обрано із списка",
                           "Оберіть студента", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Спочатку оберіть групу з випадаючого списку і студента",
                           "Оберіть групу", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
