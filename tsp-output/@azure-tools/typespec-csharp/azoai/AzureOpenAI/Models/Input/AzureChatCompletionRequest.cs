using OpenAI.Models;
using System.ClientModel.Primitives;
using System.Diagnostics;
using System.Text.Json;

namespace AzureOpenAI.Models.Input
{
    internal class AzureCreateChatCompletionRequest : IJsonModel<AzureCreateChatCompletionRequest>
    {
        private readonly CreateChatCompletionRequest _request;
        private readonly JsonModelList<AzureChatExtensionConfiguration>? _dataSources;

        public AzureCreateChatCompletionRequest(CreateChatCompletionRequest request)
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
            if (_dataSources is not null)
            {
                writer.WritePropertyName("data_sources");
                ((IJsonModel<object>)_dataSources).Write(writer, new ModelReaderWriterOptions("W"));
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
}
