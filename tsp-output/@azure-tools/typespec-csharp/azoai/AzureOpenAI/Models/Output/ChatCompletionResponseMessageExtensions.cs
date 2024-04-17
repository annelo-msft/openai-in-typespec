using OpenAI.Models;

namespace AzureOpenAI.Models;

public static class ChatCompletionResponseMessageExtensions
{
    // Output property
    public static AzureChatExtensionsMessageContext? GetAzureExtensionsContext(this ChatCompletionResponseMessage message)
    {
        // TODO: How to throw if used incorrectly?

        // TODO: retrieve from dictionary
        //return azureMessage.AzureExtensionsContext;

        throw new NotImplementedException();
    }
}
