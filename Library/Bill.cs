using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public class Bill
    {
        public enum Status
        {
            NoSuchCostumer,
            NotYetPayed,
            Payed,

        }

        public Status BillStatus;
        public decimal Amount;
    }
}
