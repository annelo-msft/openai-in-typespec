using OpenAI.Models;
using System.ClientModel;
using System.ClientModel.Primitives;
using System.Diagnostics;

namespace AzureOpenAI.Models;

public static class CreateChatCompletionRequestExtensions
{
    public static IList<AzureChatExtensionConfiguration> GetDataSources(this CreateChatCompletionRequest request)
    {
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
        AzureCreateChatCompletionRequest azureRequest = new(request);
        return BinaryContent.Create(azureRequest, new ModelReaderWriterOptions("W"));
    }
}
