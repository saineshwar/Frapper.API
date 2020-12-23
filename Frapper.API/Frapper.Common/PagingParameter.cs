using System;
using System.Collections.Generic;
using System.Text;

namespace Frapper.Common
{
    public class PagingParameter
    {
        const int MaxPageSize = 20;

        public int PageNumber { get; set; } = 1;

        private int DefaultpageSize { get; set; } = 10;

        public int PageSize
        {

            get { return DefaultpageSize; }
            set
            {
                DefaultpageSize = (value > MaxPageSize) ? MaxPageSize : value;
            }
        }
    }

    public class PaginationMetadata
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string PreviousPage { get; set; }
        public string NextPage { get; set; }
    }
}
