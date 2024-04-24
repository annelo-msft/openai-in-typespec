using OpenAI.Models;
using System.ClientModel;
using System.ClientModel.Primitives;

namespace AzureOpenAI.Models;

public static class CreateChatCompletionRequestExtensions
{
    public static IList<AzureChatExtensionConfiguration> GetDataSources(this CreateChatCompletionRequest request)
    {
        if (request is not JsonModel<CreateChatCompletionRequest> model)
        {
            throw new InvalidOperationException("TODO");
        }

        JsonModelList<AzureChatExtensionConfiguration> dataSources;

        if (model.TryGetUnknownProperty("data_sources", out JsonModelList<AzureChatExtensionConfiguration>? value))
        {
            dataSources = value!;
        }
        else
        {
            dataSources = [];
            model.SetUnknownProperty("data_sources", dataSources);
        }

        return dataSources;
    }

    public static BinaryContent ToBinaryContent(this CreateChatCompletionRequest request)
    {
        AzureCreateChatCompletionRequest azureRequest = new(request);
        return BinaryContent.Create(azureRequest, new ModelReaderWriterOptions("W"));
    }
}
