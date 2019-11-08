using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IDataInterface
{
    public class Costumer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CostumerID { get; set; }
        public string CostumerBirthDate { get; set; }
        public string CostumerName { get; set; }
        public string CostumerAddress { get; set; }
        public bool IsInDebt { get; set; }
        public bool HasBorrowedBook { get; set; }
        public int AmountOfBooks { get; set; }

        public ICollection<Book> Books { get; set; }
        public ICollection<LibraryBill> LibraryBills { get; set; }
    }
}
