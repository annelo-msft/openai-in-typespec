using AzureOpenAI.Models;
using OpenAI;
using OpenAI.Models;
using System;
using System.ClientModel;
using System.ClientModel.Primitives;
using System.Threading;
using System.Threading.Tasks;

namespace AzureOpenAI;

internal class AzureChatClient : Chat
{
    private readonly string _apiVersion;
    private readonly Uri _endpoint;

    internal AzureChatClient(ClientPipeline pipeline, ApiKeyCredential credential, Uri endpoint, string apiVersion)
        : base(pipeline, credential, endpoint)
    {
        _apiVersion = apiVersion;
        _endpoint = endpoint;
    }

    public override async Task<ClientResult<CreateChatCompletionResponse>> CreateChatCompletionAsync(CreateChatCompletionRequest createChatCompletionRequest, CancellationToken cancellationToken = default)
    {
        Argument.AssertNotNull(createChatCompletionRequest, nameof(createChatCompletionRequest));

        using BinaryContent content = createChatCompletionRequest.ToAzureContent();
        ClientResult result = await CreateChatCompletionAsync(content, context: default).ConfigureAwait(false);
        var value = ModelReaderWriter.Read<CreateChatCompletionResponse>(result.GetRawResponse().Content)!;
        return ClientResult.FromValue(value, result.GetRawResponse());
    }

    public override ClientResult<CreateChatCompletionResponse> CreateChatCompletion(CreateChatCompletionRequest createChatCompletionRequest, CancellationToken cancellationToken = default)
    {
        Argument.AssertNotNull(createChatCompletionRequest, nameof(createChatCompletionRequest));

        using BinaryContent content = createChatCompletionRequest.ToAzureContent();
        ClientResult result = CreateChatCompletion(content, context: default);
        var value = ModelReaderWriter.Read<CreateChatCompletionResponse>(result.GetRawResponse().Content)!;
        return ClientResult.FromValue(value, result.GetRawResponse());
    }

    public override async Task<ClientResult> CreateChatCompletionAsync(BinaryContent content, RequestOptions context = null)
    {
        Argument.AssertNotNull(content, nameof(content));

        using PipelineMessage message = CreateCreateChatCompletionRequest(content, context);
        return ClientResult.FromResponse(await Pipeline.ProcessMessageAsync(message, context).ConfigureAwait(false));
    }

    public override ClientResult CreateChatCompletion(BinaryContent content, RequestOptions context = null)
    {
        Argument.AssertNotNull(content, nameof(content));

        using PipelineMessage message = CreateCreateChatCompletionRequest(content, context);
        return ClientResult.FromResponse(Pipeline.ProcessMessage(message, context));
    }

    private PipelineMessage CreateCreateChatCompletionRequest(BinaryContent content, RequestOptions context)
    {
        var message = Pipeline.CreateMessage();
        message.ResponseClassifier = PipelineMessageClassifier200;

        var request = message.Request;
        request.Method = "POST";

        var uri = new ClientUriBuilder();
        uri.Reset(_endpoint);
        uri.AppendPath("/openai/deployments/", false);

        // TODO: this comes from field on client.
        uri.AppendPath("model", false);

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
