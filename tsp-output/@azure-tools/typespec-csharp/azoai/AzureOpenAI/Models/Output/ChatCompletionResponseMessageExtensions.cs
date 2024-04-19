using OpenAI.Models;
using System.ClientModel.Primitives;

namespace AzureOpenAI.Models;

public static class ChatCompletionResponseMessageExtensions
{
    // Output property
    public static AzureChatExtensionsMessageContext? GetAzureExtensionsContext(this ChatCompletionResponseMessage message)
    {
        if (!message.SerializedAdditionalRawData.TryGetValue("context", out object? value))
        {
            return null;
        }

        // It's either deserialized already or not. Find out now.
        // TODO: this works for OpenAI scenarios today, but we need to find a
        // way to handle cases where BinaryData is a valid type for extended
        // properties.
        if (value is BinaryData serializedValue)
        {
            // Qn: when can Read return null?
            var deserializedValue = ModelReaderWriter.Read<AzureChatExtensionsMessageContext>(serializedValue)!;
            message.SerializedAdditionalRawData["context"] = deserializedValue;
            return deserializedValue;
        }

        if (value is not AzureChatExtensionsMessageContext context)
        {
            throw new InvalidOperationException($"'context' value is unexpected type: '{value.GetType()}'.");
        }

        return context;
    }
}
