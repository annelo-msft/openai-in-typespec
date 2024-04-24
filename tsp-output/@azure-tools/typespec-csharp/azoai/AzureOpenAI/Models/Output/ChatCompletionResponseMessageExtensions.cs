using OpenAI.Models;
using System.ClientModel.Primitives;

namespace AzureOpenAI.Models;

public static class ChatCompletionResponseMessageExtensions
{
    // Output property
    public static AzureChatExtensionsMessageContext? GetAzureExtensionsContext(this ChatCompletionResponseMessage message)
    {
        if (message is not JsonModel<ChatCompletionResponseMessage> model)
        {
            throw new InvalidOperationException("TODO");
        }

        model.TryGetUnknownProperty("context", out AzureChatExtensionsMessageContext? value);
        return value;
    }
}
