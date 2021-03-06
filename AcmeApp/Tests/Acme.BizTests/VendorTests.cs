﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acme.Biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.Common;

namespace Acme.Biz.Tests
{
    [TestClass()]
    public class VendorTests
    {
        [TestMethod()]
        public void SendWelcomeEmail_ValidCompany_Success()
        {
            // Arrange
            var vendor = new Vendor();
            vendor.CompanyName = "ABC Corp";
            var expected = "Message sent: Hello ABC Corp";

            // Act
            var actual = vendor.SendWelcomeEmail("Test Message");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SendWelcomeEmail_EmptyCompany_Success()
        {
            // Arrange
            var vendor = new Vendor();
            vendor.CompanyName = "";
            var expected = "Message sent: Hello";

            // Act
            var actual = vendor.SendWelcomeEmail("Test Message");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SendWelcomeEmail_NullCompany_Success()
        {
            // Arrange
            var vendor = new Vendor();
            vendor.CompanyName = null;
            var expected = "Message sent: Hello";

            // Act
            var actual = vendor.SendWelcomeEmail("Test Message");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void PlaceOrderTest()
        {
            //Arrange
            var vendor = new Vendor();
            var product = new Product(1, "Saw", "");
            var expected = new OperationResult(true,
                "Order from Acme, Inc\r\nProduct: Tools-0001\r\nQuantity: 12" +
                "\r\nInstructions: Standard Delivery");

            //Act
            var actual = vendor.PlaceOrder(product, 12);

            //Assert
            Assert.AreEqual(actual.Success, expected.Success);
            Assert.AreEqual(actual.Message, expected.Message);
        }

        [TestMethod()]
        public void PlaceOrderTest_3Parameters()
        {
            //Arrange
            var vendor = new Vendor();
            var product = new Product(1, "Saw", "");
            var expected = new OperationResult(true,
                "Order from Acme, Inc\r\nProduct: Tools-0001\r\nQuantity: 12" +
                "\r\nDeliver By: 07/11/2017" +
                "\r\nInstructions: Standard Delivery");

            //Act
            var actual = vendor.PlaceOrder(product, 12,
                new DateTimeOffset(2017, 11, 07, 0, 0, 0, new TimeSpan(0, 0, 0)));

            //Assert
            Assert.AreEqual(actual.Success, expected.Success);
            Assert.AreEqual(actual.Message, expected.Message);
        }

        [TestMethod()]
        public void PlaceOrderTest_NoDeliveryDate()
        {
            //Arrange
            var vendor = new Vendor();
            var product = new Product(1, "Saw", "");
            var expected = new OperationResult(true,
                "Order from Acme, Inc\r\nProduct: Tools-0001\r\nQuantity: 12" +
                "\r\nInstructions: Deliver to Suite 42");

            //Act
            var actual = vendor.PlaceOrder(product, 12, instructions: "Deliver to Suite 42");

            //Assert
            Assert.AreEqual(actual.Success, expected.Success);
            Assert.AreEqual(actual.Message, expected.Message);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PlaceOrder_NullProduct_Exception()
        {
            //Arrange
            var vendor = new Vendor();

            //Act
            var actual = vendor.PlaceOrder(null, 12);

            //Assert
        }

        [TestMethod()]
        public void PlaceOrderTest_WithAddress()
        {
            //Arrange
            var vendor = new Vendor();
            var product = new Product(1, "Saw", "");
            var expected = new OperationResult(true, "Test with Address");

            //Act
            var actual = vendor.PlaceOrder(product, 12, Vendor.IncludeAddress.Yes, Vendor.SendCopy.No);

            //Assert
            Assert.AreEqual(actual.Success, expected.Success);
            Assert.AreEqual(actual.Message, expected.Message);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            //Arrange
            var vendor = new Vendor();
            vendor.VendorId = 1;
            vendor.CompanyName = "ABC Corp";

            var expected = "Vendor: ABC Corp";

            //Act
            var actual = vendor.ToString();

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}