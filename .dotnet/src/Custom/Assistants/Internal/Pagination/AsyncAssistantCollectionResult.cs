﻿using System.ClientModel;
using System.ClientModel.Primitives;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

#nullable enable

namespace OpenAI.Assistants;

internal class AsyncAssistantCollectionResult : AsyncCollectionResult<Assistant>
{
    private readonly AssistantClient _assistantClient;
    private readonly ClientPipeline _pipeline;
    private readonly RequestOptions _options;

    // Initial values
    private readonly int? _limit;
    private readonly string? _order;
    private readonly string? _after;
    private readonly string? _before;

    public AsyncAssistantCollectionResult(AssistantClient assistantClient,
        ClientPipeline pipeline, RequestOptions options,
        int? limit, string? order, string? after, string? before)
    {
        _assistantClient = assistantClient;
        _pipeline = pipeline;
        _options = options;

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


    protected async override IAsyncEnumerable<Assistant> GetValuesFromPageAsync(ClientResult page)
    {
        PipelineResponse response = page.GetRawResponse();
        InternalListAssistantsResponse list = ModelReaderWriter.Read<InternalListAssistantsResponse>(response.Content)!;
        foreach (Assistant message in list.Data)
        {
            // TODO: Address this.
            await Task.Delay(0);
            yield return message;
        }
    }

    public override ContinuationToken? GetContinuationToken(ClientResult page)
        => AssistantCollectionPageToken.FromResponse(page, _limit, _order, _before);

    public async Task<ClientResult> GetFirstPageAsync()
        => await GetAssistantsAsync(_limit, _order, _after, _before, _options).ConfigureAwait(false);

    public async Task<ClientResult> GetNextPageAsync(ClientResult result)
    {
        PipelineResponse response = result.GetRawResponse();

        using JsonDocument doc = JsonDocument.Parse(response.Content);
        string lastId = doc.RootElement.GetProperty("last_id"u8).GetString()!;

        return await GetAssistantsAsync(_limit, _order, lastId, _before, _options).ConfigureAwait(false);
    }

    public static bool HasNextPage(ClientResult result)
        => AssistantCollectionResult.HasNextPage(result);

    internal virtual async Task<ClientResult> GetAssistantsAsync(int? limit, string? order, string? after, string? before, RequestOptions? options)
    {
        using PipelineMessage message = _assistantClient.CreateGetAssistantsRequest(limit, order, after, before, options);
        return ClientResult.FromResponse(await _pipeline.ProcessMessageAsync(message, options).ConfigureAwait(false));
    }

}
