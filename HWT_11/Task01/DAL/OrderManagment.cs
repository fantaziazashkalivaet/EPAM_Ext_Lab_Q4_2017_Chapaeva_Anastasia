using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Task01.DAL.Models;

namespace Task01.DAL
{
    class OrderManagment
    {
        private DbProviderFactory factory;
        private string connectionString;

        public OrderManagment(string connectionString, string providerName)
        {
            this.connectionString = connectionString;
            factory = DbProviderFactories.GetFactory(providerName);
        }

        public List<Order> GetOrders()
        {
            var orders = new List<Order>();

            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
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

        public List<FullOrderInformation> GetOrderInformation(int orderID)
        {
            var info = new List<FullOrderInformation>();

            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                var command = (SqlCommand)connection.CreateCommand();
                command.CommandText =   "SELECT * FROM Northwind.dbo.Orders as O " +
                                        "JOIN Northwind.dbo.[Order Details] as OD " +
                                        "ON O.OrderID = OD.OrderID " +
                                        "JOIN Northwind.dbo.Products as P " +
                                        "ON OD.ProductID = P.ProductID " +
                                        "WHERE O.OrderID = @orderID; ";
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@orderID", orderID);

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tmpOrder = new Order();
                        var tmpProduct = new Product();
                        var tmpOrderDetails = new OrderDetails();

                        tmpOrder.OrderID = reader["OrderID"] as int?;
                        tmpOrder.CustomerID = reader["CustomerID"] as string;
                        tmpOrder.EmployeeID = reader["EmployeeID"] as int?;
                        tmpOrder.OrderDate = reader["OrderDate"] as DateTime?;
                        tmpOrder.ShippedDate = reader["ShippedDate"] as DateTime?;
                        tmpOrder.ShipAddress = reader["ShipAddress"] as string;
                        tmpProduct.ProductID = reader["ProductID"] as int?;
                        tmpProduct.ProductName = reader["ProductName"] as string;
                        tmpProduct.UnitPrice = reader["UnitPrice"] as double?;
                        tmpOrderDetails.Quantity = reader["Quantity"] as int?;
                        tmpOrderDetails.UnitPrice = reader["UnitPrice"] as double?;
                        tmpOrderDetails.Discount = reader["Discount"] as double?;

                        var tmpInformation = new FullOrderInformation();
                        tmpInformation.Order = tmpOrder;
                        tmpInformation.Product = tmpProduct;
                        tmpInformation.OrderDetails = tmpOrderDetails;

                        info.Add(tmpInformation);
                    }
                }
            }

            return info;
        }

        public void CreateNewOrder(Order newOrder)
        {
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                var command = (SqlCommand)connection.CreateCommand();
                command.CommandText = "INSERT INTO Northwind.dbo.Orders( " +
                                        "[CustomerID], [EmployeeID], [OrderDate], " +
                                        "[RequiredDate], [ShippedDate], [Freight], " +
                                        "[ShipAddress], [ShipCity], [ShipCountry], " +
                                        "[ShipName], [ShipPostalCode], [ShipRegion], [ShipVia] ) " +
                                        "VALUES (@customerID, @employeeID, @orderDate, " +
                                        "@requiredDate, @shippedDate, @freight, @shipAddress, " +
                                        "@shipCity, @shipCountry, @shipName, @shipPostalCode, " +
                                        "@shipRegion, @shipVia)";
                
                command.Parameters.AddWithValue("@customerID", newOrder.CustomerID);
                command.Parameters.AddWithValue("@employeeID", newOrder.EmployeeID);
                command.Parameters.AddWithValue("@orderDate", newOrder.OrderDate);
                command.Parameters.AddWithValue("@requiredDate", newOrder.RequiredDate);
                command.Parameters.AddWithValue("@shippedDate", newOrder.ShippedDate);
                command.Parameters.AddWithValue("@freight", newOrder.Freight);
                command.Parameters.AddWithValue("@shipAddress", newOrder.ShipAddress);
                command.Parameters.AddWithValue("@shipCity", newOrder.ShipCity);
                command.Parameters.AddWithValue("@shipCountry", newOrder.ShipCountry);
                command.Parameters.AddWithValue("@shipName", newOrder.ShipName);
                command.Parameters.AddWithValue("@shipPostalCode", newOrder.ShipPostalCode);
                command.Parameters.AddWithValue("@shipRegion", newOrder.ShipRegion);
                command.Parameters.AddWithValue("@shipVia", newOrder.ShipVia);
                command.CommandType = CommandType.Text;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void SetOrderDate(DateTime orderDate, Order order)
        {
            order.OrderDate = orderDate;

            using(var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                var command = (SqlCommand)connection.CreateCommand();
                command.CommandText =   "UPDATE Northwind.dbo.Orders " +
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

            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
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

        public List<CustOrderHistoryItem> CustOrderHist(string customerID)
        {
            var CustOrderHistList = new List<CustOrderHistoryItem>();

            using(var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                var command = (SqlCommand)connection.CreateCommand();
                command.CommandText = "Northwind.dbo.CustOrderHist";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CustomerID", customerID);

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CustOrderHistList.Add(new CustOrderHistoryItem());
                        CustOrderHistList.Last().CustomerID = customerID;
                        CustOrderHistList.Last().ProductName = reader["ProductName"] as string;
                        CustOrderHistList.Last().TotalSum = reader["Total"] as int?;
                    }
                }
            }

            return CustOrderHistList;
        }

        public List<CustOrderDetailItem> CustOrderDetail(int orderID)
        {
            var CustOrderDetailList = new List<CustOrderDetailItem>();

            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                var command = (SqlCommand)connection.CreateCommand();
                command.CommandText = "Northwind.dbo.CustOrdersDetail";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@OrderID", orderID);

                connection.Open();

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //CustOrderDetailList.Add(new CustOrderDetailItem());

                        //CustOrderDetailList.Last().ProductName = reader["ProductName"] as string;
                        //CustOrderDetailList.Last().UnitPrice = reader["UnitPrice"] as double?;
                        //CustOrderDetailList.Last().Quantity = reader["Quantity"] as int?;
                        //CustOrderDetailList.Last().Discount = reader["Discount"] as double?;
                        //CustOrderDetailList.Last().ExtendedPrice = reader["ExtendedPrice"] as double?;

                        var CustOrderDetailTmp = new CustOrderDetailItem();
                        CustOrderDetailTmp.ProductName = reader["ProductName"] as string;
                        CustOrderDetailTmp.UnitPrice = reader.GetValue(3) as double?;
                        CustOrderDetailTmp.Quantity = reader["Quantity"] as int?;
                        CustOrderDetailTmp.Discount = reader["Discount"] as int?;
                        CustOrderDetailTmp.ExtendedPrice = reader["ExtendedPrice"] as double?;
                        
                        CustOrderDetailList.Add(CustOrderDetailTmp);
                    }
                }
            }

            return CustOrderDetailList;
        }
    }
}
