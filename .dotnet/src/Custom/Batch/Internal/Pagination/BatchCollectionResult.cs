﻿using System.ClientModel;
using System.ClientModel.Primitives;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

#nullable enable

namespace OpenAI.Batch;

internal class BatchCollectionResult : CollectionResult
{
    private readonly BatchClient _batchClient;
    private readonly ClientPipeline _pipeline;
    private readonly RequestOptions _options;

    // Initial values
    private readonly int? _limit;
    private readonly string _after;

    public BatchCollectionResult(BatchClient batchClient,
        ClientPipeline pipeline, RequestOptions options,
        int? limit, string after)
    {
        _batchClient = batchClient;
        _pipeline = pipeline;
        _options = options;

        _limit = limit;
        _after = after;
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

    public override ContinuationToken? GetContinuationToken(ClientResult page)
        => BatchCollectionPageToken.FromResponse(page, _limit);

    public ClientResult GetFirstPage()
        => GetBatches(_after, _limit, _options);

    public ClientResult GetNextPage(ClientResult result)
    {
        PipelineResponse response = result.GetRawResponse();

        using JsonDocument doc = JsonDocument.Parse(response.Content);
        string lastId = doc.RootElement.GetProperty("last_id"u8).GetString()!;

        return GetBatches(lastId, _limit, _options);
    }

    public static bool HasNextPage(ClientResult result)
    {
        PipelineResponse response = result.GetRawResponse();

        using JsonDocument doc = JsonDocument.Parse(response.Content);
        bool hasMore = doc.RootElement.GetProperty("has_more"u8).GetBoolean();

        return hasMore;
    }

    // TODO: Can we make these go away?
    internal virtual async Task<ClientResult> GetBatchesAsync(string after, int? limit, RequestOptions options)
    {
        using PipelineMessage message = _batchClient.CreateGetBatchesRequest(after, limit, options);
        return ClientResult.FromResponse(await _pipeline.ProcessMessageAsync(message, options).ConfigureAwait(false));
    }

    internal virtual ClientResult GetBatches(string after, int? limit, RequestOptions options)
    {
        using PipelineMessage message = _batchClient.CreateGetBatchesRequest(after, limit, options);
        return ClientResult.FromResponse(_pipeline.ProcessMessage(message, options));
    }
}
