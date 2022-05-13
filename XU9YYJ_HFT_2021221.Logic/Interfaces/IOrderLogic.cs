using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XU9YYJ_HFT_2021221.Models.Models;
using XU9YYJ_HFT_2021221.Models.Entities;

namespace XU9YYJ_HFT_2021221.Logic.Interfaces
{
    public interface IOrderLogic
    {
        IList<Order> ReadAll();
        Order Read(int id);
        Order Create(Order entity);
        Order Update(Order entity);
        IList<Order> POListOfSupplier(int supplier);
      
        IList<Order> POListOfItem(int itemid);
        int TotalQtyOfItem(int itemid);
        IEnumerable<AveragePOValue> AvaragePOValueBySupplier();
        IList<Order> Top3OrderByValue();
        void Delete(int id);
    }
}
