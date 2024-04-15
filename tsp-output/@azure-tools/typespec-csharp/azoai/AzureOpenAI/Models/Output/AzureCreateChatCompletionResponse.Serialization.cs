using OpenAI.Models;
using System.ClientModel.Primitives;
using System.Text.Json;

namespace AzureOpenAI.Models;

internal partial class AzureCreateChatCompletionResponse : IJsonModel<AzureCreateChatCompletionResponse>
{
    AzureCreateChatCompletionResponse IJsonModel<AzureCreateChatCompletionResponse>.Create(ref Utf8JsonReader reader, ModelReaderWriterOptions options)
    {
        var format = options.Format == "W" ? ((IPersistableModel<CreateChatCompletionResponse>)this).GetFormatFromOptions(options) : options.Format;
        if (format != "J")
        {
            throw new FormatException($"The model {nameof(CreateChatCompletionResponse)} does not support reading '{format}' format.");
        }

        using JsonDocument document = JsonDocument.ParseValue(ref reader);
        return DeserializeAzureCreateChatCompletionResponse(document.RootElement, options);
    }

    AzureCreateChatCompletionResponse IPersistableModel<AzureCreateChatCompletionResponse>.Create(BinaryData data, ModelReaderWriterOptions options)
    {
        var format = options.Format == "W" ? ((IPersistableModel<CreateChatCompletionResponse>)this).GetFormatFromOptions(options) : options.Format;

        switch (format)
        {
            case "J":
                {
                    using JsonDocument document = JsonDocument.Parse(data);
                    return DeserializeAzureCreateChatCompletionResponse(document.RootElement, options);
                }
            default:
                throw new FormatException($"The model {nameof(CreateChatCompletionResponse)} does not support reading '{options.Format}' format.");
        }
    }

    internal static AzureCreateChatCompletionResponse DeserializeAzureCreateChatCompletionResponse(JsonElement element, ModelReaderWriterOptions options = null)
    {
        throw new NotImplementedException();
    }

    string IPersistableModel<AzureCreateChatCompletionResponse>.GetFormatFromOptions(ModelReaderWriterOptions options)
        => "J";

    void IJsonModel<AzureCreateChatCompletionResponse>.Write(Utf8JsonWriter writer, ModelReaderWriterOptions options)
    {
        throw new NotImplementedException();
    }

    BinaryData IPersistableModel<AzureCreateChatCompletionResponse>.Write(ModelReaderWriterOptions options)
    {
        throw new NotImplementedException();
    }

    /// <summary> Deserializes the model from a raw response. </summary>
    /// <param name="response"> The result to deserialize the model from. </param>
    internal static CreateChatCompletionResponse FromResponse(PipelineResponse response)
    {
        using var document = JsonDocument.Parse(response.Content);
        return DeserializeAzureCreateChatCompletionResponse(document.RootElement);
    }
}
