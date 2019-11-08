using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public enum BorrowBookCodes
    {
        NoBooksToBorrow,
        CostumerIsInDebt,
        CostumerHaveMaxAmountOfBooks,
        BookIsAlreadyBorrowed,
        NoSuchCostumer,
        Ok
    }
}
