using OpenAI.Models;
using System.Diagnostics;

namespace AzureOpenAI.Models;

internal partial class AzureCreateChatCompletionRequest : CreateChatCompletionRequest
{
    public AzureCreateChatCompletionRequest(CreateChatCompletionRequest request)
        : base(request)
    {
        // Initialize Azure-specific properties

        // Azure 'data_sources' property
        if (SerializedAdditionalRawData.TryGetValue("data_sources", out object? value))
        {
            Debug.Assert(value is JsonModelList<AzureChatExtensionConfiguration>);

            DataSources = (value as JsonModelList<AzureChatExtensionConfiguration>)!;

            SerializedAdditionalRawData.Remove("data_sources");
        }
        else
        {
            DataSources = [];
        }

        // Azure 'enhancements' property
        // TODO: add this.
    }

    public IList<AzureChatExtensionConfiguration> DataSources { get; }
}
