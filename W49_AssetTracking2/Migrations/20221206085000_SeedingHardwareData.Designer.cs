// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using W49_AssetTracking2;

#nullable disable

namespace W49AssetTracking2.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20221206085000_SeedingHardwareData")]
    partial class SeedingHardwareData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("W49_AssetTracking2.Hardware", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfPurchase")
                        .HasColumnType("datetime2");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Hardwares");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Brand = "Lenovo",
                            DateOfPurchase = new DateTime(2021, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Model = "Legion",
                            Price = 1600,
                            Type = "Computer"
                        },
                        new
                        {
                            Id = 2,
                            Brand = "Acer",
                            DateOfPurchase = new DateTime(2019, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Model = "Aspire XC",
                            Price = 1550,
                            Type = "Computer"
                        },
                        new
                        {
                            Id = 3,
                            Brand = "Apple",
                            DateOfPurchase = new DateTime(2021, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Model = "MacBook",
                            Price = 650,
                            Type = "Laptop"
                        },
                        new
                        {
                            Id = 4,
                            Brand = "Samsung",
                            DateOfPurchase = new DateTime(2020, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Model = "Galaxy",
                            Price = 550,
                            Type = "Laptop"
                        },
                        new
                        {
                            Id = 5,
                            Brand = "Samsung",
                            DateOfPurchase = new DateTime(2022, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Model = "S21",
                            Price = 200,
                            Type = "Phone"
                        },
                        new
                        {
                            Id = 6,
                            Brand = "Huawei",
                            DateOfPurchase = new DateTime(2019, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Model = "P30 Pro",
                            Price = 150,
                            Type = "Phone"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
