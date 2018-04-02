using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.DAL.Models
{
    class FullOrderInformation
    {
        public Order Order
        {
            get;
            set;
        }

        public Product Product
        {
            get;
            set;
        }

        public OrderDetails OrderDetails
        {
            get;
            set;
        }
    }
}
