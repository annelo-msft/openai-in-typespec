using OpenAI.Models;
using System.ClientModel.Primitives;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;

namespace AzureOpenAI.Models;

internal class AzureCreateChatCompletionRequest : IJsonModel<AzureCreateChatCompletionRequest>
{
    private readonly CreateChatCompletionRequest _request;

    public AzureCreateChatCompletionRequest(CreateChatCompletionRequest request)
    {
        _request = request;

        // TODO: Argument assert instead?
        if (request is not IJsonModel)
        {
            throw new InvalidOperationException("TODO");
        }
    }

    AzureCreateChatCompletionRequest IJsonModel<AzureCreateChatCompletionRequest>.Create(ref Utf8JsonReader reader, ModelReaderWriterOptions options)
    {
        throw new NotSupportedException("Not supported for input types.");
    }

    AzureCreateChatCompletionRequest IPersistableModel<AzureCreateChatCompletionRequest>.Create(BinaryData data, ModelReaderWriterOptions options)
    {
        throw new NotSupportedException("Not supported for input types.");
    }

    string IPersistableModel<AzureCreateChatCompletionRequest>.GetFormatFromOptions(ModelReaderWriterOptions options)
        => "J";

    void IJsonModel<AzureCreateChatCompletionRequest>.Write(Utf8JsonWriter writer, ModelReaderWriterOptions options)
    {
        writer.WriteStartObject();

        // Note that we write the base model differently in this context
        ((IJsonModel<object>)_request).Write(writer, new ModelReaderWriterOptions("W*"));

        // Write the additional Azure properties

        if (((IJsonModel)_request).AdditionalProperties.TryGetValue("data_sources", out BinaryData? value))
        {
            writer.WritePropertyName("data_sources");
#if NET6_0_OR_GREATER
            writer.WriteRawValue(value);
#else
            using (JsonDocument document = JsonDocument.Parse(value))
            {
                JsonSerializer.Serialize(writer, document.RootElement);
            }
#endif
        }

        writer.WriteEndObject();
    }

    BinaryData IPersistableModel<AzureCreateChatCompletionRequest>.Write(ModelReaderWriterOptions options)
    {
        var format = options.Format == "W" ? ((IPersistableModel<CreateChatCompletionRequest>)this).GetFormatFromOptions(options) : options.Format;

        return format switch
        {
            "J" => ModelReaderWriter.Write(this, options),
            _ => throw new FormatException($"The model {nameof(CreateChatCompletionRequest)} does not support writing '{options.Format}' format."),
        };
    }
}
