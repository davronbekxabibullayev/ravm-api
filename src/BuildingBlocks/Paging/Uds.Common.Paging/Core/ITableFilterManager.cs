namespace Devhub.Common.Paging.Core;

using Devhub.Common.Paging;
using Devhub.Common.Paging.Models;

public interface ITableFilterManager<out TEntity>
{
    void OrderDataSet(ISortingRequest tableFilterPayload);
    void FilterDataSet(string key, FilterMeta value, OperatorType operatorType);
    void FiltersDataSet(string key, IEnumerable<FilterMeta> values);
    void ExecuteFilter();
    IQueryable<TEntity> GetResult();
}
