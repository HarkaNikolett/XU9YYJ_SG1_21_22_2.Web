using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XU9YYJ_HFT_2021221.Logic.Interfaces;
using XU9YYJ_HFT_2021221.Models.Entities;
using XU9YYJ_HFT_2021221.Repository.Interfaces;
using XU9YYJ_HFT_2021221.Logic.Validation;
using XU9YYJ_HFT_2021221.Models.Models;

namespace XU9YYJ_HFT_2021221.Logic.Services
{
    public class SupplierLogic : ISupplierLogic
    {
        ISupplierRepository _supplierRepository;
        IItemRepository _itemRepository;
        IOrderRepository _orderRepository;
        public SupplierLogic(ISupplierRepository supplierRepository, IItemRepository itemRepository, IOrderRepository orderRepository)
        {
            _supplierRepository = supplierRepository;
            _itemRepository = itemRepository;
            _orderRepository = orderRepository;
        }
        public Supplier Create(Supplier entity)
        {
        //TODO: check access, log
            var validator = new Validator();
            if (validator.Validate(entity))
            {
                var result = _supplierRepository.Create(entity);
                return result;
             }
            else
            {
                throw new InvalidOperationException("Incorrect object data.");
            }
        }

        public void Delete(int id)
        {
            //TODO: check access, check relations, Repoba + függvények
            _supplierRepository.Delete(id);
        }

        public Supplier Read(int id)
        {
            return _supplierRepository.Read(id);
        }

        public IList<Supplier> ReadAll()
        {
            return _supplierRepository.ReadAll().ToList();
        }

        public Supplier Update(Supplier entity)
        {
            //TODO: validate, check access, log, map
            var result = _supplierRepository.Update(entity);
            return result;
        }
        // NON-CRUD
        // Number of items by supplier
        public IEnumerable<QtyOfItems> QtyOfItemsBySupplier()
        {
            var qties = from item in _itemRepository.ReadAll()
                           group item by item.SupplierId into grouped
                           select new
                           {
                               SupplierId = grouped.Key,
                               Qty = grouped.Count()
                           };
            var result = from supplier in _supplierRepository.ReadAll()
                         from qty in qties.Where(x => x.SupplierId == supplier.Id)/*.DefaultIfEmpty()*/
                         select new QtyOfItems()
                         {
                             SupplierId = supplier.Id,
                             Qty = qty != null ? qty.Qty : 0
                         };
            return result.ToList();
        }
        //Total value of orders by supplier
        public IEnumerable<TotalValue> TotalValueofOrdersBySupplier()
        {
            var values = from order in _orderRepository.ReadAll()
                         group order by order.SupplierId into grouped
                         select new
                         {
                             SupplierId = grouped.Key,
                             Value = grouped.Sum(x => (x.Quantity*x.UnitPrice))
                        };
            var result = from supplier in _supplierRepository.ReadAll()
                         from value in values.Where(x => x.SupplierId == supplier.Id)/*.DefaultIfEmpty()*/
                         select new TotalValue()
                         {
                             SupplierName = supplier.Name,
                             Value = value != null ? value.Value : 0
                         };
            return result.ToList();
        }
    }
}
