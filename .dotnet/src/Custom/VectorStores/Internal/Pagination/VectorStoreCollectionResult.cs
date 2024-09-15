using System.ClientModel;
using System.ClientModel.Primitives;
using System.Collections.Generic;
using System.Text.Json;

#nullable enable

namespace OpenAI.VectorStores;

internal class VectorStoreCollectionResult : CollectionResult<VectorStore>
{
    private readonly VectorStoreClient _vectorStoreClient;
    private readonly ClientPipeline _pipeline;
    private readonly RequestOptions _options;

    // Initial values
    private readonly int? _limit;
    private readonly string? _order;
    private readonly string? _after;
    private readonly string? _before;

    public VectorStoreCollectionResult(VectorStoreClient vectorStoreClient,
        ClientPipeline pipeline, RequestOptions options,
        int? limit, string? order, string? after, string? before)
    {
        _vectorStoreClient = vectorStoreClient;
        _pipeline = pipeline;
        _options = options;

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

    protected override IEnumerable<VectorStore> GetValuesFromPage(ClientResult page)
    {
        PipelineResponse response = page.GetRawResponse();
        InternalListVectorStoresResponse list = ModelReaderWriter.Read<InternalListVectorStoresResponse>(response.Content)!;
        return list.Data;
    }

    public override ContinuationToken? GetContinuationToken(ClientResult page)
        => VectorStoreCollectionPageToken.FromResponse(page, _limit, _order, _before);

    public ClientResult GetFirstPage()
        => GetVectorStores( _limit, _order, _after, _before, _options);

    public ClientResult GetNextPage(ClientResult result)
    {
        PipelineResponse response = result.GetRawResponse();

        using JsonDocument doc = JsonDocument.Parse(response.Content);
        string lastId = doc.RootElement.GetProperty("last_id"u8).GetString()!;

        return GetVectorStores( _limit, _order, lastId, _before, _options);
    }

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

    internal virtual ClientResult GetVectorStores(int? limit, string? order, string? after, string? before, RequestOptions options)
    {
        using PipelineMessage message = _vectorStoreClient.CreateGetVectorStoresRequest(limit, order, after, before, options);
        return ClientResult.FromResponse(_pipeline.ProcessMessage(message, options));
    }
}
