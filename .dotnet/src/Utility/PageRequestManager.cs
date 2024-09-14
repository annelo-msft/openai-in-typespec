using System.ClientModel;
using System.Collections.Generic;

#nullable enable

namespace OpenAI;

internal abstract class PageRequestManager
{
    //// This will throw of convenience layer isn't implemented.
    //public abstract PageEnumerator<T> CreatePageEnumerator<T>();

    // This will throw of convenience layer isn't implemented.
    public abstract IEnumerable<T> GetValuesFromPage<T>(ClientResult pageResult);

    public abstract PageEnumerator CreatePageEnumerator();

    public abstract ContinuationToken? GetNextPageToken(ClientResult result);
}
