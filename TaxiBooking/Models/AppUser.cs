using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace TaxiBooking.Models
{
    [Index(nameof(PhoneNumber), IsUnique = true)]
    public class AppUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        public string Image { get; set; }
        [Required]
        [MaxLength(12)]
        public override string PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
    }
}
