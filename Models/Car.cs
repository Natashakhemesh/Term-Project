using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Wheels_Away.Data.Enums;

namespace Wheels_Away.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Seats { get; set; }
        public int RentalPrice { get; set; }
        public FuelType FuelType {get;set;}
        public GearType GearType {get;set;}
        public string Image { get; set; }

        public int FuelTypeId => (int)FuelType;
        public int GearTypeId => (int)GearType;

    }
}
