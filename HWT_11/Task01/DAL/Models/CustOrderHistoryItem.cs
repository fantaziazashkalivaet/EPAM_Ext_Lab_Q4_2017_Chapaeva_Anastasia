using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.DAL.Models
{
    class CustOrderHistoryItem
    {
        public string CustomerID
        {
            get;
            set;
        }

        public double? TotalSum
        {
            get;
            set;
        }

        public string ProductName
        {
            get;
            set;
        }
    }
}
