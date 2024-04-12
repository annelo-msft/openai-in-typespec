using OpenAI;
using OpenAI.Models;
using System.ClientModel;
using System.ClientModel.Primitives;

namespace AzureOpenAI;

internal class AzureChatClient : Chat
{
    private readonly string _apiVersion;

    // TODO/Note: doesn't need to be virtual -- derived client could also set it in the 
    // constructor.  Is one approach better?  I guess setting it in the constructor
    // requires making it settable which maybe we don't want.
    protected override ModelReaderWriterOptions ModelReaderWriterOptions => new("AzureWire");

    internal AzureChatClient(ClientPipeline pipeline, ApiKeyCredential credential, Uri endpoint, string apiVersion)
        : base(pipeline, credential, endpoint)
    {
        _apiVersion = apiVersion;
    }

    public override Task<ClientResult<CreateChatCompletionResponse>> CreateChatCompletionAsync(CreateChatCompletionRequest createChatCompletionRequest, CancellationToken cancellationToken = default)
    {
        return base.CreateChatCompletionAsync(createChatCompletionRequest, cancellationToken);
    }

    public override ClientResult<CreateChatCompletionResponse> CreateChatCompletion(CreateChatCompletionRequest createChatCompletionRequest, CancellationToken cancellationToken = default)
    {
        return base.CreateChatCompletion(createChatCompletionRequest, cancellationToken);
    }

    public override Task<ClientResult> CreateChatCompletionAsync(BinaryContent content, RequestOptions context = null)
    {
        throw new InvalidOperationException("Improperly formatted content -- Azure service requires different format. " +
            $"Please consult the REST API documentation and call the '{nameof(CreateChatCompletion)}' method overload that " +
            "takes a 'model' parameter.");
    }

    public override ClientResult CreateChatCompletion(BinaryContent content, RequestOptions context = null)
    {
        throw new InvalidOperationException("Improperly formatted content -- Azure service requires different format. " +
            $"Please consult the REST API documentation and call the '{nameof(CreateChatCompletion)}' method overload that " +
            "takes a 'model' parameter.");
    }

    public ClientResult CreateChatCompletion(string model, BinaryContent content, RequestOptions context = null)
    {
        Argument.AssertNotNull(model, nameof(model));
        Argument.AssertNotNull(content, nameof(content));

        using PipelineMessage message = CreateCreateChatCompletionRequest(model, content, context);
        return ClientResult.FromResponse(Pipeline.ProcessMessage(message, context));
    }

    public async Task<ClientResult> CreateChatCompletionAsync(string model, BinaryContent content, RequestOptions context = null)
    {
        Argument.AssertNotNull(model, nameof(model));
        Argument.AssertNotNull(content, nameof(content));

        using PipelineMessage message = CreateCreateChatCompletionRequest(model, content, context);
        return ClientResult.FromResponse(await Pipeline.ProcessMessageAsync(message, context).ConfigureAwait(false));
    }

    private PipelineMessage CreateCreateChatCompletionRequest(string model, BinaryContent content, RequestOptions context)
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
