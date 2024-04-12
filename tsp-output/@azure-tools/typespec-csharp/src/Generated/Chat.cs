// <auto-generated/>

#nullable disable

using System;
using System.ClientModel;
using System.ClientModel.Primitives;
using System.Threading;
using System.Threading.Tasks;
using OpenAI.Models;

namespace OpenAI
{
    // Data plane generated sub-client.
    /// <summary> The Chat sub-client. </summary>
    public partial class Chat
    {
        private const string AuthorizationHeader = "Authorization";
        private readonly ApiKeyCredential _keyCredential;
        private const string AuthorizationApiKeyPrefix = "Bearer";
        private readonly ClientPipeline _pipeline;
        private readonly Uri _endpoint;

        protected Uri Endpoint => _endpoint;

        protected virtual ModelReaderWriterOptions ModelReaderWriterOptions =>
            new ModelReaderWriterOptions("W");

        /// <summary> The HTTP pipeline for sending and receiving REST requests and responses. </summary>
        public virtual ClientPipeline Pipeline => _pipeline;

        /// <summary> Initializes a new instance of Chat for mocking. </summary>
        protected Chat()
        {
        }

        /// <summary> Initializes a new instance of Chat. </summary>
        /// <param name="pipeline"> The HTTP pipeline for sending and receiving REST requests and responses. </param>
        /// <param name="keyCredential"> The key credential to copy. </param>
        /// <param name="endpoint"> OpenAI Endpoint. </param>
        protected internal Chat(ClientPipeline pipeline, ApiKeyCredential keyCredential, Uri endpoint)
        {
            _pipeline = pipeline;
            _keyCredential = keyCredential;
            _endpoint = endpoint;
        }

        /// <summary> Creates a model response for the given chat conversation. </summary>
        /// <param name="createChatCompletionRequest"> The <see cref="CreateChatCompletionRequest"/> to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="createChatCompletionRequest"/> is null. </exception>
        public virtual async Task<ClientResult<CreateChatCompletionResponse>> CreateChatCompletionAsync(CreateChatCompletionRequest createChatCompletionRequest, CancellationToken cancellationToken = default)
        {
            Argument.AssertNotNull(createChatCompletionRequest, nameof(createChatCompletionRequest));

            RequestOptions context = FromCancellationToken(cancellationToken);
            using BinaryContent content = createChatCompletionRequest.ToBinaryBody(ModelReaderWriterOptions);
            ClientResult result = await CreateChatCompletionAsync(createChatCompletionRequest.Model.ToString(), content, context).ConfigureAwait(false);
            return ClientResult.FromValue(CreateChatCompletionResponse.FromResponse(result.GetRawResponse()), result.GetRawResponse());
        }

        /// <summary> Creates a model response for the given chat conversation. </summary>
        /// <param name="createChatCompletionRequest"> The <see cref="CreateChatCompletionRequest"/> to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="createChatCompletionRequest"/> is null. </exception>
        public virtual ClientResult<CreateChatCompletionResponse> CreateChatCompletion(CreateChatCompletionRequest createChatCompletionRequest, CancellationToken cancellationToken = default)
        {
            Argument.AssertNotNull(createChatCompletionRequest, nameof(createChatCompletionRequest));

            RequestOptions context = FromCancellationToken(cancellationToken);
            using BinaryContent content = createChatCompletionRequest.ToBinaryBody(ModelReaderWriterOptions);
            ClientResult result = CreateChatCompletion(createChatCompletionRequest.Model.ToString(), content, context);
            return ClientResult.FromValue(CreateChatCompletionResponse.FromResponse(result.GetRawResponse()), result.GetRawResponse());
        }

        /// <summary>
        /// [Protocol Method] Creates a model response for the given chat conversation.
        /// <list type="bullet">
        /// <item>
        /// <description>
        /// This <see href="https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/core/Azure.Core/samples/ProtocolMethods.md">protocol method</see> allows explicit creation of the request and processing of the response for advanced scenarios.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Please try the simpler <see cref="CreateChatCompletionAsync(CreateChatCompletionRequest,CancellationToken)"/> convenience overload with strongly typed models first.
        /// </description>
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="content"> The content to send as the body of the request. </param>
        /// <param name="context"> The request context, which can override default behaviors of the client pipeline on a per-call basis. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="content"/> is null. </exception>
        /// <exception cref="ClientResultException"> Service returned a non-success status code. </exception>
        /// <returns> The response returned from the service. </returns>
        public virtual async Task<ClientResult> CreateChatCompletionAsync(string model, BinaryContent content, RequestOptions context = null)
        {
            Argument.AssertNotNull(content, nameof(content));

            using PipelineMessage message = CreateCreateChatCompletionRequest(model, content, context);
            return ClientResult.FromResponse(await _pipeline.ProcessMessageAsync(message, context).ConfigureAwait(false));
        }

        /// <summary>
        /// [Protocol Method] Creates a model response for the given chat conversation.
        /// <list type="bullet">
        /// <item>
        /// <description>
        /// This <see href="https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/core/Azure.Core/samples/ProtocolMethods.md">protocol method</see> allows explicit creation of the request and processing of the response for advanced scenarios.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Please try the simpler <see cref="CreateChatCompletion(CreateChatCompletionRequest,CancellationToken)"/> convenience overload with strongly typed models first.
        /// </description>
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="content"> The content to send as the body of the request. </param>
        /// <param name="context"> The request context, which can override default behaviors of the client pipeline on a per-call basis. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="content"/> is null. </exception>
        /// <exception cref="ClientResultException"> Service returned a non-success status code. </exception>
        /// <returns> The response returned from the service. </returns>

        // TODO: Note -- maybe we don't want to add a required parameter to the protocol
        // method on the base type?

        public virtual ClientResult CreateChatCompletion(string model, BinaryContent content, RequestOptions context = null)
        {
            Argument.AssertNotNull(model, nameof(model));
            Argument.AssertNotNull(content, nameof(content));

            using PipelineMessage message = CreateCreateChatCompletionRequest(model, content, context);
            return ClientResult.FromResponse(_pipeline.ProcessMessage(message, context));
        }

        protected virtual PipelineMessage CreateCreateChatCompletionRequest(string model, BinaryContent content, RequestOptions context)
        {
            var message = _pipeline.CreateMessage();
            if (context != null)
            {
                message.Apply(context);
            }
            message.ResponseClassifier = PipelineMessageClassifier200;
            var request = message.Request;
            request.Method = "POST";
            var uri = new ClientUriBuilder();
            uri.Reset(_endpoint);
            uri.AppendPath("/chat/completions", false);
            request.Uri = uri.ToUri();
            request.Headers.Set("Accept", "application/json");
            request.Headers.Set("Content-Type", "application/json");
            request.Content = content;
            return message;
        }

        private static RequestOptions DefaultRequestContext = new RequestOptions();
        internal static RequestOptions FromCancellationToken(CancellationToken cancellationToken = default)
        {
            if (!cancellationToken.CanBeCanceled)
            {
                return DefaultRequestContext;
            }

            return new RequestOptions() { CancellationToken = cancellationToken };
        }

        private static PipelineMessageClassifier _pipelineMessageClassifier200;
        private static PipelineMessageClassifier PipelineMessageClassifier200 => _pipelineMessageClassifier200 ??= PipelineMessageClassifier.Create(stackalloc ushort[] { 200 });
    }
}
