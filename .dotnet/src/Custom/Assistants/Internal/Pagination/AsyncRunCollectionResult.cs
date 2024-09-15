﻿using System.ClientModel;
using System.ClientModel.Primitives;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

#nullable enable

namespace OpenAI.Assistants;

internal class AsyncRunCollectionResult : AsyncCollectionResult<ThreadRun>
{
    private readonly InternalAssistantRunClient _runClient;
    private readonly RequestOptions _options;

    // Initial values
    private readonly string _threadId;
    private readonly int? _limit;
    private readonly string? _order;
    private readonly string? _after;
    private readonly string? _before;

    public AsyncRunCollectionResult(InternalAssistantRunClient runClient,
    RequestOptions options,
    string threadId, int? limit, string? order, string? after, string? before)
    {
        _runClient = runClient;
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
            page = await GetNextPageAsync(page);
            yield return page;
        }
    }

    protected async override IAsyncEnumerable<ThreadRun> GetValuesFromPageAsync(ClientResult page)
    {
        PipelineResponse response = page.GetRawResponse();
        InternalListRunsResponse list = ModelReaderWriter.Read<InternalListRunsResponse>(response.Content)!;
        foreach (ThreadRun run in list.Data)
        {
            // TODO: Address this.
            await Task.Delay(0);
            yield return run;
        }
    }

    public override ContinuationToken? GetContinuationToken(ClientResult page)
        => RunCollectionPageToken.FromResponse(page, _threadId, _limit, _order, _before);

    public async Task<ClientResult> GetFirstPageAsync()
        => await _runClient.GetRunsAsync(_threadId, _limit, _order, _after, _before, _options).ConfigureAwait(false);

    public async Task<ClientResult> GetNextPageAsync(ClientResult result)
    {
        PipelineResponse response = result.GetRawResponse();

        using JsonDocument doc = JsonDocument.Parse(response.Content);
        string lastId = doc.RootElement.GetProperty("last_id"u8).GetString()!;

        return await _runClient.GetRunsAsync(_threadId, _limit, _order, lastId, _before, _options).ConfigureAwait(false);
    }

    public bool HasNextPage(ClientResult result)
        => RunCollectionResult.HasNextPage(result);
}
