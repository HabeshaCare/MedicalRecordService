public class Package
{
    public string Id { get; set; } = string.Empty;
    // public string Name { get; set; }
    // public double Price { get; set; }
    // public int DurationInMonths { get; set; }
    // public SubscriptionStatus Status { get; set; }
    public double AmountInBirr { get; set; } // Allowed service provider type for this package (Normal, Specialist, SubSpecialist)
    
}

[Flags]
public enum SubscriptionStatus
{
    Active = 1,
    Inactive = 2
}

public enum ServiceProviderType
{
    Normal = 100,
    Specialist = 200,
    SubSpecialist = 400
}