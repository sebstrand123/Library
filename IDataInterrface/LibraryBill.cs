using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IDataInterface
{
    public class LibraryBill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LibraryBillID { get; set; }
        public int LibraryBillNumber { get; set; }
        public decimal MonthlyFee { get; set; }
        public decimal DelayFee { get; set; }
        public bool IsActive { get; set; }

        public int CostumerID { get; set; }
        public Costumer Costumer { get; set; }
    }
}
