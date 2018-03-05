USE Northwind



/*1.1 ������� � ������� Orders ������, ������� ���� ���������� ����� 6 ��� 1998 ���� (������� ShippedDate)
������������ � ������� ���������� � ShipVia >= 2.*/
SELECT OrderID, ShippedDate, ShipVia
FROM Orders
WHERE ShippedDate >= DATEFROMPARTS(1998, 05, 06) AND ShipVia >= 2;
/*������, ������� �� ����� ���� �������� �� ������������, 
��� ��� �������� ��������� NULL � ������ ���������� �������� UNKNOWN*/

/*
1.2 �������� ������, ������� ������� ������ �������������� ������ �� ������� Orders. 
� ����������� ������� ����������� ��� ������� ShippedDate ������ �������� NULL ������ �Not Shipped� �
- ������������ ��������� ������� CAS�. ������ ������ ����������� ������ ������� OrderID � ShippedDate.
*/
SELECT OrderID,
	CASE 
		WHEN ShippedDate IS NULL
			THEN 'Not Shipped'
		END ShippedDate
FROM Orders
WHERE ShippedDate IS NULL;

/*
1.3 ������� � ������� Orders ������, ������� ���� ���������� ����� 6 ��� 1998 ���� (ShippedDate)
 �� ������� ��� ���� ��� ������� ��� �� ����������. 
 � ������� ������ ������������� ������ ������� OrderID (������������� � Order Number) 
 � ShippedDate (������������� � Shipped Date). 
 � ����������� ������� ����������� ��� ������� ShippedDate ������ �������� NULL ������ �Not Shipped�,
  ��� ��������� �������� ����������� ���� � ������� �� ���������.
*/
SELECT OrderID [Order Number], 
	CASE 
		WHEN ShippedDate IS NULL
			THEN 'Not Shipped'
		ELSE CAST(ShippedDate AS char(20))
	END [Shipped Date]
FROM Orders
WHERE ShippedDate > DATEFROMPARTS(1998, 05, 06) OR ShippedDate IS NULL;

/*
2.1 ������� �� ������� Customers ���� ����������, ����������� � USA � Canada. 
������ ������� � ������ ������� ��������� IN. 
����������� ������� � ������ ������������ � ��������� ������ � ����������� �������. 
����������� ���������� ������� �� ����� ���������� � �� ����� ����������.
*/
SELECT ContactName, Country
FROM Customers
WHERE Country IN ('USA', 'Canada')
ORDER BY ContactName, Country;


/*
2.2 ������� �� ������� Customers ���� ����������, �� ����������� � USA � Canada. 
������ ������� � ������� ��������� IN. 
����������� ������� � ������ ������������ � ��������� ������ � ����������� �������. 
����������� ���������� ������� �� ����� ����������.
*/
SELECT ContactName, Country
FROM Customers
WHERE Country NOT IN ('USA', 'Canada')
ORDER BY ContactName;

/*
2.3 ������� �� ������� Customers ��� ������, � ������� ��������� ���������. 
������ ������ ���� ��������� ������ ���� ��� � ������ ������������ �� ��������. 
�� ������������ ����������� GROUP BY. ����������� ������ ���� ������� � ����������� �������.
*/
SELECT DISTINCT Country
FROM Customers
ORDER BY Country DESC;

/*
3.1 ������� ��� ������ (OrderID) �� ������� Order Details (������ �� ������ �����������), 
��� ����������� �������� � ����������� �� 3 �� 10 ������������ � ��� ������� Quantity � ������� Order Details. 
������������ �������� BETWEEN. 
������ ������ ����������� ������ ������� OrderID.
*/
SELECT DISTINCT OrderID
FROM [Order Details]
WHERE Quantity BETWEEN 3 AND 10;

/*
3.2 ������� ���� ���������� �� ������� Customers, 
� ������� �������� ������ ���������� �� ����� �� ��������� b � g. 
������������ �������� BETWEEN. 
���������, ��� � ���������� ������� �������� Germany. 
������ ������ ����������� ������ ������� CustomerID � Country � ������������ �� Country.
*/
SELECT CustomerID, Country
FROM Customers
/* WHERE Country LIKE '[b-g]%'*/
WHERE LEFT(Country, 1) BETWEEN 'b' AND 'g'
ORDER BY Country;

