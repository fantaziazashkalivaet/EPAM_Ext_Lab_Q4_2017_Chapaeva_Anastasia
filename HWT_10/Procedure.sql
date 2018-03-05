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
Написать процедуру, которая возвращает самый крупный заказ 
для каждого из продавцов за определенный год. 
В результатах не может быть несколько заказов одного продавца, 
должен быть только один и самый крупный. 
В результатах запроса должны быть выведены следующие колонки: 
колонка с именем и фамилией продавца (FirstName и LastName – пример: Nancy Davolio), 
номер заказа и его стоимость. 
В запросе надо учитывать Discount при продаже товаров. 
Процедуре передается год, за который надо сделать отчет, и количество возвращаемых записей. 
Результаты запроса должны быть упорядочены по убыванию суммы заказа. 
Процедура должна быть реализована с использованием оператора SELECT и БЕЗ ИСПОЛЬЗОВАНИЯ КУРСОРОВ.
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


