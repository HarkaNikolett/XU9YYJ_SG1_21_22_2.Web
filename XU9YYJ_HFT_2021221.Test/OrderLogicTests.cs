using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

using XU9YYJ_HFT_2021221.Logic.Services;
using XU9YYJ_HFT_2021221.Models.Entities;
using XU9YYJ_HFT_2021221.Models.Models;
using XU9YYJ_HFT_2021221.Repository.Interfaces;

namespace XU9YYJ_HFT_2021221.Test
{
    [TestFixture]
    public class OrderLogicTests
    {
        [Test]
        public void CreateTest()
        {
            //Arrange
            var orderRepo = new Mock<IOrderRepository>();
           
            var logic = new OrderLogic(orderRepo.Object, null);
            var ordertest = new Order() { Currency = "", Date = new DateTime(2021, 11, 04), Id = 1, ItemId = 1, Note = "", Quantity = 3, UnitPrice = 34, SupplierName = "jjf" };
            //Act
            //Assert

            var exception = Assert.Throws(typeof(InvalidOperationException), () => logic.Create(ordertest));

            Assert.That(exception.Message, Is.EqualTo("Incorrect object data."));
            
           
        }
        [TestCaseSource(nameof(CreateTestData))]
        public void CreateTest(List<Order> orders, Order order, List<Order> expected)
        {
            
            //Arrange
            var orderRepo = new Mock<IOrderRepository>();
            orderRepo.Setup(x => x.ReadAll()).Returns(orders.AsQueryable());
            var logic = new OrderLogic(orderRepo.Object, null);

            //Act
            var result = logic.Create(order);

            //Assert
            //Assert.That(result, Is.Not.Null);
            Assert.That(orders.Count(), Is.EqualTo(expected.Count() - 1));
        }
        [TestCaseSource(nameof(POListOfItemData))]
        public void POListOfItemTest(List<Order> orders, int id, List<Order> expected)
        {
            // Arrange
            var orderRepo = new Mock<IOrderRepository>();
            orderRepo.Setup(x => x.ReadAll()).Returns(orders.AsQueryable());
            var logic = new OrderLogic(orderRepo.Object, null);

            //Act
            var result = logic.POListOfItem(id);

            //Assert
            Assert.That(result, Is.EquivalentTo(expected));
            Assert.That(result.Count, Is.EqualTo(expected.Count()));

        }
        [TestCaseSource(nameof(GetTop3OrderByValueData))]
        public void Top3OrderByValueTest(List<Order> orders, List<Order> expected)
        {
            //Arrange
            var orderRepo = new Mock<IOrderRepository>();
            orderRepo.Setup(x => x.ReadAll()).Returns(orders.AsQueryable());
            var logic = new OrderLogic(orderRepo.Object, null);
            //Act

            var result = logic.Top3OrderByValue();

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EquivalentTo(expected));
            Assert.That(result.Count, Is.EqualTo(expected.Count));
        }
        [TestCaseSource(nameof(GetTotalQtyOfItemData))]
        public void TotalQtyOfItemTest(List<Order> orders, int itemid, int qty)
        {
            //Arrange
            var orderRepo = new Mock<IOrderRepository>();

            orderRepo.Setup(x => x.ReadAll()).Returns(orders.AsQueryable());
            var logic = new OrderLogic(orderRepo.Object, null);

            //Act

            var result = logic.TotalQtyOfItem(itemid);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(qty));


        }
        [TestCaseSource(nameof(GetAveragePOValueBySupplierData))]
        public void AveragePOValueBySupplierTest(List<Order> orders, List<Supplier> suppliers, List<AveragePOValue> expected)
        {
            //Arrange
            var orderRepo = new Mock<IOrderRepository>();
            var supplierRepo = new Mock<ISupplierRepository>();

            orderRepo.Setup(x => x.ReadAll()).Returns(orders.AsQueryable());
            supplierRepo.Setup(x => x.ReadAll()).Returns(suppliers.AsQueryable());

            var logic = new OrderLogic(orderRepo.Object, supplierRepo.Object);

            //Act
            var result = logic.AvaragePOValueBySupplier();

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EquivalentTo(expected));

        }
        #region Utils
        static List<TestCaseData> POListOfItemData()
        {
            var result = new List<TestCaseData>();

            //no orders in the list
            //List<Order> expected_ = new List<Order>(null);
            //result.Add(new TestCaseData(
            //    new List<Order>(), 1, expected_
            //    ));
            //return result;

            // 3 orders in the list
            var order1 = new Order() { Currency = "EUR", Date = new DateTime(2021, 11, 04), Id = 1, ItemId = 1, Note = "", Quantity = 3, UnitPrice = 34, SupplierName = "jjf" };
            var order2 = new Order() { Currency = "EUR", Date = new DateTime(2021, 11, 04), Id = 2, ItemId = 1, Note = "", Quantity = 7, UnitPrice = 34, SupplierName = "jjf" };
            var order3 = new Order() { Currency = "EUR", Date = new DateTime(2021, 11, 04), Id = 3, ItemId = 4, Note = "", Quantity = 9, UnitPrice = 34, SupplierName = "534" };
            List<Order> expected = new List<Order>() { order1, order2, };
            result.Add(new TestCaseData(

            new List<Order>() { order1, order2, order3 }, 1, expected));
            return result;

            // 3 orders in the list but no result
            var order4 = new Order() { Currency = "EUR", Date = new DateTime(2021, 11, 04), Id = 1, ItemId = 2, Note = "", Quantity = 3, UnitPrice = 34, SupplierName = "jjf" };
            var order5 = new Order() { Currency = "EUR", Date = new DateTime(2021, 11, 04), Id = 2, ItemId = 3, Note = "", Quantity = 7, UnitPrice = 34, SupplierName = "jjf" };
            var order6 = new Order() { Currency = "EUR", Date = new DateTime(2021, 11, 04), Id = 3, ItemId = 4, Note = "", Quantity = 9, UnitPrice = 34, SupplierName = "534" };
            List<Order> expected2 = new List<Order>(null);
            result.Add(new TestCaseData(

            new List<Order>() { order4, order5, order6 }, 1, expected2));
            return result;


        }
        static List<TestCaseData> CreateTestData()
        {
            //empty table +1 order
            var result = new List<TestCaseData>();
            var order1 = new Order() { Currency = "EUR", Date = new DateTime(2021, 11, 04), Id = 1, ItemId = 1, Note = "", Quantity = 3, UnitPrice = 34, SupplierName = "jjf" };
            List<Order> expected = new List<Order>() { order1 };
            result.Add(new TestCaseData(
                new List<Order>(), order1, expected));
            return result;

            //empty table +1 empty order
            var result2 = new List<TestCaseData>();
            var order2 = new Order(); //{ Currency = "EUR", Date = new DateTime(2021, 11, 04), Id = 1, ItemId = 1, Note = "", Quantity = 3, UnitPrice = 34, SupplierName = "jjf" };
            List<Order> expected2 = new List<Order>();
            result.Add(new TestCaseData(
                new List<Order>(), order2, expected2));
            return result2;
        }
        static List<TestCaseData> GetTop3OrderByValueData()
        {
            var result = new List<TestCaseData>();
            
            // 5 orders in the list
            var order1 = new Order() { Currency = "EUR", Date = new DateTime(2021, 11, 04), Id = 1, ItemId = 1, Note = "", Quantity = 3, UnitPrice = 34, SupplierName = "jjf" };
            var order2 = new Order() { Currency = "EUR", Date = new DateTime(2021, 11, 04), Id = 2, ItemId = 1, Note = "", Quantity = 7, UnitPrice = 34, SupplierName = "jjf" };
            var order3 = new Order() { Currency = "EUR", Date = new DateTime(2021, 11, 04), Id = 3, ItemId = 4, Note = "", Quantity = 9, UnitPrice = 34, SupplierName = "534" };
            var order4 = new Order() { Currency = "EUR", Date = new DateTime(2021, 11, 04), Id = 1, ItemId = 3, Note = "", Quantity = 22, UnitPrice = 34, SupplierName = "534" };
            var order5 = new Order() { Currency = "EUR", Date = new DateTime(2021, 11, 04), Id = 2, ItemId = 2, Note = "", Quantity = 40, UnitPrice = 34, SupplierName = "kds" };

            List<Order> expected = new List<Order>() { order3, order4, order5 };
            result.Add(new TestCaseData(

            new List<Order>() { order1, order2, order3, order4, order5 }, expected));
            return result;
        }

        static List<TestCaseData> GetTotalQtyOfItemData()
        {
            var result = new List<TestCaseData>();

            // no orders in the list

            result.Add(new TestCaseData(
                new List<Order>(), 1, 0
                ));

            return result;

            // 3 orders in the list
            var order1 = new Order() { Currency = "EUR", Date = new DateTime(2021, 11, 04), Id = 1, ItemId = 1, Note = "", Quantity = 3, UnitPrice = 34, SupplierName = "jjf" };
            var order2 = new Order() { Currency = "EUR", Date = new DateTime(2021, 11, 04), Id = 2, ItemId = 1, Note = "", Quantity = 7, UnitPrice = 34, SupplierName = "jjf" };
            var order3 = new Order() { Currency = "EUR", Date = new DateTime(2021, 11, 04), Id = 3, ItemId = 4, Note = "", Quantity = 3, UnitPrice = 34, SupplierName = "jjf" };
            result.Add(new TestCaseData(
                new List<Order>() { order1, order2, order3 }, 1, order1.Quantity + order2.Quantity));
            return result;
        }
        static List<TestCaseData> GetAveragePOValueBySupplierData()
        {
            var result = new List<TestCaseData>();

            // no supplier, no order
            result.Add(new TestCaseData(
                new List<Order>(),
                new List<Supplier>(),
                new List<AveragePOValue>()
                ));

            // one supplier, one order
            var order1 = new Order() { Currency = "EUR", Date = new DateTime(2021, 11, 04), Id = 1, ItemId = 1, Note = "", Quantity = 3, UnitPrice = 34, SupplierName = "Company", SupplierId = 1 };
            var order2 = new Order() { Currency = "EUR", Date = new DateTime(2021, 11, 04), Id = 2, ItemId = 1, Note = "", Quantity = 7, UnitPrice = 34, SupplierName = "Company", SupplierId = 1 };
            var order3 = new Order() { Currency = "EUR", Date = new DateTime(2021, 11, 04), Id = 3, ItemId = 4, Note = "", Quantity = 3, UnitPrice = 34, SupplierName = "AnotherCompany", SupplierId = 2 };
            var supplier = new Supplier() { Name = "Company", Address = "Fiction", Id = 1, VATNumber = 1234 };
            result.Add(new TestCaseData(
                new List<Order>() { order1},
                new List<Supplier>(){supplier},
                new List<AveragePOValue>()
                { new AveragePOValue() { SupplierName = "Company", AverageValue = order1.Quantity*order1.UnitPrice}}));
            // one supplier, another order
            result.Add(new TestCaseData(
                new List<Order>()
                {order3},
                new List<Supplier>()
                {supplier},
                new List<AveragePOValue>()
                {new AveragePOValue() { SupplierName = "Company", AverageValue = 0}}));
            // one supplier, multiple orders
            
            result.Add(new TestCaseData(
                new List<Order>()
                {
                    order1, order2, order3
                },
                new List<Supplier>()
                {supplier},
                new List<AveragePOValue>()
                {
                    new AveragePOValue() { SupplierName = "Company", AverageValue =((order1.Quantity*order1.UnitPrice + order2.Quantity * order2.UnitPrice)/2.0)}
                }
                ));
            return result;
        }

        #endregion

    }
}
