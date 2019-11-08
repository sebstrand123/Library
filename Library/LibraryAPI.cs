using IDataInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public class LibraryAPI
    {
        private ISectionManager sectionManager;
        private IShelfManager shelfManager;
        private IBookManager bookManager;

        public LibraryAPI(ISectionManager sectionManager, IShelfManager shelfManager, IBookManager bookManager)
        {
            this.sectionManager = sectionManager;
            this.shelfManager = shelfManager;
            this.bookManager = bookManager;
        }

        public bool AddSection(int sectionNumber)
        {
            var existingSection = sectionManager.GetSectionBySectionNumber(sectionNumber);
            if (existingSection != null)
                return false;

            sectionManager.AddSection(sectionNumber);
            return true;
        }

        public bool AddShelf(int shelfNumber)
        {
            var existingShelf = shelfManager.GetShelfByShelfNumber(shelfNumber);
            if (existingShelf != null)
                return false;

            shelfManager.AddShelf(shelfNumber);
            return true;
        }

        public bool AddBook(string bookName, int costPrice, long ISBNNumber, bool isValid, bool inLibrary, int bookCondition)
        {
            var existingBook = bookManager.GetBookByName(bookName, inLibrary, bookCondition);
            if (existingBook != null)
                return false;

            bookManager.AddBook(bookName, costPrice, ISBNNumber, isValid, inLibrary, bookCondition);
            return true;
        }

        public bool SetBookPrice(string bookName, int costPrice, bool inLibrary, int bookCondition)
        {
            var existingBook = bookManager.GetBookByName(bookName, inLibrary, bookCondition);
            if (existingBook == null)
                return false;


            bookManager.SetBookPrice(bookName, costPrice);
            return true;
        }

        public RemoveSectionCodes RemoveSection(int sectionNumber)
        {
            var newSection = sectionManager.GetSectionBySectionNumber(sectionNumber);
            if (newSection == null)
                return RemoveSectionCodes.NoSuchSection;

            if (newSection.Shelfs.Count > 0)
                return RemoveSectionCodes.SectionHasShelves;

            sectionManager.RemoveSection(newSection.SectionID);
            return RemoveSectionCodes.Ok;
        }

        public RemoveShelfCodes RemoveShelf(int shelfNumber)
        {
            var newShelf = shelfManager.GetShelfByShelfNumber(shelfNumber);
            if (newShelf == null)
                return RemoveShelfCodes.NoSuchShelf;

            if (newShelf.Books.Count > 0)
                return RemoveShelfCodes.ShelfHasBooks;

            shelfManager.RemoveShelf(newShelf.ShelfID);
            return RemoveShelfCodes.Ok;
        }

        public RemoveBookCodes RemoveBook(string bookName, bool inLibrary, int bookCondition)
        {
            var newBook = bookManager.GetBookByName(bookName, inLibrary, bookCondition);
            if (newBook == null)
                return RemoveBookCodes.NoSuchBook;

            if (newBook.InLibrary == false)
                return RemoveBookCodes.CantRemoveBorrowedBook;

            if(newBook.InLibrary == true)
            bookManager.RemoveBook(newBook.BookID);
            return RemoveBookCodes.Ok;
        }

        public MoveShelfCodes MoveShelf(int shelfNumber, int sectionNumber)
        {
            var newSection = sectionManager.GetSectionBySectionNumber(sectionNumber);
            if (newSection == null)
                return MoveShelfCodes.NoSuchSection;

            var newShelf = shelfManager.GetShelfByShelfNumber(shelfNumber);
            if (newShelf == null)
                return MoveShelfCodes.NoSuchShelf;

            shelfManager.MoveShelf(newShelf.ShelfID, newSection.SectionID);
            return MoveShelfCodes.Ok;
        }

        public MoveBookCodes MoveBook(string bookName, bool inLibrary, int shelfNumber, int bookCondition)
        {
            var newShelf = shelfManager.GetShelfByShelfNumber(shelfNumber);
            if (newShelf == null)
                return MoveBookCodes.NoSuchShelf;

            var newBook = bookManager.GetBookByName(bookName, inLibrary, bookCondition);
            if (newBook == null)
                return MoveBookCodes.NoSuchBook;

            bookManager.MoveBook(newBook.BookID, newShelf.ShelfID);
            return MoveBookCodes.Ok;
        }
    }
}
