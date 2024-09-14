using System;
using System.ClientModel;
using System.ClientModel.Primitives;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

#nullable enable

namespace OpenAI.Assistants;

internal class MessagesPageRequestManager : PageRequestManager
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

    public MessagesPageRequestManager(
        ClientPipeline pipeline, Uri endpoint, RequestOptions options,
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

    public override PageEnumerator CreatePageEnumerator()
        => new MessagesPageEnumerator(_pipeline, _endpoint, _options, _threadId, _limit, _order, _after, _before);

    public override ContinuationToken? GetNextPageToken(ClientResult result)
    {
        PipelineResponse response = result.GetRawResponse();
        using JsonDocument doc = JsonDocument.Parse(response.Content);
        string lastId = doc.RootElement.GetProperty("last_id"u8).GetString()!;
        bool hasMore = doc.RootElement.GetProperty("has_more"u8).GetBoolean();

        if (!hasMore || lastId is null)
        {
            return null;
        }

        return MessagesPageToken.FromOptions(_threadId, _limit, _order, lastId, _before);
    }
}
