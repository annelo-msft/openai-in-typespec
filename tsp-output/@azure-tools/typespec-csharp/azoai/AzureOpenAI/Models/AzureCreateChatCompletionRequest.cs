using OpenAI.Models;

namespace AzureOpenAI.Models;

internal class AzureCreateChatCompletionRequest : CreateChatCompletionRequest
{
    // _serializedAdditionalRawData
    private readonly Dictionary<string, BinaryData> _azureProperties;
    internal Dictionary<string, BinaryData> AzureProperties => _azureProperties;

    public AzureCreateChatCompletionRequest(IEnumerable<BinaryData> messages, CreateChatCompletionRequestModel model)
        : base(messages, model)
    {
        _azureProperties = new Dictionary<string, BinaryData>();
    }

    // TODO: Add data_sources field
}
