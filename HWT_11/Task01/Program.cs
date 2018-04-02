using System;
using System.Configuration;
using System.Collections.Specialized;
using System.Data.Common;
using System.Data;
using Task01.DAL;
using Task01.DAL.Models;

namespace Task01
{
    class Program
    {
        static void Main(string[] args)
        {

            //string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=usersdb;Integrated Security=True";
            // получаем строку подключения
            //string connectionString = ConfigurationManager.ConnectionStrings["NorthwindConnectionString"].ConnectionString;
            //Console.WriteLine(connectionString);

            //Console.Read();

           
            //IDbConnection connection = providerFactory.CreateConnection();
            //IDbCommand command = providerFactory.CreateCommand();
            //IDbDataAdapter dataAdapter = providerFactory.CreateDataAdapter();

            var connectionStringItem = ConfigurationManager.ConnectionStrings["NorthwindConnection"];
            var connectionString = connectionStringItem.ConnectionString;
            var providerName = connectionStringItem.ProviderName;
            //var providerFactory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            var factory = DbProviderFactories.GetFactory(providerName);

            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "select count(*) from Customers";
                command.CommandType = CommandType.Text;
                var customersCount = command.ExecuteScalar();
                Console.WriteLine(customersCount);
            }
            Console.WriteLine();

            var newOrder = new Order();
            newOrder.CustomerID = "TORTU";
            newOrder.EmployeeID = 1;
            newOrder.OrderDate = DateTime.Today;
            newOrder.ShipCity = "Izhevsk";
            newOrder.RequiredDate = DateTime.Today; 
            newOrder.ShippedDate = DateTime.Today;
            newOrder.Freight = 123;
            newOrder.ShipAddress = "asd";
            newOrder.ShipCountry = "rus";
            newOrder.ShipName = "asd";
            newOrder.ShipPostalCode = "asdf";
            newOrder.ShipRegion = "qwe";
            newOrder.ShipVia = 1;


            var orderm = new OrderManagment(connectionString, providerName);
            orderm.CreateNewOrder(newOrder);
            var orders = orderm.GetOrders();
            //orderm.SetOrderDate(DateTime.Now, orders[orders.Count - 1]);
            
            //Console.WriteLine(orders[orders.Count - 1].OrderID + " " + orders[orders.Count - 1].OrderDate);
            //orderm.SetShippedDate(DateTime.Now, orders[orders.Count - 1]);

            //var orderss = orderm.GetOrders();
            Console.WriteLine(orders[orders.Count - 1].OrderID + " " + orders[orders.Count - 1].ShippedDate);
            //var orHist = orderm.CustOrderHist("TORTU");
            //Console.WriteLine("{0} {1} {2}", orHist[0].CustomerID, orHist[0].ProductName, orHist[0].TotalSum);

            var orDet = orderm.CustOrderDetail(10250);
            Console.WriteLine("{0} {1}", orDet[0].ProductName, orDet[0].UnitPrice);
            var info = orderm.GetOrderInformation(10248);
            Console.WriteLine(orders[0].OrderID + " " + orders[1].OrderID);
        }
    }
}
