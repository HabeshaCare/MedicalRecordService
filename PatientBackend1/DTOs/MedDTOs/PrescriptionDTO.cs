using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PatientBackend1.Models;

namespace PatientBackend1.DTOs.MedDTOs
{
    public class PrescriptionDTO
    {
        public string? Id { get; set; }
        public required string Diagnosis { get; set; }
        public required string MedicineName { get; set; }
        public required string DoctorId { get; set; }
        public required DateTime Date { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public PrescriptionStatus Status { get; set; } = PrescriptionStatus.Unsold;
    }
}