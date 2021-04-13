﻿// <auto-generated />
using System;
using CustomersREST.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CustomersREST.Migrations
{
    [DbContext(typeof(CustomersContext))]
    [Migration("20210413114915_AddOrders")]
    partial class AddOrders
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CustomersREST.Database.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b0ffb191-f1c1-4af5-bb37-b2a6ac49253f"),
                            Email = "john.doe@gmail.com",
                            FirstName = "John",
                            LastName = "Doe"
                        },
                        new
                        {
                            Id = new Guid("160a78a4-7076-4deb-a369-8a02c25abd23"),
                            FirstName = "Paul",
                            LastName = "Curtny"
                        },
                        new
                        {
                            Id = new Guid("e0afc479-4259-4f54-b7f3-64bc9460b6b5"),
                            Email = "sp@taipan.ch",
                            FirstName = "Suji",
                            LastName = "Paji"
                        });
                });

            modelBuilder.Entity("CustomersREST.Database.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CloseDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("VehicleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e5125ed8-7a96-4c7d-9399-64c6f604a1d0"),
                            CloseDate = new DateTime(2021, 4, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CustomerId = new Guid("b0ffb191-f1c1-4af5-bb37-b2a6ac49253f"),
                            Total = 113.25m,
                            VehicleId = new Guid("4c264d3a-903f-4428-b372-cf7fdfea9855")
                        },
                        new
                        {
                            Id = new Guid("4f3bedd8-2e80-405f-b188-713c11d844c5"),
                            CloseDate = new DateTime(2021, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CustomerId = new Guid("b0ffb191-f1c1-4af5-bb37-b2a6ac49253f"),
                            Total = 99m,
                            VehicleId = new Guid("f841cd18-d50b-444f-96cb-a5e241e42a8a")
                        });
                });

            modelBuilder.Entity("CustomersREST.Database.Entities.Vehicle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Make")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Mileage")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Vehicles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f841cd18-d50b-444f-96cb-a5e241e42a8a"),
                            CustomerId = new Guid("b0ffb191-f1c1-4af5-bb37-b2a6ac49253f"),
                            Make = "Lada",
                            Mileage = 340000
                        },
                        new
                        {
                            Id = new Guid("4c264d3a-903f-4428-b372-cf7fdfea9855"),
                            CustomerId = new Guid("b0ffb191-f1c1-4af5-bb37-b2a6ac49253f"),
                            Make = "Opel",
                            Mileage = 113000
                        },
                        new
                        {
                            Id = new Guid("25400098-4516-489c-99b5-9e78a36034a2"),
                            CustomerId = new Guid("e0afc479-4259-4f54-b7f3-64bc9460b6b5"),
                            Make = "Mazda",
                            Mileage = 22000
                        });
                });

            modelBuilder.Entity("CustomersREST.Database.Entities.Order", b =>
                {
                    b.HasOne("CustomersREST.Database.Entities.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CustomersREST.Database.Entities.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("CustomersREST.Database.Entities.Vehicle", b =>
                {
                    b.HasOne("CustomersREST.Database.Entities.Customer", "Customer")
                        .WithMany("Vehicles")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("CustomersREST.Database.Entities.Customer", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Vehicles");
                });
#pragma warning restore 612, 618
        }
    }
}
