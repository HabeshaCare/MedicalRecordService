using System;

namespace patientBackend1.Models
{
    public class Test
    {
        public string DoctorId { get; set; }
        public string Laboratorist { get; set; }
        public DateTime RequestedDate { get; set; }
        public TestStatus Status { get; set; }
    }

    public enum TestStatus
    {
        Pending,
        Completed
    }
}