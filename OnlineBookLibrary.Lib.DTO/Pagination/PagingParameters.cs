using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookLibrary.Lib.DTO.Pagination
{
    public class PagingParameters
    {
        const int MaxPageSize = 15;
        public int PageNumber { get; set; } = 1;
        
        public int PageSize = 5;
        public string SearchQuery { get; set; }
    }
}
