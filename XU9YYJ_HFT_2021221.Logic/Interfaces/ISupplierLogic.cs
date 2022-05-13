using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XU9YYJ_HFT_2021221.Models.Entities;
using XU9YYJ_HFT_2021221.Models.Models;

namespace XU9YYJ_HFT_2021221.Logic.Interfaces
{
    public interface ISupplierLogic
    {
        IList<Supplier> ReadAll();
        Supplier Read(int id);
        Supplier Create(Supplier entity);
        Supplier Update(Supplier entity);
        void Delete(int id);
        IEnumerable<TotalValue> TotalValueofOrdersBySupplier();
        IEnumerable<QtyOfItems> QtyOfItemsBySupplier();
    }
}
