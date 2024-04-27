using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace KidsWallet.API.Configuration.Errors;

public sealed class ApiProblemDetails
{
    public int Status { get; set; }
    
    public string? Title { get; set; }
    
    public string? Detail { get; set; }
    
    public string? Type { get; set; }
    
    public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
    
    public static IDictionary<string, string[]> CreateErrorDictionary(ModelStateDictionary? modelState)
    {
        ArgumentNullException.ThrowIfNull(modelState);
        Dictionary<string, string[]> errorDictionary = new(StringComparer.Ordinal);
        
        foreach ((string? property, ModelStateEntry? error) in modelState)
        {
            ModelErrorCollection errors = error.Errors;
            
            if (errors is not { Count: > 0 })
            {
                continue;
            }
            
            if (errors.Count == 1)
            {
                string errorMessage = errors[0].ErrorMessage;
                
                errorDictionary.Add(property, [
                    errorMessage
                ]);
            }
            else
            {
                string[] errorMessages = new string[errors.Count];
                
                for (int i = 0; i < errors.Count; i++)
                {
                    errorMessages[i] = errors[i].ErrorMessage;
                }
                
                errorDictionary.Add(property, errorMessages);
            }
        }
        
        return errorDictionary;
    }
}