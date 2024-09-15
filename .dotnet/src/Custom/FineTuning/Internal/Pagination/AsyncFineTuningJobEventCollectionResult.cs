using System.ClientModel;
using System.ClientModel.Primitives;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

#nullable enable

namespace OpenAI.FineTuning;

internal class AsyncFineTuningJobEventCollectionResult : AsyncCollectionResult
{
    private readonly FineTuningClient _fineTuningClient;
    private readonly ClientPipeline _pipeline;
    private readonly RequestOptions _options;

    // Initial values
    private readonly string _jobId;
    private readonly int? _limit;
    private readonly string _after;

    public AsyncFineTuningJobEventCollectionResult(FineTuningClient fineTuningClient,
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

    public override ContinuationToken? GetContinuationToken(ClientResult page)
        => FineTuningJobEventCollectionPageToken.FromResponse(page, _jobId, _limit);

    public async Task<ClientResult> GetFirstPageAsync()
        => await GetJobEventsAsync(_jobId, _after, _limit, _options).ConfigureAwait(false);

    public async Task<ClientResult> GetNextPageAsync(ClientResult result)
    {
        PipelineResponse response = result.GetRawResponse();

        using JsonDocument doc = JsonDocument.Parse(response.Content);
        string lastId = doc.RootElement.GetProperty("last_id"u8).GetString()!;

        return await GetJobEventsAsync(_jobId, lastId!, _limit, _options).ConfigureAwait(false);
    }

    public static bool HasNextPage(ClientResult result)
        => FineTuningJobEventCollectionResult.HasNextPage(result);


    internal virtual async Task<ClientResult> GetJobEventsAsync(string jobId, string after, int? limit, RequestOptions options)
    {
        Argument.AssertNotNullOrEmpty(jobId, nameof(jobId));

        using PipelineMessage message = _fineTuningClient.CreateGetFineTuningEventsRequest(jobId, after, limit, options);
        return ClientResult.FromResponse(await _pipeline.ProcessMessageAsync(message, options).ConfigureAwait(false));
    }
}