/*
3.3 ������� ���� ���������� �� ������� Customers, 
� ������� �������� ������ ���������� �� ����� �� ��������� b � g, 
�� ��������� �������� BETWEEN. 
� ������� ����� �Execution Plan� ���������� 
����� ������ ���������������� 3.2 ��� 3.3 � ��� ����� ���� ������ � ������
 ���������� ���������� Execution Plan-a ��� ���� ���� ��������, 
 ���������� ���������� Execution Plan ���� ������ � ������ � ���� ����������� 
 � �� �� ����������� ���� ����� �� ������ � �� ������ ��������� ���� ��������� ���������. 
 ������ ������ ����������� ������ ������� CustomerID � Country � ������������ �� Country.
*/

SELECT CustomerID, Country
FROM Customers
WHERE Country LIKE '[b-g]%'
ORDER BY Country;

/* 
���� �� �����������, ��� ���� � ��������� ������� ����������, 
�� ���� ������ �������� ���� ���������� ��������, �� �� �������, ��� 3.2 � 3.3 �����������
*/

/*
4.1 � ������� Products ����� ��� �������� (������� ProductName), ��� ����������� ��������� 'chocolade'. 
��������, ��� � ��������� 'chocolade' ����� ���� �������� ���� ����� 'c' � �������� - 
����� ��� ��������, ������� ������������� ����� �������. 
���������: ���������� ������� ������ ����������� 2 ������.
*/
SELECT ProductName
FROM Products
WHERE ProductName LIKE '%cho_olade%';

/*
5.1 ����� ����� ����� ���� ������� �� ������� Order Details 
� ������ ���������� ����������� ������� � ������ �� ���. 
��������� ��������� �� ����� � ��������� � ����� 1 ��� ���� ������ money. 
������ (������� Discount) ���������� ������� �� ��������� ��� ������� ������. 
��� ����������� �������������� ���� �� ��������� ������� ���� ������� ������ 
�� ��������� � ������� UnitPrice ����. 
����������� ������� ������ ���� ���� ������ � ����� �������� � ��������� ������� 'Totals'.
*/
/*
SELECT CONVERT(money, ROUND(SUM(Quantity*UnitPrice*(1 - (Discount / 100))), 2), 1) [5.1] -- � ���� ������ ����� ����������
FROM [Order Details];*/

--� ������, ���� Discount ��-���� �� � ���������:
SELECT CONVERT(money, ROUND(SUM(Quantity*UnitPrice*(1 - Discount)), 2), 1) [5.1]
FROM [Order Details]; 



/*
5.2 �� ������� Orders ����� ���������� �������, ������� ��� �� ���� ���������� 
(�.�. � ������� ShippedDate ��� �������� ���� ��������). 
������������ ��� ���� ������� ������ �������� COUNT. 
�� ������������ ����������� WHERE � GROUP.
*/
SELECT COUNT(*) - COUNT(ShippedDate) [5.2]
FROM Orders;

/*
5.3 �� ������� Orders ����� ���������� ��������� ����������� (CustomerID), ��������� ������. 
������������ ������� COUNT � �� ������������ ����������� WHERE � GROUP.
*/
SELECT COUNT(DISTINCT ShippedDate) [5.3]
FROM Orders;


/*
6.1 �� ������� Orders ����� ���������� ������� � ������������ �� �����. 
� ����������� ������� ���� ����������� ��� ������� c ���������� Year � Total. 
�������� ����������� ������, ������� ��������� ���������� ���� �������.
*/
SELECT YEAR(OrderDate) [Year], COUNT(*) [Total]
FROM Orders
GROUP BY YEAR(OrderDate);

/*���*/
SELECT COUNT(*) [Total]
From Orders;


/*
6.2 �� ������� Orders ����� ���������� �������, c�������� ������ ���������. 
����� ��� ���������� �������� � ��� ����� ������ � ������� Orders, 
��� � ������� EmployeeID ������ �������� ��� ������� ��������. 
� ����������� ������� ���� ����������� ������� � ������ �������� 
(������ ������������� ��� ���������� ������������� LastName & FirstName. 
��� ������ LastName & FirstName ������ ���� �������� ��������� �������� � ������� ��������� �������. 
����� �������� ������ ������ ������������ ����������� �� EmployeeID.) 
� ��������� ������� �Seller� � ������� c ����������� ������� ����������� � ��������� 'Amount'. 
���������� ������� ������ ���� ����������� �� �������� ���������� �������.
*/
SELECT --������, ��������� ���� select
	(SELECT CONCAT(FirstName, ' ', LastName)
	FROM Employees 
	WHERE Employees.EmployeeID = Orders.EmployeeID) [Seller],
	COUNT(*) [Amount]
