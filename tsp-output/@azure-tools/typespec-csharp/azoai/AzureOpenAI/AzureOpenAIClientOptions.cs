using OpenAI;

namespace AzureOpenAI;

public class AzureOpenAIClientOptions : OpenAIClientOptions
{
    public AzureOpenAIClientOptions(string? version = default)
    {
        ApiVersion = version ?? "1.0";
    }

    public string ApiVersion { get; set; }
}
