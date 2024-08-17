// <auto-generated/>

#nullable disable

using System;
using System.ClientModel;
using System.ClientModel.Primitives;
using System.Threading.Tasks;

namespace OpenAI.LegacyCompletions
{
    // Data plane generated sub-client.
    internal partial class LegacyCompletionClient
    {
        private const string AuthorizationHeader = "Authorization";
        private readonly ApiKeyCredential _keyCredential;
        private const string AuthorizationApiKeyPrefix = "Bearer";
        private readonly ClientPipeline _pipeline;
        private readonly Uri _endpoint;

        public virtual ClientPipeline Pipeline => _pipeline;

        protected LegacyCompletionClient()
        {
        }

        internal LegacyCompletionClient(ClientPipeline pipeline, ApiKeyCredential keyCredential, Uri endpoint)
        {
            _pipeline = pipeline;
            _keyCredential = keyCredential;
            _endpoint = endpoint;
        }

        public virtual async Task<ClientResult<InternalCreateCompletionResponse>> CreateCompletionAsync(InternalCreateCompletionRequest requestBody)
        {
            Argument.AssertNotNull(requestBody, nameof(requestBody));

            using BinaryContent content = requestBody.ToBinaryContent();
            ClientResult result = await CreateCompletionAsync(content, null).ConfigureAwait(false);
            return ClientResult.FromValue(InternalCreateCompletionResponse.FromResponse(result.GetRawResponse()), result.GetRawResponse());
        }

        public virtual ClientResult<InternalCreateCompletionResponse> CreateCompletion(InternalCreateCompletionRequest requestBody)
        {
            Argument.AssertNotNull(requestBody, nameof(requestBody));

            using BinaryContent content = requestBody.ToBinaryContent();
            ClientResult result = CreateCompletion(content, null);
            return ClientResult.FromValue(InternalCreateCompletionResponse.FromResponse(result.GetRawResponse()), result.GetRawResponse());
        }

        public virtual async Task<ClientResult> CreateCompletionAsync(BinaryContent content, RequestOptions options = null)
        {
            Argument.AssertNotNull(content, nameof(content));

            using PipelineMessage message = CreateCreateCompletionRequest(content, options);
            return ClientResult.FromResponse(await _pipeline.ProcessMessageAsync(message, options).ConfigureAwait(false));
        }

        public virtual ClientResult CreateCompletion(BinaryContent content, RequestOptions options = null)
        {
            Argument.AssertNotNull(content, nameof(content));

            using PipelineMessage message = CreateCreateCompletionRequest(content, options);
            return ClientResult.FromResponse(_pipeline.ProcessMessage(message, options));
        }

        internal PipelineMessage CreateCreateCompletionRequest(BinaryContent content, RequestOptions options)
        {
            var message = _pipeline.CreateMessage();
            message.ResponseClassifier = PipelineMessageClassifier200;
            var request = message.Request;
            request.Method = "POST";
            UriBuilder uriBuilder = new UriBuilder(_endpoint);
            
            uriBuilder.AppendPath("/completions", false);
            request.Uri = uriBuilder.Uri;
            request.Headers.Set("Accept", "application/json");
            request.Headers.Set("Content-Type", "application/json");
            request.Content = content;
            message.Apply(options);
            return message;
        }

        private static PipelineMessageClassifier _pipelineMessageClassifier200;
        private static PipelineMessageClassifier PipelineMessageClassifier200 => _pipelineMessageClassifier200 ??= PipelineMessageClassifier.Create(stackalloc ushort[] { 200 });
    }
}
