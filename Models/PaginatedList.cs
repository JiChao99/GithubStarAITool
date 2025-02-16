namespace GithubStarAITool.Models;

public class PaginatedList<T>
{
    public List<T> Items { get; }
    public int PageIndex { get; }
    public int PageSize { get; }
    public bool HasNextPage { get; }

    public PaginatedList(List<T> items, int pageIndex, int pageSize, bool hasNextPage)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        Items = items;
        HasNextPage = hasNextPage;
    }

    public bool HasPreviousPage => PageIndex > 1;
}
