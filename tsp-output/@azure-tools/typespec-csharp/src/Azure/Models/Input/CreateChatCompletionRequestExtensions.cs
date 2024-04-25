using OpenAI.Models;
using System.ClientModel;
using System.ClientModel.Primitives;

namespace AzureOpenAI.Models;

internal static class CreateChatCompletionRequestExtensions
{
    public static BinaryContent ToAzureContent(this CreateChatCompletionRequest request)
    {
        AzureCreateChatCompletionRequest azureRequest = new(request);
        return BinaryContent.Create(azureRequest, new ModelReaderWriterOptions("W"));
    }
}
