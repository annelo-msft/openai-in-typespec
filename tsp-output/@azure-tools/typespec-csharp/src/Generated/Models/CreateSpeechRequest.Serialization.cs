// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using System.ClientModel.Primitives;
using System.Collections.Generic;
using System.Text.Json;
using Azure;
using Azure.Core;

namespace OpenAI.Models
{
    public partial class CreateSpeechRequest : IUtf8JsonSerializable, IJsonModel<CreateSpeechRequest>
    {
        void IUtf8JsonSerializable.Write(Utf8JsonWriter writer) => ((IJsonModel<CreateSpeechRequest>)this).Write(writer, new ModelReaderWriterOptions("W"));

        void IJsonModel<CreateSpeechRequest>.Write(Utf8JsonWriter writer, ModelReaderWriterOptions options)
        {
            var format = options.Format == "W" ? ((IPersistableModel<CreateSpeechRequest>)this).GetFormatFromOptions(options) : options.Format;
            if (format != "J")
            {
                throw new FormatException($"The model {nameof(CreateSpeechRequest)} does not support '{format}' format.");
            }

            writer.WriteStartObject();
            writer.WritePropertyName("model"u8);
            writer.WriteStringValue(Model.ToString());
            writer.WritePropertyName("input"u8);
            writer.WriteStringValue(Input);
            writer.WritePropertyName("voice"u8);
            writer.WriteStringValue(Voice.ToString());
            if (Optional.IsDefined(ResponseFormat))
            {
                writer.WritePropertyName("response_format"u8);
                writer.WriteStringValue(ResponseFormat.Value.ToString());
            }
            if (Optional.IsDefined(Speed))
            {
                writer.WritePropertyName("speed"u8);
                writer.WriteNumberValue(Speed.Value);
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

        CreateSpeechRequest IJsonModel<CreateSpeechRequest>.Create(ref Utf8JsonReader reader, ModelReaderWriterOptions options)
        {
            var format = options.Format == "W" ? ((IPersistableModel<CreateSpeechRequest>)this).GetFormatFromOptions(options) : options.Format;
            if (format != "J")
            {
                throw new FormatException($"The model {nameof(CreateSpeechRequest)} does not support '{format}' format.");
            }

            using JsonDocument document = JsonDocument.ParseValue(ref reader);
            return DeserializeCreateSpeechRequest(document.RootElement, options);
        }

        internal static CreateSpeechRequest DeserializeCreateSpeechRequest(JsonElement element, ModelReaderWriterOptions options = null)
        {
            options ??= new ModelReaderWriterOptions("W");

            if (element.ValueKind == JsonValueKind.Null)
            {
                return null;
            }
            CreateSpeechRequestModel model = default;
            string input = default;
            CreateSpeechRequestVoice voice = default;
            Optional<CreateSpeechRequestResponseFormat> responseFormat = default;
            Optional<double> speed = default;
            IDictionary<string, BinaryData> serializedAdditionalRawData = default;
            Dictionary<string, BinaryData> additionalPropertiesDictionary = new Dictionary<string, BinaryData>();
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("model"u8))
                {
                    model = new CreateSpeechRequestModel(property.Value.GetString());
                    continue;
                }
                if (property.NameEquals("input"u8))
                {
                    input = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("voice"u8))
                {
                    voice = new CreateSpeechRequestVoice(property.Value.GetString());
                    continue;
                }
                if (property.NameEquals("response_format"u8))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        continue;
                    }
                    responseFormat = new CreateSpeechRequestResponseFormat(property.Value.GetString());
                    continue;
                }
                if (property.NameEquals("speed"u8))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        continue;
                    }
                    speed = property.Value.GetDouble();
                    continue;
                }
                if (options.Format != "W")
                {
                    additionalPropertiesDictionary.Add(property.Name, BinaryData.FromString(property.Value.GetRawText()));
                }
            }
            serializedAdditionalRawData = additionalPropertiesDictionary;
            return new CreateSpeechRequest(model, input, voice, Optional.ToNullable(responseFormat), Optional.ToNullable(speed), serializedAdditionalRawData);
        }

        BinaryData IPersistableModel<CreateSpeechRequest>.Write(ModelReaderWriterOptions options)
        {
            var format = options.Format == "W" ? ((IPersistableModel<CreateSpeechRequest>)this).GetFormatFromOptions(options) : options.Format;

            switch (format)
            {
                case "J":
                    return ModelReaderWriter.Write(this, options);
                default:
                    throw new FormatException($"The model {nameof(CreateSpeechRequest)} does not support '{options.Format}' format.");
            }
        }

        CreateSpeechRequest IPersistableModel<CreateSpeechRequest>.Create(BinaryData data, ModelReaderWriterOptions options)
        {
            var format = options.Format == "W" ? ((IPersistableModel<CreateSpeechRequest>)this).GetFormatFromOptions(options) : options.Format;

            switch (format)
            {
                case "J":
                    {
                        using JsonDocument document = JsonDocument.Parse(data);
                        return DeserializeCreateSpeechRequest(document.RootElement, options);
                    }
                default:
                    throw new FormatException($"The model {nameof(CreateSpeechRequest)} does not support '{options.Format}' format.");
            }
        }

        string IPersistableModel<CreateSpeechRequest>.GetFormatFromOptions(ModelReaderWriterOptions options) => "J";

        /// <summary> Deserializes the model from a raw response. </summary>
        /// <param name="response"> The response to deserialize the model from. </param>
        internal static CreateSpeechRequest FromResponse(Response response)
        {
            using var document = JsonDocument.Parse(response.Content);
            return DeserializeCreateSpeechRequest(document.RootElement);
        }

        /// <summary> Convert into a Utf8JsonRequestContent. </summary>
        internal virtual RequestContent ToRequestContent()
        {
            var content = new Utf8JsonRequestContent();
            content.JsonWriter.WriteObjectValue(this);
            return content;
        }
    }
}
