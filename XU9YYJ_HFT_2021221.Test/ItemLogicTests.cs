using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

using XU9YYJ_HFT_2021221.Logic.Services;
using XU9YYJ_HFT_2021221.Models.Entities;
using XU9YYJ_HFT_2021221.Repository.Interfaces;
namespace XU9YYJ_HFT_2021221.Test
{
    [TestFixture]
    public class ItemLogicTests
    {
        [Test]
        public void CreateTest()
        {
            //Arrange
            var itemRepo = new Mock<IItemRepository>();
           
            var logic = new ItemLogic(itemRepo.Object, null);
            var itemtest = new Item() { Id = 1, Name = "", SupplierId = 3, UnitOfMeasure="pc" };

            //Act
            //Assert
            var exception = Assert.Throws(typeof(InvalidOperationException), () => logic.Create(itemtest));

            Assert.That(exception.Message, Is.EqualTo("Incorrect object data."));

        }
        [TestCaseSource(nameof(CreateData))]
        public void CreateTest(List<Item> items, Item item, List<Item> expected)
        {
            //Arrange
            var itemRepo = new Mock<IItemRepository>();
            itemRepo.Setup(x => x.ReadAll()).Returns(items.AsQueryable());
            var logic = new ItemLogic(itemRepo.Object, null);

            //Act
            var result = logic.Create(item);

            //Assert
            //Assert.That(result, Is.Not.Null);
            Assert.That(items.Count(), Is.EqualTo(expected.Count()-1));
            



        }
        [TestCaseSource(nameof(ItemListOfSupplierData))]
        public void ItemListOfSupplierTest(List<Item> items, int supplierid, List<Item> expected)
        {
            //Arrange
            var itemRepo = new Mock<IItemRepository>();
            itemRepo.Setup(x => x.ReadAll()).Returns(items.AsQueryable());
            var logic = new ItemLogic(itemRepo.Object, null);

            //Act
            var result=logic.ItemListOfSupplier(supplierid);

            //Assert
            Assert.That(result, Is.EquivalentTo(expected));

        }
        #region Utils
        static List<TestCaseData> CreateData()
        {
            //empty table + 1 item
            var result = new List<TestCaseData>();
            var item1 = new Item() { Name = "aaa", SupplierId = 3, UnitOfMeasure="pc" };
            List<Item> expected = new List<Item>() { item1 };
            result.Add(new TestCaseData(
                new List<Item>(), item1, expected
                ));
            return result;

           

        }

        static List<TestCaseData> ItemListOfSupplierData()
        {
            var result = new List<TestCaseData>();


            // 3 items by 3 suppliers in the list
            var item1 = new Item() { Id = 1, Name = "aaa", SupplierId = 3 };
            var item2 = new Item() { Id = 2, Name = "bbb", SupplierId = 2 };
            var item3 = new Item() { Id = 3, Name = "ccc", SupplierId = 1 };
            List<Item> exp = new List<Item>() { item3 };
            result.Add(new TestCaseData(

            new List<Item>() { item1, item2, item3 }, 1, exp));
            return result;

            // 3 items by 1 supplier in the list
            var item4 = new Item() { Id = 1, Name = "aaa", SupplierId = 1 };
            var item5 = new Item() { Id = 2, Name = "bbb", SupplierId = 1 };
            var item6 = new Item() { Id = 3, Name = "ccc", SupplierId = 1 };
            List<Item> exp2 = new List<Item>() { item4, item5, item6 };
            result.Add(new TestCaseData(

            new List<Item>() { item4, item5, item6 }, 1, exp2));
            return result;



        }


        #endregion


    }
}
