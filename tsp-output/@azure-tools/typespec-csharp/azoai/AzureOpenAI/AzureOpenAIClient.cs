using OpenAI;
using System.ClientModel;

namespace AzureOpenAI;

public class AzureOpenAIClient : OpenAIClient
{
    public AzureOpenAIClient(Uri endpoint, ApiKeyCredential credential, AzureOpenAIClientOptions? options = default)
        : base(endpoint, credential, options)
    {
    }
}
