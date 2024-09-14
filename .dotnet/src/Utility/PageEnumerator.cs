using System;
using System.ClientModel;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable enable

namespace OpenAI;


/// <summary>
/// Abstract type defining methods that service clients must provide in order
/// to implement an enumerator over raw HTTP responses that represent the pages
/// that deliver subsets of items in a paginated collection.
/// </summary>
internal abstract class PageEnumerator : IAsyncEnumerator<ClientResult>,
    IEnumerator<ClientResult>
{
    private ClientResult? _current;
    private bool _hasNext = true;

    public ClientResult Current => _current!;

    /// <summary>
    /// Creates a new enumerator instance that can send the same sequence of
    /// requests as the current instance.  This is needed so that the 
    /// CollectionResult's GetRawPages method can return a collection with an
    /// enumerator that is independent from the one returned from its 
    /// GetEnumerator method.  i.e. if a user of CollectionResult<T> enumerates
    /// the values (the T's) and also calls GetRawPages, the two collections
    /// can be advanced independently without interfering with each other.
    /// </summary>
    public abstract PageEnumerator CreateEnumerator();

    public abstract Task<ClientResult> GetFirstAsync();

    public abstract ClientResult GetFirst();

    public abstract Task<ClientResult> GetNextAsync(ClientResult result);

    public abstract ClientResult GetNext(ClientResult result);

    public abstract bool HasNext(ClientResult result);

    /// <summary>
    /// Gets the continuation token that a client method can use to obtain the
    /// page after <paramref name="result"/>, or null if there is no next page.
    /// </summary>
    public abstract ContinuationToken? GetNextPageToken(ClientResult result);

    object IEnumerator.Current => ((IEnumerator<ClientResult>)this).Current;

    public bool MoveNext()
    {
        if (!_hasNext)
        {
            return false;
        }

        if (_current == null)
        {
            _current = GetFirst();
        }
        else
        {
            _current = GetNext(_current);
        }

        _hasNext = HasNext(_current);
        return true;
    }

    void IEnumerator.Reset() => _current = null;

    void IDisposable.Dispose() { }

    public async ValueTask<bool> MoveNextAsync()
    {
        if (!_hasNext)
        {
            return false;
        }

        if (_current == null)
        {
            _current = await GetFirstAsync().ConfigureAwait(false);
        }
        else
        {
            _current = await GetNextAsync(_current).ConfigureAwait(false);
        }

        _hasNext = HasNext(_current);
        return true;
    }

    ValueTask IAsyncDisposable.DisposeAsync() => default;
}
