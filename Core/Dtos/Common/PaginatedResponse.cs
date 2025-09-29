using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.Common;

public class PaginatedResponse<T>
{
    public int TotalItemsCount { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public int TotalPagesCount => (int)Math.Ceiling(TotalItemsCount / (double)PageSize);
    public IEnumerable<T> Items { get; set; } = [];
}
