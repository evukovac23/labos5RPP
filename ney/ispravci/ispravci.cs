

//AUTHOR REPOSITORY ispravak

using DataAccessLayer;
using EntitiesLayer;
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

// ovo su neki jebe me ovaj copilot pastema jos u sljedeceom commitu  
