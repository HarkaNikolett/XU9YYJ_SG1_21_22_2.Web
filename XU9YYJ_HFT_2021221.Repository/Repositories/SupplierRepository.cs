using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XU9YYJ_HFT_2021221.Data.DbContexts;
using XU9YYJ_HFT_2021221.Models.Entities;
using XU9YYJ_HFT_2021221.Repository.Interfaces;

namespace XU9YYJ_HFT_2021221.Repository.Repositories
{
    public class SupplierRepository : RepositoryBase<Supplier, int>, ISupplierRepository
    {
        public SupplierRepository(XU9YYJ_DbContext context) : base(context)
        {
        }
        public void Delete(Supplier entity)
        {
            Context.Remove(entity);
            Context.SaveChanges();
        }

        public override Supplier Read(int id)
        {
            return ReadAll().SingleOrDefault(x => x.Id == id); 
        }

        
    }
}
