
using System.Collections.Generic;
using System.Text;

public class Paginated<T>
{
    public IEnumerable<T> Data { get; set; }
    public int TotalCount { get; set; }
    public int TotalPage { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int FirstPage { get; set; }
    public int LastPage { get; set; }
}

