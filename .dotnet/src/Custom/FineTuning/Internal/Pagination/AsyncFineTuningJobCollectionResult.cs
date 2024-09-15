using System.ClientModel;
using System.ClientModel.Primitives;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

#nullable enable

namespace OpenAI.FineTuning;

internal class AsyncFineTuningJobCollectionResult : AsyncCollectionResult
{
    private readonly FineTuningClient _fineTuningClient;
    private readonly ClientPipeline _pipeline;
    private readonly RequestOptions _options;

    // Initial values
    private readonly int? _limit;
    private readonly string _after;

    public AsyncFineTuningJobCollectionResult(FineTuningClient fineTuningClient,
        ClientPipeline pipeline, RequestOptions options,
        int? limit, string after)
    {
        _fineTuningClient = fineTuningClient;
        _pipeline = pipeline;
        _options = options;

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
        => FineTuningJobCollectionPageToken.FromResponse(page, _limit);

    public async Task<ClientResult> GetFirstPageAsync()
        => await GetJobsAsync(_after, _limit, _options).ConfigureAwait(false);

    public async Task<ClientResult> GetNextPageAsync(ClientResult result)
    {
        PipelineResponse response = result.GetRawResponse();

        string lastId = null!;
        using JsonDocument doc = JsonDocument.Parse(response?.Content);
        if (doc?.RootElement.TryGetProperty("data", out JsonElement dataElement) == true
            && dataElement.EnumerateArray().LastOrDefault().TryGetProperty("id", out JsonElement idElement) == true)
        {
            lastId = idElement.GetString()!;
        }

        return await GetJobsAsync(_after, _limit, _options).ConfigureAwait(false);
    }

    public static bool HasNextPage(ClientResult result)
    {
        PipelineResponse response = result.GetRawResponse();

        using JsonDocument doc = JsonDocument.Parse(response.Content);
        bool hasMore = doc.RootElement.GetProperty("has_more"u8).GetBoolean();

        return hasMore;
    }

    internal virtual async Task<ClientResult> GetJobsAsync(string after, int? limit, RequestOptions options)
    {
        using PipelineMessage message = _fineTuningClient.CreateGetPaginatedFineTuningJobsRequest(after, limit, options);
        return ClientResult.FromResponse(await _pipeline.ProcessMessageAsync(message, options).ConfigureAwait(false));
    }
}
