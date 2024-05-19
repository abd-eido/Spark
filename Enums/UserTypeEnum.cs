using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Spark.Enums
{
    public enum UserTypeEnum
    {
        [Display(Name = "Student")]
        Student = 0,

        [Display(Name = "Doctor")]
        Doctor = 1,

        [Display(Name = "Investor")]
        Investor = 2
    }
}
