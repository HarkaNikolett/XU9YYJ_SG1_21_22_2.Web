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
    public class SupplierLogicTests
    {
        [Test]
        public void CreateTest()
        {
            //Arrange
            var supplierRepo = new Mock<ISupplierRepository>();

            var logic = new SupplierLogic(supplierRepo.Object, null, null);
            var suppliertest = new Supplier() { Id = 1, Address = "", Name = "Company", VATNumber = 2355 };


            //Act
            //Assert
            var exception = Assert.Throws(typeof(InvalidOperationException), () => logic.Create(suppliertest));

            Assert.That(exception.Message, Is.EqualTo("Incorrect object data."));
        }
        [TestCaseSource(nameof(CreateData))]
        public void CreateTest(List<Supplier> suppliers, Supplier item, List<Supplier>expected)
        {
            // Arrange
            var supplierRepo = new Mock<ISupplierRepository>();
            supplierRepo.Setup(x => x.ReadAll()).Returns(suppliers.AsQueryable());
            var logic = new SupplierLogic(supplierRepo.Object, null, null);

            //Act
            var result = logic.Create(item);
            //Assert
            
            Assert.That(suppliers.Count(), Is.EqualTo(expected.Count() - 1));
        }
        [TestCaseSource(nameof(TotalValueofOrdersBySupplierData))]
        public void TotalValueofOrdersBySupplierTest(List<Order> orders, List<Supplier> suppliers, List<TotalValue> expected)
        {
            //Arrange

            var orderRepo = new Mock<IOrderRepository>();
            var supplierRepo = new Mock<ISupplierRepository>();
            supplierRepo.Setup(x => x.ReadAll()).Returns(suppliers.AsQueryable());
            orderRepo.Setup(x => x.ReadAll()).Returns(orders.AsQueryable());
            var logic = new SupplierLogic(supplierRepo.Object, null, orderRepo.Object);
            //Act
            var result = logic.TotalValueofOrdersBySupplier();
            //Assert

        }

        [TestCaseSource(nameof(QtyOfItemsBySupplierData))]
        public void QtyOfItemsBySupplierTest(List<Item> items, List<Supplier> suppliers, List<QtyOfItems> expected)
        {
            //Arrange
            
            var itemRepo = new Mock<IItemRepository>();
            var supplierRepo = new Mock<ISupplierRepository>();
            supplierRepo.Setup(x => x.ReadAll()).Returns(suppliers.AsQueryable());
            itemRepo.Setup(x => x.ReadAll()).Returns(items.AsQueryable());

            var logic = new SupplierLogic(supplierRepo.Object, itemRepo.Object, null);
            //Act
            var result = logic.QtyOfItemsBySupplier();
            //Assert
            Assert.That(result, Is.EquivalentTo(expected));

        }
        #region Utils
        static List<TestCaseData> CreateData()
        {
            var result = new List<TestCaseData>();
            var supplier = new Supplier() { Id = 1, Address = "dd", Name = "Company", VATNumber = 2355 };
            List<Supplier> expected = new List<Supplier>() { supplier };
            result.Add(new TestCaseData(
                new List<Supplier>(), supplier, expected
                ));
            return result;
        }
        static List<TestCaseData> TotalValueofOrdersBySupplierData()
        {
            //1 supplier 2 order
            var result = new List<TestCaseData>();
            var order1 = new Order() { Currency = "EUR", Date = new DateTime(2021, 11, 04), Id = 1, ItemId = 1, Note = "", Quantity = 1, UnitPrice = 5, SupplierName = "Company" };
            var order2 = new Order() { Currency = "EUR", Date = new DateTime(2021, 11, 04), Id = 2, ItemId = 1, Note = "", Quantity = 2, UnitPrice = 10, SupplierName = "Company" };
            var order3 = new Order() { Currency = "EUR", Date = new DateTime(2021, 11, 04), Id = 3, ItemId = 4, Note = "", Quantity = 2, UnitPrice = 15, SupplierName = "AnotherCompany" };
            var supplier = new Supplier() { Id = 1, Address = "dd", Name = "Company", VATNumber = 2355 };
            List<Order> orders = new List<Order>() { order1, order2, order3 };
            List<Supplier> suppliers = new List<Supplier>() { supplier };
            result.Add(new TestCaseData(
                orders,
               suppliers,
                new List<TotalValue>()
                {
                    new TotalValue() { SupplierName="Company", Value=15}
                }
                ));
            return result;

            // 1 supplier 0 order
            var supplier2 = new Supplier() { Id = 1, Address = "dd", Name = "TheCompany", VATNumber = 2355 };
            List<Supplier> suppliers2 = new List<Supplier>() { supplier2 };
            result.Add(new TestCaseData(
                orders,
               suppliers2,
                new List<TotalValue>()
                {
                    new TotalValue() { SupplierName="TheCompany", Value=0}
                }
                ));
            return result;
        }
        static List<TestCaseData> QtyOfItemsBySupplierData()
        {
            // 1 supplier  3 items
            var result = new List<TestCaseData>();
            var item1 = new Item() { Name = "nev", SupplierId = 1, UnitOfMeasure = "pc" };
            var item2 = new Item() { Name = "nev", SupplierId = 1, UnitOfMeasure = "pc" };
            var item3 = new Item() { Name = "nev", SupplierId = 1, UnitOfMeasure = "pc" };
            var item4 = new Item() { Name = "nev", SupplierId = 3, UnitOfMeasure = "pc" };
            List<Item> items = new List<Item>() { item1, item2, item3, item4 };
            var supplier = new Supplier() { Id = 1, Address = "dd", Name = "a cég", VATNumber = 2355 };
            List<Supplier> suppliers = new List<Supplier>() { supplier };
           
            
            result.Add(new TestCaseData(
                items,
               suppliers,
                new List<QtyOfItems>()
                {
                    new QtyOfItems() { SupplierId = 1, Qty = 3}
                }
                ));
            return result;

            // 1 supplier 0 items
            var supplier2 = new Supplier() { Id = 2, Address = "dd", Name = "a cég", VATNumber = 2355 };
            List<Supplier> suppliers2 = new List<Supplier>() { supplier2 };
            result.Add(new TestCaseData(
                items,
               suppliers2,
                new List<QtyOfItems>()
                {
                    new QtyOfItems() { SupplierId = 2, Qty = 0}
                }
                ));
            return result;
        }
        #endregion

    }
}
