using System;
using System.Collections.Generic;
using System.Text;

namespace IDataInterface
{
    public interface ICostumerManager
    {
        Costumer GetCostumerByCostumerName(string costumerName, string costumerBirthDate,
            bool isInDebt, bool hasBorrowedBook, int amountOfBooks, string costumerAddress);
        void AddCostumer(string costumerName, string costumerBirthDate, string costumerAddress, bool isInDebt, bool hasBorrowedBook, int amountOfBooks);
        void RemoveCostumer(int costumerID);
        void SetCostumerToBook(int costumerID, int bookID);
        void ReturnBookToLibrary(int costumerID, bool inLibrary);
        void ValidateCostumerBirthDate(int costumerID, string costumerBirthDate);
    }
}
