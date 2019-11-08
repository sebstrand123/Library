using IDataInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class LibraryBillManager : ILibraryBillManager
    {
        public void CreateActiveBill(int costumerID, decimal monthlyFee, decimal delayFee)
        {
            using var libraryContext = new LibraryContext();
            var bill = new LibraryBill();
            bill.CostumerID = costumerID;
            bill.MonthlyFee = monthlyFee;
            bill.DelayFee = delayFee;
            bill.IsActive = true;
            libraryContext.LibraryBills.Add(bill);
            libraryContext.SaveChanges();
        }

        public LibraryBill GetActiveBill(int costumerID)
        {
            using var libraryContext = new LibraryContext();
            return (from l in libraryContext.LibraryBills
                    where l.CostumerID == costumerID && l.IsActive
                    select l)
                    .FirstOrDefault();
        }

        public void CloseBill(int libraryBillID)
        {
            using var libraryContext = new LibraryContext();
            var bill = (from l in libraryContext.LibraryBills
                         where l.LibraryBillID == libraryBillID
                         select l)
                    .FirstOrDefault();
            bill.IsActive = false;
            libraryContext.SaveChanges();
        }
    }
}
