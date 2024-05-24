using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.Users;
using patientBackend1.Models;

namespace PatientBackend1.Models
{
    public class Patient : User
    {
        public required string NationalId { get; set; }
        public required DateTime? DateOfBirth { get; set; }
        public required int Height { get; set; }
        public required int Weight { get; set; }
        public required string Location  { get; set; }
        // public Package CurrentPackage { get; set; } = new Package();
        public double CurrentBalance { get; set; } = 0.0;
    }
}