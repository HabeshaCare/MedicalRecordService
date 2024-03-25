using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.Users;

namespace patientBacken1.Models
{
    public class Doctor : User
    {
        public string? LicensePath { get; set; }
        public string Specialization { get; set; } = "";
        public int? YearOfExperience { get; set; }
        public bool? Verified { get; set; }
    }
}