using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GuildCars.Models.Tables
{
    public class ContactRecord
    {
        public int ContactRecordId { get; set; }
        [Required(ErrorMessage = "Your Name is Required")]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        [Required(ErrorMessage = "A Message is required")]
        public string Message { get; set; }
    }
}