FROM Orders
GROUP BY EmployeeID
ORDER BY [Amount] DESC;

--� ����� select:
SELECT CONCAT(E.FirstName, ' ', E.LastName), COUNT(*) [Amount]
FROM Employees as E JOIN Orders as O ON
E.EmployeeID = O.EmployeeID
GROUP BY O.EmployeeID, E.FirstName, E.LastName
ORDER BY [Amount] DESC;


/*
6.3 �� ������� Orders ����� ���������� �������, c�������� ������ ��������� � ��� ������� ����������. 
���������� ���������� ��� ������ ��� ������� ��������� � 1998 ����. 
� ����������� ������� ���� ����������� ������� � ������ �������� (�������� ������� �Seller�), 
������� � ������ ���������� (�������� ������� �Customer�) 
� ������� c ����������� ������� ����������� � ��������� 'Amount'. 
� ������� ���������� ������������ ����������� �������� ����� T-SQL ��� ������ � ���������� GROUP 
(���� �� �������� ������� �������� ������ �ALL� � ����������� �������). 
����������� ������ ���� ������� �� ID �������� � ����������. 
���������� ������� ������ ���� ����������� �� ��������, ���������� � �� �������� ���������� ������. 
� ����������� ������ ���� ������� ���������� �� ��������.
*/
SELECT 
	(SELECT CONCAT(FirstName, ' ', LastName)
	FROM Employees 
	WHERE Employees.EmployeeID = Orders.EmployeeID) [Seller],

	(SELECT CompanyName
	FROM Customers 
	WHERE Customers.CustomerID = Orders.CustomerID) [Customer],

	COUNT(*) [Amount]
FROM Orders
WHERE YEAR(OrderDate) = 1998
GROUP BY CUBE(EmployeeID, CustomerID)
ORDER BY EmployeeID, CustomerID, [Amount] DESC;

/*
6.4 ����� ����������� � ���������, ������� ����� � ����� ������. 
���� � ������ ����� ������ ���� ��� ��������� ��������� 
��� ������ ���� ��� ��������� �����������, 
�� ���������� � ����� ���������� � ��������� �� ������ �������� � �������������� �����. 
�� ������������ ����������� JOIN. 
� ����������� ������� ���������� ������� ��������� ��������� ��� ����������� �������:
 �Person�, �Type� (����� ���� �������� ������ �Customer� ��� �Seller� � ��������� �� ���� ������), �City�. 
 ������������� ���������� ������� �� ������� �City� � �� �Person�.
*/
SELECT ContactName [Person], 'Customer' [Type], City
FROM Customers
WHERE City IN (
	SELECT City 
	FROM Employees)

UNION

SELECT CONCAT(FirstName, ' ', LastName) [Person], 'Seller' [Type], City
FROM Employees
WHERE City IN (
	SELECT City 
	FROM Customers)
ORDER BY City, Person;

/*
6.5 ����� ���� �����������, ������� ����� � ����� ������. 
� ������� ������������ ���������� ������� Customers c ����� - ��������������. 
��������� ������� CustomerID � City. 
������ �� ������ ����������� ����������� ������. 
��� �������� �������� ������, ������� ����������� ������, 
������� ����������� ����� ������ ���� � ������� Customers. 
��� �������� ��������� ������������ �������.
*/
SELECT DISTINCT Cust1.CustomerID, Cust2.City
FROM Customers as Cust1 JOIN Customers as Cust2
ON Cust1.CustomerID <> Cust2.CustomerID AND Cust1.City = Cust2.City;

--���
SELECT City
FROM Customers
GROUP BY City
HAVING COUNT(*) > 1;

/*
6.6 �� ������� Employees ����� ��� ������� �������� ��� ������������, 
�.�. ���� �� ������ �������. 
��������� ������� � ������� 'User Name' (LastName) � 'Boss'. 
� �������� ������ ���� ��������� ����� �� ������� LastName. 
��������� �� ��� �������� � ���� �������?
*/
SELECT Emp1.LastName [User Name], Emp2.LastName [Boss]
FROM Employees as Emp1 JOIN Employees as Emp2
ON Emp1.ReportsTo = Emp2.EmployeeID;
--�������� �� ��� ��������. ������, ��� Fuller ���� ���� ������

