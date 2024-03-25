using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace patientBackend1.OptionsDTO
{
    public class DoctorFilterDTO
    {
        public int? MinYearExperience { get; set; }
        public int? MaxYearExperience { get; set; }
        public string? Specialization { get; set; }
    }
}