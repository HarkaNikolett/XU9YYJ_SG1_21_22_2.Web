using Microsoft.EntityFrameworkCore;
using System;
using XU9YYJ_HFT_2021221.Models.Entities;

namespace XU9YYJ_HFT_2021221.Data.DbContexts
{
    public class XU9YYJ_DbContext : DbContext
    {
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public XU9YYJ_DbContext()
        {
            Database.EnsureCreated();
        }
       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DB.mdf;Integrated Security=true;MultipleActiveResultSets=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set relation
            modelBuilder.Entity<Item>(e =>
                 e.HasOne(c => c.Supplier)
                 .WithMany(b => b.Items)
                 .HasForeignKey(c => c.SupplierId)
                 .OnDelete(DeleteBehavior.ClientSetNull));
            modelBuilder.Entity<Order>(e =>
                    e.HasOne(c => c.Item)
                    .WithMany(b => b.Orders)
                    .HasForeignKey(c => c.ItemId)
                   .OnDelete(DeleteBehavior.ClientSetNull));
            modelBuilder.Entity<Order>(e =>
                    e.HasOne(c => c.Supplier)
                    .WithMany(b => b.Orders)
                    .HasForeignKey(c => c.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull));

            // Seed
            var supplier1 = new Supplier() { Id = 1, Name = "SKF", Address = "cim", VATNumber = 364829187 };
            var supplier2 = new Supplier() { Id = 2, Name = "Szuperszíj", Address = "cim", VATNumber = 38376462 };
            var supplier3 = new Supplier() { Id = 3, Name = "NST", Address = "cim", VATNumber = 37495842 };
            var supplier4 = new Supplier() { Id = 4, Name = "PowerBelt", Address = "cim", VATNumber = 8745272 };

            var item1 = new Item() { Id = 1, Name = "bearing 245", SupplierId = supplier1.Id, UnitOfMeasure = "pc" };
            var item2 = new Item() { Id = 2, Name = "hose 14", SupplierId = supplier3.Id, UnitOfMeasure = "m" };
            var item3 = new Item() { Id = 3, Name = "belt 372", SupplierId = supplier4.Id, UnitOfMeasure = "m" };
            var item4 = new Item() { Id = 4, Name = "belt 165", SupplierId = supplier2.Id, UnitOfMeasure = "m" };
            var item5 = new Item() { Id = 5, Name = "cylinder", SupplierId = supplier1.Id, UnitOfMeasure = "pc" };
            var item6 = new Item() { Id = 6, Name = "pliers", SupplierId = supplier2.Id, UnitOfMeasure = "pc" };
            var item7 = new Item() { Id = 7, Name = "screwdriver", SupplierId = supplier3.Id, UnitOfMeasure = "set" };
            var item8 = new Item() { Id = 8, Name = "chain", SupplierId = supplier4.Id, UnitOfMeasure = "pc" };
            var item9 = new Item() { Id = 9, Name = "allen key", SupplierId = supplier3.Id, UnitOfMeasure = "set" };

            var order1 = new Order() { Id = 1,SupplierId = supplier1.Id ,SupplierName = "SKF", ItemId = item1.Id, Currency = "EUR", UnitPrice = 13, Quantity = 3, Date = new DateTime(2021, 10, 16), Note = "Urgent delivery!" };
            var order2 = new Order() { Id = 2, SupplierId = supplier3.Id, SupplierName = "NST", ItemId = item2.Id, Currency = " EUR", UnitPrice = 8, Quantity = 2, Date = new DateTime(2021, 10, 16), Note = "Please deliver in 2 weeks!" };
            var order3 = new Order() { Id = 3, SupplierId = supplier4.Id, SupplierName = "PowerBelt", ItemId = item3.Id, Currency = " EUR", UnitPrice = 54, Quantity = 1, Date = new DateTime(2021, 10, 16), Note = "Please pack separately!" };
            var order4 = new Order() { Id = 4, SupplierId = supplier2.Id, SupplierName = "Szuperszíj", ItemId = item4.Id, Currency = " EUR", UnitPrice = 5, Quantity = 17, Date = new DateTime(2021, 10, 16), Note = "Colour: black" };
            var order5 = new Order() { Id = 5, SupplierId = supplier2.Id, SupplierName = "Szuperszíj", ItemId = item6.Id, Currency = " EUR", UnitPrice = 7, Quantity = 10, Date = new DateTime(2021, 10, 16), Note = "Colour: black" };
            var order6 = new Order() { Id = 6, SupplierId = supplier4.Id, SupplierName = "PowerBelt", ItemId = item8.Id, Currency = " EUR", UnitPrice = 75, Quantity = 3, Date = new DateTime(2021, 10, 16), Note = "Please pack separately!" };
            var order7 = new Order() { Id = 7, SupplierId = supplier3.Id, SupplierName = "NST", ItemId = item7.Id, Currency = " EUR", UnitPrice = 10, Quantity = 8, Date = new DateTime(2021, 10, 16), Note = "Please pack separately!" };
            var order9 = new Order() { Id = 9, SupplierId = supplier1.Id, SupplierName = "SKF", ItemId = item5.Id, Currency = "EUR", UnitPrice = 153, Quantity = 4, Date = new DateTime(2021, 10, 16), Note = "Urgent delivery!" };
            var order8 = new Order() { Id = 8, SupplierId = supplier3.Id, SupplierName = "NST", ItemId = item9.Id, Currency = " EUR", UnitPrice = 10, Quantity = 84, Date = new DateTime(2021, 10, 16), Note = "Please pack separately!" };
            modelBuilder.Entity<Supplier>().HasData(supplier1, supplier2, supplier3, supplier4);
            modelBuilder.Entity<Item>().HasData(item1, item2, item3, item4, item5, item6, item7, item8, item9);
            modelBuilder.Entity<Order>().HasData(order1, order2, order3, order4, order5, order6, order7, order8, order9);
        }
    }
}
