using System;

namespace patientBackend1.DTOs.MedDTOs
{
    public class MedRecDTO
    {
        public int Height { get; set; }
        public int Weight { get; set; }
        public string GeneralAppearance { get; set; }
        public string DoctorId { get; set; }
        public DateTime Date { get; set; }
    }
}