using OpenAI;
using System.ClientModel;
using System.ClientModel.Primitives;

namespace AzureOpenAI;

internal class AzureChatClient : Chat
{
    private readonly string _apiVersion;

    protected override ModelReaderWriterOptions ModelReaderWriterOptions => new("AzureWire");

    internal AzureChatClient(ClientPipeline pipeline, ApiKeyCredential credential, Uri endpoint, string apiVersion)
        : base(pipeline, credential, endpoint)
    {
        _apiVersion = apiVersion;
    }

    // TODO: Show how this would differ for this case.  Do we still need OperationName and the
    // remapping policy?
    //   1. Version parameter
    //   2. Auth key is different - does that show up here or in the client?
    //   3. DeploymentId in path
    // 
    // Note: Model content is already serialized by the time we get here.  Nothing 
    // content-related should happen in this method.  If we can show that, do we need
    // to make these methods protected virtual?
    protected override PipelineMessage CreateCreateChatCompletionRequest(string model, BinaryContent content, RequestOptions context)
    {
        var message = Pipeline.CreateMessage();
        message.ResponseClassifier = PipelineMessageClassifier200;

        var request = message.Request;
        request.Method = "POST";

        var uri = new ClientUriBuilder();
        uri.Reset(Endpoint);
        uri.AppendPath("/openai/deployments/", false);
        uri.AppendPath(model, false);
        uri.AppendPath("/chat/completions", false);
        uri.AppendQuery("api-version", _apiVersion, true);
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
