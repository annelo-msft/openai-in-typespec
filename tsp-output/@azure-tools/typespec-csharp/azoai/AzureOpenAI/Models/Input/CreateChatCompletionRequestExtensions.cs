using OpenAI.Models;
using System.ClientModel;
using System.ClientModel.Primitives;
using System.Diagnostics;
using System.Text.Json;

namespace AzureOpenAI.Models;

public static class CreateChatCompletionRequestExtensions
{
    public static IList<AzureChatExtensionConfiguration> GetDataSources(this CreateChatCompletionRequest request)
    {
        // TODO: How can we validate that this is being called in the right context,
        // e.g. user is using an Azure client instance and not a third-party one?
        // Or does it matter?

        if (request is not IJsonModel model)
        {
            throw new InvalidOperationException("TODO");
        }

        JsonModelList<AzureChatExtensionConfiguration> dataSources;

        if (model.AdditionalProperties.TryGetValue("data_sources", out object? value))
        {
            Debug.Assert(value is JsonModelList<AzureChatExtensionConfiguration>);

            dataSources = (value as JsonModelList<AzureChatExtensionConfiguration>)!;
        }
        else
        {
            dataSources = [];
            model.AdditionalProperties.Add("data_sources", dataSources);
        }

        return dataSources;
    }

    public static BinaryContent ToBinaryContent(this CreateChatCompletionRequest request)
    {
        AzureChatCompletionRequest azureRequest = new(request);
        return BinaryContent.Create(azureRequest, new ModelReaderWriterOptions("W"));
    }

    internal class AzureChatCompletionRequest : IJsonModel<AzureChatCompletionRequest>
    {
        private readonly CreateChatCompletionRequest _request;
        private readonly JsonModelList<AzureChatExtensionConfiguration>? _dataSources;

        public AzureChatCompletionRequest(CreateChatCompletionRequest request)
        {
            _request = request;

            if (request is not IJsonModel model)
            {
                throw new InvalidOperationException("TODO");
            }

            if (model.AdditionalProperties.TryGetValue("data_sources", out object? value))
            {
                Debug.Assert(value is JsonModelList<AzureChatExtensionConfiguration>);

                _dataSources = (value as JsonModelList<AzureChatExtensionConfiguration>)!;
            }
        }

        AzureChatCompletionRequest IJsonModel<AzureChatCompletionRequest>.Create(ref Utf8JsonReader reader, ModelReaderWriterOptions options)
        {
            throw new NotSupportedException("Not supported for input types.");
        }

        AzureChatCompletionRequest IPersistableModel<AzureChatCompletionRequest>.Create(BinaryData data, ModelReaderWriterOptions options)
        {
            throw new NotSupportedException("Not supported for input types.");
        }

        string IPersistableModel<AzureChatCompletionRequest>.GetFormatFromOptions(ModelReaderWriterOptions options)
            => "J";

        void IJsonModel<AzureChatCompletionRequest>.Write(Utf8JsonWriter writer, ModelReaderWriterOptions options)
        {
            writer.WriteStartObject();

            // Note that we write the base model differently in this context
            ((IJsonModel<object>)_request).Write(writer, new ModelReaderWriterOptions("W*"));

            // Write the additional Azure properties
            if (_dataSources is not null)
            {
                ((IJsonModel<object>)_dataSources).Write(writer, new ModelReaderWriterOptions("W"));
            }

            writer.WriteEndObject();
        }

        BinaryData IPersistableModel<AzureChatCompletionRequest>.Write(ModelReaderWriterOptions options)
        {
            var format = options.Format == "W" ? ((IPersistableModel<CreateChatCompletionRequest>)this).GetFormatFromOptions(options) : options.Format;

            return format switch
            {
                "J" => ModelReaderWriter.Write(this, options),
                _ => throw new FormatException($"The model {nameof(CreateChatCompletionRequest)} does not support writing '{options.Format}' format."),
            };
        }
    }
}
