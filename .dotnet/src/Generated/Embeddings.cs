// <auto-generated/>

using System;
using System.ClientModel;
using System.ClientModel.Primitives;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenAI.Internal.Models;

namespace OpenAI.Internal
{
    // Data plane generated sub-client.
    /// <summary> The Embeddings sub-client. </summary>
    internal partial class Embeddings
    {
        private const string AuthorizationHeader = "Authorization";
        private readonly ApiKeyCredential _credential;
        private const string AuthorizationApiKeyPrefix = "Bearer";
        private readonly ClientPipeline _pipeline;
        private readonly Uri _endpoint;

        /// <summary> The HTTP pipeline for sending and receiving REST requests and responses. </summary>
        public virtual ClientPipeline Pipeline => _pipeline;

        /// <summary> Initializes a new instance of Embeddings for mocking. </summary>
        protected Embeddings()
        {
        }

        /// <summary> Initializes a new instance of Embeddings. </summary>
        /// <param name="pipeline"> The HTTP pipeline for sending and receiving REST requests and responses. </param>
        /// <param name="credential"> The key credential to copy. </param>
        /// <param name="endpoint"> OpenAI Endpoint. </param>
        internal Embeddings(ClientPipeline pipeline, ApiKeyCredential credential, Uri endpoint)
        {
            _pipeline = pipeline;
            _credential = credential;
            _endpoint = endpoint;
        }

        /// <summary> Creates an embedding vector representing the input text. </summary>
        /// <param name="embedding"> The <see cref="CreateEmbeddingRequest"/> to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="embedding"/> is null. </exception>
        public virtual async Task<ClientResult<CreateEmbeddingResponse>> CreateEmbeddingAsync(CreateEmbeddingRequest embedding)
        {
            Argument.AssertNotNull(embedding, nameof(embedding));

            using BinaryContent content = BinaryContent.Create(embedding);
            ClientResult result = await CreateEmbeddingAsync(content, DefaultRequestContext).ConfigureAwait(false);
            return ClientResult.FromValue(CreateEmbeddingResponse.FromResponse(result.GetRawResponse()), result.GetRawResponse());
        }

        /// <summary> Creates an embedding vector representing the input text. </summary>
        /// <param name="embedding"> The <see cref="CreateEmbeddingRequest"/> to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="embedding"/> is null. </exception>
        public virtual ClientResult<CreateEmbeddingResponse> CreateEmbedding(CreateEmbeddingRequest embedding)
        {
            Argument.AssertNotNull(embedding, nameof(embedding));

            using BinaryContent content = BinaryContent.Create(embedding);
            ClientResult result = CreateEmbedding(content, DefaultRequestContext);
            return ClientResult.FromValue(CreateEmbeddingResponse.FromResponse(result.GetRawResponse()), result.GetRawResponse());
        }

        /// <summary>
        /// [Protocol Method] Creates an embedding vector representing the input text.
        /// <list type="bullet">
        /// <item>
        /// <description>
        /// This <see href="https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/core/Azure.Core/samples/ProtocolMethods.md">protocol method</see> allows explicit creation of the request and processing of the response for advanced scenarios.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Please try the simpler <see cref="CreateEmbeddingAsync(CreateEmbeddingRequest)"/> convenience overload with strongly typed models first.
        /// </description>
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="content"> The content to send as the body of the request. </param>
        /// <param name="options"> The request options, which can override default behaviors of the client pipeline on a per-call basis. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="content"/> is null. </exception>
        /// <exception cref="ClientResultException"> Service returned a non-success status code. </exception>
        /// <returns> The response returned from the service. </returns>
        public virtual async Task<ClientResult> CreateEmbeddingAsync(BinaryContent content, RequestOptions options = null)
        {
            Argument.AssertNotNull(content, nameof(content));

            options ??= new RequestOptions();
            // using var scope = ClientDiagnostics.CreateSpan("Embeddings.CreateEmbedding"\);
            // scope.Start();
            try
            {
                using PipelineMessage message = CreateCreateEmbeddingRequest(content, options);
                return ClientResult.FromResponse(await _pipeline.ProcessMessageAsync(message, options).ConfigureAwait(false));
            }
            catch (Exception e)
            {
                // scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// [Protocol Method] Creates an embedding vector representing the input text.
        /// <list type="bullet">
        /// <item>
        /// <description>
        /// This <see href="https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/core/Azure.Core/samples/ProtocolMethods.md">protocol method</see> allows explicit creation of the request and processing of the response for advanced scenarios.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Please try the simpler <see cref="CreateEmbedding(CreateEmbeddingRequest)"/> convenience overload with strongly typed models first.
        /// </description>
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="content"> The content to send as the body of the request. </param>
        /// <param name="options"> The request options, which can override default behaviors of the client pipeline on a per-call basis. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="content"/> is null. </exception>
        /// <exception cref="ClientResultException"> Service returned a non-success status code. </exception>
        /// <returns> The response returned from the service. </returns>
        public virtual ClientResult CreateEmbedding(BinaryContent content, RequestOptions options = null)
        {
            Argument.AssertNotNull(content, nameof(content));

            options ??= new RequestOptions();
            // using var scope = ClientDiagnostics.CreateSpan("Embeddings.CreateEmbedding"\);
            // scope.Start();
            try
            {
                using PipelineMessage message = CreateCreateEmbeddingRequest(content, options);
                return ClientResult.FromResponse(_pipeline.ProcessMessage(message, options));
            }
            catch (Exception e)
            {
                // scope.Failed(e);
                throw;
            }
        }

        internal PipelineMessage CreateCreateEmbeddingRequest(BinaryContent content, RequestOptions options)
        {
            PipelineMessage message = _pipeline.CreateMessage();
            message.ResponseClassifier = ResponseErrorClassifier200;
            PipelineRequest request = message.Request;
            request.Method = "POST";
            UriBuilder uriBuilder = new(_endpoint.ToString());
            StringBuilder path = new();
            path.Append("/embeddings");
            uriBuilder.Path += path.ToString();
            request.Uri = uriBuilder.Uri;
            request.Headers.Set("Accept", "application/json");
            request.Headers.Set("Content-Type", "application/json");
            request.Content = content;
            message.Apply(options);
            return message;
        }

        private static RequestOptions DefaultRequestContext = new RequestOptions();

        private static PipelineMessageClassifier _responseErrorClassifier200;
        private static PipelineMessageClassifier ResponseErrorClassifier200 => _responseErrorClassifier200 ??= PipelineMessageClassifier.Create(stackalloc ushort[] { 200 });
    }
}
