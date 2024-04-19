using OpenAI;
using OpenAI.Models;
using System.ClientModel;
using System.ClientModel.Primitives;

namespace AzureOpenAI;

internal class AzureChatClient : Chat
{
    private readonly string _apiVersion;

    // Needed to workaround the fact that generated models don't write 
    // additional values when format is "W".
    private readonly ModelReaderWriterOptions _modelOptions = new("AW");

    internal AzureChatClient(ClientPipeline pipeline, ApiKeyCredential credential, Uri endpoint, string apiVersion)
        : base(pipeline, credential, endpoint)
    {
        _apiVersion = apiVersion;
    }

    public override async Task<ClientResult<CreateChatCompletionResponse>> CreateChatCompletionAsync(CreateChatCompletionRequest createChatCompletionRequest, CancellationToken cancellationToken = default)
    {
        Argument.AssertNotNull(createChatCompletionRequest, nameof(createChatCompletionRequest));

        using BinaryContent content = BinaryContent.Create(createChatCompletionRequest, _modelOptions);
        ClientResult result = await CreateChatCompletionAsync(createChatCompletionRequest.Model.ToString(), content, context: default).ConfigureAwait(false);
        var value = ModelReaderWriter.Read<CreateChatCompletionResponse>(result.GetRawResponse().Content)!;
        return ClientResult.FromValue(value, result.GetRawResponse());
    }

    public override ClientResult<CreateChatCompletionResponse> CreateChatCompletion(CreateChatCompletionRequest createChatCompletionRequest, CancellationToken cancellationToken = default)
    {
        Argument.AssertNotNull(createChatCompletionRequest, nameof(createChatCompletionRequest));

        using BinaryContent content = BinaryContent.Create(createChatCompletionRequest, _modelOptions);
        ClientResult result = CreateChatCompletion(createChatCompletionRequest.Model.ToString(), content, context: default);
        var value = ModelReaderWriter.Read<CreateChatCompletionResponse>(result.GetRawResponse().Content)!;
        return ClientResult.FromValue(value, result.GetRawResponse());
    }

    public override Task<ClientResult> CreateChatCompletionAsync(BinaryContent content, RequestOptions context = null)
    {
        // Note, that we can later remap the values from the 3rd party client format to the 
        // Azure client format, but this has a perf cost and it's an improvement that can 
        // come later.
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
