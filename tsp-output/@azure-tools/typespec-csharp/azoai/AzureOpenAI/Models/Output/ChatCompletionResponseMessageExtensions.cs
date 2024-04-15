using OpenAI.Models;

namespace AzureOpenAI.Models;

public static class ChatCompletionResponseMessageExtensions
{
    // Output property
    public static AzureChatExtensionsMessageContext? GetAzureExtensionsContext(this ChatCompletionResponseMessage message)
    {
        if (message is not AzureChatCompletionResponseMessage azureMessage)
        {
            throw new NotSupportedException("Cannot get AzureExtensionsContext when not using the Azure OpeAI client.");
        }

        return azureMessage.AzureExtensionsContext;
    }
}
