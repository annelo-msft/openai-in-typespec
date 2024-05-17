// <auto-generated/>

#nullable disable

using System;
using System.ClientModel;
using System.ClientModel.Primitives;
using System.Threading.Tasks;

namespace OpenAI.VectorStores
{
    // Data plane generated sub-client.
    /// <summary> The VectorStore sub-client. </summary>
    public partial class VectorStoreClient
    {
        private const string AuthorizationHeader = "Authorization";
        private readonly ApiKeyCredential _keyCredential;
        private const string AuthorizationApiKeyPrefix = "Bearer";
        private readonly ClientPipeline _pipeline;
        private readonly Uri _endpoint;

        /// <summary> The HTTP pipeline for sending and receiving REST requests and responses. </summary>
        public virtual ClientPipeline Pipeline => _pipeline;

        /// <summary> Initializes a new instance of VectorStoreClient for mocking. </summary>
        protected VectorStoreClient()
        {
        }

        /// <summary> Initializes a new instance of VectorStoreClient. </summary>
        /// <param name="pipeline"> The HTTP pipeline for sending and receiving REST requests and responses. </param>
        /// <param name="keyCredential"> The key credential to copy. </param>
        /// <param name="endpoint"> OpenAI Endpoint. </param>
        internal VectorStoreClient(ClientPipeline pipeline, ApiKeyCredential keyCredential, Uri endpoint)
        {
            _pipeline = pipeline;
            _keyCredential = keyCredential;
            _endpoint = endpoint;
        }

        /// <summary> Retrieves a vector store. </summary>
        /// <param name="vectorStoreId"> The ID of the vector store to retrieve. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="vectorStoreId"/> is null. </exception>
        /// <exception cref="ArgumentException"> <paramref name="vectorStoreId"/> is an empty string, and was expected to be non-empty. </exception>
        /// <remarks> Get vector store. </remarks>
        public virtual async Task<ClientResult<VectorStore>> GetVectorStoreAsync(string vectorStoreId)
        {
            Argument.AssertNotNullOrEmpty(vectorStoreId, nameof(vectorStoreId));

            ClientResult result = await GetVectorStoreAsync(vectorStoreId, null).ConfigureAwait(false);
            return ClientResult.FromValue(VectorStore.FromResponse(result.GetRawResponse()), result.GetRawResponse());
        }

        /// <summary> Retrieves a vector store. </summary>
        /// <param name="vectorStoreId"> The ID of the vector store to retrieve. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="vectorStoreId"/> is null. </exception>
        /// <exception cref="ArgumentException"> <paramref name="vectorStoreId"/> is an empty string, and was expected to be non-empty. </exception>
        /// <remarks> Get vector store. </remarks>
        public virtual ClientResult<VectorStore> GetVectorStore(string vectorStoreId)
        {
            Argument.AssertNotNullOrEmpty(vectorStoreId, nameof(vectorStoreId));

            ClientResult result = GetVectorStore(vectorStoreId, null);
            return ClientResult.FromValue(VectorStore.FromResponse(result.GetRawResponse()), result.GetRawResponse());
        }

        /// <summary> Modifies a vector store. </summary>
        /// <param name="vectorStoreId"> The ID of the vector store to modify. </param>
        /// <param name="vectorStore"> The <see cref="VectorStoreModificationOptions"/> to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="vectorStoreId"/> or <paramref name="vectorStore"/> is null. </exception>
        /// <exception cref="ArgumentException"> <paramref name="vectorStoreId"/> is an empty string, and was expected to be non-empty. </exception>
        /// <remarks> Modify vector store. </remarks>
        public virtual async Task<ClientResult<VectorStore>> ModifyVectorStoreAsync(string vectorStoreId, VectorStoreModificationOptions vectorStore)
        {
            Argument.AssertNotNullOrEmpty(vectorStoreId, nameof(vectorStoreId));
            Argument.AssertNotNull(vectorStore, nameof(vectorStore));

            using BinaryContent content = vectorStore.ToBinaryContent();
            ClientResult result = await ModifyVectorStoreAsync(vectorStoreId, content, null).ConfigureAwait(false);
            return ClientResult.FromValue(VectorStore.FromResponse(result.GetRawResponse()), result.GetRawResponse());
        }

