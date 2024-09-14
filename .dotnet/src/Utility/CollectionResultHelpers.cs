using System.ClientModel;
using System.ClientModel.Primitives;
using System.Collections.Generic;
using System.Threading;

#nullable enable

namespace OpenAI;
internal class CollectionResultHelpers
{
    public static AsyncCollectionResult<T> CreateAsync<T>(PageEnumerator<T> enumerator)
        => new AsyncPaginatedCollectionResult<T>(enumerator);

    public static CollectionResult<T> Create<T>(PageEnumerator<T> enumerator)
        => new PaginatedCollectionResult<T>(enumerator);

    public static AsyncCollectionResult CreateAsync(PageEnumerator enumerator)
        => new AsyncPaginatedCollectionResult(enumerator);

    public static CollectionResult Create(PageRequestManager requestManager)
        => new PaginatedCollectionResult(requestManager);

    private class AsyncPaginatedCollectionResult<T> : AsyncCollectionResult<T>
    {
        private readonly PageEnumerator<T> _pageEnumerator;

        public AsyncPaginatedCollectionResult(PageEnumerator<T> pageEnumerator)
        {
            _pageEnumerator = pageEnumerator;
        }

        public async override IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            // TODO: use provided continuation token?
            while (await _pageEnumerator.MoveNextAsync())
            {
                IEnumerable<T> page = await _pageEnumerator.GetCurrentPageAsync();
                foreach (T value in page)
                {
                    yield return value;
                }
            }
        }

        public override ContinuationToken? GetContinuationToken(ClientResult page)
            => _pageEnumerator.GetNextPageToken(page);

        public async override IAsyncEnumerable<ClientResult> GetRawPagesAsync()
        {
            // TODO: this must use a different enumerator each time it's called.
            while (await _pageEnumerator.MoveNextAsync())
            {
                yield return _pageEnumerator.Current;
            }
        }
    }

    private class PaginatedCollectionResult<T> : CollectionResult<T>
    {
        private readonly PageRequestManager _requestManager;

        public PaginatedCollectionResult(PageRequestManager requestManager)
        {
            _requestManager = requestManager;
        }

        public override ContinuationToken? GetContinuationToken(ClientResult page)
            => _requestManager.GetNextPageToken(page);

        public override IEnumerator<T> GetEnumerator()
        {
            foreach(ClientResult page in GetRawPages())
            {
                foreach (T value in _requestManager.ReadValues<T>(page))
                {
                    yield return value;
                }
            }
        }

        public override IEnumerable<ClientResult> GetRawPages()
        {
            IEnumerator<ClientResult> pages = _requestManager.CreatePageEnumerator();
            while (pages.MoveNext())
            {
                yield return pages.Current;
            }
        }
    }

    private class AsyncPaginatedCollectionResult : AsyncCollectionResult
    {
        private readonly PageEnumerator _pageEnumerator;

        public AsyncPaginatedCollectionResult(PageEnumerator pageEnumerator)
        {
            _pageEnumerator = pageEnumerator;
        }

        public override ContinuationToken? GetContinuationToken(ClientResult page)
            => _pageEnumerator.GetNextPageToken(page);

        public async override IAsyncEnumerable<ClientResult> GetRawPagesAsync()
        {
            IAsyncEnumerator<ClientResult> enumerator = _pageEnumerator.CreateEnumerator();
            while (await enumerator.MoveNextAsync())
            {
                yield return enumerator.Current;
            }
        }
    }

    private class PaginatedCollectionResult : CollectionResult
    {
        private readonly PageRequestManager _requestManager;

        public PaginatedCollectionResult(PageRequestManager requestManager)
        {
            _requestManager = requestManager;
        }

        public override ContinuationToken? GetContinuationToken(ClientResult page)
            => _requestManager.GetNextPageToken(page);

        public override IEnumerable<ClientResult> GetRawPages()
        {
            IEnumerator<ClientResult> pages = _requestManager.CreatePageEnumerator();
            while (pages.MoveNext())
            {
                yield return pages.Current;
            }
        }
    }
}