/*
7.1 ���������� ���������, ������� ����������� ������ 'Western' (������� Region). 
���������� ������� ������ ����������� ��� ����: 
'LastName' �������� � �������� ������������� ���������� ('TerritoryDescription' �� ������� Territories). 
������ ������ ������������ JOIN � ����������� FROM. 
��� ����������� ������ ����� ��������� Employees � Territories ���� ������������ ����������� ��������� ��� ���� Northwind.
*/
SELECT LastName, TerritoryDescription
FROM Employees as Emp INNER JOIN EmployeeTerritories as EmpTerr
ON Emp.EmployeeID = EmpTerr.EmployeeID INNER JOIN Territories as Terr
ON EmpTerr.TerritoryID = Terr.TerritoryID INNER JOIN Region as Reg
ON Terr.RegionID = Reg.RegionID
WHERE Reg.RegionDescription = 'Western';

/*
8.1 ��������� � ����������� ������� ����� ���� ���������� �� ������� Customers
 � ��������� ���������� �� ������� �� ������� Orders. 
 ������� �� ��������, ��� � ��������� ���������� ��� �������, 
 �� ��� ����� ������ ���� �������� � ����������� �������. 
 ����������� ���������� ������� �� ����������� ���������� �������.
*/
SELECT C.CompanyName, COUNT(O.CustomerID) [Count]
FROM Customers as C LEFT JOIN  Orders as O
ON C.CustomerID = O.CustomerID
GROUP BY C.CompanyName
ORDER BY [Count];

/*
9.1 ��������� ���� ����������� (������� CompanyName � ������� Suppliers),
 � ������� ��� ���� �� ������ �������� �� ������ (UnitsInStock � ������� Products ����� 0). 
 ������������ ��������� SELECT ��� ����� ������� � �������������� ��������� IN. 
 ����� �� ������������ ������ ��������� IN �������� '=' ?
*/
SELECT CompanyName
FROM Suppliers 
WHERE Suppliers.SupplierID IN 
	(SELECT SupplierID
	FROM Products
	WHERE UnitsInStock = 0);

/*
10.1 ��������� ���� ���������, ������� ����� ����� 150 �������. 
������������ ��������� ��������������� SELECT.
*/
SELECT LastName
FROM Employees
WHERE EmployeeID IN
	(SELECT EmployeeID
	FROM Orders	
	GROUP BY EmployeeID
	HAVING COUNT(*) > 150);

/*
11.1 ��������� ���� ���������� (������� Customers), ������� �� ����� �� ������ ������ (��������� �� ������� Orders). 
������������ ��������������� SELECT � �������� EXISTS.
*/
SELECT CompanyName
FROM Customers as C
WHERE  NOT EXISTS 
	(SELECT *
	FROM Orders as O
	WHERE C.CustomerID = O.CustomerID);

/*
12.2 ��� ������������ ����������� ��������� Employees
 ��������� �� ������� Employees ������ ������ ��� ���� ��������, 
 � ������� ���������� ������� Employees (������� LastName ) �� ���� �������. 
 ���������� ������ ������ ���� ������������ �� �����������.
*/
SELECT DISTINCT LEFT(LastName, 1) [Letter]
FROM Employees
ORDER BY [Letter];

--13.1
EXECUTE dbo.GreatestOrders
@Year = 1998,
@Quantity = 5;



--13.2
EXECUTE dbo.ShippedOrdersDiff
@SpecifiedDelay = 10;


--13.3
EXECUTE dbo.SubordinationInfo
@empID = 2;

--13.4
/*
�������� �������, ������� ����������, ���� �� � �������� �����������. 
���������� ��� ������ BIT. 
� �������� �������� ��������� ������� ������������ EmployeeID. 
�������� ������� IsBoss. 
������������������ ������������� ������� ��� ���� ��������� �� ������� Employees.
*/
SELECT CONCAT(FirstName, ' ', LastName) as EmployeeName,
CASE WHEN dbo.IsBoss(EmployeeID) = 1
THEN 'True'
ELSE 'False'
END as [IsBoss]
FROM Employees;
