using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Helper
{   
    
    public class Pagination<T>

    { 
        public Pagination(int pageIndex, int pageSize, int Totalcount, IReadOnlyList<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;                                //const
            TotalCount = Totalcount;
            Data = data;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public IReadOnlyList<T> Data { get; set; }
}   }

