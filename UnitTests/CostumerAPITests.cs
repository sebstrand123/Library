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
    public class TestAddCostumer
    {
        [TestMethod]
        public void TestAddCostumers()
        {
            var costumerManagerMock = new Mock<ICostumerManager>();
            var bookManagerMock = new Mock<IBookManager>();

            costumerManagerMock.Setup(m =>
            m.GetCostumerByCostumerName(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<string>()))
            .Returns((Costumer)null);

            costumerManagerMock.Setup(m =>
            m.AddCostumer(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int>()));

            var costumerAPI = new CostumerAPI(costumerManagerMock.Object, bookManagerMock.Object);
            var successfull = costumerAPI.AddCostumer("Jan", "1990-01-01", "Fågelvägen 1", false, false, 0);
            Assert.IsTrue(successfull);
            costumerManagerMock.Verify(
            m => m.AddCostumer("Jan", "1990-01-01", "Fågelvägen 1", false, false, 0),
            Times.Once());
        }

        [TestMethod]
        public void TestAddExistingCostumer()
        {
            var costumerManagerMock = new Mock<ICostumerManager>();
            var bookManagerMock = new Mock<IBookManager>();

            costumerManagerMock.Setup(m =>
            m.GetCostumerByCostumerName(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<string>()))
            .Returns(new Costumer());

            costumerManagerMock.Setup(m =>
            m.AddCostumer(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int>()));

            var costumerAPI = new CostumerAPI(costumerManagerMock.Object, bookManagerMock.Object);
            var successfull = costumerAPI.AddCostumer("Jan", "1990-01-01", "Fågelvägeen 1", false, false, 0);
            Assert.IsFalse(successfull);
            costumerManagerMock.Verify(
            m => m.AddCostumer("Jan", "1990-01-01", "Fågelvägen 1", false, false, 0),
            Times.Never());
        }
    }
    [TestClass]
    public class TestRemoveCostumer
    {
        [TestMethod]
        public void TestCostumerRemove()
        {
            var costumerManagerMock = new Mock<ICostumerManager>();
            var bookManagerMock = new Mock<IBookManager>();

            costumerManagerMock.Setup(m =>
            m.GetCostumerByCostumerName(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<string>()))
            .Returns(new Costumer
            {
                CostumerID = 1,
                IsInDebt = false,
                HasBorrowedBook = false
            });

            costumerManagerMock.Setup(m =>
            m.RemoveCostumer(It.IsAny<int>()));

            var costumerAPI = new CostumerAPI(costumerManagerMock.Object, bookManagerMock.Object);
            var successfull = costumerAPI.RemoveCostumer("Jan", "1990-01-01", false, false, 0, "Fågelvägen 1");
            Assert.AreEqual(RemoveCostumerCodes.Ok, successfull);
            costumerManagerMock.Verify(
                m => m.RemoveCostumer(It.IsAny<int>()),
                Times.Once());
        }
        [TestMethod]
        public void TestRemoveNoneExistingCostumer()
        {
            var costumerManagerMock = new Mock<ICostumerManager>();
            var bookManagerMock = new Mock<IBookManager>();

            costumerManagerMock.Setup(m =>
            m.GetCostumerByCostumerName(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<string>()))
            .Returns((Costumer)null);

            costumerManagerMock.Setup(m =>
            m.RemoveCostumer(It.IsAny<int>()));

            var costumerAPI = new CostumerAPI(costumerManagerMock.Object, bookManagerMock.Object);
            var successfull = costumerAPI.RemoveCostumer("Jan", "1990-01-01", false, false, 0, "Fågelvägen 1");
            Assert.AreEqual(RemoveCostumerCodes.NoSuchCostumer, successfull);
            costumerManagerMock.Verify(
                m => m.RemoveCostumer(It.IsAny<int>()),
                Times.Never());
        }
        [TestMethod]
        public void TestRemoveCostumerWithDebt()
        {
            var costumerManagerMock = new Mock<ICostumerManager>();
            var bookManagerMock = new Mock<IBookManager>();

            costumerManagerMock.Setup(m =>
            m.GetCostumerByCostumerName(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<string>()))
            .Returns(new Costumer
            {
                CostumerID = 1,
                IsInDebt = true,
                HasBorrowedBook = false
            });

            costumerManagerMock.Setup(m =>
            m.RemoveCostumer(It.IsAny<int>()));

            var costumerAPI = new CostumerAPI(costumerManagerMock.Object, bookManagerMock.Object);
            var successfull = costumerAPI.RemoveCostumer("Jan", "1990-01-01", true, false, 0, "Fågelvägen 1");
            Assert.AreEqual(RemoveCostumerCodes.CostumerOwesLibraryMoney, successfull);
            costumerManagerMock.Verify(
                m => m.RemoveCostumer(It.IsAny<int>()),
                Times.Never());
        }
        [TestMethod]
        public void TestRemoveCostumerWithBorrowedBooks()
        {
            var costumerManagerMock = new Mock<ICostumerManager>();
            var bookManagerMock = new Mock<IBookManager>();

            costumerManagerMock.Setup(m =>
            m.GetCostumerByCostumerName(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<string>()))
            .Returns(new Costumer
            {
                CostumerID = 1,
                IsInDebt = false,
                HasBorrowedBook = true
            });

            costumerManagerMock.Setup(m =>
            m.RemoveCostumer(It.IsAny<int>()));

            var costumerAPI = new CostumerAPI(costumerManagerMock.Object, bookManagerMock.Object);
            var successfull = costumerAPI.RemoveCostumer("Jan", "1990-01-01", false, true, 0, "Fågelvägen 1");
            Assert.AreEqual(RemoveCostumerCodes.CostumerHasBorrowedBooks, successfull);
            costumerManagerMock.Verify(
                m => m.RemoveCostumer(It.IsAny<int>()),
                Times.Never());
        }
    }
    [TestClass]
    public class TestCostumerBorrowBook
    {
        [TestMethod]
        public void TestBorrowNoneExistingBook()
        {
            var costumerManagerMock = new Mock<ICostumerManager>();
            var bookManagerMock = new Mock<IBookManager>();

            var costumerAPI = new CostumerAPI(costumerManagerMock.Object, bookManagerMock.Object);


            costumerManagerMock.Setup(m =>
            m.GetCostumerByCostumerName(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<string>()))
            .Returns(new Costumer());

            var successfull = costumerAPI.SetCostumerToBook(0, 0, "Jan", "1990-01-01", false, false, 0, "Clean Code", false, 5, "Fågelvägen 1");
            Assert.AreEqual(BorrowBookCodes.NoBooksToBorrow, successfull);
            costumerManagerMock.Verify(
                m => m.SetCostumerToBook(0, 0),
                Times.Never());
        }
        [TestMethod]
        public void TestBorrowBookOk()
        {
            var costumerManagerMock = new Mock<ICostumerManager>();
            var bookManagerMock = new Mock<IBookManager>();

            var costumerAPI = new CostumerAPI(costumerManagerMock.Object, bookManagerMock.Object);
            bookManagerMock.Setup(m =>
            m.GetBookByName(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>()))
            .Returns(new Book
            {
                BookID = 20,
                BookName = "Clean Code",
                InLibrary = true,
                BookCondition = 4,
            });

            costumerManagerMock.Setup(m =>
            m.GetCostumerByCostumerName(It.IsAny<string>(), It.IsAny<string>(), It.Is<bool>(b => b == false), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<string>()))
            .Returns(new Costumer
            { 
                CostumerName = "Jan",
                CostumerID = 10,
                AmountOfBooks = 0,
                IsInDebt = false
            });

            var successfull = costumerAPI.SetCostumerToBook(0, 0, "Jan", "1990-01-01", false, false, 0, "Clean Code", false, 5, "Fågelvägen 1");
            Assert.AreEqual(BorrowBookCodes.Ok, successfull);
            costumerManagerMock.Verify(
                m => m.SetCostumerToBook(0, 0),
                Times.Once());
        }
        [TestMethod]
        public void TestBorrowBookWithNoCostumer()
        {
            var costumerManagerMock = new Mock<ICostumerManager>();
            var bookManagerMock = new Mock<IBookManager>();

            var costumerAPI = new CostumerAPI(costumerManagerMock.Object, bookManagerMock.Object);
            bookManagerMock.Setup(m =>
            m.GetBookByName(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>()))
            .Returns(new Book
            {
                BookID = 20,
                BookName = "Clean Code",
                InLibrary = true,
                BookCondition = 4,
            });

            costumerManagerMock.Setup(m =>
            m.GetCostumerByCostumerName(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<string>()))
            .Returns((Costumer)null);

            var successfull = costumerAPI.SetCostumerToBook(0, 0, "Jan", "1990-01-01", false, false, 0, "Clean Code", false, 5, "Fågelvägen 1");
            Assert.AreEqual(BorrowBookCodes.NoSuchCostumer, successfull);
            costumerManagerMock.Verify(
                m => m.SetCostumerToBook(0, 0),
                Times.Never());
        }
        [TestMethod]
        public void TestBorrowAlredyBorrowedBook()
        {
            var costumerManagerMock = new Mock<ICostumerManager>();
            var bookManagerMock = new Mock<IBookManager>();

            var costumerAPI = new CostumerAPI(costumerManagerMock.Object, bookManagerMock.Object);
            bookManagerMock.Setup(m =>
            m.GetBookByName(It.IsAny<string>(), It.Is<bool>(b => b == false), It.IsAny<int>()))
            .Returns(new Book
            {
                BookID = 20,
                 BookName = "Clean Code",
                InLibrary = false,
                BookCondition = 4,
            });

            costumerManagerMock.Setup(m =>
            m.GetCostumerByCostumerName(It.IsAny<string>(), It.IsAny<string>(), It.Is<bool>(b => b == false), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<string>()))
            .Returns(new Costumer
            {
                CostumerName = "Jan",
                CostumerID = 10,
                AmountOfBooks = 0,
                IsInDebt = false
            });

            var successfull = costumerAPI.SetCostumerToBook(0, 0, "Jan", "1990-01-01", false, false, 0, "Clean Code", false, 5, "Fågelvägen 1");
            Assert.AreEqual(BorrowBookCodes.BookIsAlreadyBorrowed, successfull);
            costumerManagerMock.Verify(
                m => m.SetCostumerToBook(0, 0),
                Times.Never());
        }
        [TestMethod]
        public void TestBorrowBookAndCostumerIsInDebt()
        {
            var costumerManagerMock = new Mock<ICostumerManager>();
            var bookManagerMock = new Mock<IBookManager>();

            var costumerAPI = new CostumerAPI(costumerManagerMock.Object, bookManagerMock.Object);
            bookManagerMock.Setup(m =>
            m.GetBookByName(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>()))
            .Returns(new Book
            {
                BookID = 20,
                BookName = "Clean Code",
                InLibrary = true,
                BookCondition = 4,
            });

            costumerManagerMock.Setup(m =>
            m.GetCostumerByCostumerName(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<string>()))
            .Returns(new Costumer
            {
                CostumerName = "Jan",
                CostumerID = 10,
                AmountOfBooks = 0,
                IsInDebt = true
            });

            var successfull = costumerAPI.SetCostumerToBook(0, 0, "Jan", "1990-01-01", false, false, 0, "Clean Code", false, 5, "Fågelvägen 1");
            Assert.AreEqual(BorrowBookCodes.CostumerIsInDebt, successfull);
            costumerManagerMock.Verify(
                m => m.SetCostumerToBook(0, 0),
                Times.Never());
        }
        [TestMethod]
        public void TestBorrowBookWithAlreadyMaxAmountOfBooks()
        {
            var costumerManagerMock = new Mock<ICostumerManager>();
            var bookManagerMock = new Mock<IBookManager>();

            var costumerAPI = new CostumerAPI(costumerManagerMock.Object, bookManagerMock.Object);
            bookManagerMock.Setup(m =>
            m.GetBookByName(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>()))
            .Returns(new Book
            {
                BookID = 20,
                BookName = "Clean Code",
                InLibrary = true,
                BookCondition = 4,
            });

            costumerManagerMock.Setup(m =>
            m.GetCostumerByCostumerName(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<string>()))
            .Returns(new Costumer
            {
                CostumerName = "Jan",
                CostumerID = 10,
                AmountOfBooks = 5,
                IsInDebt = false
            });

            var successfull = costumerAPI.SetCostumerToBook(0, 0, "Jan", "1990-01-01", false, false, 0, "Clean Code", false, 5, "Fågelvägen 1");
            Assert.AreEqual(BorrowBookCodes.CostumerHaveMaxAmountOfBooks, successfull);
            costumerManagerMock.Verify(
                m => m.SetCostumerToBook(0, 0),
                Times.Never());
        }
    }
    [TestClass]
    public class TestCostumerBirthDate
    {
        [TestMethod]
        public void TestCostumerBirthDates()
        {
            var costumerManagerMock = new Mock<ICostumerManager>();
            var bookManagerMock = new Mock<IBookManager>();

            costumerManagerMock.Setup(m =>
            m.GetCostumerByCostumerName(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<string>()))
            .Returns(new Costumer());

            var costumerAPI = new CostumerAPI(costumerManagerMock.Object, bookManagerMock.Object);
            var expectedBirthDate = costumerAPI.ValidateCostumerBirthDate(1, "19900101", "Jan", false, false, 4, "Fågelvägen 1");
            Assert.IsTrue(expectedBirthDate);
            costumerManagerMock.Verify(
                m => m.ValidateCostumerBirthDate(1 ,"19900101"),
                Times.Once());
        }
        [TestMethod]
        public void TestCostumerBirthDateWithoutCostumer()
        {
            var costumerManagerMock = new Mock<ICostumerManager>();
            var bookManagerMock = new Mock<IBookManager>();

            costumerManagerMock.Setup(m =>
            m.GetCostumerByCostumerName(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<string>()))
            .Returns((Costumer)null);

            var costumerAPI = new CostumerAPI(costumerManagerMock.Object, bookManagerMock.Object);
            var expectedBirthDate = costumerAPI.ValidateCostumerBirthDate(1, "19900101", "Jan", false, false, 4, "Fågelvägen 1");
            Assert.IsFalse(expectedBirthDate);
            costumerManagerMock.Verify(
                m => m.ValidateCostumerBirthDate(0, "19900101"),
                Times.Never());
        }
    }
    [TestClass]
    public class TestReturnBooks
    {
        [TestMethod]
        public void TestReturnBookOk()
        {
            var costumerManagerMock = new Mock<ICostumerManager>();
            var bookManagerMock = new Mock<IBookManager>();

            var costumerAPI = new CostumerAPI(costumerManagerMock.Object, bookManagerMock.Object);

            costumerManagerMock.Setup(m =>
            m.GetCostumerByCostumerName(It.IsAny<string>(), It.IsAny<string>(), It.Is<bool>(b => b == false), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<string>()))
            .Returns(new Costumer
            {
                CostumerName = "Jan",
                CostumerID = 10,
                AmountOfBooks = 1,
                IsInDebt = false
            });

            bookManagerMock.Setup(m =>
            m.GetBookByName(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>()))
            .Returns(new Book
            {
                BookID = 20,
                BookName = "Clean Code",
                InLibrary = false,
                BookCondition = 4,
            });

            var successfull = costumerAPI.ReturnBookToLibrary(0, false, "Clean Code", 5, "Jan", "1990-01-01", false, true, 1, "Fågelvägen 1");
            Assert.AreEqual(ReturnBookCodes.Ok, successfull);
            costumerManagerMock.Verify(
                m => m.ReturnBookToLibrary(0, false),
                Times.Once());
        }
        [TestMethod]
        public void TestReturnBookNoSuchCostumer()
        {
            var costumerManagerMock = new Mock<ICostumerManager>();
            var bookManagerMock = new Mock<IBookManager>();

            var costumerAPI = new CostumerAPI(costumerManagerMock.Object, bookManagerMock.Object);

            costumerManagerMock.Setup(m =>
            m.GetCostumerByCostumerName(It.IsAny<string>(), It.IsAny<string>(), It.Is<bool>(b => b == false), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<string>()))
            .Returns((Costumer)null);

            bookManagerMock.Setup(m =>
            m.GetBookByName(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>()))
            .Returns(new Book
            {
                BookID = 20,
                BookName = "Clean Code",
                InLibrary = false,
                BookCondition = 4,
            });

            var successfull = costumerAPI.ReturnBookToLibrary(0, false, "Clean Code", 5, "Jan", "1990-01-01", false, true, 1, "Fågelvägen 1");
            Assert.AreEqual(ReturnBookCodes.NoSuchCostumer, successfull);
            costumerManagerMock.Verify(
                m => m.ReturnBookToLibrary(0, false),
                Times.Never());
        }
        [TestMethod]
        public void TestReturnBookCostumerHasNoBooks()
        {
            var costumerManagerMock = new Mock<ICostumerManager>();
            var bookManagerMock = new Mock<IBookManager>();

            var costumerAPI = new CostumerAPI(costumerManagerMock.Object, bookManagerMock.Object);

            costumerManagerMock.Setup(m =>
            m.GetCostumerByCostumerName(It.IsAny<string>(), It.IsAny<string>(), It.Is<bool>(b => b == false), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<string>()))
            .Returns(new Costumer
            {
                CostumerName = "Jan",
                CostumerID = 10,
                AmountOfBooks = 0,
                IsInDebt = false
            });

            var successfull = costumerAPI.ReturnBookToLibrary(0, false, "Clean Code", 5, "Jan", "1990-01-01", false, true, 1, "Fågelvägen 1");
            Assert.AreEqual(ReturnBookCodes.CostumerHasNoBooksToReturn, successfull);
            costumerManagerMock.Verify(
                m => m.ReturnBookToLibrary(0, false),
                Times.Never());
        }
    }
    [TestClass]
    public class UpdateBookConditionTests
    {
        [TestMethod]
        public void TestUpdateBook()
        {
            var costumerManagerMock = new Mock<ICostumerManager>();
            var bookManagerMock = new Mock<IBookManager>();

            var costumerAPI = new CostumerAPI(costumerManagerMock.Object, bookManagerMock.Object);

            bookManagerMock.Setup(m =>
            m.GetBookByName(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>()))
            .Returns(new Book
            {
                BookID = 20,
                BookName = "Clean Code",
                InLibrary = false,
                BookCondition = 5,
            });

            costumerManagerMock.Setup(m =>
            m.ReturnBookToLibrary(It.IsAny<int>(), It.IsAny<bool>()));

            var successfull = costumerAPI.UpdateBookCondition(0, 0, "Clean Code", true);
            Assert.IsTrue(successfull);
            bookManagerMock.Verify(
                m => m.UpdateBookCondition(0, 0),
                Times.Once());
        }
        [TestMethod]
        public void TestUpdateNoneExistingBook()
        {
            var costumerManagerMock = new Mock<ICostumerManager>();
            var bookManagerMock = new Mock<IBookManager>();

            var costumerAPI = new CostumerAPI(costumerManagerMock.Object, bookManagerMock.Object);

            costumerManagerMock.Setup(m =>
            m.ReturnBookToLibrary(It.IsAny<int>(), It.IsAny<bool>()));

            var successfull = costumerAPI.UpdateBookCondition(0, 0, "Clean Code", true);
            Assert.IsFalse(successfull);
            bookManagerMock.Verify(
                m => m.UpdateBookCondition(0, 0),
                Times.Never());
        }
    }
}
