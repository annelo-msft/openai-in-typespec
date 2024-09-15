using System.ClientModel;
using System.ClientModel.Primitives;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

#nullable enable

namespace OpenAI.FineTuning;

internal class FineTuningJobEventCollectionResult : CollectionResult
{
    private readonly FineTuningClient _fineTuningClient;
    private readonly ClientPipeline _pipeline;
    private readonly RequestOptions _options;
    
    // Initial values
    private readonly string _jobId;
    private readonly int? _limit;
    private readonly string _after;

    public FineTuningJobEventCollectionResult(FineTuningClient fineTuningClient,
        ClientPipeline pipeline, RequestOptions options,
        string jobId, int? limit, string after)
    {
        _fineTuningClient = fineTuningClient;
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
            ClientResult nextPage = GetNextPage(page);
            yield return nextPage;
        }
    }

    public override ContinuationToken? GetContinuationToken(ClientResult page)
        => FineTuningJobEventCollectionPageToken.FromResponse(page, _jobId, _limit);

    public ClientResult GetFirstPage()
        => GetJobEvents(_jobId, _after, _limit, _options);

    public ClientResult GetNextPage(ClientResult result)
    {
        PipelineResponse response = result.GetRawResponse();

        string lastId = null!;
        using JsonDocument doc = JsonDocument.Parse(response?.Content);
        if (doc?.RootElement.TryGetProperty("data", out JsonElement dataElement) == true
            && dataElement.EnumerateArray().LastOrDefault().TryGetProperty("id", out JsonElement idElement) == true)
        {
            lastId = idElement.GetString()!;
        }

        return GetJobEvents(_jobId, lastId!, _limit, _options);
    }

    public static bool HasNextPage(ClientResult result)
    {
        PipelineResponse response = result.GetRawResponse();

        using JsonDocument doc = JsonDocument.Parse(response.Content);
        bool hasMore = doc.RootElement.GetProperty("has_more"u8).GetBoolean();

        return hasMore;
    }

    internal virtual ClientResult GetJobEvents(string jobId, string after, int? limit, RequestOptions options)
    {
        Argument.AssertNotNullOrEmpty(jobId, nameof(jobId));

        using PipelineMessage message = _fineTuningClient.CreateGetFineTuningEventsRequest(jobId, after, limit, options);
        return ClientResult.FromResponse(_pipeline.ProcessMessage(message, options));
    }
}
