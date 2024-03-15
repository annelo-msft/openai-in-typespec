// <auto-generated/>

#nullable disable

using System;
using System.ClientModel;
using System.ClientModel.Internal;
using System.ClientModel.Primitives;
using System.ClientModel.Primitives.Pipeline;
using System.Threading;
using System.Threading.Tasks;
using OpenAI.Models;

namespace OpenAI
{
    // Data plane generated sub-client.
    /// <summary> The FineTuning sub-client. </summary>
    public partial class FineTuning
    {
        private const string AuthorizationHeader = "Authorization";
        private readonly KeyCredential _keyCredential;
        private const string AuthorizationApiKeyPrefix = "Bearer";
        private readonly MessagePipeline _pipeline;
        private readonly Uri _endpoint;

        /// <summary> The ClientDiagnostics is used to provide tracing support for the client library. </summary>
        internal TelemetrySource ClientDiagnostics { get; }

        /// <summary> The HTTP pipeline for sending and receiving REST requests and responses. </summary>
        public virtual MessagePipeline Pipeline => _pipeline;

        /// <summary> Initializes a new instance of FineTuning for mocking. </summary>
        protected FineTuning()
        {
        }

        /// <summary> Initializes a new instance of FineTuning. </summary>
        /// <param name="clientDiagnostics"> The handler for diagnostic messaging in the client. </param>
        /// <param name="pipeline"> The HTTP pipeline for sending and receiving REST requests and responses. </param>
        /// <param name="keyCredential"> The key credential to copy. </param>
        /// <param name="endpoint"> OpenAI Endpoint. </param>
        internal FineTuning(TelemetrySource clientDiagnostics, MessagePipeline pipeline, KeyCredential keyCredential, Uri endpoint)
        {
            ClientDiagnostics = clientDiagnostics;
            _pipeline = pipeline;
            _keyCredential = keyCredential;
            _endpoint = endpoint;
        }

        /// <summary>
        /// Creates a fine-tuning job which begins the process of creating a new model from a given dataset.
        ///
        /// Response includes details of the enqueued job including job status and the name of the fine-tuned models once complete.
        ///
        /// [Learn more about fine-tuning](/docs/guides/fine-tuning)
        /// </summary>
        /// <param name="job"> The <see cref="CreateFineTuningJobRequest"/> to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="job"/> is null. </exception>
        public virtual async Task<Result<FineTuningJob>> CreateFineTuningJobAsync(CreateFineTuningJobRequest job, CancellationToken cancellationToken = default)
        {
            ClientUtilities.AssertNotNull(job, nameof(job));

            RequestOptions context = FromCancellationToken(cancellationToken);
            using RequestBody content = job.ToRequestBody();
            Result result = await CreateFineTuningJobAsync(content, context).ConfigureAwait(false);
            return Result.FromValue(FineTuningJob.FromResponse(result.GetRawResponse()), result.GetRawResponse());
        }

        /// <summary>
        /// Creates a fine-tuning job which begins the process of creating a new model from a given dataset.
        ///
        /// Response includes details of the enqueued job including job status and the name of the fine-tuned models once complete.
        ///
        /// [Learn more about fine-tuning](/docs/guides/fine-tuning)
        /// </summary>
        /// <param name="job"> The <see cref="CreateFineTuningJobRequest"/> to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="job"/> is null. </exception>
        public virtual Result<FineTuningJob> CreateFineTuningJob(CreateFineTuningJobRequest job, CancellationToken cancellationToken = default)
        {
            ClientUtilities.AssertNotNull(job, nameof(job));

            RequestOptions context = FromCancellationToken(cancellationToken);
            using RequestBody content = job.ToRequestBody();
            Result result = CreateFineTuningJob(content, context);
            return Result.FromValue(FineTuningJob.FromResponse(result.GetRawResponse()), result.GetRawResponse());
        }

