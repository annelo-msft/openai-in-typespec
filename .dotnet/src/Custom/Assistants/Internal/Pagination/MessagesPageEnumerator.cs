using System;
using System.ClientModel;
using System.ClientModel.Primitives;
using System.Text.Json;
using System.Threading.Tasks;

#nullable enable

namespace OpenAI.Assistants;

internal partial class MessagesPageEnumerator : PageEnumerator
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

    public MessagesPageEnumerator(ClientPipeline pipeline, Uri endpoint, 
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

        _lastSeenItem = after;
    }

    public override PageEnumerator CreateEnumerator()
        => new MessagesPageEnumerator(_pipeline, _endpoint, _options, _threadId, _limit, _order, _after, _before);

    //public override ContinuationToken? GetNextPageToken(ClientResult result)
    //{
    //    PipelineResponse response = result.GetRawResponse();
    //    using JsonDocument doc = JsonDocument.Parse(response.Content);
    //    string lastId = doc.RootElement.GetProperty("last_id"u8).GetString()!;
    //    bool hasMore = doc.RootElement.GetProperty("has_more"u8).GetBoolean();

    //    MessagesPageToken pageToken = MessagesPageToken.FromOptions(_threadId, _limit, _order, _after, _before);
    //    return pageToken.GetNextPageToken(hasMore, lastId);
    //}

    public override async Task<ClientResult> GetFirstAsync()
        => await GetMessagesAsync(_threadId, _limit, _order, _lastSeenItem, _before, _options).ConfigureAwait(false);

    public override ClientResult GetFirst()
        => GetMessages(_threadId, _limit, _order, _lastSeenItem, _before, _options);

    public override async Task<ClientResult> GetNextAsync(ClientResult result)
    {
        PipelineResponse response = result.GetRawResponse();

        using JsonDocument doc = JsonDocument.Parse(response.Content);
        _lastSeenItem = doc.RootElement.GetProperty("last_id"u8).GetString()!;

        return await GetMessagesAsync(_threadId, _limit, _order, _lastSeenItem, _before, _options).ConfigureAwait(false);
    }

    public override ClientResult GetNext(ClientResult result)
    {
        PipelineResponse response = result.GetRawResponse();

        using JsonDocument doc = JsonDocument.Parse(response.Content);
        _lastSeenItem = doc.RootElement.GetProperty("last_id"u8).GetString()!;

        return GetMessages(_threadId, _limit, _order, _lastSeenItem, _before, _options);
    }

    public override bool HasNext(ClientResult result)
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

    internal virtual ClientResult GetMessages(string threadId, int? limit, string? order, string? after, string? before, RequestOptions? options)
    {
        Argument.AssertNotNullOrEmpty(threadId, nameof(threadId));

        using PipelineMessage message = CreateGetMessagesRequest(threadId, limit, order, after, before, options);
        return ClientResult.FromResponse(_pipeline.ProcessMessage(message, options));
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
