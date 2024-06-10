﻿using System;
using System.ClientModel;
using System.ClientModel.Primitives;
using System.Threading.Tasks;

#nullable enable

namespace OpenAI;

internal class PageableResultHelpers
{
    public static AsyncPageableResult<T> Create<T>(Func<string, Task<ClientPage<T>>> getPageAsync) where T : notnull
        => new FuncAsyncPageable<T>(getPageAsync);

    public static PageableResult<T> Create<T>(Func<string, ClientPage<T>> getPage) where T : notnull
        => new FuncPageable<T>(getPage);

    private class FuncAsyncPageable<T> : AsyncPageableResult<T> where T : notnull
    {
        private readonly Func<string, Task<ClientPage<T>>> _getPageAsync;
        
        public FuncAsyncPageable(Func<string, Task<ClientPage<T>>> getPageAsync)
        {
            _getPageAsync = getPageAsync;
        }

        protected override async Task<ClientPage<T>> GetPageCoreAsync(string pageToken = "")
        {
            return await _getPageAsync(pageToken).ConfigureAwait(false);
        }
    }

    private class FuncPageable<T> : PageableResult<T> where T : notnull
    {
        private readonly Func<string, ClientPage<T>> _getPage;
        
        public FuncPageable(Func<string, ClientPage<T>> getPage)
        {
            _getPage = getPage;
        }

        protected override ClientPage<T> GetPageCore(string pageToken)
        {
            return _getPage(pageToken);
        }
    }
}
