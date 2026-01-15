namespace Devhub.Common.Paging.Core;

using System.Collections.Generic;
using System.Linq;
using Devhub.Common.Paging;
using Devhub.Common.Paging.Models;
using Devhub.Common.Paging.Utils;

/// <summary>
/// Class of PrimeNG table filter manager for Entity
/// </summary>
public class TableFilterManager<TEntity>(IQueryable<TEntity> dataSet) : ITableFilterManager<TEntity>
{
    private const int StartIndex = 1;
    private readonly ILinqOperator<TEntity> _linqOperator = new LinqOperator<TEntity>(dataSet);

    /// <summary> 
    /// Set single condition for ordering data set to LINQ Operation context
    /// </summary>
    public void OrderDataSet(ISortingRequest sort)
    {
        if (string.IsNullOrWhiteSpace(sort?.SortBy))
            return;

        var count = 0;
        foreach (var sortItem in sort.SortBy.Split(","))
        {
            if (string.IsNullOrWhiteSpace(sortItem))
                continue;

            var sortField = sortItem;
            var sortDir = SortingType.OrderByAsc;

            if (sortItem[0] == '-')
            {
                sortDir = SortingType.OrderByDesc;
                sortField = sortItem[StartIndex..];
            }
            else if (sortItem[0] == '+')
            {
                sortField = sortItem[StartIndex..];
            }

            if (string.IsNullOrEmpty(sortField))
                return;

            switch (sortDir)
            {
                case SortingType.OrderByDesc:
                    if (count == 0)
                        _linqOperator.OrderByDescending(sortField.FirstCharToUpper());
                    else
                        _linqOperator.ThenByDescending(sortField.FirstCharToUpper());
                    break;

                case SortingType.OrderByAsc:
                default:
                    if (count == 0)
                        _linqOperator.OrderBy(sortField.FirstCharToUpper());
                    else
                        _linqOperator.ThenBy(sortField.FirstCharToUpper());
                    break;
            }
            count++;
        }
    }

    /// <summary>
    /// The base method for set filter condition data to LINQ Operation context
    /// </summary>
    private void BaseFilterDataSet(string key, FilterMeta value, OperatorType operatorAction)
    {
        if (value.Value == null)
            return;

        var propertyName = string.Join('.', key.Split('.').Select(k => k.FirstCharToUpper()));

        switch (value.MatchMode)
        {
            case MatchModeTypes.StartsWith:
                _linqOperator.AddFilterProperty(propertyName, value.Value, LinqOperatorConstants.ConstantStartsWith, operatorAction);
                break;

            case MatchModeTypes.Contains:
                _linqOperator.AddFilterProperty(propertyName, value.Value, LinqOperatorConstants.ConstantContains, operatorAction);
                break;

            case MatchModeTypes.In:
                _linqOperator.AddFilterListProperty(propertyName, value.Value, operatorAction);
                break;

            case MatchModeTypes.EndsWith:
                _linqOperator.AddFilterProperty(propertyName, value.Value, LinqOperatorConstants.ConstantEndsWith, OperatorType.None);
                break;

            case MatchModeTypes.Equals:
                _linqOperator.AddFilterProperty(propertyName, value.Value, LinqOperatorConstants.ConstantEquals, operatorAction);
                break;

            case MatchModeTypes.NotContains:
                _linqOperator.AddFilterProperty(propertyName, value.Value, LinqOperatorConstants.ConstantContains, OperatorType.None, true);
                break;

            case MatchModeTypes.NotEquals:
                _linqOperator.AddFilterProperty(propertyName, value.Value, LinqOperatorConstants.ConstantEquals, operatorAction, true);
                break;
            case MatchModeTypes.DateIs:
                _linqOperator.AddFilterProperty(propertyName, value.Value, LinqOperatorConstants.ConstantDateIs, operatorAction);
                break;
            case MatchModeTypes.DateIsNot:
                _linqOperator.AddFilterProperty(propertyName, value.Value, LinqOperatorConstants.ConstantDateIs, operatorAction, true);
                break;
            case MatchModeTypes.DateBefore:
                _linqOperator.AddFilterProperty(propertyName, value.Value, LinqOperatorConstants.ConstantBefore, operatorAction);
                break;
            case MatchModeTypes.DateAfter:
                _linqOperator.AddFilterProperty(propertyName, value.Value, LinqOperatorConstants.ConstantAfter, operatorAction);
                break;
            case MatchModeTypes.LessThan:
                _linqOperator.AddFilterProperty(propertyName, value.Value, LinqOperatorConstants.ConstantLessThan, operatorAction);
                break;
            case MatchModeTypes.LessOrEqualsThan:
                _linqOperator.AddFilterProperty(propertyName, value.Value, LinqOperatorConstants.ConstantLessThanOrEqual, operatorAction);
                break;
            case MatchModeTypes.GreaterThan:
                _linqOperator.AddFilterProperty(propertyName, value.Value, LinqOperatorConstants.ConstantGreaterThan, operatorAction);
                break;
            case MatchModeTypes.GreaterOrEqualsThan:
                _linqOperator.AddFilterProperty(propertyName, value.Value, LinqOperatorConstants.ConstantGreaterThanOrEqual, operatorAction);
                break;

            default:
                _linqOperator.AddFilterProperty(propertyName, value.Value, LinqOperatorConstants.ConstantEquals, operatorAction);
                break;
        }
    }

    /// <summary>
    /// Set filter condition data to LINQ Operation context
    /// </summary>
    public void FilterDataSet(string key, FilterMeta value, OperatorType operatorType)
    {
        BaseFilterDataSet(key, value, operatorType);
    }

    /// <summary>
    /// Set multiple filter condition data to LINQ Operation context
    /// </summary>
    public void FiltersDataSet(string key, IEnumerable<FilterMeta> values)
    {
        foreach (var filterContext in values)
        {
            var operatorType = OperatorTypes.ConvertToOperatorType(filterContext.Operator);
            BaseFilterDataSet(key, filterContext, operatorType);
        }
    }

    /// <summary>
    /// Invoke filter data set from filter context setting
    /// </summary>
    public void ExecuteFilter()
    {
        _linqOperator.WhereExecute();
    }

    /// <summary>
    /// Get the filter result
    /// </summary>
    public IQueryable<TEntity> GetResult()
    {
        return _linqOperator.GetResult();
    }
}
