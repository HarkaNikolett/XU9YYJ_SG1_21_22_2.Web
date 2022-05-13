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
    public class SupplierController : ControllerBase
    {
        readonly ISupplierLogic supplierLogic;

        public SupplierController(ISupplierLogic supplierLogic)
        {
            this.supplierLogic = supplierLogic;
        }
        // GET: api/Supplier/GetAll
        [HttpGet]
        [ActionName("GetAll")]
        public IEnumerable<Supplier> Get()
        {
            return supplierLogic.ReadAll();
        }

        // GET api/Supplier/Get/5
        [HttpGet("{id}")]
        public Supplier Get(int id)
        {
            return supplierLogic.Read(id);
        }

        // POST api/Supplier/Create
        [HttpPost]
        [ActionName("Create")]
        public ApiResult Post(Supplier supplier)
        {
            var result = new ApiResult(true);
            try
            {
                supplierLogic.Create(supplier);
            }
            catch (Exception)
            {

                result.IsSuccess = false;
            }
            return result;

        }

        // PUT api/Supplier/Update
        [HttpPut]
        [ActionName("Update")]
        public ApiResult Put(Supplier supplier)
        {
            var result = new ApiResult(true);
            try
            {
                supplierLogic.Update(supplier);
            }
            catch (Exception)
            {

                result.IsSuccess = false;
            }
            return result;
        }

        // DELETE api/Supplier/Delete/5
        [HttpDelete("{id}")]
        public ApiResult Delete(int id)
        {
            var result = new ApiResult(true);
            try
            {
                supplierLogic.Delete(id);
            }
            catch (Exception)
            {

                result.IsSuccess = false;
            }
            return result;
        }
        [HttpGet]
        public IEnumerable<TotalValue> TotalValueofOrdersBySupplier()
        {
            return supplierLogic.TotalValueofOrdersBySupplier();
        }
        [HttpGet]
        public IEnumerable<QtyOfItems> QtyOfItemsBySupplier()
        { return supplierLogic.QtyOfItemsBySupplier(); }

    }
}
