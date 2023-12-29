using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Wheels_Away.Data.Enums;
using Wheels_Away.Models;

namespace Wheels_Away.Data
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.Property(c => c.Model).IsRequired();
            builder.Property(c => c.Year).IsRequired();
            builder.Property(c => c.Seats).IsRequired();
            builder.Property(c => c.RentalPrice).IsRequired();
            builder.Property(c => c.FuelType).IsRequired();
            builder.HasData(

                new Car
                {
                    Id = 1,
                    Model = "Toyota Camry",
                    Year = 2022,
                    Seats = 5,
                    RentalPrice = 1050,
                    FuelType = FuelType.Diesel,
                    GearType = GearType.Automatic,
                    Image = "Toyota Camry.jpg"
                },
                new Car
                {
                    Id = 2,
                    Model = "Honda Accord",
                    Year = 2021,
                    Seats = 5,
                    RentalPrice = 955,
                    FuelType = FuelType.Petrol,
                    GearType = GearType.Manual,
                    Image = "Honda Accord.jpg"
                },
                new Car
                {
                    Id = 3,
                    Model = "Ford Mustang",
                    Year = 2023,
                    Seats = 4,
                    RentalPrice = 1070,
                    FuelType = FuelType.Hybrid,
                    GearType = GearType.Automatic,
                    Image = "Ford Mustang.jpg"
                },
                new Car
                {
                    Id = 4,
                    Model = "Tesla Model 3",
                    Year = 2022,
                    Seats = 5,
                    RentalPrice = 1000,
                    FuelType = FuelType.Electric,
                    GearType = GearType.Manual,
                    Image = "Tesla Model 3.jpg"
                },
                new Car
                {
                    Id = 5,
                    Model = "Chevrolet Tahoe",
                    Year = 2021,
                    Seats = 8,
                    RentalPrice = 990,
                    FuelType = FuelType.Petrol,
                    GearType = GearType.Automatic,
                    Image = "Chevrolet Tahoe.jpg"
                },
                new Car
                {
                    Id = 6,
                    Model = "Nissan Altima",
                    Year = 2020,
                    Seats = 5,
                    RentalPrice = 960,
                    FuelType = FuelType.Diesel,
                    GearType = GearType.Automatic,
                    Image = "Nissan Altima.jpg"
                },
                new Car
                {
                    Id = 7,
                    Model = "BMW X5",
                    Year = 2023,
                    Seats = 5,
                    RentalPrice = 1000,
                    FuelType = FuelType.Hybrid,
                    GearType = GearType.Automatic,
                    Image = "BMW X5.jpg"
                },
                new Car
                {
                    Id = 8,
                    Model = "Hyundai Sonata",
                    Year = 2022,
                    Seats = 5,
                    RentalPrice = 955,
                    FuelType = FuelType.Petrol,
                    GearType = GearType.Automatic,
                    Image = "Hyundai Sonata.jpg"
                },
                new Car
                {
                    Id = 9,
                    Model = "Kia Sportage",
                    Year = 2021,
                    Seats = 5,
                    RentalPrice = 965,
                    FuelType = FuelType.Diesel,
                    GearType = GearType.Automatic,
                    Image = "Kia Sportage.jpg"
                },
                new Car
                {
                    Id = 10,
                    Model = "Audi A4",
                    Year = 2023,
                    Seats = 5,
                    RentalPrice = 1095,
                    FuelType = FuelType.Hybrid,
                    GearType = GearType.Manual,
                    Image = "Audi A4.jpg"
                }
            );
        }
    }
}