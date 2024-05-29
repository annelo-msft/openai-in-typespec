﻿using System;
using System.ClientModel;
using System.ClientModel.Primitives;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable enable

namespace OpenAI;

internal class PageableResultHelpers
{
    public static PageableCollection<T> Create<T>(Func<int?, PageResult<T>> firstPageFunc, Func<string?, int?, PageResult<T>>? nextPageFunc, int? pageSize = default) where T : notnull
    {
        PageResult<T> first(string? _, int? pageSizeHint) => firstPageFunc(pageSizeHint);
        return new FuncPageable<T>(first, nextPageFunc, pageSize);
    }

    public static AsyncPageableCollection<T> Create<T>(Func<int?, Task<PageResult<T>>> firstPageFunc, Func<string?, int?, Task<PageResult<T>>>? nextPageFunc, int? pageSize = default) where T : notnull
    {
        Task<PageResult<T>> first(string? _, int? pageSizeHint) => firstPageFunc(pageSizeHint);
        return new FuncAsyncPageable<T>(first, nextPageFunc, pageSize);
    }

    private class FuncAsyncPageable<T> : AsyncPageableCollection<T> where T : notnull
    {
        private readonly Func<string?, int?, Task<PageResult<T>>> _firstPageFunc;
        private readonly Func<string?, int?, Task<PageResult<T>>>? _nextPageFunc;
        private readonly int? _defaultPageSize;

        public FuncAsyncPageable(Func<string?, int?, Task<PageResult<T>>> firstPageFunc, Func<string?, int?, Task<PageResult<T>>>? nextPageFunc, int? defaultPageSize = default)
        {
            _firstPageFunc = firstPageFunc;
            _nextPageFunc = nextPageFunc;
            _defaultPageSize = defaultPageSize;
        }

        public override async IAsyncEnumerable<PageResult<T>> AsPagesAsync(string? continuationToken = default, int? pageSizeHint = default)
        {
            Func<string?, int?, Task<PageResult<T>>>? pageFunc = string.IsNullOrEmpty(continuationToken) ? _firstPageFunc : _nextPageFunc;

            if (pageFunc == null)
            {
                yield break;
            }

            int? pageSize = pageSizeHint ?? _defaultPageSize;
            do
            {
                PageResult<T> page = await pageFunc(continuationToken, pageSize).ConfigureAwait(false);
                SetRawResponse(page.GetRawResponse());
                yield return page;
                continuationToken = page.ContinuationToken;
                pageFunc = _nextPageFunc;
            }
            while (!string.IsNullOrEmpty(continuationToken) && pageFunc != null);
        }
    }

    private class FuncPageable<T> : PageableCollection<T> where T : notnull
    {
        private readonly Func<string?, int?, PageResult<T>> _firstPageFunc;
        private readonly Func<string?, int?, PageResult<T>>? _nextPageFunc;
        private readonly int? _defaultPageSize;

        public FuncPageable(Func<string?, int?, PageResult<T>> firstPageFunc, Func<string?, int?, PageResult<T>>? nextPageFunc, int? defaultPageSize = default)
        {
            _firstPageFunc = firstPageFunc;
            _nextPageFunc = nextPageFunc;
            _defaultPageSize = defaultPageSize;
        }

        public override IEnumerable<PageResult<T>> AsPages(string? continuationToken = default, int? pageSizeHint = default)
        {
            Func<string?, int?, PageResult<T>>? pageFunc = string.IsNullOrEmpty(continuationToken) ? _firstPageFunc : _nextPageFunc;

            if (pageFunc == null)
            {
                yield break;
            }

            int? pageSize = pageSizeHint ?? _defaultPageSize;
            do
            {
                PageResult<T> page = pageFunc(continuationToken, pageSize);
                SetRawResponse(page.GetRawResponse());
                yield return page;
                continuationToken = page.ContinuationToken;
                pageFunc = _nextPageFunc;
            }
            while (!string.IsNullOrEmpty(continuationToken) && pageFunc != null);
        }
    }
}
