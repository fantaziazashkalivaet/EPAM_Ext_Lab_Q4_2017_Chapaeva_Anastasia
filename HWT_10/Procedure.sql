-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
USE Northwind

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/*13.1
�������� ���������, ������� ���������� ����� ������� ����� 
��� ������� �� ��������� �� ������������ ���. 
� ����������� �� ����� ���� ��������� ������� ������ ��������, 
������ ���� ������ ���� � ����� �������. 
� ����������� ������� ������ ���� �������� ��������� �������: 
������� � ������ � �������� �������� (FirstName � LastName � ������: Nancy Davolio), 
����� ������ � ��� ���������. 
� ������� ���� ��������� Discount ��� ������� �������. 
��������� ���������� ���, �� ������� ���� ������� �����, � ���������� ������������ �������. 
���������� ������� ������ ���� ����������� �� �������� ����� ������. 
��������� ������ ���� ����������� � �������������� ��������� SELECT � ��� ������������� ��������.
*/
GO
CREATE OR ALTER PROCEDURE GreatestOrders (@Year int, @Quantity int)
AS
BEGIN
SELECT TOP(@Quantity) [Name], MAX([Amount]) as [MaxAmount]
FROM 
	(SELECT E.FirstName as [Name], 
		O.OrderID as [ID], 
		OD.UnitPrice*OD.Quantity*(1 - OD.Discount) as [Amount]
	FROM Employees as E JOIN Orders as O 
	ON E.EmployeeID = O.EmployeeID JOIN [Order Details] as OD
	ON O.OrderID = OD.OrderID
	WHERE YEAR(O.OrderDate) = @Year) as Tab
GROUP BY [Name];
END;
GO


/*13.2
�������� ���������, ������� ���������� ������ � ������� Orders, 
�������� ���������� ����� �������� � ���� (������� ����� OrderDate � ShippedDate). 
� ����������� ������ ���� ���������� ������, ���� ������� ��������� ���������� �������� ��� ��� �������������� ������. 
�������� �� ��������� ��� ������������� ����� 35 ����. 
�������� ��������� ShippedOrdersDiff. 
��������� ������ ����������� ��������� �������: 
OrderID, OrderDate, ShippedDate, ShippedDelay (�������� � ���� ����� ShippedDate � OrderDate), 
SpecifiedDelay (���������� � ��������� ��������).
*/
GO
CREATE OR ALTER PROCEDURE ShippedOrdersDiff (@SpecifiedDelay int)
AS
BEGIN
IF(@SpecifiedDelay is null) SET @SpecifiedDelay = 35
SELECT OrderID, OrderDate, ShippedDate, 
	DAY(ShippedDate - OrderDate) as SDelay, @SpecifiedDelay as [SpecifiedDelay]
FROM Orders
WHERE DAY(ShippedDate - OrderDate) > @SpecifiedDelay OR DAY(ShippedDate - OrderDate) = NULL;
END;
GO


/*13.3
�������� ���������, ������� ����������� ���� ����������� ��������� ��������, 
��� ����������������, ��� � ����������� ��� �����������. 
� �������� �������� ��������� ������� ������������ EmployeeID. 
���������� ����������� ����� ����������� 
� ��������� �� � ������ (������������ �������� PRINT) �������� �������� ����������. 
��������, ��� �������� ���� ����� ����������� ����� ������ ���� ��������. 
�������� ��������� SubordinationInfo. 
� �������� ��������� ��� ������� ���� ������ ���� ������������ ������, 
����������� � Books Online � ��������������� Microsoft ��� ������� ��������� ���� �����. 
������������������ ������������� ���������.
*/
GO
CREATE OR ALTER PROCEDURE SubordinationInfo (@empID int)
AS
BEGIN
SELECT CONCAT(Emp.FirstName, ' ', Emp.LastName)
FROM Employees as Emp
WHERE Emp.ReportsTo IN
	(SELECT E.EmployeeID
	FROM Employees as E
	WHERE E.ReportsTo = @empID) OR
	Emp.ReportsTo = @empID
END;
GO

/*13.4
�������� �������, ������� ����������, ���� �� � �������� �����������. 
���������� ��� ������ BIT. 
� �������� �������� ��������� ������� ������������ EmployeeID. 
�������� ������� IsBoss. 
������������������ ������������� ������� ��� ���� ��������� �� ������� Employees.
*/
CREATE OR ALTER FUNCTION dbo.IsBoss(@empID AS int)
RETURNS BIT
AS
BEGIN
RETURN
(SELECT CASE WHEN EXISTS
	(SELECT *
	FROM Employees as E
	WHERE E.ReportsTo = @empID)
	THEN 1
	ELSE 0
	END)
END;
GO