using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Tables
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Style { get; set; }
        [Required]
        public int ModelId { get; set; }
        [Required]
        public int MakeId { get; set; }
        [Required]
        public string Transmission { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public string Interior { get; set; }
        [Required]
        public int Mileage { get; set; }
        [Required]
        public string Vin { get; set; }
        [Required]
        public decimal SalePrice { get; set; }
        [Required]
        public decimal MSRP { get; set; }
        [Required]
        public string Description { get; set; }
        public bool Sold { get; set; }
    }
}
