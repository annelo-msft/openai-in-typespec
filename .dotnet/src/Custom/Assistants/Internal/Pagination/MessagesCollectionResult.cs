using System;
using System.ClientModel;
using System.ClientModel.Primitives;
using System.Collections.Generic;
using System.Text.Json;

#nullable enable

namespace OpenAI.Assistants;

// Internal subclient that handles paginated requests
internal class MessagesCollectionResult : CollectionResult<ThreadMessage>
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

    public MessagesCollectionResult(ClientPipeline pipeline, Uri endpoint,
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

    public override IEnumerable<ClientResult> GetRawPages()
    {
        ClientResult page = GetFirst();
        yield return page;

        while (HasNext(page))
        {
            ClientResult nextPage = GetNext(page);
            yield return nextPage;
        }
    }

    protected override IEnumerable<ThreadMessage> GetValuesFromPage(ClientResult page)
    {
        PipelineResponse response = page.GetRawResponse();
        InternalListMessagesResponse list = ModelReaderWriter.Read<InternalListMessagesResponse>(response.Content)!;
        return list.Data;
    }

    public override ContinuationToken? GetContinuationToken(ClientResult page)
    {
        PipelineResponse response = page.GetRawResponse();
        using JsonDocument doc = JsonDocument.Parse(response.Content);
        string lastId = doc.RootElement.GetProperty("last_id"u8).GetString()!;
        bool hasMore = doc.RootElement.GetProperty("has_more"u8).GetBoolean();

        if (!hasMore || lastId is null)
        {
            return null;
        }

        return MessagesPageToken.FromOptions(_threadId, _limit, _order, lastId, _before);
    }

    public ClientResult GetFirst()
        => GetMessages(_threadId, _limit, _order, _lastSeenItem, _before, _options);

    public ClientResult GetNext(ClientResult result)
    {
        PipelineResponse response = result.GetRawResponse();

        using JsonDocument doc = JsonDocument.Parse(response.Content);
        _lastSeenItem = doc.RootElement.GetProperty("last_id"u8).GetString()!;

        return GetMessages(_threadId, _limit, _order, _lastSeenItem, _before, _options);
    }

    public bool HasNext(ClientResult result)
    {
        PipelineResponse response = result.GetRawResponse();
        Utf8JsonReader reader = new Utf8JsonReader(response.Content);

        bool hasMore = default;
        bool foundValue = false;

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                // TODO: can we do it in UTF8 bytes?
                string? propertyName = reader.GetString();
                if (propertyName == "has_more")
                {
                    reader.Read();
                    hasMore = reader.GetBoolean();
                    foundValue = true;
                }
            }
        }

        if (!foundValue)
        {
            throw new JsonException("'has_more' value was not present in response.");
        }

        return hasMore;
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
