

//AUTHOR REPOSITORY ispravak

using BusinessLogicLayer;
using DataAccessLayer;
using EntitiesLayer;
using G1_BookApp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer
{
    public class AuthorRepository : Repository<Author>
    {
        public AuthorRepository()
        {
            Context = new BooksAndGenresModel();
            Entities = Context.Set<Author>();
        }

        public IEnumerable<Author> GetAuthors()
        {
            return Entities.ToList();
        }
    }
}


//REPOSITORY cs ispravak

using System;
using System.Data.Entity;

namespace DataAccessLayer
{
    public class Repository<T> : IDisposable where T : class
    {
        public DbSet<T> Entities { get; set; }
        public BooksAndGenresModel Context { get; set; }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}


//REPOSITORY cs ispravak

using System;
using System.Data.Entity;

namespace DataAccessLayer
{
    public class Repository<T> : IDisposable where T : class
    {
        public DbSet<T> Entities { get; set; }
        public BooksAndGenresModel Context { get; set; }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}


//REPOSITORY cs ispravak

using System;
using System.Data.Entity;

namespace DataAccessLayer
{
    public class Repository<T> : IDisposable where T : class
    {
        public DbSet<T> Entities { get; set; }
        public BooksAndGenresModel Context { get; set; }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}


//REPOSITORY cs ispravak

using System;
using System.Data.Entity;

namespace DataAccessLayer
{
    public class Repository<T> : IDisposable where T : class
    {
        public DbSet<T> Entities { get; set; }
        public BooksAndGenresModel Context { get; set; }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}

//AUTHOR SERVICE cs ispravak

using DataAccessLayer;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class AuthorService
    {
        public List<Author> GetAuthors()
        {
            using (var repo = new AuthorRepository())
            {
                return repo.GetAuthors().ToList();
            }
        }
    }
}

//BOOK service cs ispravak

using DataAccessLayer;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicLayer
{
    public class BookService
    {
        public List<Book> GetBooks()
        {
            using (var repo = new BookRepository())
            {
                return repo.GetBooks().ToList();
            }
        }

        public Author GetAuthor(Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));
            return book.Author;
        }

        public bool AddBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book), "Book object cannot be empty!");
            }

            if (string.IsNullOrWhiteSpace(book.ISBN) || book.ISBN.Length != 13)
            {
                throw new InvalidISBNException("ISBN must be 13 digits!");
            }

            if (book.WeightInGrams.HasValue && book.WeightInGrams.Value > 2000)
            {
                throw new BookTooHeavyException("Book is too heavy!");
            }

            if (book.NumOfPages < 0)
            {
                return false;
            }

            using (var repo = new BookRepository())
            {
                repo.AddBook(book);
            }

            return true;
        }
    }
}

//FRMNewBook cs ispravak

using BusinessLogicLayer;
using EntitiesLayer;
using System;
using System.Windows.Forms;

namespace G1_BookApp
{
    public partial class FrmNewBook : Form
    {
        BookService bookService;
        AuthorService authorService;

        public FrmNewBook()
        {
            InitializeComponent();
            bookService = new BookService();
            authorService = new AuthorService();
            cmbAuthor.DataSource = authorService.GetAuthors();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtNumOfPages.Text, out var numOfPages))
            {
                MessageBox.Show("Please enter a valid number of pages.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int? weight = null;
            if (!string.IsNullOrWhiteSpace(txtWeightInGrams.Text))
            {
                if (int.TryParse(txtWeightInGrams.Text, out var w))
                {
                    weight = w;
                }
                else
                {
                    MessageBox.Show("Please enter a valid weight in grams.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            var selectedAuthor = cmbAuthor.SelectedItem as Author;
            if (selectedAuthor == null)
            {
                MessageBox.Show("Please select an author.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var book = new Book
            {
                Title = txtTitle.Text,
                Genre = txtGenre.Text,
                NumOfPages = numOfPages,
                Author = selectedAuthor,
                ISBN = txtISBN.Text,
                ShelfLocation = txtShelfLocation.Text,
                WeightInGrams = weight,
                AuthorId = selectedAuthor.Id
            };

            try
            {
                bookService.AddBook(book);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error saving book", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

// Kreirajte validacijsku logiku u klasi BookService u metodi GetAuthor koja bi provjeravala...
using DataAccessLayer;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicLayer
{
    public class BookService
    {
        public List<Book> GetBooks()
        {
            // Napomena: .AsQueryable() na kraju ToList() je obično suvišan 
            // osim ako specifični repozitorij to ne zahtijeva za daljnje filtriranje.
            return (new BookRepository()).GetBooks().ToList();
        }

        public Author GetAuthor(Book book)
        {
            // Dodana validacijska logika
            if (book.Authors == null)
            {
                throw new EmptyAuthorException("Author is empty!");
            }

            return book.Authors;
        }

        public bool AddBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException("Book object cannot be empty!");
            }

            if (book.ISBN.Length != 13)
            {
                throw new InvalidISBNException("ISBN must be 13 digits!");
            }

            if (book.WeightInGrams > 2000)
            {
                throw new BookTooHeavyException("Book is too heavy!");
            }

            if (book.NumOfPages < 0)
            {
                return false;
            }

            (new BookRepository()).AddBook(book);
            return true;
        }
    }
}


//Navedenu iznimku uhvatite u formi FrmBookApp na prikladnom mjestu u kodu 

using BusinessLogicLayer;
using EntitiesLayer;
using G1_BookApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookApp
{
    public partial class FrmBookApp : Form
    {
        private BookService bookService;

        public FrmBookApp()
        {
            InitializeComponent();
            bookService = new BookService();
        }

        private void btnShowData_Click(object sender, EventArgs e)
        {
            try
            {
                dgvBooks.DataSource = bookService.GetBooks();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvBooks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Provjera da li je red validan (nije header)
                if (e.RowIndex < 0)
                    return;

                Book book = dgvBooks.CurrentRow.DataBoundItem as Book;

                if (book != null)
                {
                    txtAuthor.Text = book.Author.Name.ToString();
                    txtBio.Text = book.Author.Bio;
                    txtBook.Text = book.Title;
                }
                else
                {
                    throw new Exception("Nije moguće dohvatiti podatke o odabranoj knjizi.");
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Greška: Podaci o knjizi ili autoru nisu dostupni.",
                    "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmBookApp_Load(object sender, EventArgs e)
        {
            // Ovdje ne bi trebao biti Close() - možda Load podataka?
            try
            {
                dgvBooks.DataSource = bookService.GetBooks();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška pri učitavanju",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                FrmNewBook frm = new FrmNewBook();
                frm.ShowDialog();

                // Osvježi prikaz nakon dodavanja nove knjige
                dgvBooks.DataSource = bookService.GetBooks();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}


//DEFINIRANJE IZNIMAKA 

using DataAccessLayer;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class BookService
    {
        public List<Book> GetBooks()
        {
            return (new BookRepository()).GetBooks().ToList();
        }

        public Author GetAuthor(Book book)
        {
            return book.Author;
        }

        public bool AddBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException("Book object cannot be empty!");
            }

            if (book.ISBN.Length != 13)
            {
                throw new InvalidISBNException("ISBN must be 13 digits!");
            }

            if (book.WeightInGrams > 2000)
            {
                throw new BookTooHeavyException("Book is too heavy!");
            }

            if (book.NumOfPages < 0)
            {
                return false;
            }

            (new BookRepository()).AddBook(book);
            return true;
        }
    }
}

