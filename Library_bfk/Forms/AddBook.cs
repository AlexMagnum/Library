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
    public partial class AddBook : Form
    {
        public string bookName { get; set; }
        public string bookPublisher { get; set; }
        public short bookYear { get; set; }
        public string bookAuthor { get; set; }
        public short bookPages { get; set; }
        public string bookIsbn { get; set; }
        public string bookNumber { get; set; }
        public string bookStatus { get; set; }
        public AddBook()
        {
            InitializeComponent();
        }

        public AddBook(string bookName, string bookPublisher, short bookYear, string bookAuthor,
            short bookPages, string bookIsbn, string bookNumber, string bookStatus)
        {
            InitializeComponent();
            guna2TextBox1.Text = bookName;
            guna2TextBox2.Text = bookPublisher;
            guna2TextBox3.Text = bookYear.ToString();
            guna2TextBox4.Text = bookAuthor;
            guna2TextBox5.Text = bookPages.ToString();
            if (bookIsbn != null)
                guna2TextBox6.Text = bookIsbn;
            else
                guna2TextBox6.Text = "";
            guna2TextBox7.Text = bookNumber;
            guna2ComboBox1.SelectedIndex = guna2ComboBox1.FindStringExact(bookStatus);
        }
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (guna2TextBox1.Text != "" && guna2TextBox2.Text != "" && guna2TextBox3.Text != ""
                    && guna2TextBox4.Text != "" && guna2TextBox5.Text != "" && guna2TextBox7.Text != "")
                {
                    bookName = guna2TextBox1.Text;
                    bookPublisher = guna2TextBox2.Text;
                    bookYear = Convert.ToInt16(guna2TextBox3.Text);
                    bookAuthor = guna2TextBox4.Text;
                    bookPages = Convert.ToInt16(guna2TextBox5.Text);
                    if (guna2TextBox6.Text == "")
                        bookIsbn = "";
                    else
                        bookIsbn = guna2TextBox6.Text;
                    bookNumber = guna2TextBox7.Text;
                    bookStatus = guna2ComboBox1.Text;

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Потрібно заповнити обов'язкові поля помічені зірочкою", "Не заповнені дані",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка у введених даних чи їх форматі", "Помилка даних",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
