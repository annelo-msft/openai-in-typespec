using OpenAI.Models;
using System.ClientModel;
using System.ClientModel.Primitives;
using System.Text.Json;

#nullable disable

namespace AzureOpenAI.Models;

internal partial class AzureCreateChatCompletionRequest : IJsonModel<AzureCreateChatCompletionRequest>
{
    AzureCreateChatCompletionRequest IJsonModel<AzureCreateChatCompletionRequest>.Create(ref Utf8JsonReader reader, ModelReaderWriterOptions options)
    {
        throw new NotImplementedException();
    }

    AzureCreateChatCompletionRequest IPersistableModel<AzureCreateChatCompletionRequest>.Create(BinaryData data, ModelReaderWriterOptions options)
    {
        throw new NotImplementedException();
    }

    string IPersistableModel<AzureCreateChatCompletionRequest>.GetFormatFromOptions(ModelReaderWriterOptions options)
        => "J";

    void IJsonModel<AzureCreateChatCompletionRequest>.Write(Utf8JsonWriter writer, ModelReaderWriterOptions options)
    {
        var format = options.Format == "W" ? ((IPersistableModel<AzureCreateChatCompletionRequest>)this).GetFormatFromOptions(options) : options.Format;
        if (format != "J")
        {
            throw new FormatException($"The model {nameof(AzureCreateChatCompletionRequest)} does not support writing '{format}' format.");
        }

        writer.WriteStartObject();
        writer.WritePropertyName("messages"u8);
        writer.WriteStartArray();
        foreach (var item in Messages)
        {
            // TODO: does this have an Azure-specific type we should write instead?
            writer.WriteObjectValue<BinaryData>(item, options);
        }
        writer.WriteEndArray();
        if (Optional.IsCollectionDefined(Functions))
        {
            writer.WritePropertyName("functions"u8);
            writer.WriteStartArray();
            foreach (var item in Functions)
            {
                writer.WriteObjectValue<ChatCompletionFunctions>(item, options);
            }
            writer.WriteEndArray();
        }
        if (Optional.IsDefined(FunctionCall))
        {
            // TODO: Address this custom serialization

            writer.WritePropertyName("function_call"u8);
            writer.WriteObjectValue<BinaryData>(FunctionCall, options);

            // CUSTOM CODE NOTE:
            //   This is an important custom deserialization step for the intended merging of presets (none, auto)
            //   and named function definitions. Because presets serialize as a string instead of as object contents,
            //   the customization has to occur at this parent options level.

            //writer.WritePropertyName("function_call"u8);
            //if (FunctionCall.IsPredefined)
            //{
            //    writer.WriteStringValue(FunctionCall.Name);
            //}
            //else
            //{
            //    writer.WriteStartObject();
            //    writer.WritePropertyName("name");
            //    writer.WriteStringValue(FunctionCall.Name);
            //    writer.WriteEndObject();
            //}
        }
        if (Optional.IsDefined(MaxTokens))
        {
            writer.WritePropertyName("max_tokens"u8);
            writer.WriteNumberValue(MaxTokens.Value);
        }
        if (Optional.IsDefined(Temperature))
        {
            writer.WritePropertyName("temperature"u8);
            writer.WriteNumberValue(Temperature.Value);
        }

        // TODO: what are these ones?
        //if (Optional.IsDefined(NucleusSamplingFactor))
        //{
        //    writer.WritePropertyName("top_p"u8);
        //    writer.WriteNumberValue(NucleusSamplingFactor.Value);
        //}
        //if (Optional.IsCollectionDefined(TokenSelectionBiases))
        //{
        //    writer.WritePropertyName("logit_bias"u8);
        //    SerializeTokenSelectionBiases(writer);
        //}
        //if (Optional.IsDefined(User))
        //{
        //    writer.WritePropertyName("user"u8);
        //    writer.WriteStringValue(User);
        //}
        //if (Optional.IsDefined(ChoiceCount))
        //{
        //    writer.WritePropertyName("n"u8);
        //    writer.WriteNumberValue(ChoiceCount.Value);
        //}
        //if (Optional.IsCollectionDefined(StopSequences))
        //{
        //    writer.WritePropertyName("stop"u8);
        //    writer.WriteStartArray();
        //    foreach (var item in StopSequences)
        //    {
        //        writer.WriteStringValue(item);
        //    }
        //    writer.WriteEndArray();
        //}
        if (Optional.IsDefined(PresencePenalty))
        {
            writer.WritePropertyName("presence_penalty"u8);
            writer.WriteNumberValue(PresencePenalty.Value);
        }
        if (Optional.IsDefined(FrequencyPenalty))
        {
            writer.WritePropertyName("frequency_penalty"u8);
            writer.WriteNumberValue(FrequencyPenalty.Value);
        }
        //if (Optional.IsDefined(InternalShouldStreamResponse))
        //{
        //    writer.WritePropertyName("stream"u8);
        //    writer.WriteBooleanValue(InternalShouldStreamResponse.Value);
        //}

        // Note: We don't need to preserve the name "DeploymentName" in the
        // public API because this model type is not public.  We can just use
        // the property on the base type instead.
        if (Optional.IsDefined(Model))
        {
            writer.WritePropertyName("model"u8);
            writer.WriteStringValue(Model.ToString());
        }
        //if (AzureExtensionsOptions != null)
        //{
        //    // CUSTOM CODE NOTE: Extensions options currently deserialize directly into the payload (not as a
        //    //                      property value therein)
        //    ((IUtf8JsonSerializable)AzureExtensionsOptions).Write(writer);
        //}

        // TODO: add this as Azure custom property
        //if (Optional.IsDefined(Enhancements))
        //{
        //    writer.WritePropertyName("enhancements"u8);
        //    writer.WriteObjectValue<AzureChatEnhancementConfiguration>(Enhancements, options);
        //}
        if (Optional.IsDefined(Seed))
        {
            writer.WritePropertyName("seed"u8);
            writer.WriteNumberValue(Seed.Value);
        }
        //if (Optional.IsDefined(EnableLogProbabilities))
        //{
        //    writer.WritePropertyName("logprobs"u8);
        //    writer.WriteBooleanValue(EnableLogProbabilities.Value);
        //}
        //if (Optional.IsDefined(LogProbabilitiesPerToken))
        //{
        //    writer.WritePropertyName("top_logprobs"u8);
        //    writer.WriteNumberValue(LogProbabilitiesPerToken.Value);
        //}
        if (Optional.IsDefined(ResponseFormat))
        {
            writer.WritePropertyName("response_format"u8);
            writer.WriteObjectValue<CreateChatCompletionRequestResponseFormat>(ResponseFormat, options);
        }

        // Note: if nested models have different ways to serialize as well,
        // we will need to create parallel Azure model types for those as well.
        if (Optional.IsCollectionDefined(Tools))
        {
            writer.WritePropertyName("tools"u8);
            writer.WriteStartArray();
            foreach (var item in Tools)
            {
                writer.WriteObjectValue<ChatCompletionTool>(item, options);
            }
            writer.WriteEndArray();
        }
        if (Optional.IsDefined(ToolChoice))
        {
            // TODO: Address this:
            // CUSTOM CODE NOTE:
            //   ChatCompletionsToolChoice is a fully custom type and needs integrated custom serialization here.
            //writer.WritePropertyName("tool_choice"u8);
            //writer.WriteObjectValue<ChatCompletionsToolChoice>(ToolChoice, options);

            writer.WritePropertyName("tool_choice"u8);
            writer.WriteObjectValue<BinaryData>(ToolChoice, options);
        }
        if (options.Format != "W" && SerializedAdditionalRawData != null)
        {
            foreach (var item in SerializedAdditionalRawData)
            {
                if (item.Value is not BinaryData serializedValue)
                {
                    throw new InvalidOperationException("_serializedAdditionalRawData should not hold un-serialized values at the time of serialization.");
                }

                writer.WritePropertyName(item.Key);
#if NET6_0_OR_GREATER
                writer.WriteRawValue(serializedValue);
#else
                using (JsonDocument document = JsonDocument.Parse(serializedValue))
                {
                    JsonSerializer.Serialize(writer, document.RootElement);
                }
#endif
            }
        }
        writer.WriteEndObject();
    }

    BinaryData IPersistableModel<AzureCreateChatCompletionRequest>.Write(ModelReaderWriterOptions options)
    {
        var format = options.Format == "W" ? ((IPersistableModel<AzureCreateChatCompletionRequest>)this).GetFormatFromOptions(options) : options.Format;

        switch (format)
        {
            case "J":
                return ModelReaderWriter.Write(this, options);
            default:
                throw new FormatException($"The model {nameof(AzureCreateChatCompletionRequest)} does not support writing '{options.Format}' format.");
        }
    }

    /// <summary> Convert into a Utf8JsonRequestBody. </summary>
    internal virtual BinaryContent ToBinaryBody()
    {
        return BinaryContent.Create(this, new ModelReaderWriterOptions("W"));
    }
}
