// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.ClientModel.Primitives;
using System.Text.Json;

namespace AzureOpenAI.Models;

public partial class AzureChatExtensionsMessageContext :  IJsonModel<AzureChatExtensionsMessageContext>
{
    void IJsonModel<AzureChatExtensionsMessageContext>.Write(Utf8JsonWriter writer, ModelReaderWriterOptions options)
    {
        var format = options.Format == "W" ? ((IPersistableModel<AzureChatExtensionsMessageContext>)this).GetFormatFromOptions(options) : options.Format;
        if (format != "J")
        {
            throw new FormatException($"The model {nameof(AzureChatExtensionsMessageContext)} does not support writing '{format}' format.");
        }

        writer.WriteStartObject();
        if (Optional.IsCollectionDefined(Citations))
        {
            writer.WritePropertyName("citations"u8);
            writer.WriteStartArray();
            foreach (var item in Citations)
            {
                writer.WriteObjectValue<AzureChatExtensionDataSourceResponseCitation>(item, options);
            }
            writer.WriteEndArray();
        }
        if (Optional.IsDefined(Intent))
        {
            writer.WritePropertyName("intent"u8);
            writer.WriteStringValue(Intent);
        }
        if (options.Format != "W" && _serializedAdditionalRawData != null)
        {
            foreach (var item in _serializedAdditionalRawData)
            {
                writer.WritePropertyName(item.Key);
#if NET6_0_OR_GREATER
				writer.WriteRawValue(item.Value);
#else
                using (JsonDocument document = JsonDocument.Parse(item.Value))
                {
                    JsonSerializer.Serialize(writer, document.RootElement);
                }
#endif
            }
        }
        writer.WriteEndObject();
    }

    AzureChatExtensionsMessageContext IJsonModel<AzureChatExtensionsMessageContext>.Create(ref Utf8JsonReader reader, ModelReaderWriterOptions options)
    {
        var format = options.Format == "W" ? ((IPersistableModel<AzureChatExtensionsMessageContext>)this).GetFormatFromOptions(options) : options.Format;
        if (format != "J")
        {
            throw new FormatException($"The model {nameof(AzureChatExtensionsMessageContext)} does not support reading '{format}' format.");
        }

        using JsonDocument document = JsonDocument.ParseValue(ref reader);
        return DeserializeAzureChatExtensionsMessageContext(document.RootElement, options);
    }

    internal static AzureChatExtensionsMessageContext DeserializeAzureChatExtensionsMessageContext(JsonElement element, ModelReaderWriterOptions options = null)
    {
        options ??= new ModelReaderWriterOptions("W");

        if (element.ValueKind == JsonValueKind.Null)
        {
            return null;
        }
        IReadOnlyList<AzureChatExtensionDataSourceResponseCitation> citations = default;
        string intent = default;
        IDictionary<string, BinaryData> serializedAdditionalRawData = default;
        Dictionary<string, BinaryData> rawDataDictionary = new Dictionary<string, BinaryData>();
        foreach (var property in element.EnumerateObject())
        {
            if (property.NameEquals("citations"u8))
            {
                if (property.Value.ValueKind == JsonValueKind.Null)
                {
                    continue;
                }
                List<AzureChatExtensionDataSourceResponseCitation> array = new List<AzureChatExtensionDataSourceResponseCitation>();
                foreach (var item in property.Value.EnumerateArray())
                {
                    array.Add(AzureChatExtensionDataSourceResponseCitation.DeserializeAzureChatExtensionDataSourceResponseCitation(item, options));
                }
                citations = array;
                continue;
            }
            if (property.NameEquals("intent"u8))
            {
                intent = property.Value.GetString();
                continue;
            }
            if (options.Format != "W")
            {
                rawDataDictionary.Add(property.Name, BinaryData.FromString(property.Value.GetRawText()));
            }
        }
        serializedAdditionalRawData = rawDataDictionary;
        return new AzureChatExtensionsMessageContext(citations ?? new ChangeTrackingList<AzureChatExtensionDataSourceResponseCitation>(), intent, serializedAdditionalRawData);
    }

    BinaryData IPersistableModel<AzureChatExtensionsMessageContext>.Write(ModelReaderWriterOptions options)
    {
        var format = options.Format == "W" ? ((IPersistableModel<AzureChatExtensionsMessageContext>)this).GetFormatFromOptions(options) : options.Format;

        switch (format)
        {
            case "J":
                return ModelReaderWriter.Write(this, options);
            default:
                throw new FormatException($"The model {nameof(AzureChatExtensionsMessageContext)} does not support writing '{options.Format}' format.");
        }
    }

    AzureChatExtensionsMessageContext IPersistableModel<AzureChatExtensionsMessageContext>.Create(BinaryData data, ModelReaderWriterOptions options)
    {
        var format = options.Format == "W" ? ((IPersistableModel<AzureChatExtensionsMessageContext>)this).GetFormatFromOptions(options) : options.Format;

        switch (format)
        {
            case "J":
                {
                    using JsonDocument document = JsonDocument.Parse(data);
                    return DeserializeAzureChatExtensionsMessageContext(document.RootElement, options);
                }
            default:
                throw new FormatException($"The model {nameof(AzureChatExtensionsMessageContext)} does not support reading '{options.Format}' format.");
        }
    }

    string IPersistableModel<AzureChatExtensionsMessageContext>.GetFormatFromOptions(ModelReaderWriterOptions options) => "J";

    ///// <summary> Deserializes the model from a raw response. </summary>
    ///// <param name="response"> The response to deserialize the model from. </param>
    //internal static AzureChatExtensionsMessageContext FromResponse(Response response)
    //{
    //    using var document = JsonDocument.Parse(response.Content);
    //    return DeserializeAzureChatExtensionsMessageContext(document.RootElement);
    //}

    ///// <summary> Convert into a Utf8JsonRequestContent. </summary>
    //internal virtual RequestContent ToRequestContent()
    //{
    //    var content = new Utf8JsonRequestContent();
    //    content.JsonWriter.WriteObjectValue<AzureChatExtensionsMessageContext>(this, new ModelReaderWriterOptions("W"));
    //    return content;
    //}
}
