using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XU9YYJ_HFT_2021221.Models.Entities;


namespace XU9YYJ_HFT_2021221.Logic.Interfaces
{
    public interface IItemLogic
    {
        IList<Item> ReadAll();
        Item Read(int id);
        Item Create(Item entity);
        Item Update(Item entity);
        IList<Item> ItemListOfSupplier(int supplierId);
        void Delete(int id);
        

    }
}
