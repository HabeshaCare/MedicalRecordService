public class Package
{
    public string Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int DurationInMonths { get; set; }
    public SubscriptionStatus Status { get; set; }
    public ServiceProviderType AllowedProviderType { get; set; } // Allowed service provider type for this package (Normal, Specialist, SubSpecialist)
    
}

[Flags]
public enum SubscriptionStatus
{
    Active = 1,
    Inactive = 2
}

public enum ServiceProviderType
{
    Normal = 1,
    Specialist = 2,
    SubSpecialist = 4
}