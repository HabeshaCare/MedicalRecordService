namespace patientBackend1.Models
{
public class LabTest
{
    public string TestName { get; set; }
    public string TestResult { get; set; }

    public LabTest(string testName, string testResult)
    {
        TestName = testName;
        TestResult = testResult;
    }
}
}