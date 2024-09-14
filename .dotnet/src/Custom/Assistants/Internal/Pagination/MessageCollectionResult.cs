using System.ClientModel;
using System.ClientModel.Primitives;
using System.Collections.Generic;
using System.Text.Json;

#nullable enable

namespace OpenAI.Assistants;

// Internal subclient that handles paginated requests
internal class MessageCollectionResult : CollectionResult<ThreadMessage>
{
    private readonly InternalAssistantMessageClient _messageClient;
    private readonly RequestOptions _options;

    // Initial values
    private readonly string _threadId;
    private readonly int? _limit;
    private readonly string? _order;
    private readonly string? _after;
    private readonly string? _before;

    public MessageCollectionResult(InternalAssistantMessageClient messageClient,
        RequestOptions options,
        string threadId, int? limit, string? order, string? after, string? before)
    {
        _messageClient = messageClient;
        _options = options;

        _threadId = threadId;
        _limit = limit;
        _order = order;
        _after = after;
        _before = before;
    }

    public override IEnumerable<ClientResult> GetRawPages()
    {
        ClientResult page = GetFirstPage();
        yield return page;

        while (HasNextPage(page))
        {
            ClientResult nextPage = GetNextPage(page);
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
        => MessageCollectionPageToken.FromResponse(page, _threadId, _limit, _order, _before);

    public ClientResult GetFirstPage()
        => _messageClient.GetMessages(_threadId, _limit, _order, _after, _before, _options);

    public ClientResult GetNextPage(ClientResult result)
    {
        PipelineResponse response = result.GetRawResponse();

        using JsonDocument doc = JsonDocument.Parse(response.Content);
        string lastId = doc.RootElement.GetProperty("last_id"u8).GetString()!;

        return _messageClient.GetMessages(_threadId, _limit, _order, lastId, _before, _options);
    }

    // Note: we could remove this in favor of calling GetContinuationToken and
    // checking it for null, but this way avoids allocating a ContinuationToken
    // if no one has asked for it.
    public static bool HasNextPage(ClientResult result)
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
}