        /// <summary> Modifies a vector store. </summary>
        /// <param name="vectorStoreId"> The ID of the vector store to modify. </param>
        /// <param name="vectorStore"> The <see cref="VectorStoreModificationOptions"/> to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="vectorStoreId"/> or <paramref name="vectorStore"/> is null. </exception>
        /// <exception cref="ArgumentException"> <paramref name="vectorStoreId"/> is an empty string, and was expected to be non-empty. </exception>
        /// <remarks> Modify vector store. </remarks>
        public virtual ClientResult<VectorStore> ModifyVectorStore(string vectorStoreId, VectorStoreModificationOptions vectorStore)
        {
            Argument.AssertNotNullOrEmpty(vectorStoreId, nameof(vectorStoreId));
            Argument.AssertNotNull(vectorStore, nameof(vectorStore));

            using BinaryContent content = vectorStore.ToBinaryContent();
            ClientResult result = ModifyVectorStore(vectorStoreId, content, null);
            return ClientResult.FromValue(VectorStore.FromResponse(result.GetRawResponse()), result.GetRawResponse());
        }

        internal PipelineMessage CreateGetVectorStoresRequest(int? limit, string order, string after, string before, RequestOptions options)
        {
            var message = _pipeline.CreateMessage();
            message.ResponseClassifier = PipelineMessageClassifier200;
            var request = message.Request;
            request.Method = "GET";
            var uri = new ClientUriBuilder();
            uri.Reset(_endpoint);
            uri.AppendPath("/vector_stores", false);
            if (limit != null)
            {
                uri.AppendQuery("limit", limit.Value, true);
            }
            if (order != null)
            {
                uri.AppendQuery("order", order, true);
            }
            if (after != null)
            {
                uri.AppendQuery("after", after, true);
            }
            if (before != null)
            {
                uri.AppendQuery("before", before, true);
            }
            request.Uri = uri.ToUri();
            request.Headers.Set("Accept", "application/json");
            message.Apply(options);
            return message;
        }

        internal PipelineMessage CreateCreateVectorStoreRequest(BinaryContent content, RequestOptions options)
        {
            var message = _pipeline.CreateMessage();
            message.ResponseClassifier = PipelineMessageClassifier200;
            var request = message.Request;
            request.Method = "POST";
            var uri = new ClientUriBuilder();
            uri.Reset(_endpoint);
            uri.AppendPath("/vector_stores", false);
            request.Uri = uri.ToUri();
            request.Headers.Set("Accept", "application/json");
            request.Headers.Set("Content-Type", "application/json");
            request.Content = content;
            message.Apply(options);
            return message;
        }

        internal PipelineMessage CreateGetVectorStoreRequest(string vectorStoreId, RequestOptions options)
        {
            var message = _pipeline.CreateMessage();
            message.ResponseClassifier = PipelineMessageClassifier200;
            var request = message.Request;
            request.Method = "GET";
            var uri = new ClientUriBuilder();
            uri.Reset(_endpoint);
            uri.AppendPath("/vector_stores/", false);
            uri.AppendPath(vectorStoreId, true);
            request.Uri = uri.ToUri();
            request.Headers.Set("Accept", "application/json");
            message.Apply(options);
            return message;
        }

        internal PipelineMessage CreateModifyVectorStoreRequest(string vectorStoreId, BinaryContent content, RequestOptions options)
        {
            var message = _pipeline.CreateMessage();
            message.ResponseClassifier = PipelineMessageClassifier200;
            var request = message.Request;
            request.Method = "POST";
            var uri = new ClientUriBuilder();
            uri.Reset(_endpoint);
            uri.AppendPath("/vector_stores/", false);
            uri.AppendPath(vectorStoreId, true);
            request.Uri = uri.ToUri();
            request.Headers.Set("Accept", "application/json");
            request.Headers.Set("Content-Type", "application/json");
            request.Content = content;
            message.Apply(options);
            return message;
        }

