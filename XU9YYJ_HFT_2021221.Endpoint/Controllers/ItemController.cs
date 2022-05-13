using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using XU9YYJ_HFT_2021221.Logic.Interfaces;
using XU9YYJ_HFT_2021221.Models.Entities;
using XU9YYJ_HFT_2021221.Models.Models;
namespace XU9YYJ_HFT_2021221.Endpoint.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        readonly IItemLogic itemLogic;

        public ItemController(IItemLogic itemLogic)
        {
            this.itemLogic = itemLogic;
        }
        // GET: api/Item/GetAll
        [HttpGet]
        [ActionName("GetAll")]
        public IEnumerable<Item> Get()
        {
            return itemLogic.ReadAll();
        }

        // GET api/Item/Get/5
        [HttpGet("{id}")]
        public Item Get(int id)
        {
            return itemLogic.Read(id);
        }

        // POST api/Item/Create
        [HttpPost]
        [ActionName("Create")]
        public ApiResult Post(Item item)
        {
            var result = new ApiResult(true);
            try
            {
                itemLogic.Create(item);
            }
            catch (Exception)
            {

                result.IsSuccess = false;
            }
            return result;

        }

        // PUT api/Item/Update
        [HttpPut]
        [ActionName("Update")]
        public ApiResult Put(Item item)
        {
            var result = new ApiResult(true);
            try
            {
                itemLogic.Update(item);
            }
            catch (Exception)
            {

                result.IsSuccess = false;
            }
            return result;
        }

        // DELETE api/Item/Delete/5
        [HttpDelete("{id}")]
        public ApiResult Delete(int id)
        {
            var result = new ApiResult(true);
            try
            {
                itemLogic.Delete(id);
            }
            catch (Exception)
            {

                result.IsSuccess = false;
            }
            return result;
        }
        [HttpGet]
        [ActionName("GetAll/ItemListOfSupplier")]
        public IList<Item> ItemListOfSupplier(int supplierId)
        {
            return itemLogic.ItemListOfSupplier(supplierId);
        }
    }
}
