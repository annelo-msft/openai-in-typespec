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
    private readonly InternalAssistantMessageClient _messageClient;
    private readonly RequestOptions _options;

    // Initial values
    private readonly string _threadId;
    private readonly int? _limit;
    private readonly string? _order;
    private readonly string? _after;
    private readonly string? _before;

    public AsyncMessagesCollectionResult(InternalAssistantMessageClient messageClient,
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

    public async override IAsyncEnumerable<ClientResult> GetRawPagesAsync()
    {
        ClientResult page = await GetFirstPageAsync().ConfigureAwait(false);
        yield return page;

        while (HasNextPage(page))
        {
            ClientResult nextPage = await GetNextPageAsync(page);
            yield return nextPage;
        }
    }

    protected async override IAsyncEnumerable<ThreadMessage> GetValuesFromPageAsync(ClientResult page)
    {
        PipelineResponse response = page.GetRawResponse();
        InternalListMessagesResponse list = ModelReaderWriter.Read<InternalListMessagesResponse>(response.Content)!;
        foreach (ThreadMessage message in list.Data)
        {
            // TODO: Address this.
            await Task.Delay(0);
            yield return message;
        }
    }

    public override ContinuationToken? GetContinuationToken(ClientResult page)
        => MessagesPageToken.FromResponse(page, _threadId, _limit, _order, _before);

    public async Task<ClientResult> GetFirstPageAsync()
        => await _messageClient.GetMessagesAsync(_threadId, _limit, _order, _after, _before, _options).ConfigureAwait(false);

    public async Task<ClientResult> GetNextPageAsync(ClientResult result)
    {
        PipelineResponse response = result.GetRawResponse();

        using JsonDocument doc = JsonDocument.Parse(response.Content);
        string lastId = doc.RootElement.GetProperty("last_id"u8).GetString()!;

        return await _messageClient.GetMessagesAsync(_threadId, _limit, _order, lastId, _before, _options).ConfigureAwait(false);
    }

    public static bool HasNextPage(ClientResult result)
        => MessagesCollectionResult.HasNextPage(result);
}
