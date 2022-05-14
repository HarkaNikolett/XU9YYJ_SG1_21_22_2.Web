using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using XU9YYJ_HFT_2021221.Logic.Interfaces;
using XU9YYJ_HFT_2021221.Models.DTOs;
using XU9YYJ_HFT_2021221.Models.Entities;
using XU9YYJ_HFT_2021221.Models.Models;


namespace XU9YYJ_HFT_2021221.Endpoint.Controllers
{
    //add controller -> api -> empty
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        readonly IOrderLogic orderLogic;
        readonly ISupplierLogic supplierLogic;
        readonly IItemLogic itemLogic;
        public OrderController(IOrderLogic orderLogic, IItemLogic itemLogic, ISupplierLogic supplierLogic)
        {
            this.orderLogic = orderLogic;
            this.itemLogic = itemLogic;
            this.supplierLogic = supplierLogic;
        }
        // GET: api/Order/GetAll
        [HttpGet]
        [ActionName("GetAll")]
        public IEnumerable<Order> Get()
        {
            return orderLogic.ReadAll();
        }

        // GET api/Order/Get/5
        [HttpGet("{id}")]
        public Order Get(int id)
        {
            return orderLogic.Read(id);
        }

        // POST api/Order/Create
        [HttpPost]
        [ActionName("Create")]
        public ApiResult Post(OrderDTO order)
        {
            var result = new ApiResult(true);
            try
            {
                orderLogic.Create(new Order()
                {
                    Id = order.Id,
                    ItemId = order.ItemId,
                    Quantity = order.Quantity,
                    UnitPrice = order.UnitPrice,
                    Currency = order.Currency,
                    Note = order.Note,
                    Date = order.Date,
                    SupplierName = order.SupplierName,
                    SupplierId = order.SupplierId
                });
            }
            catch (Exception ex)
            {

                result.IsSuccess = false;
                result.Messages = new List<string>() { ex.Message };
            }
            return result;

        }

        // PUT api/Order/Update
        [HttpPut]
        [ActionName("Update")]
        public ApiResult Put(OrderDTO order)
        {
            var result = new ApiResult(true);
            try
            {
                orderLogic.Update(new Order()
                {
                    Id = order.Id,
                    ItemId = order.ItemId,
                    Quantity = order.Quantity,
                    UnitPrice = order.UnitPrice,
                    Currency = order.Currency,
                    Note = order.Note,
                    Date = order.Date,
                    SupplierName = order.SupplierName,
                    SupplierId = order.SupplierId

                });
            }
            catch (Exception ex)
            {

                result.IsSuccess = false;
                result.Messages = new List<string>() { ex.Message };
            }
            return result;
        }

        // DELETE api/Order/Delete/5
        [HttpDelete("{id}")]

        public ApiResult Delete(int id)
        {
            var result = new ApiResult(true);
            try
            {
                orderLogic.Delete(id);                
            }
            catch (Exception ex)
            {

                result.IsSuccess = false;
                result.Messages = new List<string>() { ex.Message };
            }
            return result;
        }
        //GET: api/Order/AvaragePOValueBySupplier
        [HttpGet]

        public IEnumerable<AveragePOValue> AvaragePOValueBySupplier()
        {
            return orderLogic.AvaragePOValueBySupplier();
        }
        [HttpGet]
        [ActionName("GetAll/Top3OrderByValue")]
        public IList<Order> Top3OrderByValue()
        { return orderLogic.Top3OrderByValue(); }
        [HttpGet]
        [ActionName("GetAll/TotalQtyOfItem")]
        public int TotalQtyOfItem(int itemid)
        { return orderLogic.TotalQtyOfItem(itemid); }

        [HttpGet]
        [ActionName("GetAll/POListOfItem")]
        public IList<Order> POListOfItem(int itemid)
        {
            return orderLogic.POListOfItem(itemid);
        }

        [HttpGet]
        [ActionName("GetAll/POListOfSupplier")]
        public IList<Order> POListOfSupplier(int supplier)
        {

            return orderLogic.POListOfSupplier(supplier);
        }
        [HttpGet]

        public IEnumerable<Supplier> GetAllSupplier()
        {
            //TODO: Adatbázisból lekérni service-n keresztül!!!
            //return supplierLogic.ReadAll();
            return new Supplier[]
            {
                new Supplier( ) {Id = 1, Name = "SKF", Address ="7400, Kaposvár, Kossuth L. utca 24.", VATNumber=364829187},
                   new Supplier(){Id= 2, Name ="Szuperszíj", VATNumber=38376462, Address="3300, Eger, Bíboros utca 7." },
                   new Supplier() {Id = 3, Name = "NST", VATNumber = 37495842, Address = "8000, Székesfehérvár, Baross G. utca 18." },
                   new Supplier() { Id = 4, Name = "PowerBelt", VATNumber = 8745272, Address = "2310, Szigetszentmiklós, Petőfi utca 87." },
            };
        }
        [HttpGet]
        public IEnumerable<Item> GetAllItem()
        {
            //TODO: Adatbázisból lekérni service-n keresztül!!!
           
            //return itemLogic.ReadAll();

            return new Item[]
            {
                new Item() {Id = 1,Name ="bearing 245",UnitOfMeasure = "pc", SupplierId =1 },
                    new Item() {Id = 2, Name = "hose 14", UnitOfMeasure = "m", SupplierId= 3 },
                    new Item() {Id = 3, Name = "belt 372",UnitOfMeasure="m", SupplierId = 4 },
                    new Item() {Id=4, Name= "belt 165", UnitOfMeasure= "m", SupplierId = 2 },
                    new Item() {Id = 5, Name = "cylinder",UnitOfMeasure = "pc",SupplierId= 1 },
                    new Item() {Id = 6, Name ="pliers",UnitOfMeasure= "pc", SupplierId = 2 },
                    new Item() {Id = 7, Name = "screwdriver",UnitOfMeasure= "set",SupplierId = 3 },
                    new Item() {Id = 8, Name = "chain",UnitOfMeasure = "pc", SupplierId = 4 },
                    new Item() {Id = 9, Name = "allen key", UnitOfMeasure = "set",SupplierId = 3 },
            };
        }
    }
}
