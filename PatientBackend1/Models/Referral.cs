using System;

namespace patientBackend1.Models
{
    public class Referal
    {
        public string Id { get; set; } // Unique ID generated by the system
        public string ReferredTo { get; set; } // The health center ID referred to
        public string ReferredFrom { get; set; } // The health center ID referred from
        public string PatientId { get; set; } // The referred Patient ID
        public MedicalReport Report { get; set; } // The report from referring doctor
        public MedicalReport ReferralFeedback { get; set; } // The feedback from the referred health center
        public string Reason { get; set; } // The reason for referral
        public string ReferringDoctorId { get; set; } // The ID of the referring doctor

        // Additional properties or methods can be added here
    }
}