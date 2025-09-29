using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.Common;

public class PaginatedRequest
{
    const int maxPageSize = 50;
    private int _pageSize = 10;
    public int PageSize
    {
        get
        {
            return _pageSize;
        }
        set
        {
            _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
    }
    public int PageNumber { get; set; } = 1;
    public string? SearchTerm { get; set; } = "";
    public string? OrderBy { get; set; } = "id";
}
