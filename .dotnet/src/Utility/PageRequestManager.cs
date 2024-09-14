using System.ClientModel;
using System.Collections.Generic;

#nullable enable

namespace OpenAI;

internal abstract class PageRequestManager
{
    // This will throw of convenience layer isn't implemented or the wrong
    // type of T is requested.
    public abstract IEnumerable<T> ReadValues<T>(ClientResult page);

    public abstract PageEnumerator CreatePageEnumerator();

    public abstract ContinuationToken? GetNextPageToken(ClientResult result);
}
