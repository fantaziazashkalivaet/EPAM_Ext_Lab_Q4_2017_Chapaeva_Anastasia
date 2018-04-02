using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.DAL.Models
{
    class Product
    {
        public int? ProductID
        {
            get;
            set;
        }

        public string ProductName
        {
            get;
            set;
        }

        public int? SupplierID
        {
            get;
            set;
        }

        public int? CategoryID
        {
            get;
            set;
        }

        public string QuantityPerUnit
        {
            get;
            set;
        }

        public double? UnitPrice
        {
            get;
            set;
        }

        public int? UnitsInStock
        {
            get;
            set;
        }

        public int? UnitsOnOrder
        {
            get;
            set;
        }

        public int? ReorderLevel
        {
            get;
            set;
        }

        public int? Discontinued
        {
            get;
            set;
        }
    }
}
