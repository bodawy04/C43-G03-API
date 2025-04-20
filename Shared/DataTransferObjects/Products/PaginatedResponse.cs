namespace Shared.DataTransferObjects.Products;

public record PaginatedResponse<TData>(int PageSize,int PageIndex,int TotalCount,IEnumerable<TData> data);
