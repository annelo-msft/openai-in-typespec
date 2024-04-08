using OpenAI;
using OpenAI.Chat;
using System.ClientModel;
using System.ClientModel.Primitives;

namespace AzureOpenAI;

internal class AzureChatClient : ChatClient
{
    public AzureChatClient(string model, ApiKeyCredential? credential = null, OpenAIClientOptions? options = null) 
        : base(model, credential, options)
    {
    }

    protected override PipelineMessage CreateCustomRequestMessage(IEnumerable<ChatRequestMessage> messages, int? choiceCount, ChatCompletionOptions options)
    {
    }
}
