using IDataInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public class CostumerAPI
    {
        private ICostumerManager costumerManager;
        private IBookManager bookManager;

        public CostumerAPI(ICostumerManager costumerManager, IBookManager bookManager)
        {
            this.costumerManager = costumerManager;
            this.bookManager = bookManager;
        }

        public bool AddCostumer(string costumerName, string costumerBirthDate, string costumerAddress, 
                                bool isInDebt, bool hasBorrowedBook, int amountOfBooks)
        {
            var existingCostumer = costumerManager.GetCostumerByCostumerName(costumerName, costumerBirthDate, isInDebt, 
                                                                            hasBorrowedBook, amountOfBooks, costumerAddress);
            if (existingCostumer != null)
                return false;

            costumerManager.AddCostumer(costumerName, costumerBirthDate, costumerAddress, isInDebt, hasBorrowedBook, amountOfBooks);
            return true;
        }

        public RemoveCostumerCodes RemoveCostumer(string costumerName, string costumerBirthDate, bool isInDebt,
                                                  bool hasBorrowedBook, int amountOfBooks, string costumerAddress)
        {
            var newCostumer = costumerManager.GetCostumerByCostumerName(costumerName, costumerBirthDate,
                                                isInDebt, hasBorrowedBook, amountOfBooks, costumerAddress);
            if (newCostumer == null)
                return RemoveCostumerCodes.NoSuchCostumer;

            if (newCostumer.HasBorrowedBook == true)
                return RemoveCostumerCodes.CostumerHasBorrowedBooks;

            if (newCostumer.IsInDebt == true)
                return RemoveCostumerCodes.CostumerOwesLibraryMoney;

            if (newCostumer.IsInDebt == false && newCostumer.HasBorrowedBook == false)
                costumerManager.RemoveCostumer(newCostumer.CostumerID);
            return RemoveCostumerCodes.Ok;
        }

        public BorrowBookCodes SetCostumerToBook(int bookID, int costumerID, string costumerName,
            string costumerBirthDate, bool isInDebt, bool hasBorrowedBook, int amountOfBooks, string bookName,bool inLibrary, int bookCondition, string costumerAddress)
        {
            var newBook = bookManager.GetBookByName(bookName, inLibrary, bookCondition);
            if (newBook == null)
                return BorrowBookCodes.NoBooksToBorrow;

            if (newBook.InLibrary == false)
                return BorrowBookCodes.BookIsAlreadyBorrowed;

            var newCostumer = costumerManager.GetCostumerByCostumerName(costumerName, costumerBirthDate, isInDebt, hasBorrowedBook, amountOfBooks, costumerAddress);
            if (newCostumer == null)
                return BorrowBookCodes.NoSuchCostumer;

            if (newCostumer.IsInDebt == true)
                return BorrowBookCodes.CostumerIsInDebt;

            if (newCostumer.AmountOfBooks >= 5)
                return BorrowBookCodes.CostumerHaveMaxAmountOfBooks;

            if (newCostumer.IsInDebt == false && newCostumer.AmountOfBooks < 5)
                costumerManager.SetCostumerToBook(bookID, costumerID);
            return BorrowBookCodes.Ok;
        }

        public bool ValidateCostumerBirthDate(int costumerID, string costumerBirthDate, string costumerName, bool isInDebt, bool hasBorrowedBook, int amountOfBooks, string costumerAddress)
        {
            var existingCostumer = costumerManager.GetCostumerByCostumerName(costumerName, costumerBirthDate, isInDebt, hasBorrowedBook, amountOfBooks, costumerAddress);
            if (existingCostumer == null)
                return false;

            
            if (TryParseCostumerBirthDate(costumerBirthDate))
                costumerManager.ValidateCostumerBirthDate(costumerID, costumerBirthDate);
            return true;
        }

        private static bool TryParseCostumerBirthDate(string costumerBirthDate)
        {
            bool correctCostumerBirthDate;
            int birthDate;
            correctCostumerBirthDate = int.TryParse(costumerBirthDate, out birthDate);
            return correctCostumerBirthDate;
        }

    public ReturnBookCodes ReturnBookToLibrary(int costumerID, bool inLibrary, string bookName, int bookCondition, 
            string costumerName, string costumerBirthDate, bool isInDebt, bool hasBorrowedBook, int amountOfBooks, string costumerAddress)
        {
            var newBook = bookManager.GetBookByName(bookName, inLibrary, bookCondition);
            var newCostumer = costumerManager.GetCostumerByCostumerName(costumerName, costumerBirthDate, isInDebt, hasBorrowedBook, amountOfBooks, costumerAddress);
            if (newCostumer == null)
                return ReturnBookCodes.NoSuchCostumer;

            if (newCostumer.AmountOfBooks == 0 && newBook == null)
                return ReturnBookCodes.CostumerHasNoBooksToReturn;

            if(newCostumer.AmountOfBooks > 0)
                costumerManager.ReturnBookToLibrary(costumerID, inLibrary);
            return ReturnBookCodes.Ok;
        }

        public bool UpdateBookCondition(int bookID, int bookCondition, string bookName, bool inLibrary)
        {
            var existingBook = bookManager.GetBookByName(bookName, inLibrary, bookCondition);
            if (existingBook == null)
                return false;

            bookManager.UpdateBookCondition(bookID, bookCondition);
            return true;
        }
    }
}
