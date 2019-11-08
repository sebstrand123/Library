using IDataInterface;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class BookManager : IBookManager
    {
        public void AddBook(string bookName, int costPrice, long ISBNNumber, bool isValid, bool inLibrary, int bookCondition)
        {
            using var libraryContext = new LibraryContext();
            var book = new Book();
            book.BookName = bookName;
            book.CostPrice = costPrice;
            book.ISBNNumber = ISBNNumber;
            book.IsValid = isValid;
            book.InLibrary = inLibrary;
            book.BookCondition = bookCondition;
            libraryContext.Books.Add(book);
            libraryContext.SaveChanges();

        }

        public Book GetBookByName(string bookName, bool inLibrary, int bookCondition)
        {
            using var libraryContext = new LibraryContext();
            return (from b in libraryContext.Books
                    where b.BookName == bookName
                    select b)
                    .FirstOrDefault();
        }

        public void MoveBook(int bookID, int shelfID)
        {
            using var libraryContext = new LibraryContext();
            var book = (from b in libraryContext.Books
                        where b.BookID == bookID
                        select b).First();
            book.ShelfID = shelfID;
            libraryContext.SaveChanges();
        }

        public void RemoveBook(int bookID)
        {
            using var libraryContext = new LibraryContext();
                var book = (from b in libraryContext.Books
                           where b.BookID == bookID
                           select b)
                           .FirstOrDefault();
            libraryContext.Books.Remove(book);
            libraryContext.SaveChanges();
        }

        public void SetBookPrice(string bookName, int costPrice)
        {
            using var libraryContext = new LibraryContext();
            var price = (from b in libraryContext.Books
                where b.BookName == bookName
                select b).FirstOrDefault();
            price.CostPrice = costPrice;
            libraryContext.SaveChanges();
        }

        public List<Book> GetAllBooks()
        {
            using var librarycontext = new LibraryContext();
            return (from b in librarycontext.Books
                    select b)
                    .ToList();
        }

        public void UpdateBookCondition(int bookID, int bookCondition)
        {
            using var librarycontext = new LibraryContext();
            var updateBook = (from b in librarycontext.Books
                              where b.BookID == bookID
                              select b).FirstOrDefault();
            updateBook.BookCondition = bookCondition;
            librarycontext.SaveChanges();
        }
    }
}
