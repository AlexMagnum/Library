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
            using (AddBook f = new AddBook())
            {
                DialogResult result = f.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    using (library_bfkEntities context = new library_bfkEntities())
                    {
                        var number = context.books.Where(b => b.inventory_number == f.bookNumber).FirstOrDefault();
                        if (number != null)
                        {
                            MessageBox.Show("Книга з таким інвентарним номером вже існує у базі", "Дублікат",
                         MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            book b = new book();

                            b.name = f.bookName;
                            b.publisher = f.bookPublisher;
                            b.year = f.bookYear;
                            b.author = f.bookAuthor;
                            b.pages = f.bookPages;
                            b.isbn = f.bookIsbn;
                            b.inventory_number = f.bookNumber;
                            b.status = f.bookStatus;

                            context.books.Add(b);
                            context.SaveChanges();

                            MessageBox.Show("Книга успішно додана до бази!", "Книгу додано",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
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
                    book b = context.books.Find(id);
                    using (AddBook f = new AddBook(b.name, b.publisher, b.year, b.author, b.pages,
                        b.isbn, b.inventory_number, b.status))
                    {
                        DialogResult result = f.ShowDialog(this);

                        if (result == DialogResult.OK)
                        {
                            if (b.inventory_number != f.bookNumber)
                            {
                                var number = context.books.Where(x => x.inventory_number == f.bookNumber).FirstOrDefault();
                                if (number != null)
                                {
                                    MessageBox.Show("Книга з таким інвентарним номером вже існує у базі", "Дублікат",
                                 MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    b.name = f.bookName;
                                    b.publisher = f.bookPublisher;
                                    b.year = f.bookYear;
                                    b.author = f.bookAuthor;
                                    b.pages = f.bookPages;
                                    b.isbn = f.bookIsbn;
                                    b.inventory_number = f.bookNumber;
                                    b.status = f.bookStatus;

                                    context.SaveChanges();

                                    MessageBox.Show("Книга успішно оновлена у базі!", "Книгу оновлено",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                                }
                            }
                            else
                            {
                                b.name = f.bookName;
                                b.publisher = f.bookPublisher;
                                b.year = f.bookYear;
                                b.author = f.bookAuthor;
                                b.pages = f.bookPages;
                                b.isbn = f.bookIsbn;
                                b.inventory_number = f.bookNumber;
                                b.status = f.bookStatus;

                                context.SaveChanges();

                                MessageBox.Show("Книга успішно оновлена у базі!", "Книгу оновлено",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                ClearData();
                LoadData();
            }
            else
            {
                MessageBox.Show("Спочатку оберіть книгу для редагування",
                   "Оберіть книгу", MessageBoxButtons.OK,
                   MessageBoxIcon.Warning);
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (id != 0)
            {
                DialogResult question = MessageBox.Show("Ви дійсно бажаєте видалити цю" +
                        "книгу?", "Видалити", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (question == DialogResult.Yes)
                {
                    using (library_bfkEntities context = new library_bfkEntities())
                    {

                        book b = context.books.Find(id);
                        context.books.Remove(b);
                        context.SaveChanges();
                    }
                    MessageBox.Show("Книгу було успішно видалено",
                  "Книгу видалено", MessageBoxButtons.OK,
                  MessageBoxIcon.Information);
                    ClearData();
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Спочатку оберіть книгу для видалення",
                   "Оберіть книгу", MessageBoxButtons.OK,
                   MessageBoxIcon.Warning);
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            using (Search f = new Search())
            {
                DialogResult result = f.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    using (library_bfkEntities context = new library_bfkEntities())
                    {
                        if (f.querySearch != "")
                        {
                            string querySearch = "SELECT * FROM book WHERE " + f.querySearch.Remove(f.querySearch.Length - 4, 4);
                            var books = context.books.SqlQuery(querySearch).ToList();

                            if (books.Count > 0)
                            {
                                studentsGrid.DataSource = books;
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

                using(library_bfkEntities context = new library_bfkEntities())
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

            }
            catch (Exception ex)
            {
            }
        }
    }
}
