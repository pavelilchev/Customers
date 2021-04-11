namespace CustomersREST.Database.Context
{
    using CustomersREST.Database.Entities;
    using Microsoft.EntityFrameworkCore;
    using System;

    public class CustomersContext : DbContext
    {
        public CustomersContext(DbContextOptions<CustomersContext> options)
             : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // seed the database with dummy data
            modelBuilder.Entity<Customer>().HasData(
                new Customer()
                {
                    Id = Guid.Parse("b0ffb191-f1c1-4af5-bb37-b2a6ac49253f"),
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@gmail.com",
                },
                new Customer()
                {
                    Id = Guid.Parse("160a78a4-7076-4deb-a369-8a02c25abd23"),
                    FirstName = "Paul",
                    LastName = "Curtny",
                },
                new Customer()
                {
                    Id = Guid.Parse("e0afc479-4259-4f54-b7f3-64bc9460b6b5"),
                    FirstName = "Suji",
                    LastName = "Paji",
                    Email = "sp@taipan.ch",
                }
                );

            modelBuilder.Entity<Vehicle>().HasData(
                new Vehicle
                {
                    Id = Guid.Parse("f841cd18-d50b-444f-96cb-a5e241e42a8a"),
                    CustomerId = Guid.Parse("b0ffb191-f1c1-4af5-bb37-b2a6ac49253f"),
                    Make = "Lada",
                    Mileage = 340000,
                },
                new Vehicle
                {
                    Id = Guid.Parse("4c264d3a-903f-4428-b372-cf7fdfea9855"),
                    CustomerId = Guid.Parse("b0ffb191-f1c1-4af5-bb37-b2a6ac49253f"),
                    Make = "Opel",
                    Mileage = 113000,
                },
                new Vehicle
                {
                    Id = Guid.Parse("25400098-4516-489c-99b5-9e78a36034a2"),
                    CustomerId = Guid.Parse("e0afc479-4259-4f54-b7f3-64bc9460b6b5"),
                    Make = "Mazda",
                    Mileage = 22000,
                }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
