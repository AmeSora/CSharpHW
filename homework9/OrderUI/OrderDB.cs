namespace OrderUI
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class OrderDB : DbContext
    {
        public DbSet<OrderDetail> details { get; set; }

        public DbSet<Order> orders { get; set; }

        public DbSet<Goods> goods { get; set; }

        public DbSet<Customer> customers { get; set; }

        public OrderDB()
            : base("name=OrderDB")
        {
            Database.SetInitializer<OrderDB>(new DropCreateDatabaseAlways<OrderDB>());
            //Database.SetInitializer<OrderDB>(new DropCreateDatabaseIfModelChanges<OrderDB>());
            //Database.SetInitializer<OrderDB>(new CreateDatabaseIfNotExists<OrderDB>());

        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<OrderDetail>().HasKey(o => o.Id);
            //modelBuilder.Entity<Order>().HasKey(o => o.Id);
            //modelBuilder.Entity<Goods>().HasKey(o => o.Id);
            //modelBuilder.Entity<Customer>().HasKey(o => o.Id);
        }
    }
}
