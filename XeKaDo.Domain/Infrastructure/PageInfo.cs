using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XeKaDo.Domain.Infrastructure
{
    public struct PageInfo
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public PageInfo(int pageNumber = 1, int pageSize = 10)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
