using System;
using System.ComponentModel.DataAnnotations;

namespace CarInsuranceMVC.Models
{
    public class Insuree
    {
        public int Id { get; set; }

        [Required, Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required, Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required, EmailAddress, Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required, Display(Name = "Car Year")]
        public int CarYear { get; set; }

        [Required, Display(Name = "Car Make")]
        public string CarMake { get; set; }

        [Required, Display(Name = "Car Model")]
        public string CarModel { get; set; }

        [Range(0, int.MaxValue), Display(Name = "Speeding Tickets")]
        public int SpeedingTickets { get; set; }

        [Display(Name = "DUI?")]
        public bool DUI { get; set; }

        [Display(Name = "Full Coverage?")]
        public bool CoverageType { get; set; }   // true = Full coverage

        [DataType(DataType.Currency)]
        public decimal Quote { get; set; }       // calculated, not entered by user
    }
}
