namespace patientBackend1.Models{
public class LabTestResult
{
    public string Id { get; set; }
    public string RequestingUserId { get; set; }
    public string RequestedDoctorId { get; set; }
    public DateTime Date { get; set; }
    public bool Confirmed { get; set; }
    public bool InPerson { get; set; }
}
}