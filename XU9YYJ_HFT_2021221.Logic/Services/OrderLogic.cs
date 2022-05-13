using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XU9YYJ_HFT_2021221.Logic.Interfaces;

using XU9YYJ_HFT_2021221.Models.Entities;
using XU9YYJ_HFT_2021221.Repository.Interfaces;
using XU9YYJ_HFT_2021221.Models.Models;
using XU9YYJ_HFT_2021221.Logic.Validation;

namespace XU9YYJ_HFT_2021221.Logic.Services
{
    public class OrderLogic : IOrderLogic
    {
        IOrderRepository _orderRepository;
        ISupplierRepository _supplierRepository;
        public OrderLogic(IOrderRepository orderRepository, ISupplierRepository supplierRepository)
        {
            _orderRepository = orderRepository;
            _supplierRepository = supplierRepository;
        }
        public Order Create(Order entity)
        {
            var validator = new Validator();
            if (validator.Validate(entity))
            {
                var result = _orderRepository.Create(entity);
                return result;
        }
            else
            {
                throw new InvalidOperationException("Incorrect object data.");
    }

    //TODO: check access, log

}
        public void Delete(int id)
        {

            //TODO: check access, check relations: kulcsok ellenőrzése?, Repoba + függvények
            if (Read(id) != null)
            {
                _orderRepository.Delete(id);
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public Order Read(int id) // táblaolvasási teszt
        {       
                return _orderRepository.Read(id);  
        }

        public IList<Order> ReadAll()
        {
            return _orderRepository.ReadAll().ToList();
        }

        public Order Update(Order entity)
        {
            //TODO: validate, check access, log, map
            var result = _orderRepository.Update(entity);
            return result;
        }

        // NON-CRUD


        public IList<Order> POListOfSupplier(int supplierid) // list of orders of given supplier

        {

            var result = from orders in _orderRepository.ReadAll()
                         where orders.SupplierId == supplierid
                         select orders;
            return result.ToList();
                                
            
        }
        public IList<Order> POListOfItem(int itemid) // list of orders of given Item
            
        {
            var allPO = _orderRepository.ReadAll();
            List<Order> filtered = new List<Order>();
            foreach (var item in allPO)
            {
                if (item.ItemId == itemid)
                {
                    filtered.Add(item);
                }
            }
            return filtered;
            //return _orderRepository.ReadAll().Where(x => x.ItemId.Equals(itemid)).ToList();
        }
        // Total ordered qty of given item
        public int TotalQtyOfItem(int itemid) 
        {
            var orders = _orderRepository.ReadAll().Where(x => x.ItemId == itemid).ToList();
            int result = 0;
            foreach (var item in orders)
            {
                result += item.Quantity;
            }
            return result;
        } 
        
        
        public IEnumerable<AveragePOValue> AvaragePOValueBySupplier() 
        {
            
            var averages = from order in _orderRepository.ReadAll()
                           group order by order.SupplierId into grouped
                           select new
                           {
                               SupplierId = grouped.Key,
                               AverageValue = grouped.Average(x => x.Quantity * x.UnitPrice)
                               };
            var result = from supplier in _supplierRepository.ReadAll() //foreach vagy join ... on ... -> nem kell Deafault
                         from average in averages.Where(x => x.SupplierId == supplier.Id)/*.DefaultIfEmpty()*/
                         select new AveragePOValue()
                         {
                             SupplierName = supplier.Name,
                             AverageValue = average != null ? average.AverageValue : 0
                         };
           
            return result.ToList();
        }
        
        // Top3 Order by Value
        public IList<Order> Top3OrderByValue()
        {

            return _orderRepository.ReadAll().OrderByDescending(x => (x.Quantity * x.UnitPrice)).Take(3).ToList();
            
        }
        

        
       


    }
}
