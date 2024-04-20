using OpenAI.Models;
using System.ClientModel;
using System.ClientModel.Primitives;
using System.Diagnostics;
using System.Text.Json;

namespace AzureOpenAI.Models;

public static class CreateChatCompletionRequestExtensions
{
    private static readonly Dictionary<CreateChatCompletionRequest, JsonModelList<AzureChatExtensionConfiguration>> _dataSources = new();

    public static IList<AzureChatExtensionConfiguration> GetDataSources(this CreateChatCompletionRequest request)
    {
        // Note: no need to validate in this scenario because base client won't serialize
        // result.  It would be a courtesy, otherwise this will fail silently, but won't
        // leak data to the wrong service.

        JsonModelList<AzureChatExtensionConfiguration> dataSources;

        if (_dataSources.TryGetValue(request, out JsonModelList<AzureChatExtensionConfiguration>? value))
        {
            Debug.Assert(value != null);
            dataSources = value!;
        }
        else
        {
            dataSources = new JsonModelList<AzureChatExtensionConfiguration>();
            _dataSources[request] = dataSources;
        }

        return dataSources;
    }

    internal static BinaryContent ToBinaryContent(this CreateChatCompletionRequest request)
    {
        _dataSources.TryGetValue(request, out JsonModelList<AzureChatExtensionConfiguration>? dataSources);
        return BinaryContent.Create(new AzureChatCompletionRequest(request, dataSources));
    }

    internal class AzureChatCompletionRequest : IJsonModel<AzureChatCompletionRequest>
    {
        private readonly CreateChatCompletionRequest _request;
        private readonly JsonModelList<AzureChatExtensionConfiguration>? _dataSources;

        public AzureChatCompletionRequest(CreateChatCompletionRequest request, JsonModelList<AzureChatExtensionConfiguration>? dataSources)
        {
            _request = request;
            _dataSources = dataSources;
        }

        AzureChatCompletionRequest IJsonModel<AzureChatCompletionRequest>.Create(ref Utf8JsonReader reader, ModelReaderWriterOptions options)
        {
            throw new NotImplementedException();
        }

        AzureChatCompletionRequest IPersistableModel<AzureChatCompletionRequest>.Create(BinaryData data, ModelReaderWriterOptions options)
        {
            throw new NotImplementedException();
        }

        string IPersistableModel<AzureChatCompletionRequest>.GetFormatFromOptions(ModelReaderWriterOptions options)
            => "J";

        void IJsonModel<AzureChatCompletionRequest>.Write(Utf8JsonWriter writer, ModelReaderWriterOptions options)
        {
            writer.WriteStartObject();
            ((IJsonModel<object>)_request).Write(writer, new ModelReaderWriterOptions("W*"));
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
