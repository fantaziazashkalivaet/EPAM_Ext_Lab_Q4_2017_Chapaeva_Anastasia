using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.DAL.Models
{
    class Order
    {
        private DateTime? orderDate;
        private DateTime? shippedDate;

        public Order()
        {
            this.CustomerID = null;
            this.EmployeeID = null;
            this.Freight = null;
            this.OrderDate = null;
            this.RequiredDate = null;
            this.ShipAddress = null;
            this.ShipCity = null;
            this.ShipCountry = null;
            this.ShipName = null;
            this.ShippedDate = null;
            this.ShipPostalCode = null;
            this.ShipRegion = null;
            this.ShipVia = null;
        }

        public int? OrderID
        {
            get;
            set;
        }

        public string CustomerID
        {
            get;
            set;
        }

        public int? EmployeeID
        {
            get;
            set;
        }

        public DateTime? OrderDate
        {
            get
            {
                return this.orderDate;
            }

            set
            {
                this.orderDate = value;

                if (orderDate == null)
                {
                    this.Status = OrderStatus.New;
                }
                else
                {
                    this.Status = OrderStatus.InProcess;
                }
            }
        }

        public DateTime? RequiredDate
        {
            get;
            set;
        }

        public DateTime? ShippedDate
        {
            get
            {
                return shippedDate;
            }

            set
            {
                shippedDate = value;

                if(shippedDate != null)
                {
                    Status = OrderStatus.Completed;
                }
                else
                {
                    Status = OrderStatus.InProcess;
                }
            }
        }

        public int? ShipVia
        {
            get;
            set;
        }

        public double? Freight
        {
            get;
            set;
        }

        public string ShipName
        {
            get;
            set;
        }

        public string ShipAddress
        {
            get;
            set;
        }

        public string ShipCity
        {
            get;
            set;
        }

        public string ShipRegion
        {
            get;
            set;
        }

        public string ShipPostalCode
        {
            get;
            set;
        }

        public string ShipCountry
        {
            get;
            set;
        }

        public OrderStatus Status
        {
            get;
            set;
        }
    }
}
