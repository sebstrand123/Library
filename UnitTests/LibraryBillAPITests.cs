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
    public class LibraryBillAPITests
    {
        [TestMethod]
        public void TestCreateActiveBill()
        {
            var libraryBillManagerMock = new Mock<ILibraryBillManager>();
            var costumerManagerMock = new Mock<ICostumerManager>();

            libraryBillManagerMock.Setup(m =>
            m.GetActiveBill(It.IsAny<int>()))
            .Returns((LibraryBill)null);

            libraryBillManagerMock.Setup(m =>
            m.CreateActiveBill(It.IsAny<int>(), It.IsAny<decimal>(), It.IsAny<decimal>()));

            var libraryBillAPI = new LibraryBillAPI(costumerManagerMock.Object, libraryBillManagerMock.Object);
            var successfull = libraryBillAPI.CreateActiveBill(1, 1.1m, 1.1m);
            Assert.IsTrue(successfull);
            libraryBillManagerMock.Verify(
            m => m.CreateActiveBill(1, 1.1m, 1.1m),
            Times.Once());
        }
        [TestMethod]
        public void TestCreateExistingActiveBill()
        {
            var libraryBillManagerMock = new Mock<ILibraryBillManager>();
            var costumerManagerMock = new Mock<ICostumerManager>();

            libraryBillManagerMock.Setup(m =>
            m.GetActiveBill(It.IsAny<int>()))
            .Returns(new LibraryBill());

            libraryBillManagerMock.Setup(m =>
            m.CreateActiveBill(It.IsAny<int>(), It.IsAny<decimal>(), It.IsAny<decimal>()));

            var libraryBillAPI = new LibraryBillAPI(costumerManagerMock.Object, libraryBillManagerMock.Object);
            var successfull = libraryBillAPI.CreateActiveBill(1, 1.1m, 1.1m);
            Assert.IsFalse(successfull);
            libraryBillManagerMock.Verify(
            m => m.CreateActiveBill(1, 1.1m, 1.1m),
            Times.Never());
        }
    }
}
