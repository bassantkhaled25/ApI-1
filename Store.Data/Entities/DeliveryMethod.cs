using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Entities
{
    public class DeliveryMethod : BaseEntity<int>                               // set at Dbcontext + migration + add in StorContextSeed

    {                                                                  //delivery.json in SeedData => string in table
                         
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string DeliveryTime { get; set; }
        public decimal Price { get; set; }

    }
}
