// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.ClientModel;
using System.ClientModel.Primitives;

namespace ClientModel.Tests.Mocks;

public class MockPipelineTransport : PipelineTransport
{
    private readonly Func<int, (int Status, BinaryData? Content)> _responseFactory;
    private int _retryCount;

    public string Id { get; }

    public Action<int, PipelineMessage>? OnSendingRequest { get; set; }
    public Action<int, PipelineMessage>? OnReceivedResponse { get; set; }

    public MockPipelineTransport(string id, params int[] codes)
        : this(id, i => (codes[i], BinaryData.FromString(string.Empty)))
    {
    }

    public MockPipelineTransport(string id, Func<int, (int Status, BinaryData? Content)> responseFactory)
    {
        Id = id;
        _responseFactory = responseFactory;
    }

    protected override PipelineMessage CreateMessageCore()
    {
        return new RetriableTransportMessage();
    }

    protected override void ProcessCore(PipelineMessage message)
    {
        try
        {
            Stamp(message, "Transport");

            OnSendingRequest?.Invoke(_retryCount, message);

            if (message is RetriableTransportMessage transportMessage)
            {
                (int status, BinaryData? content) = _responseFactory(_retryCount);
                transportMessage.SetResponse(status, content);
            }

            OnReceivedResponse?.Invoke(_retryCount, message);
        }
        finally
        {
            _retryCount++;
        }
    }

    protected override ValueTask ProcessCoreAsync(PipelineMessage message)
    {
        try
        {
            Stamp(message, "Transport");

            OnSendingRequest?.Invoke(_retryCount, message);

            if (message is RetriableTransportMessage transportMessage)
            {
                (int status, BinaryData? content) = _responseFactory(_retryCount);
                transportMessage.SetResponse(status, content);
            }

            OnReceivedResponse?.Invoke(_retryCount, message);
        }
        finally
        {
            _retryCount++;
        }

        return new ValueTask();
    }

    private void Stamp(PipelineMessage message, string prefix)
    {
        List<string> values;

        if (message.TryGetProperty(typeof(ObservablePolicy), out object? prop) &&
            prop is List<string> list)
        {
            values = list;
        }
        else
        {
            values = new List<string>();
            message.SetProperty(typeof(ObservablePolicy), values);
        }

        values.Add($"{prefix}:{Id}");
    }

    private class RetriableTransportMessage : PipelineMessage
    {
        public RetriableTransportMessage() : this(new TransportRequest())
        {
        }

        protected internal RetriableTransportMessage(PipelineRequest request) : base(request)
        {
        }

        public void SetResponse(int status, BinaryData? content)
        {
            Response = new RetriableTransportResponse(status, content);
        }
    }

    private class TransportRequest : PipelineRequest
    {
        private Uri? _uri;
        private readonly PipelineRequestHeaders _headers;
        private string _method;
        private BinaryContent? _content;

        public TransportRequest()
        {
            _headers = new MockRequestHeaders();
            _uri = new Uri("https://www.example.com");
            _method = "GET";
        }

        public override void Dispose() { }

        protected override BinaryContent? ContentCore
        {
            get => _content;
            set => _content = value;
        }

        protected override PipelineRequestHeaders HeadersCore
            => _headers;

        protected override string MethodCore
        {
            get => _method;
            set => _method = value;
        }

        protected override Uri? UriCore
        {
            get => _uri;
            set => _uri = value;
        }
    }

    private class RetriableTransportResponse : PipelineResponse
    {
        private Stream? _contentStream;
        private BinaryData _content;

        public RetriableTransportResponse(int status, BinaryData? content)
        {
            Status = status;
            ContentStream = content?.ToStream();
            _content = content ?? BinaryData.FromString(string.Empty);
        }

        public override int Status { get; }

        public override string ReasonPhrase => throw new NotImplementedException();

        public override Stream? ContentStream
        {
            get => _contentStream;
            set => _contentStream = value;
        }

        public override BinaryData Content => _content;

        protected override PipelineResponseHeaders HeadersCore
            => throw new NotImplementedException();

        public override void Dispose() { }

        public override BinaryData BufferContent(CancellationToken cancellationToken = default)
        {
            return _content = _contentStream == null ?
                BinaryData.FromString(string.Empty) :
                BinaryData.FromStream(_contentStream);
        }

        public override ValueTask<BinaryData> BufferContentAsync(CancellationToken cancellationToken = default)
        {
            return new(_content = _contentStream == null ?
                BinaryData.FromString(string.Empty) :
                BinaryData.FromStream(_contentStream));
        }
    }
}
