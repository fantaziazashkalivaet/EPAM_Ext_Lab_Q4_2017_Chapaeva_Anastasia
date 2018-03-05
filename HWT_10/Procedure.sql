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


/*
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
CREATE PROCEDURE GreatestOrders (@Year int, @Quantity int)
AS
BEGIN
SELECT CONCAT(E.FirstName, ' ', E.LastName), O.OrderID, (OD.UnitPrice * OD.Quantity * (1 - OD.Discount)) as [Amount]
FROM Employees as E JOIN Orders as O 
ON E.EmployeeID = O.EmployeeID JOIN [Order Details] as OD
ON O.OrderID = OD.OrderID
WHERE YEAR(O.OrderDate) = @Year
END;
GO


