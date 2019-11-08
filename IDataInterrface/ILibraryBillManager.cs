using System;
using System.Collections.Generic;
using System.Text;

namespace IDataInterface
{
    public interface ILibraryBillManager
    {
        LibraryBill GetActiveBill(int costumerID);
        void CreateActiveBill(int costumerID, decimal monthlyFee, decimal delayFee);
        void CloseBill(int libraryBillID);
    }
}
