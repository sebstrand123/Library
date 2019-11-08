using System;
using System.Collections.Generic;
using System.Text;

namespace IDataInterface
{
    public interface IBookManager
    {
        Book GetBookByName(string bookName, bool inLibrary, int bookCondition);
        void AddBook(string bookName, int costPrice, long ISBNNumber, bool isValid, bool inLibrary, int bookCondition);
        void SetBookPrice(string bookName, int costPrice);
        void RemoveBook(int bookID);
        void MoveBook(int bookID, int shelfID);
        List<Book> GetAllBooks();
        void UpdateBookCondition(int bookID, int bookCondition);
    }
}
