using IDataInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class CostumerManager : ICostumerManager
    {
        public void AddCostumer(string costumerName, string costumerBirthDate, string costumerAddress, bool isInDebt, bool hasBorrowedBook, int amountOfBooks)
        {
            using var libraryContext = new LibraryContext();

            var costumer = new Costumer();
            costumer.CostumerName = costumerName;
            costumer.CostumerBirthDate = costumerBirthDate;
            costumer.CostumerAddress = costumerAddress;
            costumer.IsInDebt = isInDebt;
            costumer.HasBorrowedBook = hasBorrowedBook;
            costumer.AmountOfBooks = amountOfBooks;
            libraryContext.Costumers.Add(costumer);
            libraryContext.SaveChanges();
        }

        public Costumer GetCostumerByCostumerName(string costumerName, string costumerBirthDate, bool isInDebt, bool hasBorrowedBook, int amountOfBooks, string costumerAddress)
        {
            using var libraryContext = new LibraryContext();
            return (from c in libraryContext.Costumers
                    where c.CostumerName == costumerName
                    select c).FirstOrDefault();
        }

        public void RemoveCostumer(int costumerID)
        {
            using var libraryContext = new LibraryContext();
            var costumer = (from c in libraryContext.Costumers
                            where c.CostumerID == costumerID
                            select c)
                       .FirstOrDefault();
            libraryContext.Costumers.Remove(costumer);
            libraryContext.SaveChanges();
        }

        public void ReturnBookToLibrary(int costumerID, bool inLibrary)
        {
            using var libraryContext = new LibraryContext();
            var returnBook = (from c in libraryContext.Costumers
                              join b in libraryContext.Books
                              on c.CostumerID equals b.CostumerID
                              where b.CostumerID == costumerID
                              select b)
                              .FirstOrDefault();
            returnBook.CostumerID = 0;
            returnBook.InLibrary = true;
            libraryContext.SaveChanges();
        }

        public void SetCostumerToBook(int costumerID, int bookID)
        {
            using var libraryContext = new LibraryContext();
            var setToBook = (from c in libraryContext.Costumers
                             where c.CostumerID == costumerID
                             select c)
                             .Include(b => b.Books)
                             .FirstOrDefault();
            setToBook.CostumerID = bookID;
            libraryContext.SaveChanges();          
        }

        public void ValidateCostumerBirthDate(int costumerID, string costumerBirthDate)
        {
            using var libraryContext = new LibraryContext();
            var costumerAge = (from c in libraryContext.Costumers
                               where c.CostumerID == costumerID
                               select c).FirstOrDefault();
            costumerAge.CostumerBirthDate = costumerBirthDate;
            libraryContext.SaveChanges();
        }
    }
}
