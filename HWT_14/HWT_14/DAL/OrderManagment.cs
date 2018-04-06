namespace HWT_14.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Web.Mvc;

    using Model;   

    public class OrderManagment
    {
        private const float StandartDiscount = 0;
        private DbProviderFactory factory;
        private string connectionString;       

        public OrderManagment()
        {
            var connectionStringItem = ConfigurationManager.ConnectionStrings["NorthwindConnection"];
            this.connectionString = connectionStringItem.ConnectionString;
            var providerName = connectionStringItem.ProviderName;
            this.factory = DbProviderFactories.GetFactory(providerName);
        }

        public OrderManagment(string connectionString, string providerName)
        {
            this.connectionString = connectionString;
            this.factory = DbProviderFactories.GetFactory(providerName);
        }

        public List<Order> GetOrders()
        {
            var orders = new List<Order>();

            using (var connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Northwind.dbo.Orders";
                command.CommandType = CommandType.Text;
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        orders.Add(new Order());
                        orders.Last().OrderID = reader["OrderID"] as int?;
                        orders.Last().CustomerID = reader["CustomerID"] as string;
                        orders.Last().EmployeeID = reader["EmployeeID"] as int?;
                        orders.Last().OrderDate = reader["OrderDate"] as DateTime?;
                        orders.Last().ShippedDate = reader["ShippedDate"] as DateTime?;
                        orders.Last().ShipAddress = reader["ShipAddress"] as string;
                    }
                }
            }

            return orders;
        }

        public SummaryOfOrder GetSummaryOfOrder(int orderID)
        {
            var fullInfo = this.GetOrderInformation(orderID);
            var summaryOfOrder = new SummaryOfOrder();

            summaryOfOrder.OrderID = fullInfo.Order.OrderID;
            summaryOfOrder.CustomerID = fullInfo.Order.CustomerID;
            summaryOfOrder.Status = fullInfo.Order.Status;
            summaryOfOrder.Date = fullInfo.Order.OrderDate;

            foreach (var unit in fullInfo.OrderDetails)
            {               
                summaryOfOrder.Amount += (decimal)(unit.UnitPrice == null ? 0 : unit.UnitPrice) *
                    (decimal)(unit.Quantity == null ? 0 : unit.Quantity) *
                    (decimal)(1 - (unit.Discount == null ? 0 : unit.Discount));
            }

            return summaryOfOrder;
        }

        public List<SummaryOfOrder> GetSummaryOfOrders()
        {
            var orders = new List<SummaryOfOrder>();

            var ids = new List<int?>();
            using (var connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                var command = connection.CreateCommand();
                command.CommandText = "SELECT OrderID FROM Northwind.dbo.Orders";
                command.CommandType = CommandType.Text;
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ids.Add(reader["OrderID"] as int?);
                    }
                }
            }

            foreach (var id in ids)
            {
                orders.Add(this.GetSummaryOfOrder((int)id));
            }

            return orders;
        }

        public FullOrderInformation GetOrderInformation(int orderID)
        {
            var info = new FullOrderInformation();

            using (var connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                var command = (SqlCommand)connection.CreateCommand();
                command.CommandText = "SELECT * FROM Northwind.dbo.Orders as O " +
                                        "FULL JOIN Northwind.dbo.[Order Details] as OD " +
                                        "ON O.OrderID = OD.OrderID " +
                                        "FULL JOIN Northwind.dbo.Products as P " +
                                        "ON OD.ProductID = P.ProductID " +
                                        "WHERE O.OrderID = @orderID; ";
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@orderID", orderID);

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tmpProduct = new Product();
                        var tmpOrderDetails = new OrderDetails();

                        info.Order.OrderID = reader["OrderID"] as int?;
                        info.Order.CustomerID = reader["CustomerID"] as string;
                        info.Order.EmployeeID = reader["EmployeeID"] as int?;
                        info.Order.OrderDate = reader["OrderDate"] as DateTime?;
                        info.Order.RequiredDate = reader["RequiredDate"] as DateTime?;
                        info.Order.ShippedDate = reader["ShippedDate"] as DateTime?;
                        info.Order.ShipVia = reader["ShipVia"] as int?;
                        info.Order.Freight = reader["Freight"] as decimal?;
                        info.Order.ShipName = reader["ShipName"] as string;
                        info.Order.ShipAddress = reader["ShipAddress"] as string;
                        info.Order.ShipCity = reader["ShipCity"] as string;
                        info.Order.ShipRegion = reader["ShipRegion"] as string;
                        info.Order.ShipPostalCode = reader["ShipPostalCode"] as string;
                        info.Order.ShipCountry = reader["ShipCountry"] as string;

                        tmpOrderDetails.OrderID = (int)reader["OrderID"];
                        tmpOrderDetails.ProductID = reader["ProductID"] as int?;
                        tmpOrderDetails.ProductName = reader["ProductName"] as string;
                        tmpOrderDetails.UnitPrice = reader["UnitPrice"] as decimal?;
                        tmpOrderDetails.Quantity = reader["Quantity"] as short?;
                        tmpOrderDetails.Discount = reader["Discount"] as float?;

                        tmpProduct.ProductID = reader["ProductID"] as int?;
                        tmpProduct.ProductName = reader["ProductName"] as string;
                        tmpProduct.UnitPrice = reader["UnitPrice"] as double?;

                        info.Products.Add(tmpProduct);
                        info.OrderDetails.Add(tmpOrderDetails);
                    }
                }
            }

            return info;
        }

        public int? CreateNewOrder(Order newOrder)
        {
            int? id;

            using (var connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                var command = (SqlCommand)connection.CreateCommand();
                command.CommandText = "INSERT INTO Northwind.dbo.Orders( " +
                                        "[CustomerID], [EmployeeID], [OrderDate], " +
                                        "[RequiredDate], [ShippedDate], [Freight], " +
                                        "[ShipAddress], [ShipCity], [ShipCountry], " +
                                        "[ShipName], [ShipPostalCode], [ShipRegion], [ShipVia] ) " +
                                        "VALUES (@customerID, @employeeID, @orderDate, " +
                                        "@requiredDate, @shippedDate, @freight, @shipAddress, " +
                                        "@shipCity, @shipCountry, @shipName, @shipPostalCode, " +
                                        "@shipRegion, @shipVia); SET @INSERTED_ID=SCOPE_IDENTITY();";

                command.Parameters.AddWithValue("@customerID", newOrder.CustomerID);
                command.Parameters.AddWithValue("@employeeID", newOrder.EmployeeID);
                command.Parameters.AddWithValue("@orderDate", newOrder.OrderDate);
                command.Parameters.AddWithValue("@requiredDate", newOrder.RequiredDate);
                command.Parameters.AddWithValue(
                    "@shippedDate", 
                    newOrder.ShippedDate == null ? DBNull.Value : (object)newOrder.ShippedDate); // вот как это сделать так, чтобы нормально?
                command.Parameters.AddWithValue("@freight", newOrder.Freight);
                command.Parameters.AddWithValue("@shipAddress", newOrder.ShipAddress);
                command.Parameters.AddWithValue("@shipCity", newOrder.ShipCity);
                command.Parameters.AddWithValue("@shipCountry", newOrder.ShipCountry);
                command.Parameters.AddWithValue("@shipName", newOrder.ShipName);
                command.Parameters.AddWithValue("@shipPostalCode", newOrder.ShipPostalCode);
                command.Parameters.AddWithValue("@shipRegion", newOrder.ShipRegion);
                command.Parameters.AddWithValue("@shipVia", newOrder.ShipVia);
                command.CommandType = CommandType.Text;

                var pID = new SqlParameter();
                pID.ParameterName = "INSERTED_ID";
                pID.Size = 15;
                pID.Direction = ParameterDirection.Output;
                command.Parameters.Add(pID);

                connection.Open();

                command.ExecuteNonQuery();

                id = int.Parse(pID.Value.ToString());
            }

            return id;
        }

        public void AddOrderDetails(OrderDetails newDetails)
        {
            using (var connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                var command = (SqlCommand)connection.CreateCommand();
                command.CommandText = "INSERT INTO Northwind.dbo.[Order Details]( " +
                                        "[OrderID], [ProductID], [UnitPrice], " +
                                        "[Quantity], [Discount] ) " +
                                        "VALUES (@orderID, @productID, @unitPrice, " +
                                        "@quantity, @discount)";

                command.Parameters.AddWithValue("@orderID", newDetails.OrderID);
                command.Parameters.AddWithValue("@productID", newDetails.ProductID);
                command.Parameters.AddWithValue("@unitPrice", newDetails.UnitPrice);
                command.Parameters.AddWithValue("@quantity", newDetails.Quantity);
                command.Parameters.AddWithValue(
                    "@discount",
                    newDetails.Discount == null ? StandartDiscount : newDetails.Discount); // та же история
                command.CommandType = CommandType.Text;

                connection.Open();

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    return;
                }
            }
        }

        public List<SelectListItem> GetCustomersID()
        {
            var ids = new List<SelectListItem>();

            using (var connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                var command = (SqlCommand)connection.CreateCommand();
                command.CommandText = "SELECT [CustomerID] FROM Northwind.dbo.Customers;";
                command.CommandType = CommandType.Text;

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ids.Add(new SelectListItem { Text = reader["CustomerID"] as string, Value = reader["CustomerID"] as string });
                    }
                }
            }

            return ids;
        }

        public List<SelectListItem> GetProductsID()
        {
            var ids = new List<SelectListItem>();

            using (var connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                var command = (SqlCommand)connection.CreateCommand();
                command.CommandText = "SELECT [ProductID] FROM Northwind.dbo.Products;";
                command.CommandType = CommandType.Text;

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ids.Add(new SelectListItem { Text = reader["ProductID"].ToString(), Value = reader["ProductID"].ToString() });
                    }
                }
            }

            return ids;
        }

        public List<SelectListItem> GetEmployeesID()
        {
            var ids = new List<SelectListItem>();

            using (var connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                var command = (SqlCommand)connection.CreateCommand();
                command.CommandText = "SELECT [EmployeeID] FROM Northwind.dbo.Employees;";
                command.CommandType = CommandType.Text;

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ids.Add(new SelectListItem { Text = reader["EmployeeID"].ToString(), Value = reader["EmployeeID"].ToString() });
                    }
                }
            }
            //// тут должна быть сортировка, но её нет

            return ids;
        }

        public void SetOrderDate(DateTime orderDate, Order order)
        {
            order.OrderDate = orderDate;

            using (var connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                var command = (SqlCommand)connection.CreateCommand();
                command.CommandText = "UPDATE Northwind.dbo.Orders " +
                                        "SET OrderDate = @orderDate " +
                                        "WHERE OrderID = @orderID";
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@orderDate", orderDate);
                command.Parameters.AddWithValue("@orderID", order.OrderID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void SetShippedDate(DateTime shippedDate, Order order)
        {
            order.ShippedDate = shippedDate;

            using (var connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                var command = (SqlCommand)connection.CreateCommand();
                command.CommandText = "UPDATE Northwind.dbo.Orders " +
                                        "SET ShippedDate = @shippedDate " +
                                        "WHERE OrderID = @orderID";
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@shippedDate", shippedDate);
                command.Parameters.AddWithValue("@orderID", order.OrderID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
