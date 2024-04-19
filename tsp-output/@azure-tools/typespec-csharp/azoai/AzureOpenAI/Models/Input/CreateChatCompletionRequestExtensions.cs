using OpenAI.Models;
using System.Diagnostics;

namespace AzureOpenAI.Models;

public static class CreateChatCompletionRequestExtensions
{
    public static IList<AzureChatExtensionConfiguration> GetDataSources(this CreateChatCompletionRequest request)
    {
        // TODO: How can we validate that this is being called in the right context,
        // e.g. user is using an Azure client instance and not a third-party one?

        JsonModelList<AzureChatExtensionConfiguration> dataSources;

        if (request.SerializedAdditionalRawData.TryGetValue("data_sources", out object? value))
        {
            Debug.Assert(value is JsonModelList<AzureChatExtensionConfiguration>);

            dataSources = (value as JsonModelList<AzureChatExtensionConfiguration>)!;
        }
        else
        {
            dataSources = [];
            request.SerializedAdditionalRawData.Add("data_sources", dataSources);
        }

        return dataSources;
    }
}