        /// <summary>
        /// [Protocol Method] Creates a fine-tuning job which begins the process of creating a new model from a given dataset.
        ///
        /// Response includes details of the enqueued job including job status and the name of the fine-tuned models once complete.
        ///
        /// [Learn more about fine-tuning](/docs/guides/fine-tuning)
        /// <list type="bullet">
        /// <item>
        /// <description>
        /// This <see href="https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/core/Azure.Core/samples/ProtocolMethods.md">protocol method</see> allows explicit creation of the request and processing of the response for advanced scenarios.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Please try the simpler <see cref="CreateFineTuningJobAsync(CreateFineTuningJobRequest,CancellationToken)"/> convenience overload with strongly typed models first.
        /// </description>
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="content"> The content to send as the body of the request. </param>
        /// <param name="context"> The request context, which can override default behaviors of the client pipeline on a per-call basis. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="content"/> is null. </exception>
        /// <exception cref="MessageFailedException"> Service returned a non-success status code. </exception>
        /// <returns> The response returned from the service. </returns>
        public virtual async Task<Result> CreateFineTuningJobAsync(RequestBody content, RequestOptions context = null)
        {
            ClientUtilities.AssertNotNull(content, nameof(content));

            using var scope = ClientDiagnostics.CreateSpan("FineTuning.CreateFineTuningJob");
            scope.Start();
            try
            {
                using PipelineMessage message = CreateCreateFineTuningJobRequest(content, context);
                return Result.FromResponse(await _pipeline.ProcessMessageAsync(message, context).ConfigureAwait(false));
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// [Protocol Method] Creates a fine-tuning job which begins the process of creating a new model from a given dataset.
        ///
        /// Response includes details of the enqueued job including job status and the name of the fine-tuned models once complete.
        ///
        /// [Learn more about fine-tuning](/docs/guides/fine-tuning)
        /// <list type="bullet">
        /// <item>
        /// <description>
        /// This <see href="https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/core/Azure.Core/samples/ProtocolMethods.md">protocol method</see> allows explicit creation of the request and processing of the response for advanced scenarios.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Please try the simpler <see cref="CreateFineTuningJob(CreateFineTuningJobRequest,CancellationToken)"/> convenience overload with strongly typed models first.
        /// </description>
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="content"> The content to send as the body of the request. </param>
        /// <param name="context"> The request context, which can override default behaviors of the client pipeline on a per-call basis. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="content"/> is null. </exception>
        /// <exception cref="MessageFailedException"> Service returned a non-success status code. </exception>
        /// <returns> The response returned from the service. </returns>
        public virtual Result CreateFineTuningJob(RequestBody content, RequestOptions context = null)
        {
            ClientUtilities.AssertNotNull(content, nameof(content));

            using var scope = ClientDiagnostics.CreateSpan("FineTuning.CreateFineTuningJob");
            scope.Start();
            try
            {
                using PipelineMessage message = CreateCreateFineTuningJobRequest(content, context);
                return Result.FromResponse(_pipeline.ProcessMessage(message, context));
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary> List your organization's fine-tuning jobs. </summary>
        /// <param name="after"> Identifier for the last job from the previous pagination request. </param>
        /// <param name="limit"> Number of fine-tuning jobs to retrieve. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async Task<Result<ListPaginatedFineTuningJobsResponse>> GetPaginatedFineTuningJobsAsync(string after = null, int? limit = null, CancellationToken cancellationToken = default)
        {
            RequestOptions context = FromCancellationToken(cancellationToken);
            Result result = await GetPaginatedFineTuningJobsAsync(after, limit, context).ConfigureAwait(false);
            return Result.FromValue(ListPaginatedFineTuningJobsResponse.FromResponse(result.GetRawResponse()), result.GetRawResponse());
        }

        /// <summary> List your organization's fine-tuning jobs. </summary>
        /// <param name="after"> Identifier for the last job from the previous pagination request. </param>
        /// <param name="limit"> Number of fine-tuning jobs to retrieve. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Result<ListPaginatedFineTuningJobsResponse> GetPaginatedFineTuningJobs(string after = null, int? limit = null, CancellationToken cancellationToken = default)
        {
            RequestOptions context = FromCancellationToken(cancellationToken);
            Result result = GetPaginatedFineTuningJobs(after, limit, context);
            return Result.FromValue(ListPaginatedFineTuningJobsResponse.FromResponse(result.GetRawResponse()), result.GetRawResponse());
        }

        /// <summary>
        /// [Protocol Method] List your organization's fine-tuning jobs
        /// <list type="bullet">
        /// <item>
        /// <description>
        /// This <see href="https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/core/Azure.Core/samples/ProtocolMethods.md">protocol method</see> allows explicit creation of the request and processing of the response for advanced scenarios.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Please try the simpler <see cref="GetPaginatedFineTuningJobsAsync(string,int?,CancellationToken)"/> convenience overload with strongly typed models first.
        /// </description>
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="after"> Identifier for the last job from the previous pagination request. </param>
        /// <param name="limit"> Number of fine-tuning jobs to retrieve. </param>
        /// <param name="context"> The request context, which can override default behaviors of the client pipeline on a per-call basis. </param>
        /// <exception cref="MessageFailedException"> Service returned a non-success status code. </exception>
        /// <returns> The response returned from the service. </returns>
        public virtual async Task<Result> GetPaginatedFineTuningJobsAsync(string after, int? limit, RequestOptions context)
        {
            using var scope = ClientDiagnostics.CreateSpan("FineTuning.GetPaginatedFineTuningJobs");
            scope.Start();
            try
            {
                using PipelineMessage message = CreateGetPaginatedFineTuningJobsRequest(after, limit, context);
                return Result.FromResponse(await _pipeline.ProcessMessageAsync(message, context).ConfigureAwait(false));
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// [Protocol Method] List your organization's fine-tuning jobs
        /// <list type="bullet">
        /// <item>
        /// <description>
        /// This <see href="https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/core/Azure.Core/samples/ProtocolMethods.md">protocol method</see> allows explicit creation of the request and processing of the response for advanced scenarios.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Please try the simpler <see cref="GetPaginatedFineTuningJobs(string,int?,CancellationToken)"/> convenience overload with strongly typed models first.
        /// </description>
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="after"> Identifier for the last job from the previous pagination request. </param>
        /// <param name="limit"> Number of fine-tuning jobs to retrieve. </param>
        /// <param name="context"> The request context, which can override default behaviors of the client pipeline on a per-call basis. </param>
        /// <exception cref="MessageFailedException"> Service returned a non-success status code. </exception>
        /// <returns> The response returned from the service. </returns>
        public virtual Result GetPaginatedFineTuningJobs(string after, int? limit, RequestOptions context)
        {
            using var scope = ClientDiagnostics.CreateSpan("FineTuning.GetPaginatedFineTuningJobs");
            scope.Start();
            try
            {
                using PipelineMessage message = CreateGetPaginatedFineTuningJobsRequest(after, limit, context);
                return Result.FromResponse(_pipeline.ProcessMessage(message, context));
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// Get info about a fine-tuning job.
        ///
        /// [Learn more about fine-tuning](/docs/guides/fine-tuning)
        /// </summary>
        /// <param name="fineTuningJobId"> The ID of the fine-tuning job. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="fineTuningJobId"/> is null. </exception>
        /// <exception cref="ArgumentException"> <paramref name="fineTuningJobId"/> is an empty string, and was expected to be non-empty. </exception>
        public virtual async Task<Result<FineTuningJob>> RetrieveFineTuningJobAsync(string fineTuningJobId, CancellationToken cancellationToken = default)
        {
            ClientUtilities.AssertNotNullOrEmpty(fineTuningJobId, nameof(fineTuningJobId));

            RequestOptions context = FromCancellationToken(cancellationToken);
            Result result = await RetrieveFineTuningJobAsync(fineTuningJobId, context).ConfigureAwait(false);
            return Result.FromValue(FineTuningJob.FromResponse(result.GetRawResponse()), result.GetRawResponse());
        }

        /// <summary>
        /// Get info about a fine-tuning job.
        ///
        /// [Learn more about fine-tuning](/docs/guides/fine-tuning)
        /// </summary>
        /// <param name="fineTuningJobId"> The ID of the fine-tuning job. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="fineTuningJobId"/> is null. </exception>
        /// <exception cref="ArgumentException"> <paramref name="fineTuningJobId"/> is an empty string, and was expected to be non-empty. </exception>
        public virtual Result<FineTuningJob> RetrieveFineTuningJob(string fineTuningJobId, CancellationToken cancellationToken = default)
        {
            ClientUtilities.AssertNotNullOrEmpty(fineTuningJobId, nameof(fineTuningJobId));

            RequestOptions context = FromCancellationToken(cancellationToken);
            Result result = RetrieveFineTuningJob(fineTuningJobId, context);
            return Result.FromValue(FineTuningJob.FromResponse(result.GetRawResponse()), result.GetRawResponse());
        }

        /// <summary>
        /// [Protocol Method] Get info about a fine-tuning job.
        ///
        /// [Learn more about fine-tuning](/docs/guides/fine-tuning)
        /// <list type="bullet">
        /// <item>
        /// <description>
        /// This <see href="https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/core/Azure.Core/samples/ProtocolMethods.md">protocol method</see> allows explicit creation of the request and processing of the response for advanced scenarios.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Please try the simpler <see cref="RetrieveFineTuningJobAsync(string,CancellationToken)"/> convenience overload with strongly typed models first.
        /// </description>
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="fineTuningJobId"> The ID of the fine-tuning job. </param>
        /// <param name="context"> The request context, which can override default behaviors of the client pipeline on a per-call basis. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="fineTuningJobId"/> is null. </exception>
        /// <exception cref="ArgumentException"> <paramref name="fineTuningJobId"/> is an empty string, and was expected to be non-empty. </exception>
        /// <exception cref="MessageFailedException"> Service returned a non-success status code. </exception>
        /// <returns> The response returned from the service. </returns>
        public virtual async Task<Result> RetrieveFineTuningJobAsync(string fineTuningJobId, RequestOptions context)
        {
            ClientUtilities.AssertNotNullOrEmpty(fineTuningJobId, nameof(fineTuningJobId));

            using var scope = ClientDiagnostics.CreateSpan("FineTuning.RetrieveFineTuningJob");
            scope.Start();
            try
            {
                using PipelineMessage message = CreateRetrieveFineTuningJobRequest(fineTuningJobId, context);
                return Result.FromResponse(await _pipeline.ProcessMessageAsync(message, context).ConfigureAwait(false));
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// [Protocol Method] Get info about a fine-tuning job.
        ///
        /// [Learn more about fine-tuning](/docs/guides/fine-tuning)
        /// <list type="bullet">
        /// <item>
        /// <description>
        /// This <see href="https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/core/Azure.Core/samples/ProtocolMethods.md">protocol method</see> allows explicit creation of the request and processing of the response for advanced scenarios.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Please try the simpler <see cref="RetrieveFineTuningJob(string,CancellationToken)"/> convenience overload with strongly typed models first.
        /// </description>
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="fineTuningJobId"> The ID of the fine-tuning job. </param>
        /// <param name="context"> The request context, which can override default behaviors of the client pipeline on a per-call basis. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="fineTuningJobId"/> is null. </exception>
        /// <exception cref="ArgumentException"> <paramref name="fineTuningJobId"/> is an empty string, and was expected to be non-empty. </exception>
        /// <exception cref="MessageFailedException"> Service returned a non-success status code. </exception>
        /// <returns> The response returned from the service. </returns>
        public virtual Result RetrieveFineTuningJob(string fineTuningJobId, RequestOptions context)
        {
            ClientUtilities.AssertNotNullOrEmpty(fineTuningJobId, nameof(fineTuningJobId));

            using var scope = ClientDiagnostics.CreateSpan("FineTuning.RetrieveFineTuningJob");
            scope.Start();
            try
            {
                using PipelineMessage message = CreateRetrieveFineTuningJobRequest(fineTuningJobId, context);
                return Result.FromResponse(_pipeline.ProcessMessage(message, context));
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary> Immediately cancel a fine-tune job. </summary>
        /// <param name="fineTuningJobId"> The ID of the fine-tuning job to cancel. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="fineTuningJobId"/> is null. </exception>
        /// <exception cref="ArgumentException"> <paramref name="fineTuningJobId"/> is an empty string, and was expected to be non-empty. </exception>
        public virtual async Task<Result<FineTuningJob>> CancelFineTuningJobAsync(string fineTuningJobId, CancellationToken cancellationToken = default)
        {
            ClientUtilities.AssertNotNullOrEmpty(fineTuningJobId, nameof(fineTuningJobId));

            RequestOptions context = FromCancellationToken(cancellationToken);
            Result result = await CancelFineTuningJobAsync(fineTuningJobId, context).ConfigureAwait(false);
            return Result.FromValue(FineTuningJob.FromResponse(result.GetRawResponse()), result.GetRawResponse());
        }

        /// <summary> Immediately cancel a fine-tune job. </summary>
        /// <param name="fineTuningJobId"> The ID of the fine-tuning job to cancel. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="fineTuningJobId"/> is null. </exception>
        /// <exception cref="ArgumentException"> <paramref name="fineTuningJobId"/> is an empty string, and was expected to be non-empty. </exception>
        public virtual Result<FineTuningJob> CancelFineTuningJob(string fineTuningJobId, CancellationToken cancellationToken = default)
        {
            ClientUtilities.AssertNotNullOrEmpty(fineTuningJobId, nameof(fineTuningJobId));

            RequestOptions context = FromCancellationToken(cancellationToken);
            Result result = CancelFineTuningJob(fineTuningJobId, context);
            return Result.FromValue(FineTuningJob.FromResponse(result.GetRawResponse()), result.GetRawResponse());
        }

        /// <summary>
        /// [Protocol Method] Immediately cancel a fine-tune job.
        /// <list type="bullet">
        /// <item>
        /// <description>
        /// This <see href="https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/core/Azure.Core/samples/ProtocolMethods.md">protocol method</see> allows explicit creation of the request and processing of the response for advanced scenarios.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Please try the simpler <see cref="CancelFineTuningJobAsync(string,CancellationToken)"/> convenience overload with strongly typed models first.
        /// </description>
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="fineTuningJobId"> The ID of the fine-tuning job to cancel. </param>
        /// <param name="context"> The request context, which can override default behaviors of the client pipeline on a per-call basis. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="fineTuningJobId"/> is null. </exception>
        /// <exception cref="ArgumentException"> <paramref name="fineTuningJobId"/> is an empty string, and was expected to be non-empty. </exception>
        /// <exception cref="MessageFailedException"> Service returned a non-success status code. </exception>
        /// <returns> The response returned from the service. </returns>
        public virtual async Task<Result> CancelFineTuningJobAsync(string fineTuningJobId, RequestOptions context)
        {
            ClientUtilities.AssertNotNullOrEmpty(fineTuningJobId, nameof(fineTuningJobId));

            using var scope = ClientDiagnostics.CreateSpan("FineTuning.CancelFineTuningJob");
            scope.Start();
            try
            {
                using PipelineMessage message = CreateCancelFineTuningJobRequest(fineTuningJobId, context);
                return Result.FromResponse(await _pipeline.ProcessMessageAsync(message, context).ConfigureAwait(false));
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// [Protocol Method] Immediately cancel a fine-tune job.
        /// <list type="bullet">
        /// <item>
        /// <description>
        /// This <see href="https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/core/Azure.Core/samples/ProtocolMethods.md">protocol method</see> allows explicit creation of the request and processing of the response for advanced scenarios.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Please try the simpler <see cref="CancelFineTuningJob(string,CancellationToken)"/> convenience overload with strongly typed models first.
        /// </description>
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="fineTuningJobId"> The ID of the fine-tuning job to cancel. </param>
        /// <param name="context"> The request context, which can override default behaviors of the client pipeline on a per-call basis. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="fineTuningJobId"/> is null. </exception>
        /// <exception cref="ArgumentException"> <paramref name="fineTuningJobId"/> is an empty string, and was expected to be non-empty. </exception>
        /// <exception cref="MessageFailedException"> Service returned a non-success status code. </exception>
        /// <returns> The response returned from the service. </returns>
        public virtual Result CancelFineTuningJob(string fineTuningJobId, RequestOptions context)
        {
            ClientUtilities.AssertNotNullOrEmpty(fineTuningJobId, nameof(fineTuningJobId));

            using var scope = ClientDiagnostics.CreateSpan("FineTuning.CancelFineTuningJob");
            scope.Start();
            try
            {
                using PipelineMessage message = CreateCancelFineTuningJobRequest(fineTuningJobId, context);
                return Result.FromResponse(_pipeline.ProcessMessage(message, context));
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary> Get status updates for a fine-tuning job. </summary>
        /// <param name="fineTuningJobId"> The ID of the fine-tuning job to get events for. </param>
        /// <param name="after"> Identifier for the last event from the previous pagination request. </param>
        /// <param name="limit"> Number of events to retrieve. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="fineTuningJobId"/> is null. </exception>
        /// <exception cref="ArgumentException"> <paramref name="fineTuningJobId"/> is an empty string, and was expected to be non-empty. </exception>
        public virtual async Task<Result<ListFineTuningJobEventsResponse>> GetFineTuningEventsAsync(string fineTuningJobId, string after = null, int? limit = null, CancellationToken cancellationToken = default)
        {
            ClientUtilities.AssertNotNullOrEmpty(fineTuningJobId, nameof(fineTuningJobId));

            RequestOptions context = FromCancellationToken(cancellationToken);
            Result result = await GetFineTuningEventsAsync(fineTuningJobId, after, limit, context).ConfigureAwait(false);
            return Result.FromValue(ListFineTuningJobEventsResponse.FromResponse(result.GetRawResponse()), result.GetRawResponse());
        }

        /// <summary> Get status updates for a fine-tuning job. </summary>
        /// <param name="fineTuningJobId"> The ID of the fine-tuning job to get events for. </param>
        /// <param name="after"> Identifier for the last event from the previous pagination request. </param>
        /// <param name="limit"> Number of events to retrieve. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="fineTuningJobId"/> is null. </exception>
        /// <exception cref="ArgumentException"> <paramref name="fineTuningJobId"/> is an empty string, and was expected to be non-empty. </exception>
        public virtual Result<ListFineTuningJobEventsResponse> GetFineTuningEvents(string fineTuningJobId, string after = null, int? limit = null, CancellationToken cancellationToken = default)
        {
            ClientUtilities.AssertNotNullOrEmpty(fineTuningJobId, nameof(fineTuningJobId));

            RequestOptions context = FromCancellationToken(cancellationToken);
            Result result = GetFineTuningEvents(fineTuningJobId, after, limit, context);
            return Result.FromValue(ListFineTuningJobEventsResponse.FromResponse(result.GetRawResponse()), result.GetRawResponse());
        }

        /// <summary>
        /// [Protocol Method] Get status updates for a fine-tuning job.
        /// <list type="bullet">
        /// <item>
        /// <description>
        /// This <see href="https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/core/Azure.Core/samples/ProtocolMethods.md">protocol method</see> allows explicit creation of the request and processing of the response for advanced scenarios.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Please try the simpler <see cref="GetFineTuningEventsAsync(string,string,int?,CancellationToken)"/> convenience overload with strongly typed models first.
        /// </description>
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="fineTuningJobId"> The ID of the fine-tuning job to get events for. </param>
        /// <param name="after"> Identifier for the last event from the previous pagination request. </param>
        /// <param name="limit"> Number of events to retrieve. </param>
        /// <param name="context"> The request context, which can override default behaviors of the client pipeline on a per-call basis. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="fineTuningJobId"/> is null. </exception>
        /// <exception cref="ArgumentException"> <paramref name="fineTuningJobId"/> is an empty string, and was expected to be non-empty. </exception>
        /// <exception cref="MessageFailedException"> Service returned a non-success status code. </exception>
        /// <returns> The response returned from the service. </returns>
        public virtual async Task<Result> GetFineTuningEventsAsync(string fineTuningJobId, string after, int? limit, RequestOptions context)
        {
            ClientUtilities.AssertNotNullOrEmpty(fineTuningJobId, nameof(fineTuningJobId));

            using var scope = ClientDiagnostics.CreateSpan("FineTuning.GetFineTuningEvents");
            scope.Start();
            try
            {
                using PipelineMessage message = CreateGetFineTuningEventsRequest(fineTuningJobId, after, limit, context);
                return Result.FromResponse(await _pipeline.ProcessMessageAsync(message, context).ConfigureAwait(false));
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// [Protocol Method] Get status updates for a fine-tuning job.
        /// <list type="bullet">
        /// <item>
        /// <description>
        /// This <see href="https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/core/Azure.Core/samples/ProtocolMethods.md">protocol method</see> allows explicit creation of the request and processing of the response for advanced scenarios.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Please try the simpler <see cref="GetFineTuningEvents(string,string,int?,CancellationToken)"/> convenience overload with strongly typed models first.
        /// </description>
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="fineTuningJobId"> The ID of the fine-tuning job to get events for. </param>
        /// <param name="after"> Identifier for the last event from the previous pagination request. </param>
        /// <param name="limit"> Number of events to retrieve. </param>
        /// <param name="context"> The request context, which can override default behaviors of the client pipeline on a per-call basis. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="fineTuningJobId"/> is null. </exception>
        /// <exception cref="ArgumentException"> <paramref name="fineTuningJobId"/> is an empty string, and was expected to be non-empty. </exception>
        /// <exception cref="MessageFailedException"> Service returned a non-success status code. </exception>
        /// <returns> The response returned from the service. </returns>
        public virtual Result GetFineTuningEvents(string fineTuningJobId, string after, int? limit, RequestOptions context)
        {
            ClientUtilities.AssertNotNullOrEmpty(fineTuningJobId, nameof(fineTuningJobId));

            using var scope = ClientDiagnostics.CreateSpan("FineTuning.GetFineTuningEvents");
            scope.Start();
            try
            {
                using PipelineMessage message = CreateGetFineTuningEventsRequest(fineTuningJobId, after, limit, context);
                return Result.FromResponse(_pipeline.ProcessMessage(message, context));
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        internal PipelineMessage CreateCreateFineTuningJobRequest(RequestBody content, RequestOptions context)
        {
            var message = _pipeline.CreateMessage(context, ResponseErrorClassifier200);
            var request = message.Request;
            request.SetMethod("POST");
            var uri = new RequestUri();
            uri.Reset(_endpoint);
            uri.AppendPath("/fine_tuning/jobs", false);
            request.Uri = uri.ToUri();
            request.SetHeaderValue("Accept", "application/json");
            request.SetHeaderValue("Content-Type", "application/json");
            request.Content = content;
            return message;
        }

        internal PipelineMessage CreateGetPaginatedFineTuningJobsRequest(string after, int? limit, RequestOptions context)
        {
            var message = _pipeline.CreateMessage(context, ResponseErrorClassifier200);
            var request = message.Request;
            request.SetMethod("GET");
            var uri = new RequestUri();
            uri.Reset(_endpoint);
            uri.AppendPath("/fine_tuning/jobs", false);
            if (after != null)
            {
                uri.AppendQuery("after", after, true);
            }
            if (limit != null)
            {
                uri.AppendQuery("limit", limit.Value, true);
            }
            request.Uri = uri.ToUri();
            request.SetHeaderValue("Accept", "application/json");
            return message;
        }

        internal PipelineMessage CreateRetrieveFineTuningJobRequest(string fineTuningJobId, RequestOptions context)
        {
            var message = _pipeline.CreateMessage(context, ResponseErrorClassifier200);
            var request = message.Request;
            request.SetMethod("GET");
            var uri = new RequestUri();
            uri.Reset(_endpoint);
            uri.AppendPath("/fine_tuning/jobs/", false);
            uri.AppendPath(fineTuningJobId, true);
            request.Uri = uri.ToUri();
            request.SetHeaderValue("Accept", "application/json");
            return message;
        }

        internal PipelineMessage CreateCancelFineTuningJobRequest(string fineTuningJobId, RequestOptions context)
        {
            var message = _pipeline.CreateMessage(context, ResponseErrorClassifier200);
            var request = message.Request;
            request.SetMethod("POST");
            var uri = new RequestUri();
            uri.Reset(_endpoint);
            uri.AppendPath("/fine_tuning/jobs/", false);
            uri.AppendPath(fineTuningJobId, true);
            uri.AppendPath("/cancel", false);
            request.Uri = uri.ToUri();
            request.SetHeaderValue("Accept", "application/json");
            return message;
        }

        internal PipelineMessage CreateGetFineTuningEventsRequest(string fineTuningJobId, string after, int? limit, RequestOptions context)
        {
            var message = _pipeline.CreateMessage(context, ResponseErrorClassifier200);
            var request = message.Request;
            request.SetMethod("GET");
            var uri = new RequestUri();
            uri.Reset(_endpoint);
            uri.AppendPath("/fine_tuning/jobs/", false);
            uri.AppendPath(fineTuningJobId, true);
            uri.AppendPath("/events", false);
            if (after != null)
            {
                uri.AppendQuery("after", after, true);
            }
            if (limit != null)
            {
                uri.AppendQuery("limit", limit.Value, true);
            }
            request.Uri = uri.ToUri();
            request.SetHeaderValue("Accept", "application/json");
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

        private static ResponseErrorClassifier _responseErrorClassifier200;
        private static ResponseErrorClassifier ResponseErrorClassifier200 => _responseErrorClassifier200 ??= new StatusResponseClassifier(stackalloc ushort[] { 200 });
    }
}