        internal PipelineMessage CreateDeleteVectorStoreRequest(string vectorStoreId, RequestOptions options)
        {
            var message = _pipeline.CreateMessage();
            message.ResponseClassifier = PipelineMessageClassifier200;
            var request = message.Request;
            request.Method = "DELETE";
            var uri = new ClientUriBuilder();
            uri.Reset(_endpoint);
            uri.AppendPath("/vector_stores/", false);
            uri.AppendPath(vectorStoreId, true);
            request.Uri = uri.ToUri();
            request.Headers.Set("Accept", "application/json");
            message.Apply(options);
            return message;
        }

        internal PipelineMessage CreateGetVectorStoreFilesRequest(string vectorStoreId, int? limit, string order, string after, string before, string filter, RequestOptions options)
        {
            var message = _pipeline.CreateMessage();
            message.ResponseClassifier = PipelineMessageClassifier200;
            var request = message.Request;
            request.Method = "GET";
            var uri = new ClientUriBuilder();
            uri.Reset(_endpoint);
            uri.AppendPath("/vector_stores/", false);
            uri.AppendPath(vectorStoreId, true);
            uri.AppendPath("/files", false);
            if (limit != null)
            {
                uri.AppendQuery("limit", limit.Value, true);
            }
            if (order != null)
            {
                uri.AppendQuery("order", order, true);
            }
            if (after != null)
            {
                uri.AppendQuery("after", after, true);
            }
            if (before != null)
            {
                uri.AppendQuery("before", before, true);
            }
            if (filter != null)
            {
                uri.AppendQuery("filter", filter, true);
            }
            request.Uri = uri.ToUri();
            request.Headers.Set("Accept", "application/json");
            message.Apply(options);
            return message;
        }

        internal PipelineMessage CreateCreateVectorStoreFileRequest(string vectorStoreId, BinaryContent content, RequestOptions options)
        {
            var message = _pipeline.CreateMessage();
            message.ResponseClassifier = PipelineMessageClassifier200;
            var request = message.Request;
            request.Method = "POST";
            var uri = new ClientUriBuilder();
            uri.Reset(_endpoint);
            uri.AppendPath("/vector_stores/", false);
            uri.AppendPath(vectorStoreId, true);
            uri.AppendPath("/files", false);
            request.Uri = uri.ToUri();
            request.Headers.Set("Accept", "application/json");
            request.Headers.Set("Content-Type", "application/json");
            request.Content = content;
            message.Apply(options);
            return message;
        }

        internal PipelineMessage CreateGetVectorStoreFileRequest(string vectorStoreId, string fileId, RequestOptions options)
        {
            var message = _pipeline.CreateMessage();
            message.ResponseClassifier = PipelineMessageClassifier200;
            var request = message.Request;
            request.Method = "GET";
            var uri = new ClientUriBuilder();
            uri.Reset(_endpoint);
            uri.AppendPath("/vector_stores/", false);
            uri.AppendPath(vectorStoreId, true);
            uri.AppendPath("/files/", false);
            uri.AppendPath(fileId, true);
            request.Uri = uri.ToUri();
            request.Headers.Set("Accept", "application/json");
            message.Apply(options);
            return message;
        }

        internal PipelineMessage CreateDeleteVectorStoreFileRequest(string vectorStoreId, string fileId, RequestOptions options)
        {
            var message = _pipeline.CreateMessage();
            message.ResponseClassifier = PipelineMessageClassifier200;
            var request = message.Request;
            request.Method = "DELETE";
            var uri = new ClientUriBuilder();
            uri.Reset(_endpoint);
            uri.AppendPath("/vector_stores/", false);
            uri.AppendPath(vectorStoreId, true);
            uri.AppendPath("/files/", false);
            uri.AppendPath(fileId, true);
            request.Uri = uri.ToUri();
            request.Headers.Set("Accept", "application/json");
            message.Apply(options);
            return message;
        }

