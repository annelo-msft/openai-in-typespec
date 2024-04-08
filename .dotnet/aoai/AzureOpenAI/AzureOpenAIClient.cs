using OpenAI;
using OpenAI.Chat;
using System.ClientModel;

namespace AzureOpenAI;

public class AzureOpenAIClient : OpenAIClient
{
    private readonly Uri _endpoint;

    private readonly ApiKeyCredential _keyCredential;
    private readonly OpenAIClientOptions _unbrandedClientOptions;

    public AzureOpenAIClient(Uri endpoint, ApiKeyCredential credential, AzureOpenAIClientOptions? options = default)
    {
        options ??= new();

        _endpoint = endpoint;
        _keyCredential = credential;
        
        _unbrandedClientOptions = options.ToOpenAIClientOptions();
    }

    public override ChatClient GetChatClient(string model)
    {
        return new AzureChatClient(model, _keyCredential, _unbrandedClientOptions);
    }
}
