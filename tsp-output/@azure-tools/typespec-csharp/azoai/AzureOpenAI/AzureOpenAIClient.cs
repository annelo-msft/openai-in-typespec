using OpenAI;
using System.ClientModel;

namespace AzureOpenAI;

public class AzureOpenAIClient : OpenAIClient
{
    private readonly string _apiVersion;

    public AzureOpenAIClient(Uri endpoint, ApiKeyCredential credential, AzureOpenAIClientOptions? options = default)
        : base(endpoint, credential, options)
    {
        options ??= new AzureOpenAIClientOptions();

        _apiVersion = options.ApiVersion;
    }

    public override Chat GetChatClient()
    {
        return new AzureChatClient(Pipeline, KeyCredential, Endpoint, _apiVersion);
    }
}
