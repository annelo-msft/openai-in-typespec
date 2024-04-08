using OpenAI;
using System.ClientModel;
using System.ClientModel.Primitives;

namespace AzureOpenAI;

public class AzureChatClient : Chat
{
    internal AzureChatClient(ClientPipeline pipeline, ApiKeyCredential credential, Uri endpoint)
        : base(pipeline, credential, endpoint)
    {
    }

    protected override PipelineMessage CreateCreateChatCompletionRequest(BinaryContent content, RequestOptions context)
    {
        var message = Pipeline.CreateMessage();
        message.ResponseClassifier = PipelineMessageClassifier200;

        var request = message.Request;
        request.Method = "POST";

        var uri = new ClientUriBuilder();
        uri.Reset(Endpoint);
        uri.AppendPath("/chat/completions", false);
        request.Uri = uri.ToUri();

        request.Headers.Set("Accept", "application/json");
        request.Headers.Set("Content-Type", "application/json");

        request.Content = content;

        if (context != null)
        {
            message.Apply(context);
        }

        return message;
    }

    private static PipelineMessageClassifier? _pipelineMessageClassifier200;
    private static PipelineMessageClassifier PipelineMessageClassifier200 => _pipelineMessageClassifier200 ??= PipelineMessageClassifier.Create(stackalloc ushort[] { 200 });
}
