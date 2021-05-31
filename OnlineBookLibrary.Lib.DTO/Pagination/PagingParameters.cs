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
        private int _PageSize { get; set; } = 10;

        public int PageSize
        {
            get
            {
                return _PageSize;
            }
            set
            {
                _PageSize = (value > MaxPageSize) ? MaxPageSize : value;
            }
        }
        public string SearchQuery { get; set; }
    }
}
