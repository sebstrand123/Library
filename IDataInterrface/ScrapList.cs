using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IDataInterface
{
    public class ScrapList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ScrapListID{ get; set; }

        public int LibraryEmployeeID { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
