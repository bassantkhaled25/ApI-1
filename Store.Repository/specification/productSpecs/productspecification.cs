using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.specification.productSpecs
{
    public class productspecification

    {
        public int? BrandId { get; set; }                //words اللي هتظهر  => when execute //nullable => ممكن اطلبه وممكن لا 
                                                                          
        public int? TypeId { get; set; }

        public string? Sort { get; set; }

        public int PageIndex { get; set; } = 1;            //default   //pagination
        private const int MaxSize = 50;                    //constant


        private int _pageSize = 6;
        public int PageSize

        {
            get => _pageSize;
            set => _pageSize = (value > MaxSize) ? MaxSize : value;          //if .. else
        }

        private string? _search;                                              //search
        public string? Search         
            
        {
            get { return _search; }
            set { _search = value.ToLower().Trim(); }
        }



    }
}
