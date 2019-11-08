using IDataInterface;
using Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
    [TestClass]
    public class GetAndClearScrapListTests
    {
        [TestMethod]
        public void TestGetScrapList()
        {
            var sectionManagerMock = new Mock<ISectionManager>();
            var shelfManagerMock = new Mock<IShelfManager>();
            var bookManagerMock = new Mock<IBookManager>();
            var scrapListManagerMock = new Mock<IScrapListManager>();

            var scrapListAPI = new ScrapListAPI(sectionManagerMock.Object, shelfManagerMock.Object,
                bookManagerMock.Object, scrapListManagerMock.Object);
            sectionManagerMock.Setup(m =>
            m.GetSectionBySectionNumber(It.Is<int>(i => i == 11)))
            .Returns(new Section 
            { 
                SectionID = 10,
                SectionNumber = 11,
            });

            shelfManagerMock.Setup(m =>
            m.GetShelfByShelfNumber(It.Is<int>(i => i == 21)))
            .Returns(new Shelf 
            {
                SectionID = 10,
                ShelfID = 20,
                ShelfNumber = 21
            });

            bookManagerMock.Setup(m =>
            m.GetBookByName(It.Is<string>(s => s == "Clean Code"), It.Is<bool>(b => b == true), It.Is<int>(i => i == 1)))
            .Returns(new Book 
            {
                ShelfID = 20,
                BookID = 30,
                BookName = "Clean Code",
                BookCondition = 1,
                InLibrary = true,
            });

            var successfull = scrapListAPI.GetScrapList(11, 21, "Clean Code", true, 1);
            Assert.AreEqual(GetScrapListCodes.Ok, successfull);
            sectionManagerMock.Verify(
            m => m.GetScrapList(),
                Times.Once());
        }
        [TestMethod]
        public void TestGetScrapListWithNoSections()
        {
            var sectionManagerMock = new Mock<ISectionManager>();
            var shelfManagerMock = new Mock<IShelfManager>();
            var bookManagerMock = new Mock<IBookManager>();
            var scrapListManagerMock = new Mock<IScrapListManager>();

            var scrapListAPI = new ScrapListAPI(sectionManagerMock.Object, shelfManagerMock.Object,
                bookManagerMock.Object, scrapListManagerMock.Object);

            shelfManagerMock.Setup(m =>
            m.GetShelfByShelfNumber(It.IsAny<int>()))
            .Returns(new Shelf
            {
                ShelfID = 20,
            });

            bookManagerMock.Setup(m =>
            m.GetBookByName(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>()))
            .Returns(new Book
            {
                ShelfID = 20,
                BookID = 30,
                BookName = "Clean Code",
                BookCondition = 1,
                InLibrary = true,
            });

            var successfull = scrapListAPI.GetScrapList(0, 0, "Clean Code", true, 1);
            Assert.AreEqual(GetScrapListCodes.HasNoSections, successfull);
            sectionManagerMock.Verify(
            m => m.GetScrapList(),
                Times.Never());
        }
        [TestMethod]
        public void TestGetScrapListWithNoShelf()
        {
            var sectionManagerMock = new Mock<ISectionManager>();
            var shelfManagerMock = new Mock<IShelfManager>();
            var bookManagerMock = new Mock<IBookManager>();
            var scrapListManagerMock = new Mock<IScrapListManager>();

            var scrapListAPI = new ScrapListAPI(sectionManagerMock.Object, shelfManagerMock.Object,
                bookManagerMock.Object, scrapListManagerMock.Object);
            sectionManagerMock.Setup(m =>
            m.GetSectionBySectionNumber(It.IsAny<int>()))
            .Returns(new Section
            {
                SectionID = 10,
            });

            bookManagerMock.Setup(m =>
            m.GetBookByName(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>()))
            .Returns(new Book
            {
                BookID = 30,
                BookName = "Clean Code",
                BookCondition = 1,
                InLibrary = true,
            });

            var successfull = scrapListAPI.GetScrapList(0, 0, "Clean Code", true, 1);
            Assert.AreEqual(GetScrapListCodes.HasNoShelfs, successfull);
            sectionManagerMock.Verify(
            m => m.GetScrapList(),
                Times.Never());
        }
        [TestMethod]
        public void TestGetScrapListWithNoBooks()
        {
            var sectionManagerMock = new Mock<ISectionManager>();
            var shelfManagerMock = new Mock<IShelfManager>();
            var bookManagerMock = new Mock<IBookManager>();
            var scrapListManagerMock = new Mock<IScrapListManager>();

            var scrapListAPI = new ScrapListAPI(sectionManagerMock.Object, shelfManagerMock.Object,
                bookManagerMock.Object, scrapListManagerMock.Object);
            sectionManagerMock.Setup(m =>
            m.GetSectionBySectionNumber(It.IsAny<int>()))
            .Returns(new Section
            {
                SectionID = 10,
            });

            shelfManagerMock.Setup(m =>
            m.GetShelfByShelfNumber(It.IsAny<int>()))
            .Returns(new Shelf
            {
                SectionID = 10,
                ShelfID = 20,
            });

            var successfull = scrapListAPI.GetScrapList(0, 0, "Clean Code", true, 1);
            Assert.AreEqual(GetScrapListCodes.HasNoBooks, successfull);
            sectionManagerMock.Verify(
            m => m.GetScrapList(),
                Times.Never());
        }
        [TestMethod]
        public void TestGetScrapListWithBookConditionHigherThan1()
        {
            var sectionManagerMock = new Mock<ISectionManager>();
            var shelfManagerMock = new Mock<IShelfManager>();
            var bookManagerMock = new Mock<IBookManager>();
            var scrapListManagerMock = new Mock<IScrapListManager>();

            var scrapListAPI = new ScrapListAPI(sectionManagerMock.Object, shelfManagerMock.Object,
                bookManagerMock.Object, scrapListManagerMock.Object);
            sectionManagerMock.Setup(m =>
            m.GetSectionBySectionNumber(It.Is<int>(i => i == 11)))
            .Returns(new Section
            {
                SectionID = 10,
                SectionNumber = 11,
            });

            shelfManagerMock.Setup(m =>
            m.GetShelfByShelfNumber(It.Is<int>(i => i == 21)))
            .Returns(new Shelf
            {
                SectionID = 10,
                ShelfID = 20,
                ShelfNumber = 21
            });

            bookManagerMock.Setup(m =>
            m.GetBookByName(It.Is<string>(s => s == "Clean Code"), It.Is<bool>(b => b == true), It.Is<int>(i => i == 2)))
            .Returns(new Book
            {
                ShelfID = 20,
                BookID = 30,
                BookName = "Clean Code",
                BookCondition = 2,
                InLibrary = true,
            });

            var successfull = scrapListAPI.GetScrapList(11, 21, "Clean Code", true, 2);
            Assert.AreEqual(GetScrapListCodes.BookConditionIsTooHigh, successfull);
            sectionManagerMock.Verify(
            m => m.GetScrapList(),
                Times.Never());
        }
        [TestMethod]
        public void TestGetScrapListWithBorrowedBook()
        {
            var sectionManagerMock = new Mock<ISectionManager>();
            var shelfManagerMock = new Mock<IShelfManager>();
            var bookManagerMock = new Mock<IBookManager>();
            var scrapListManagerMock = new Mock<IScrapListManager>();

            var scrapListAPI = new ScrapListAPI(sectionManagerMock.Object, shelfManagerMock.Object,
                bookManagerMock.Object, scrapListManagerMock.Object);
            sectionManagerMock.Setup(m =>
            m.GetSectionBySectionNumber(It.Is<int>(i => i == 11)))
            .Returns(new Section
            {
                SectionID = 10,
                SectionNumber = 11,
            });

            shelfManagerMock.Setup(m =>
            m.GetShelfByShelfNumber(It.Is<int>(i => i == 21)))
            .Returns(new Shelf
            {
                SectionID = 10,
                ShelfID = 20,
                ShelfNumber = 21
            });

            bookManagerMock.Setup(m =>
            m.GetBookByName(It.Is<string>(s => s == "Clean Code"), It.Is<bool>(b => b == false), It.Is<int>(i => i == 1)))
            .Returns(new Book
            {
                ShelfID = 20,
                BookID = 30,
                BookName = "Clean Code",
                BookCondition = 1,
                InLibrary = false,
            });

            var successfull = scrapListAPI.GetScrapList(11, 21, "Clean Code", false, 1);
            Assert.AreEqual(GetScrapListCodes.BookIsBorrowed, successfull);
            sectionManagerMock.Verify(
            m => m.GetScrapList(),
                Times.Never());
        }
        [TestMethod]
        public void ClearScrapList()
        {
            var sectionManagerMock = new Mock<ISectionManager>();
            var shelfManagerMock = new Mock<IShelfManager>();
            var bookManagerMock = new Mock<IBookManager>();
            var scrapListManagerMock = new Mock<IScrapListManager>();

            var scrapListAPI = new ScrapListAPI(sectionManagerMock.Object, shelfManagerMock.Object,
                bookManagerMock.Object, scrapListManagerMock.Object);

            sectionManagerMock.Setup(m =>
            m.GetSectionBySectionNumber(It.Is<int>(i => i == 11)))
            .Returns(new Section
            {
                SectionID = 10,
                SectionNumber = 11,
            });

            shelfManagerMock.Setup(m =>
            m.GetShelfByShelfNumber(It.Is<int>(i => i == 21)))
            .Returns(new Shelf
            {
                SectionID = 10,
                ShelfID = 20,
                ShelfNumber = 21
            });

            bookManagerMock.Setup(m =>
            m.GetBookByName(It.Is<string>(s => s == "Clean Code"), It.Is<bool>(b => b == true), It.Is<int>(i => i == 1)))
            .Returns(new Book
            {
                ShelfID = 20,
                BookID = 30,
                BookName = "Clean Code",
                BookCondition = 1,
                InLibrary = true,
            });

            bookManagerMock.Setup(m =>
            m.RemoveBook(It.IsAny<int>()));

            var successfull = scrapListAPI.ClearScrapList(30);
            Assert.IsTrue(successfull);
            bookManagerMock.Verify(
            m => m.RemoveBook(30),
                Times.Once());
        }
    }
}
