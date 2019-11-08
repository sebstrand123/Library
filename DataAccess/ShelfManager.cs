using IDataInterface;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ShelfManager : IShelfManager
    {
        public void AddShelf(int shelfNumber)
        {
            using var libraryContext = new LibraryContext();
            var shelf = new Shelf();
            shelf.ShelfNumber = shelfNumber;
            libraryContext.Shelfs.Add(shelf);
            libraryContext.SaveChanges();
        }

        public Shelf GetShelfByShelfNumber(int shelfNumber)
        {
            using var libraryContext = new LibraryContext();
            return (from s in libraryContext.Shelfs
                    where s.ShelfNumber == shelfNumber
                    select s)
            .Include(b => b.Books)
            .FirstOrDefault();
        }

        public void MoveShelf(int shelfID, int sectionID)
        {
            using var libraryContext = new LibraryContext();
            var shelf = (from s in libraryContext.Shelfs
                         where s.ShelfID == shelfID
                         select s).First();
            shelf.SectionID = sectionID;
            libraryContext.SaveChanges();
        }

        public void RemoveShelf(int shelfID)
        {
            using var libraryContext = new LibraryContext();
            var shelf = (from s in libraryContext.Shelfs
                           where s.ShelfID == shelfID
                           select s).FirstOrDefault();
            libraryContext.Shelfs.Remove(shelf);
            libraryContext.SaveChanges();
        }
    }
}
