using OpenAI;
using OpenAI.Chat;
using System.ClientModel;

namespace AzureOpenAI;

internal class AzureChatClient : ChatClient
{
    public AzureChatClient(string model, ApiKeyCredential? credential = null, OpenAIClientOptions? options = null) 
        : base(model, credential, options)
    {
    }
}
