using IDataInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public class ScrapListAPI
    {
        private ISectionManager sectionManager;
        private IShelfManager shelfManager;
        private IBookManager bookManager;
        private IScrapListManager scrapListManager;

        public ScrapListAPI(ISectionManager sectionManager, IShelfManager shelfManager,
            IBookManager bookManager, IScrapListManager scrapListManager)
        {
            this.sectionManager = sectionManager;
            this.shelfManager = shelfManager;
            this.bookManager = bookManager;
            this.scrapListManager = scrapListManager;
        }

        public GetScrapListCodes GetScrapList(int sectionNumber, int shelfNumber,
            string bookName, bool inLibrary, int bookCondition)
        {

            var newSection = sectionManager.GetSectionBySectionNumber(sectionNumber);
            if (newSection == null)
                return GetScrapListCodes.HasNoSections;

            var newShelf = shelfManager.GetShelfByShelfNumber(shelfNumber);
            if (newShelf == null)
                return GetScrapListCodes.HasNoShelfs;

            var newBook = bookManager.GetBookByName(bookName, inLibrary, bookCondition);
            if (newBook == null)
                return GetScrapListCodes.HasNoBooks;

            if (newBook.BookCondition > 1)
                return GetScrapListCodes.BookConditionIsTooHigh;

            if (newBook.InLibrary == false)
                return GetScrapListCodes.BookIsBorrowed;

                sectionManager.GetScrapList();
            return GetScrapListCodes.Ok;
        }

        public bool ClearScrapList(int bookID)
        {
            bookManager.RemoveBook(bookID);
            return true;
        }
    }
}
