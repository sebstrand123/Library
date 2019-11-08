using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IDataInterface
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookID { get; set; }
        public string BookName { get; set; }
        public int CostPrice { get; set; }
        public long ISBNNumber { get; set; }
        public bool IsValid { get; set; }
        public bool InLibrary { get; set; }
        public int BookCondition { get; set; }

        public int ShelfID { get; set; }
        public Shelf Shelf { get; set; }

        public int ScrapListID { get; set; }
        public ScrapList ScrapList { get; set; }

        public int CostumerID { get; set; }
        public Costumer Costumer { get; set; }
    }
}
