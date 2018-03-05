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
Написать процедуру, которая возвращает заказы в таблице Orders, 
согласно указанному сроку доставки в днях (разница между OrderDate и ShippedDate). 
В результатах должны быть возвращены заказы, срок которых превышает переданное значение или еще недоставленные заказы. 
Значению по умолчанию для передаваемого срока 35 дней. 
Название процедуры ShippedOrdersDiff. 
Процедура должна высвечивать следующие колонки: 
OrderID, OrderDate, ShippedDate, ShippedDelay (разность в днях между ShippedDate и OrderDate), 
SpecifiedDelay (переданное в процедуру значение).
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
Написать процедуру, которая высвечивает всех подчиненных заданного продавца, 
как непосредственных, так и подчиненных его подчиненных. 
В качестве входного параметра функции используется EmployeeID. 
Необходимо распечатать имена подчиненных 
и выровнять их в тексте (использовать оператор PRINT) согласно иерархии подчинения. 
Продавец, для которого надо найти подчиненных также должен быть высвечен. 
Название процедуры SubordinationInfo. 
В качестве алгоритма для решения этой задачи надо использовать пример, 
приведенный в Books Online и рекомендованный Microsoft для решения подобного типа задач. 
Продемонстрировать использование процедуры.
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
Написать функцию, которая определяет, есть ли у продавца подчиненные. 
Возвращает тип данных BIT. 
В качестве входного параметра функции используется EmployeeID. 
Название функции IsBoss. 
Продемонстрировать использование функции для всех продавцов из таблицы Employees.
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