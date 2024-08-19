namespace Infrastructure.Shared.Database.Models;

public class PaginationModel(int totalCount, int currentPage, int pageSize, int totalPages)
{
    public int CurrentPage { get; private set; } = currentPage;
    public int TotalPages { get; private set; } = totalPages;
    public int PageSize { get; private set; } = pageSize;
    public int TotalCount { get; private set; } = totalCount;
}