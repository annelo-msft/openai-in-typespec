﻿using System.ClientModel;
using System.ClientModel.Primitives;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

#nullable enable

namespace OpenAI.FineTuning;

internal class FineTuningJobEventCollectionResult : CollectionResult
{
    private readonly FineTuningClient? _fineTuningClient;
    private readonly CreateJobOperation? _operation;
    private readonly ClientPipeline _pipeline;
    private readonly RequestOptions? _options;

    // Initial values
    private readonly string _jobId;
    private readonly int? _limit;
    private readonly string _after;

    public FineTuningJobEventCollectionResult(FineTuningClient fineTuningClient,
        ClientPipeline pipeline, RequestOptions? options,
        string jobId, int? limit, string after)
    {
        _fineTuningClient = fineTuningClient;
        _operation = null;
        _pipeline = pipeline;
        _options = options;

        _jobId = jobId;
        _limit = limit;
        _after = after;
    }

    public FineTuningJobEventCollectionResult(CreateJobOperation fineTuningOperation,
        ClientPipeline pipeline, RequestOptions? options,
        string jobId, int? limit, string after)
    {
        _fineTuningClient = null;
        _operation = fineTuningOperation;
        _pipeline = pipeline;
        _options = options;

        _jobId = jobId;
        _limit = limit;
        _after = after;
    }

    public override IEnumerable<ClientResult> GetRawPages()
    {
        ClientResult page = GetFirstPage();
        yield return page;

        while (HasNextPage(page))
        {
            page = GetNextPage(page);
            yield return page;
        }
    }

    public override ContinuationToken? GetContinuationToken(ClientResult page)
    {
        Argument.AssertNotNull(page, nameof(page));

        return FineTuningJobEventCollectionPageToken.FromResponse(page, _jobId, _limit);
    }

    public ClientResult GetFirstPage()
        => GetJobEvents(_jobId, _after, _limit, _options);

    public ClientResult GetNextPage(ClientResult result)
    {
        Argument.AssertNotNull(result, nameof(result));

        PipelineResponse response = result.GetRawResponse();

        using JsonDocument doc = JsonDocument.Parse(response?.Content);

        JsonElement data = doc.RootElement.GetProperty("data");
        JsonElement lastItem = data.EnumerateArray().LastOrDefault();
        string? lastId = lastItem.TryGetProperty("id", out JsonElement idElement) ?
            idElement.GetString() : null;

        return GetJobEvents(_jobId, lastId, _limit, _options);
    }

    public static bool HasNextPage(ClientResult result)
    {
        Argument.AssertNotNull(result, nameof(result));

        PipelineResponse response = result.GetRawResponse();

        using JsonDocument doc = JsonDocument.Parse(response.Content);
        bool hasMore = doc.RootElement.GetProperty("has_more"u8).GetBoolean();

        return hasMore;
    }

    internal virtual ClientResult GetJobEvents(string jobId, string? after, int? limit, RequestOptions? options)
    {
        Argument.AssertNotNullOrEmpty(jobId, nameof(jobId));

        if (_fineTuningClient != null)
        {
            using PipelineMessage message = _fineTuningClient.CreateGetFineTuningEventsRequest(jobId, after, limit, options);
            return ClientResult.FromResponse(_pipeline.ProcessMessage(message, options));
        }
        else // operation != null
        {
            using PipelineMessage message = _operation!.CreateGetFineTuningEventsRequest(jobId, after, limit, options);
            return ClientResult.FromResponse(_pipeline.ProcessMessage(message, options));
        }
    }
}
