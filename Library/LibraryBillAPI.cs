using IDataInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public class LibraryBillAPI
    {
        private ICostumerManager costumerManager;
        private ILibraryBillManager libraryBillManager;

        public LibraryBillAPI(ICostumerManager costumerManager, ILibraryBillManager libraryBillManager)
        {
            this.costumerManager = costumerManager;
            this.libraryBillManager = libraryBillManager;
        }

        public bool CreateActiveBill(int costumerID, decimal monthlyFee, decimal delayFee)
        {
            var existingBill = libraryBillManager.GetActiveBill(costumerID);
            if (existingBill != null)
                return false;

            libraryBillManager.CreateActiveBill(costumerID, monthlyFee, delayFee);
            return true;
        }

        public PayBillCodes PayBill(string costumerName, string costumerBirthDate, bool isInDebt, bool hasBorrowedBook, int amountOfBooks, string costumerAddress, decimal payedAmount)
        {
            var bill = GetBill(costumerName, costumerBirthDate, isInDebt, hasBorrowedBook, amountOfBooks, costumerAddress);
            if (bill.BillStatus == Bill.Status.NoSuchCostumer)
                return PayBillCodes.NoSuchCostumer;
            if (bill.BillStatus == Bill.Status.Payed)
                return PayBillCodes.BillIsPayed;
            if (bill.Amount > payedAmount)
                return PayBillCodes.WrongAmount;
            var costumer = costumerManager.GetCostumerByCostumerName(costumerName, costumerBirthDate, isInDebt, hasBorrowedBook, amountOfBooks, costumerAddress);
            var libraryBill = libraryBillManager.GetActiveBill(costumer.CostumerID);
            libraryBillManager.CloseBill(libraryBill.LibraryBillID);
            return PayBillCodes.Ok;

        }

        public Bill GetBill(string costumerName, string costumerBirthDate, bool isInDebt, bool hasBorrowedBook, int amountOfBooks, string costumerAddress)
        {
            var costumer = costumerManager.GetCostumerByCostumerName(costumerName, costumerBirthDate, isInDebt, hasBorrowedBook, amountOfBooks, costumerAddress);
            if (costumer == null)
                return new Bill { BillStatus = Bill.Status.NoSuchCostumer };
            var bill = libraryBillManager.GetActiveBill(costumer.CostumerID);
            if (IsBillPayed(bill))
                return new Bill { BillStatus = Bill.Status.Payed };
            Bill costumerBill = GetBillAfterChecks(bill);
            return costumerBill;
        }

        private static Bill GetBillAfterChecks(LibraryBill bill)
        {
            var libraryBill = new Bill();
            libraryBill.BillStatus = GetBillStatusFromBillAmount(libraryBill);
            return libraryBill;
        }

        private static Bill.Status GetBillStatusFromBillAmount(Bill libraryBill)
        {
            return libraryBill.Amount > 0 ? Bill.Status.NotYetPayed : Bill.Status.Payed;
        }

        private static bool IsBillPayed(LibraryBill bill)
        {
            return bill == null || !bill.IsActive;
        }
    }
}
