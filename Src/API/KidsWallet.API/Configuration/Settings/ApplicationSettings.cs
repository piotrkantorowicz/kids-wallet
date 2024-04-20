namespace KidsWallet.API.Configuration.Settings;

public class ApplicationSettings
{
    public string? Name { get; set; }
    
    public ApplicationType Type { get; set; }
    
    public string? Description { get; set; }
}

public enum ApplicationType
{
    Crud = 1
}