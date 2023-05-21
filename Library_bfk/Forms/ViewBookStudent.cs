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
    public partial class ViewBookStudent : Form
    {
        private long studId;
        public ViewBookStudent(int id)
        {
            InitializeComponent();
            studId = id;

            using(library_bfkEntities context = new library_bfkEntities())
            {
                var booksStudent = context.books_students.Where(x => x.student_id == studId).ToList();
                List<book> books = new List<book>();
                foreach (var item in booksStudent)
                {
                    books.Add(context.books.Where(x => x.id == item.book_id).FirstOrDefault());
                }
                booksGrid.DataSource = books;
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


        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
