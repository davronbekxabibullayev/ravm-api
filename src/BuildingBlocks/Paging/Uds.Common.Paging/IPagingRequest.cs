namespace Devhub.Common.Paging;
public interface IPagingRequest : ISortingRequest
{
    int Page { get; set; }

    int PageSize { get; set; }
}
