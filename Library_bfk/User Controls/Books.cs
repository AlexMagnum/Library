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
    public partial class Books : UserControl
    {
        int id = 0;
        public Books()
        {
            InitializeComponent();
        }

        private void Books_Load(object sender, EventArgs e)
        {
            LoadData();
            booksGrid.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 222, 0);
            booksGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 222, 0);
            booksGrid.EnableHeadersVisualStyles = false;
        }

        private void LoadData()
        {
            using (library_bfkEntities context = new library_bfkEntities())
            {
                context.books.Load();
                booksGrid.DataSource = context.books.Local.ToBindingList();
                booksGrid.Columns[1].HeaderText = "Назва";
                booksGrid.Columns[2].HeaderText = "Видавець";
                booksGrid.Columns[3].HeaderText = "Рік";
                booksGrid.Columns[4].HeaderText = "Автор";
                booksGrid.Columns[5].HeaderText = "Сторінки";
                booksGrid.Columns[6].HeaderText = "ISBN";
                booksGrid.Columns[7].HeaderText = "Інв. номер";
                booksGrid.Columns[8].HeaderText = "Статус";
                booksGrid.Columns[9].Visible = false;
            }
        }

        private void ClearData()
        {
            id = 0;
            bookName.Text = "";
            bookPublisher.Text = "";
            bookYear.Text = "";
            bookAuthor.Text = "";
            bookPages.Text = "";
            bookISBN.Text = "";
            bookNumber.Text = "";
            bookStatus.Text = "";
        }

        private void booksGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ClearData();
            try
            {
                id = Convert.ToInt32(booksGrid.Rows[e.RowIndex].Cells[0].Value.ToString());
                bookName.Text = booksGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
                bookPublisher.Text = booksGrid.Rows[e.RowIndex].Cells[2].Value.ToString();
                bookYear.Text = booksGrid.Rows[e.RowIndex].Cells[3].Value.ToString();
                bookAuthor.Text = booksGrid.Rows[e.RowIndex].Cells[4].Value.ToString();
                bookPages.Text = booksGrid.Rows[e.RowIndex].Cells[5].Value.ToString();
                if (booksGrid.Rows[e.RowIndex].Cells[6].Value == null)
                    bookISBN.Text = "";
                else
                    bookISBN.Text = booksGrid.Rows[e.RowIndex].Cells[6].Value.ToString();
                bookNumber.Text = booksGrid.Rows[e.RowIndex].Cells[7].Value.ToString();
                if (booksGrid.Rows[e.RowIndex].Cells[8].Value.ToString() == "У наявності")
                {
                    bookStatus.Text = booksGrid.Rows[e.RowIndex].Cells[8].Value.ToString();
                    bookStatus.ForeColor = Color.Green;
                }
                else
                {
                    bookStatus.Text = booksGrid.Rows[e.RowIndex].Cells[8].Value.ToString();
                    bookStatus.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
            }
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
                                booksGrid.DataSource = books;
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
    }
}
