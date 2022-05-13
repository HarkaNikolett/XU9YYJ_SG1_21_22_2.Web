using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XU9YYJ_HFT_2021221.Logic.Interfaces;
using XU9YYJ_HFT_2021221.Models.Entities;
using XU9YYJ_HFT_2021221.Repository.Interfaces;
using XU9YYJ_HFT_2021221.Logic.Validation;
namespace XU9YYJ_HFT_2021221.Logic.Services
{
    public class ItemLogic : IItemLogic

    {
        IItemRepository _itemRepository;
        ISupplierRepository _supplierRepository;
        public ItemLogic(IItemRepository itemRepository, ISupplierRepository supplierRepository)
        {
            _itemRepository = itemRepository;
            _supplierRepository = supplierRepository;
        }
        public Item Create(Item entity)
        {
            var validator = new Validator();
            if (validator.Validate(entity))
            {
                var result = _itemRepository.Create(entity);
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
            //TODO: check access, check relations, Repoba + függvények
            _itemRepository.Delete(id);
        }

        public Item Read(int id)
        {
            return _itemRepository.Read(id);
        }

        public IList<Item> ReadAll()
        {
            return _itemRepository.ReadAll().ToList();
        }

        public Item Update(Item entity)
        {
            //TODO: validate, check access, log, map

            var result = _itemRepository.Update(entity);
            return result;
        }
        // NON-CRUD:
        public IList<Item> ItemListOfSupplier(int supplierId) //list of items of given supplier by ID
        {
            return _itemRepository.ReadAll().Where(x => x.SupplierId == supplierId).ToList();
        }
    }
}
