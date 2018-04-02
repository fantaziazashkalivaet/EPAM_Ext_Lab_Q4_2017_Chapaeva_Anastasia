using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.DAL.Models
{
    class CustOrderDetailItem
    {
        public string ProductName
        {
            get;
            set;
        }

        public double? UnitPrice
        {
            get;
            set;
        }

        public int? Quantity
        {
            get;
            set;
        }

        public int? Discount
        {
            get;
            set;
        }

        public double? ExtendedPrice
        {
            get;
            set;
        }
    }
}
