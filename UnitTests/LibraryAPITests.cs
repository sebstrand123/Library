using IDataInterface;
using Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class TestAddItems
    {
        [TestMethod]
        public void TestAddSection()
        {
            var SectionManagerMock = new Mock<ISectionManager>();
            var shelfManagerMock = new Mock<IShelfManager>();
            var BookManagerMock = new Mock<IBookManager>();

            SectionManagerMock.Setup(m =>
            m.GetSectionBySectionNumber(It.IsAny<int>()))
            .Returns((Section)null);

            SectionManagerMock.Setup(m =>
            m.AddSection(It.IsAny<int>()));


            var libraryAPI = new LibraryAPI(SectionManagerMock.Object, shelfManagerMock.Object, BookManagerMock.Object);
            var successfull = libraryAPI.AddSection(1);
            Assert.IsTrue(successfull);
            SectionManagerMock.Verify(
                m => m.AddSection(It.Is<int>(i => i == 1)),
                Times.Once());
        }
        [TestMethod]
        public void TestAddExistingSection()
        {
            var SectionManagerMock = new Mock<ISectionManager>();
            var shelfManagerMock = new Mock<IShelfManager>();
            var BookManagerMock = new Mock<IBookManager>();

            SectionManagerMock.Setup(m =>
            m.GetSectionBySectionNumber(It.IsAny<int>()))
            .Returns(new Section());

            SectionManagerMock.Setup(m =>
            m.AddSection(It.IsAny<int>()));


            var libraryAPI = new LibraryAPI(SectionManagerMock.Object, shelfManagerMock.Object, BookManagerMock.Object);
            var successfull = libraryAPI.AddSection(1);
            Assert.IsFalse(successfull);
            SectionManagerMock.Verify(
                m => m.AddSection(It.Is<int>(i => i == 1)),
                Times.Never());
        }
        [TestMethod]
        public void TestAddShelf()
        {
            var sectionManagerMock = new Mock<ISectionManager>();
            var shelfManagerMock = new Mock<IShelfManager>();
            var BookManagerMock = new Mock<IBookManager>();

            shelfManagerMock.Setup(m =>
            m.GetShelfByShelfNumber(It.IsAny<int>()))
            .Returns((Shelf)null);

            shelfManagerMock.Setup(m =>
            m.AddShelf(It.IsAny<int>()));


            var libraryAPI = new LibraryAPI(sectionManagerMock.Object, shelfManagerMock.Object, BookManagerMock.Object);
            var successfull = libraryAPI.AddShelf(1);
            Assert.IsTrue(successfull);
            shelfManagerMock.Verify(
                m => m.AddShelf(It.Is<int>(i => i == 1)),
                Times.Once());
        }
        [TestMethod]
        public void TestAddExistingShelf()
        {
            var sectionManagerMock = new Mock<ISectionManager>();
            var shelfManagerMock = new Mock<IShelfManager>();
            var BookManagerMock = new Mock<IBookManager>();

            shelfManagerMock.Setup(m =>
            m.GetShelfByShelfNumber(It.IsAny<int>()))
            .Returns(new Shelf());

            shelfManagerMock.Setup(m =>
            m.AddShelf(It.IsAny<int>()));


            var libraryAPI = new LibraryAPI(sectionManagerMock.Object, shelfManagerMock.Object, BookManagerMock.Object);
            var successfull = libraryAPI.AddShelf(1);
            Assert.IsFalse(successfull);
            shelfManagerMock.Verify(
                m => m.AddShelf(It.Is<int>(i => i == 1)),
                Times.Never());
        }

        [TestMethod]
        public void TestAddBook()
        {
            var SectionManagerMock = new Mock<ISectionManager>();
            var shelfManagerMock = new Mock<IShelfManager>();
            var BookManagerMock = new Mock<IBookManager>();

            BookManagerMock.Setup(m =>
            m.GetBookByName(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>()))
            .Returns((Book)null);

            BookManagerMock.Setup(m =>
            m.AddBook(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<long>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int>()));


            var libraryAPI = new LibraryAPI(SectionManagerMock.Object, shelfManagerMock.Object, BookManagerMock.Object);
            var successfull = libraryAPI.AddBook("Clean Code", 199, 9780132350884, true, true, 5);
            Assert.IsTrue(successfull);
            BookManagerMock.Verify(
                m => m.AddBook(It.Is<string>(i => i == "Clean Code"), It.Is<int>(i => i == 199), It.Is<long>(i => i == 9780132350884),
                It.Is<bool>(i => i == true), It.Is<bool>(i => i == true), It.Is<int>(i => i == 5)),
                Times.Once());
        }
        [TestMethod]
        public void TestAddExistingBook()
        {
            var SectionManagerMock = new Mock<ISectionManager>();
            var shelfManagerMock = new Mock<IShelfManager>();
            var BookManagerMock = new Mock<IBookManager>();

            BookManagerMock.Setup(m =>
            m.GetBookByName(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>()))
            .Returns(new Book());

            BookManagerMock.Setup(m =>
            m.AddBook(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<long>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int>()));


            var libraryAPI = new LibraryAPI(SectionManagerMock.Object, shelfManagerMock.Object, BookManagerMock.Object);
            var successfull = libraryAPI.AddBook("Clean Code", 199, 9780132350884, true, true, 5);
            Assert.IsFalse(successfull);
            BookManagerMock.Verify(
                m => m.AddBook(It.Is<string>(i => i == "Clean Code"), It.Is<int>(i => i == 199), It.Is<long>(i => i == 9780132350884),
                It.Is<bool>(i => i == true), It.Is<bool>(i => i == true), It.Is<int>(i => i == 5)),
                Times.Never());
        }
    }
    [TestClass]
    public class TestSetPrice
    {
        [TestMethod]
        public void TestSetBookPrice()
        {
            var SectionManagerMock = new Mock<ISectionManager>();
            var shelfManagerMock = new Mock<IShelfManager>();
            var BookManagerMock = new Mock<IBookManager>();

            BookManagerMock.Setup(m =>
            m.GetBookByName(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>()))
            .Returns(new Book());

            BookManagerMock.Setup(m =>
            m.SetBookPrice(It.IsAny<string>(), It.IsAny<int>()));

            var libraryAPI = new LibraryAPI(SectionManagerMock.Object, shelfManagerMock.Object, BookManagerMock.Object);
            var successfull = libraryAPI.SetBookPrice("Clean Code", 199, true, 5);
            Assert.IsTrue(successfull);
            BookManagerMock.Verify(
                m => m.SetBookPrice(It.Is<string>(i => i == "Clean Code"), It.Is<int>(i => i == 199)),
                Times.Once());
        }
        [TestMethod]
        public void TestSetPriceOnNoneExistingBook()
        {
            var SectionManagerMock = new Mock<ISectionManager>();
            var shelfManagerMock = new Mock<IShelfManager>();
            var BookManagerMock = new Mock<IBookManager>();

            BookManagerMock.Setup(m =>
            m.GetBookByName(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>()))
            .Returns((Book)null);

            BookManagerMock.Setup(m =>
            m.SetBookPrice(It.IsAny<string>(), It.IsAny<int>()));

            var libraryAPI = new LibraryAPI(SectionManagerMock.Object, shelfManagerMock.Object, BookManagerMock.Object);
            var successfull = libraryAPI.SetBookPrice("Clean Code", 199, true, 5);
            Assert.IsFalse(successfull);
            BookManagerMock.Verify(
                m => m.SetBookPrice(It.Is<string>(i => i == "Clean Code"), It.Is<int>(i => i == 199)),
                Times.Never());
        }
    }
    [TestClass]
    public class TestRemoveItems
    {
        [TestMethod]
        public void TestRemoveEmptySection()
        {
            var sectionManagerMock = new Mock<ISectionManager>();
            var shelfManagerMock = new Mock<IShelfManager>();
            var bookManagerMock = new Mock<IBookManager>();

            sectionManagerMock.Setup(m =>
               m.GetSectionBySectionNumber(It.IsAny<int>()))
                .Returns(new Section
                {
                    SectionNumber = 1,
                    Shelfs = new List<Shelf>()
                });

            var libraryAPI = new LibraryAPI(sectionManagerMock.Object, shelfManagerMock.Object, bookManagerMock.Object);
            var successfull = libraryAPI.RemoveSection(1);
            Assert.AreEqual(RemoveSectionCodes.Ok, successfull);
            sectionManagerMock.Verify(m =>
                m.RemoveSection(It.IsAny<int>()), Times.Once);
        }
        [TestMethod]
        public void TestRemoveSectionWithOneShelf()
        {
            var sectionManagerMock = new Mock<ISectionManager>();
            var shelfManagerMock = new Mock<IShelfManager>();
            var bookManagerMock = new Mock<IBookManager>();

            sectionManagerMock.Setup(m =>
               m.GetSectionBySectionNumber(It.IsAny<int>()))
                .Returns(new Section
                {
                    SectionNumber = 1,
                    Shelfs = new List<Shelf>
                    {
                        new Shelf()
                    }
                });

            var libraryAPI = new LibraryAPI(sectionManagerMock.Object, shelfManagerMock.Object, bookManagerMock.Object);
            var successfull = libraryAPI.RemoveSection(1);
            Assert.AreEqual(RemoveSectionCodes.SectionHasShelves, successfull);
            sectionManagerMock.Verify(m =>
                m.RemoveSection(It.IsAny<int>()), Times.Never);
        }
        [TestMethod]
        public void TestRemoveNoneExistingSection()
        {
            var sectionManagerMock = new Mock<ISectionManager>();
            var shelfManagerMock = new Mock<IShelfManager>();
            var bookManagerMock = new Mock<IBookManager>();

            sectionManagerMock.Setup(m =>
               m.GetSectionBySectionNumber(It.IsAny<int>()))
               .Returns((Section)null);

            var libraryAPI = new LibraryAPI(sectionManagerMock.Object, shelfManagerMock.Object, bookManagerMock.Object);
            var successfull = libraryAPI.RemoveSection(1);
            Assert.AreEqual(RemoveSectionCodes.NoSuchSection, successfull);
            sectionManagerMock.Verify(m =>
                m.RemoveSection(It.IsAny<int>()), Times.Never);
        }
        [TestMethod]
        public void TestRemoveEmptyShelf()
        {
            var sectionManagerMock = new Mock<ISectionManager>();
            var shelfManagerMock = new Mock<IShelfManager>();
            var bookManagerMock = new Mock<IBookManager>();

            shelfManagerMock.Setup(m =>
               m.GetShelfByShelfNumber(It.IsAny<int>()))
                .Returns(new Shelf
                {
                    ShelfNumber = 1,
                    Books = new List<Book>()
                });

            var libraryAPI = new LibraryAPI(sectionManagerMock.Object, shelfManagerMock.Object, bookManagerMock.Object);
            var successfull = libraryAPI.RemoveShelf(1);
            Assert.AreEqual(RemoveShelfCodes.Ok, successfull);
            shelfManagerMock.Verify(m =>
                m.RemoveShelf(It.IsAny<int>()), Times.Once);
        }
        [TestMethod]
        public void TestRemoveShelfWithOneBook()
        {
            var sectionManagerMock = new Mock<ISectionManager>();
            var shelfManagerMock = new Mock<IShelfManager>();
            var bookManagerMock = new Mock<IBookManager>();

            shelfManagerMock.Setup(m =>
               m.GetShelfByShelfNumber(It.IsAny<int>()))
                .Returns(new Shelf
                {
                    ShelfNumber = 1,
                    Books = new List<Book>
                    {
                        new Book()
                    }
                });

            var libraryAPI = new LibraryAPI(sectionManagerMock.Object, shelfManagerMock.Object, bookManagerMock.Object);
            var successfull = libraryAPI.RemoveShelf(1);
            Assert.AreEqual(RemoveShelfCodes.ShelfHasBooks, successfull);
            shelfManagerMock.Verify(m =>
                m.RemoveShelf(It.IsAny<int>()), Times.Never);
        }
        [TestMethod]
        public void TestRemoveNoneExistingShelf()
        {
            var sectionManagerMock = new Mock<ISectionManager>();
            var shelfManagerMock = new Mock<IShelfManager>();
            var bookManagerMock = new Mock<IBookManager>();

            shelfManagerMock.Setup(m =>
               m.GetShelfByShelfNumber(It.IsAny<int>()))
               .Returns((Shelf)null);

            var libraryAPI = new LibraryAPI(sectionManagerMock.Object, shelfManagerMock.Object, bookManagerMock.Object);
            var successfull = libraryAPI.RemoveShelf(1);
            Assert.AreEqual(RemoveShelfCodes.NoSuchShelf, successfull);
            sectionManagerMock.Verify(m =>
                m.RemoveSection(It.IsAny<int>()), Times.Never);
        }
        [TestMethod]
        public void TestRemoveBookInLibrary()
        {
            var sectionManagerMock = new Mock<ISectionManager>();
            var shelfManagerMock = new Mock<IShelfManager>();
            var bookManagerMock = new Mock<IBookManager>();

            bookManagerMock.Setup(m =>
            m.GetBookByName(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>()))
            .Returns(new Book
            {
                BookID = 1,
                InLibrary = true
            });

            bookManagerMock.Setup(m =>
            m.RemoveBook(It.IsAny<int>()));

            var libraryAPI = new LibraryAPI(sectionManagerMock.Object, shelfManagerMock.Object, bookManagerMock.Object);
            var successfull = libraryAPI.RemoveBook("Clean Code", true, 5);
            Assert.AreEqual(RemoveBookCodes.Ok, successfull);
            bookManagerMock.Verify(
                m => m.RemoveBook(It.IsAny<int>()),
                Times.Once());
        }
        [TestMethod]
        public void TestRemoveNoneExistingBook()
        {
            var sectionManagerMock = new Mock<ISectionManager>();
            var shelfManagerMock = new Mock<IShelfManager>();
            var bookManagerMock = new Mock<IBookManager>();

            bookManagerMock.Setup(m =>
            m.GetBookByName(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>()))
            .Returns((Book)null);

            bookManagerMock.Setup(m =>
            m.RemoveBook(It.IsAny<int>()));

            var libraryAPI = new LibraryAPI(sectionManagerMock.Object, shelfManagerMock.Object, bookManagerMock.Object);
            var successfull = libraryAPI.RemoveBook("Clean Code", false, 5);
            Assert.AreEqual(RemoveBookCodes.NoSuchBook, successfull);
            bookManagerMock.Verify(
                m => m.RemoveBook(It.IsAny<int>()),
                Times.Never());
        }
        [TestMethod]
        public void TestRemoveBorrowedBook()
        {
            var sectionManagerMock = new Mock<ISectionManager>();
            var shelfManagerMock = new Mock<IShelfManager>();
            var bookManagerMock = new Mock<IBookManager>();

            bookManagerMock.Setup(m =>
            m.GetBookByName(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>()))
            .Returns(new Book
            {
                BookID = 1,
                InLibrary = false
            });

            bookManagerMock.Setup(m =>
            m.RemoveBook(It.IsAny<int>()));

            var libraryAPI = new LibraryAPI(sectionManagerMock.Object, shelfManagerMock.Object, bookManagerMock.Object);
            var successfull = libraryAPI.RemoveBook("Clean Code", false, 5);
            Assert.AreEqual(RemoveBookCodes.CantRemoveBorrowedBook, successfull);
            bookManagerMock.Verify(
                m => m.RemoveBook(It.IsAny<int>()),
                Times.Never());
        }
    }
    [TestClass]
    public class TestMoveItems
    {
        [TestMethod]
        public void TestMoveShelf()
        {
            var sectionManagerMock = new Mock<ISectionManager>();
            var shelfManagerMock = new Mock<IShelfManager>();
            var bookManagerMock = new Mock<IBookManager>();

            sectionManagerMock.Setup(m =>
               m.GetSectionBySectionNumber(It.IsAny<int>()))
                .Returns(new Section { SectionID = 1 });

            shelfManagerMock.Setup(m =>
              m.GetShelfByShelfNumber(It.IsAny<int>()))
               .Returns(new Shelf
               {
                   ShelfID = 1,
                   Section = new Section()
               });

            var libraryAPI = new LibraryAPI(sectionManagerMock.Object, shelfManagerMock.Object, bookManagerMock.Object);
            var result = libraryAPI.MoveShelf(2, 2);
            Assert.AreEqual(MoveShelfCodes.Ok, result);
            shelfManagerMock.Verify(m =>
                m.MoveShelf(1, 1), Times.Once());
        }
        [TestMethod]
        public void TestMoveShelfWithoutSection()
        {
            var sectionManagerMock = new Mock<ISectionManager>();
            var shelfManagerMock = new Mock<IShelfManager>();
            var bookManagerMock = new Mock<IBookManager>();

            sectionManagerMock.Setup(m =>
               m.GetSectionBySectionNumber(It.IsAny<int>()))
               .Returns((Section)null);

            shelfManagerMock.Setup(m =>
              m.GetShelfByShelfNumber(It.IsAny<int>()))
               .Returns(new Shelf
               {
                   ShelfID = 1
               });

            var libraryAPI = new LibraryAPI(sectionManagerMock.Object, shelfManagerMock.Object, bookManagerMock.Object);
            var result = libraryAPI.MoveShelf(2, 2);
            Assert.AreEqual(MoveShelfCodes.NoSuchSection, result);
            shelfManagerMock.Verify(m =>
                m.MoveShelf(1, 1), Times.Never());
        }
        [TestMethod]
        public void TestMoveWithoutShelf()
        {
            var sectionManagerMock = new Mock<ISectionManager>();
            var shelfManagerMock = new Mock<IShelfManager>();
            var bookManagerMock = new Mock<IBookManager>();

            sectionManagerMock.Setup(m =>
               m.GetSectionBySectionNumber(It.IsAny<int>()))
                .Returns(new Section { SectionID = 1 });

            shelfManagerMock.Setup(m =>
              m.GetShelfByShelfNumber(It.IsAny<int>()))
                .Returns((Shelf)null);


            var libraryAPI = new LibraryAPI(sectionManagerMock.Object, shelfManagerMock.Object, bookManagerMock.Object);
            var result = libraryAPI.MoveShelf(2, 2);
            Assert.AreEqual(MoveShelfCodes.NoSuchShelf, result);
            shelfManagerMock.Verify(m =>
                m.MoveShelf(1, 1), Times.Never());
        }
        [TestMethod]
        public void TestMoveBook()
        {
            var sectionManagerMock = new Mock<ISectionManager>();
            var shelfManagerMock = new Mock<IShelfManager>();
            var bookManagerMock = new Mock<IBookManager>();

            shelfManagerMock.Setup(m =>
               m.GetShelfByShelfNumber(It.IsAny<int>()))
                .Returns(new Shelf { ShelfID = 1 });

            bookManagerMock.Setup(m =>
              m.GetBookByName(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>()))
               .Returns(new Book
               {
                   BookID = 1,
                   Shelf = new Shelf()
               });

            var libraryAPI = new LibraryAPI(sectionManagerMock.Object, shelfManagerMock.Object, bookManagerMock.Object);
            var result = libraryAPI.MoveBook("Clean Code", true, 2, 5);
            Assert.AreEqual(MoveBookCodes.Ok, result);
            bookManagerMock.Verify(m =>
                m.MoveBook(1, 1), Times.Once());
        }
        [TestMethod]
        public void TestMoveBookWithoutBook()
        {
            var sectionManagerMock = new Mock<ISectionManager>();
            var shelfManagerMock = new Mock<IShelfManager>();
            var bookManagerMock = new Mock<IBookManager>();

            shelfManagerMock.Setup(m =>
               m.GetShelfByShelfNumber(It.IsAny<int>()))
                .Returns(new Shelf { ShelfID = 1 });

            bookManagerMock.Setup(m =>
              m.GetBookByName(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>()))
                .Returns((Book)null);


            var libraryAPI = new LibraryAPI(sectionManagerMock.Object, shelfManagerMock.Object, bookManagerMock.Object);
            var result = libraryAPI.MoveBook("Clean Code", true, 2, 5);
            Assert.AreEqual(MoveBookCodes.NoSuchBook, result);
            bookManagerMock.Verify(m =>
                m.MoveBook(1, 1), Times.Never());
        }
        [TestMethod]
        public void TestMoveBookWithoutShelf()
        {
            var sectionManagerMock = new Mock<ISectionManager>();
            var shelfManagerMock = new Mock<IShelfManager>();
            var bookManagerMock = new Mock<IBookManager>();

            shelfManagerMock.Setup(m =>
               m.GetShelfByShelfNumber(It.IsAny<int>()))
                .Returns((Shelf)null);

            bookManagerMock.Setup(m =>
              m.GetBookByName(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>()))
               .Returns(new Book
               {
                   BookID = 1,
               });

            var libraryAPI = new LibraryAPI(sectionManagerMock.Object, shelfManagerMock.Object, bookManagerMock.Object);
            var result = libraryAPI.MoveBook("Clean Code", true, 2, 5);
            Assert.AreEqual(MoveBookCodes.NoSuchShelf, result);
            bookManagerMock.Verify(m =>
                m.MoveBook(1, 1), Times.Never());
        }
    }
}
