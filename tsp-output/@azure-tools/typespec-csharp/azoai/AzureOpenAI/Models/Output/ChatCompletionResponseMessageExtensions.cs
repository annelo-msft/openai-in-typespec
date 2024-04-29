using OpenAI.Models;
using System.ClientModel.Primitives;

namespace AzureOpenAI.Models;

public static class ChatCompletionResponseMessageExtensions
{
    // Output property
    public static AzureChatExtensionsMessageContext? AzureExtensionsContext(this ChatCompletionResponseMessage message)
    {
        if (message is not IJsonModel model)
        {
            throw new InvalidOperationException("TODO");
        }

        if (!model.AdditionalProperties.TryGetValue("context", out BinaryData? value))
        {
            return null;
        }

        // Deserialize 
        return ModelReaderWriter.Read<AzureChatExtensionsMessageContext>(value)!;
    }
}
