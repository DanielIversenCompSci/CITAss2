namespace WebApi.Models;

public class PagedResultModel<T>
{
    public IEnumerable<T> Items { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
    public string? NextPage { get; set; }
    public string? PrevPage { get; set; }
}