        internal PipelineMessage CreateCreateVectorStoreFileBatchRequest(string vectorStoreId, BinaryContent content, RequestOptions options)
        {
            var message = _pipeline.CreateMessage();
            message.ResponseClassifier = PipelineMessageClassifier200;
            var request = message.Request;
            request.Method = "POST";
            var uri = new ClientUriBuilder();
            uri.Reset(_endpoint);
            uri.AppendPath("/vector_stores/", false);
            uri.AppendPath(vectorStoreId, true);
            uri.AppendPath("/file_batches", false);
            request.Uri = uri.ToUri();
            request.Headers.Set("Accept", "application/json");
            request.Headers.Set("Content-Type", "application/json");
            request.Content = content;
            message.Apply(options);
            return message;
        }

        internal PipelineMessage CreateGetVectorStoreFileBatchRequest(string vectorStoreId, string batchId, RequestOptions options)
        {
            var message = _pipeline.CreateMessage();
            message.ResponseClassifier = PipelineMessageClassifier200;
            var request = message.Request;
            request.Method = "GET";
            var uri = new ClientUriBuilder();
            uri.Reset(_endpoint);
            uri.AppendPath("/vector_stores/", false);
            uri.AppendPath(vectorStoreId, true);
            uri.AppendPath("/file_batches/", false);
            uri.AppendPath(batchId, true);
            request.Uri = uri.ToUri();
            request.Headers.Set("Accept", "application/json");
            message.Apply(options);
            return message;
        }

        internal PipelineMessage CreateCancelVectorStoreFileBatchRequest(string vectorStoreId, string batchId, RequestOptions options)
        {
            var message = _pipeline.CreateMessage();
            message.ResponseClassifier = PipelineMessageClassifier200;
            var request = message.Request;
            request.Method = "POST";
            var uri = new ClientUriBuilder();
            uri.Reset(_endpoint);
            uri.AppendPath("/vector_stores/", false);
            uri.AppendPath(vectorStoreId, true);
            uri.AppendPath("/file_batches/", false);
            uri.AppendPath(batchId, true);
            uri.AppendPath("/cancel", false);
            request.Uri = uri.ToUri();
            request.Headers.Set("Accept", "application/json");
            message.Apply(options);
            return message;
        }

        internal PipelineMessage CreateGetFilesInVectorStoreBatchesRequest(string vectorStoreId, string batchId, int? limit, string order, string after, string before, string filter, RequestOptions options)
        {
            var message = _pipeline.CreateMessage();
            message.ResponseClassifier = PipelineMessageClassifier200;
            var request = message.Request;
            request.Method = "GET";
            var uri = new ClientUriBuilder();
            uri.Reset(_endpoint);
            uri.AppendPath("/vector_stores/", false);
            uri.AppendPath(vectorStoreId, true);
            uri.AppendPath("/file_batches/", false);
            uri.AppendPath(batchId, true);
            uri.AppendPath("/files", false);
            if (limit != null)
            {
                uri.AppendQuery("limit", limit.Value, true);
            }
            if (order != null)
            {
                uri.AppendQuery("order", order, true);
            }
            if (after != null)
            {
                uri.AppendQuery("after", after, true);
            }
            if (before != null)
            {
                uri.AppendQuery("before", before, true);
            }
            if (filter != null)
            {
                uri.AppendQuery("filter", filter, true);
            }
            request.Uri = uri.ToUri();
            request.Headers.Set("Accept", "application/json");
            message.Apply(options);
            return message;
        }

        private static PipelineMessageClassifier _pipelineMessageClassifier200;
        private static PipelineMessageClassifier PipelineMessageClassifier200 => _pipelineMessageClassifier200 ??= PipelineMessageClassifier.Create(stackalloc ushort[] { 200 });
    }
}
