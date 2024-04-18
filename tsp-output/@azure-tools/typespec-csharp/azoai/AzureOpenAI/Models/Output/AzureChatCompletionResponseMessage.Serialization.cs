using OpenAI.Models;
using System.ClientModel.Primitives;
using System.Text.Json;

namespace AzureOpenAI.Models;

internal partial class AzureChatCompletionResponseMessage : IJsonModel<AzureChatCompletionResponseMessage>
{
    AzureChatCompletionResponseMessage IJsonModel<AzureChatCompletionResponseMessage>.Create(ref Utf8JsonReader reader, ModelReaderWriterOptions options)
    {
        var format = options.Format == "W" ? ((IPersistableModel<ChatCompletionResponseMessage>)this).GetFormatFromOptions(options) : options.Format;
        if (format != "J")
        {
            throw new FormatException($"The model {nameof(ChatCompletionResponseMessage)} does not support reading '{format}' format.");
        }

        using JsonDocument document = JsonDocument.ParseValue(ref reader);
        return DeserializeAzureChatCompletionResponseMessage(document.RootElement, options);
    }

    AzureChatCompletionResponseMessage IPersistableModel<AzureChatCompletionResponseMessage>.Create(BinaryData data, ModelReaderWriterOptions options)
    {
        var format = options.Format == "W" ? ((IPersistableModel<ChatCompletionResponseMessage>)this).GetFormatFromOptions(options) : options.Format;

        switch (format)
        {
            case "J":
                {
                    using JsonDocument document = JsonDocument.Parse(data);
                    return DeserializeAzureChatCompletionResponseMessage(document.RootElement, options);
                }
            default:
                throw new FormatException($"The model {nameof(ChatCompletionResponseMessage)} does not support reading '{options.Format}' format.");
        }
    }

    private AzureChatCompletionResponseMessage DeserializeAzureChatCompletionResponseMessage(JsonElement rootElement, ModelReaderWriterOptions options)
    {
        throw new NotImplementedException();
    }

    string IPersistableModel<AzureChatCompletionResponseMessage>.GetFormatFromOptions(ModelReaderWriterOptions options)
        => "J";

    void IJsonModel<AzureChatCompletionResponseMessage>.Write(Utf8JsonWriter writer, ModelReaderWriterOptions options)
    {
        throw new NotImplementedException();
    }

    BinaryData IPersistableModel<AzureChatCompletionResponseMessage>.Write(ModelReaderWriterOptions options)
    {
        throw new NotImplementedException();
    }
}
