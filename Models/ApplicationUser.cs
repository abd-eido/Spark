using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spark.Enums;
using Spark.Data;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spark.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public UserTypeEnum UserType { get; set; }
        public string MobileNumber { get; set; }
        public string Degree { get; set; }
        public string ImagePath { get; set; }
        [NotMapped]
        public IFormFile ProfileImage { get; set; }
    }
}
