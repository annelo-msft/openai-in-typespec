using OpenAI.Models;
using System;
using System.ClientModel.Primitives;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace AzureOpenAI.Models;

internal partial class AzureCreateChatCompletionResponseChoice : IJsonModel<AzureCreateChatCompletionResponseChoice>
{
    AzureCreateChatCompletionResponseChoice IJsonModel<AzureCreateChatCompletionResponseChoice>.Create(ref Utf8JsonReader reader, ModelReaderWriterOptions options)
    {
        var format = options.Format == "W" ? ((IPersistableModel<CreateCompletionResponseChoice>)this).GetFormatFromOptions(options) : options.Format;
        if (format != "J")
        {
            throw new FormatException($"The model {nameof(CreateCompletionResponseChoice)} does not support reading '{format}' format.");
        }

        using JsonDocument document = JsonDocument.ParseValue(ref reader);
        return DeserializeAzureCreateCompletionResponseChoice(document.RootElement, options);
    }


    AzureCreateChatCompletionResponseChoice IPersistableModel<AzureCreateChatCompletionResponseChoice>.Create(BinaryData data, ModelReaderWriterOptions options)
    {
        var format = options.Format == "W" ? ((IPersistableModel<CreateCompletionResponseChoice>)this).GetFormatFromOptions(options) : options.Format;

        switch (format)
        {
            case "J":
                {
                    using JsonDocument document = JsonDocument.Parse(data);
                    return DeserializeAzureCreateCompletionResponseChoice(document.RootElement, options);
                }
            default:
                throw new FormatException($"The model {nameof(CreateCompletionResponseChoice)} does not support reading '{options.Format}' format.");
        }
    }

    private AzureCreateChatCompletionResponseChoice DeserializeAzureCreateCompletionResponseChoice(JsonElement rootElement, ModelReaderWriterOptions options)
    {
        throw new NotImplementedException();
    }

    string IPersistableModel<AzureCreateChatCompletionResponseChoice>.GetFormatFromOptions(ModelReaderWriterOptions options)
        => "J";

    void IJsonModel<AzureCreateChatCompletionResponseChoice>.Write(Utf8JsonWriter writer, ModelReaderWriterOptions options)
    {
        throw new NotImplementedException();
    }

    BinaryData IPersistableModel<AzureCreateChatCompletionResponseChoice>.Write(ModelReaderWriterOptions options)
    {
        throw new NotImplementedException();
    }
}
