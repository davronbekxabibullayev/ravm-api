namespace Devhub.Common.Paging;

using System.Linq;
using Devhub.Common.Paging.Core;
using Devhub.Common.Paging.Models;
using Microsoft.EntityFrameworkCore;

public static class FilterExtensions
{
    public static async Task<PagedList<TDestination>> ToPagedListAsync<T, TDestination>(this IQueryable<T> query, IFilteringRequest? request, Func<IQueryable<T>, IQueryable<TDestination>> projection)
    {
        query = query.AsFilterable(request, out var total);

        var result = await projection(query).ToListAsync();

        return new PagedList<TDestination>(result, total);
    }

    public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> query, FilteringRequest? request)
    {
        var result = await query.AsFilterable(request, out var total).ToListAsync();

        return new PagedList<T>(result, total);
    }

    public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> query, PagingRequest? request)
    {
        var result = await query.AsPageable(request, out var total).ToListAsync();

        return new PagedList<T>(result, total);
    }

    public static IQueryable<T> AsPageable<T>(this IQueryable<T> query, PagingRequest? request, out int totalRecord)
    {
        request ??= new PagingRequest();

        var tableFilterManager = new TableFilterManager<T>(query);

        ApplySort(request, tableFilterManager);

        query = tableFilterManager.GetResult();

        totalRecord = query.Count();

        query = ApplyPagination(query, request);

        return query;
    }

    public static IQueryable<T> AsFilterable<T>(this IQueryable<T> query, IFilteringRequest? request, out int totalRecord)
    {
        request ??= new FilteringRequest();

        var tableFilterManager = new TableFilterManager<T>(query);

        ApplyFilter(request, tableFilterManager);

        ApplySort(request, tableFilterManager);

        query = tableFilterManager.GetResult();

        totalRecord = query.Count();

        query = ApplyPagination(query, request);

        return query;
    }

    public static IQueryable<T> AsFilterable<T>(this IQueryable<T> query, FilteringRequest? request)
    {
        return query.AsFilterable(request, out var _);
    }

    private static void ApplyFilter<T>(IFilteringRequest request, TableFilterManager<T> tableFilterManager)
    {
        var filterRequests = request.All();

        if (filterRequests == null || filterRequests.Count == 0)
            return;

        foreach (var filterContext in filterRequests)
        {
            if (filterContext.Value == null || filterContext.Field == null)
                continue;

            var filter = new FilterMeta
            {
                MatchMode = filterContext.Logic,
                Operator = filterContext.Operator,
                Value = filterContext.Value,
            };
            var operatorType = OperatorTypes.ConvertToOperatorType(filterContext.Operator);

            tableFilterManager.FilterDataSet(filterContext.Field, filter, operatorType);
        }

        tableFilterManager.ExecuteFilter();
    }

    private static void ApplySort<T>(ISortingRequest request, ITableFilterManager<T> tableFilterManager)
    {
        tableFilterManager.OrderDataSet(request);
    }

    private static IQueryable<T> ApplyPagination<T>(IQueryable<T> query, IPagingRequest request)
    {
        if (request.PageSize <= 0)
        {
            return query;
        }

        return query.Skip(request.Page * request.PageSize).Take(request.PageSize);
    }
}
