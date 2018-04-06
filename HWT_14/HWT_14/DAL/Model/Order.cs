namespace HWT_14.DAL.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Order
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

        [Required]
        [StringLength(5)]
        public string CustomerID
        {
            get;
            set;
        }

        [Required]
        public int? EmployeeID
        {
            get;
            set;
        }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? OrderDate
        {
            get
            {
                return this.orderDate;
            }

            set
            {
                this.orderDate = value;

                if (this.orderDate == null)
                {
                    this.Status = OrderStatus.New;
                }
                else
                {
                    this.Status = OrderStatus.InProcess;
                }
            }
        }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? RequiredDate
        {
            get;
            set;
        }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ShippedDate
        {
            get
            {
                return this.shippedDate;
            }

            set
            {
                this.shippedDate = value;

                if (this.shippedDate != null)
                {
                    this.Status = OrderStatus.Completed;
                }
                else
                {
                    this.Status = OrderStatus.InProcess;
                }
            }
        }

        [Required]
        public int? ShipVia
        {
            get;
            set;
        }

        [Required]
        public decimal? Freight
        {
            get;
            set;
        }

        [Required]
        [StringLength(40)]
        public string ShipName
        {
            get;
            set;
        }

        [Required]
        [StringLength(60)]
        public string ShipAddress
        {
            get;
            set;
        }

        [Required]
        [StringLength(15)]
        public string ShipCity
        {
            get;
            set;
        }

        [Required]
        [StringLength(15)]
        public string ShipRegion
        {
            get;
            set;
        }

        [Required]
        [StringLength(10)]
        public string ShipPostalCode
        {
            get;
            set;
        }

        [Required]
        [StringLength(15)]
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