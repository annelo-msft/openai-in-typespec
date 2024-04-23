using OpenAI.Models;
using System.ClientModel.Primitives;

namespace AzureOpenAI.Models;

public static class ChatCompletionResponseMessageExtensions
{
    // Output property
    public static AzureChatExtensionsMessageContext? GetAzureExtensionsContext(this ChatCompletionResponseMessage message)
    {
        if (message is not IJsonModel model)
        {
            throw new InvalidOperationException("TODO");
        }

        if (!model.AdditionalProperties.TryGetValue("context", out object? value))
        {
            return null;
        }

        // It's either deserialized already or not. Find out now.
        // This should work for all cases because we know the contract regarding
        // BinaryData values.
        if (value is BinaryData serializedValue)
        {
            // Qn: when can Read return null?
            var deserializedValue = ModelReaderWriter.Read<AzureChatExtensionsMessageContext>(serializedValue)!;
            model.AdditionalProperties["context"] = deserializedValue;
            return deserializedValue;
        }

        if (value is not AzureChatExtensionsMessageContext context)
        {
            throw new InvalidOperationException($"'context' value is unexpected type: '{value.GetType()}'.");
        }

        return context;
    }
}
