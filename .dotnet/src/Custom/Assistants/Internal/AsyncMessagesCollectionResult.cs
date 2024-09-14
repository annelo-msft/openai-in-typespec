using System;
using System.ClientModel;
using System.ClientModel.Primitives;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

#nullable enable

namespace OpenAI.Assistants;

internal class AsyncMessagesCollectionResult : AsyncCollectionResult<ThreadMessage>
{
    // Machinery for sending requests
    private readonly ClientPipeline _pipeline;
    private readonly Uri _endpoint;
    private readonly RequestOptions _options;

    // Initial values
    private readonly string _threadId;
    private readonly int? _limit;
    private readonly string? _order;
    private readonly string? _after;
    private readonly string? _before;

    // Pagination cursor
    private string? _lastSeenItem;

    public virtual ClientPipeline Pipeline => _pipeline;

    public AsyncMessagesCollectionResult(ClientPipeline pipeline, Uri endpoint,
        RequestOptions options,
        string threadId, int? limit, string? order, string? after, string? before)
    {
        _pipeline = pipeline;
        _endpoint = endpoint;
        _options = options;

        _threadId = threadId;
        _limit = limit;
        _order = order;
        _after = after;
        _before = before;
    }

    public async override IAsyncEnumerable<ClientResult> GetRawPagesAsync()
    {
        ClientResult page = await GetFirstAsync().ConfigureAwait(false);
        yield return page;

        while (HasNext(page))
        {
            ClientResult nextPage = await GetNextAsync(page);
            yield return nextPage;
        }
    }

    protected async override IAsyncEnumerable<ThreadMessage> GetValuesFromPageAsync(ClientResult page)
    {
        PipelineResponse response = page.GetRawResponse();
        InternalListMessagesResponse list = ModelReaderWriter.Read<InternalListMessagesResponse>(response.Content)!;
        foreach (ThreadMessage message in list.Data)
        {
            // TODO: Address this somehow.
            await Task.Delay(0);
            yield return message;
        }
    }

    public override ContinuationToken? GetContinuationToken(ClientResult page)
    {
        throw new NotImplementedException();
    }

    public async Task<ClientResult> GetFirstAsync()
        => await GetMessagesAsync(_threadId, _limit, _order, _lastSeenItem, _before, _options).ConfigureAwait(false);

    public async Task<ClientResult> GetNextAsync(ClientResult result)
    {
        PipelineResponse response = result.GetRawResponse();

        using JsonDocument doc = JsonDocument.Parse(response.Content);
        _lastSeenItem = doc.RootElement.GetProperty("last_id"u8).GetString()!;

        return await GetMessagesAsync(_threadId, _limit, _order, _lastSeenItem, _before, _options).ConfigureAwait(false);
    }

    public bool HasNext(ClientResult result)
    {
        PipelineResponse response = result.GetRawResponse();

        using JsonDocument doc = JsonDocument.Parse(response.Content);
        bool hasMore = doc.RootElement.GetProperty("has_more"u8).GetBoolean();

        return hasMore;
    }

    internal virtual async Task<ClientResult> GetMessagesAsync(string threadId, int? limit, string? order, string? after, string? before, RequestOptions? options)
    {
        Argument.AssertNotNullOrEmpty(threadId, nameof(threadId));

        using PipelineMessage message = CreateGetMessagesRequest(threadId, limit, order, after, before, options);
        return ClientResult.FromResponse(await _pipeline.ProcessMessageAsync(message, options).ConfigureAwait(false));
    }

    private PipelineMessage CreateGetMessagesRequest(string threadId, int? limit, string? order, string? after, string? before, RequestOptions? options)
    {
        var message = _pipeline.CreateMessage();
        message.ResponseClassifier = PipelineMessageClassifier200;
        var request = message.Request;
        request.Method = "GET";
        var uri = new ClientUriBuilder();
        uri.Reset(_endpoint);
        uri.AppendPath("/threads/", false);
        uri.AppendPath(threadId, true);
        uri.AppendPath("/messages", false);
        if (limit != null)
        {
            uri.AppendQuery("limit", limit.Value, true);
        }
        if (order != null)
        {
            uri.AppendQuery("order", order, true);
        }
        if (after != null)
        {
            uri.AppendQuery("after", after, true);
        }
        if (before != null)
        {
            uri.AppendQuery("before", before, true);
        }
        request.Uri = uri.ToUri();
        request.Headers.Set("Accept", "application/json");
        message.Apply(options);
        return message;
    }

    private static PipelineMessageClassifier? _pipelineMessageClassifier200;
    private static PipelineMessageClassifier PipelineMessageClassifier200 => _pipelineMessageClassifier200 ??= PipelineMessageClassifier.Create(stackalloc ushort[] { 200 });

}
