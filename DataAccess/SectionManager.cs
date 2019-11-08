using IDataInterface;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class SectionManager : ISectionManager
    {
        public void AddSection(int sectionNumber)
        {
            using var libraryContext = new LibraryContext();
            var section = new Section();
            section.SectionNumber = sectionNumber;
            libraryContext.Sections.Add(section);
            libraryContext.SaveChanges();
            
        }

        public Section GetSectionBySectionNumber(int sectionNumber)
        {
            using var libraryContext = new LibraryContext();
            return (from s in libraryContext.Sections
                    where s.SectionNumber == sectionNumber
                    select s)
                    .Include(sh => sh.Shelfs)
                    .FirstOrDefault();
        }

        public void RemoveSection(int sectionID)
        {
            using var libraryContext = new LibraryContext();
            var section = (from s in libraryContext.Sections
                           where s.SectionID == sectionID
                           select s).FirstOrDefault();
            libraryContext.Sections.Remove(section);
            libraryContext.SaveChanges();
        }

        public List<Section> GetScrapList()
        {
            using var librarycontext = new LibraryContext();
            return (from s in librarycontext.Sections
                    select s)
                    .Include(sh => sh.Shelfs)
                    .ThenInclude(b => b.Books).ToList();
        }
    }
}
