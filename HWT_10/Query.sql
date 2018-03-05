USE Northwind



/*1.1 Выбрать в таблице Orders заказы, которые были доставлены после 6 мая 1998 года (колонка ShippedDate)
включительно и которые доставлены с ShipVia >= 2.*/
SELECT OrderID, ShippedDate, ShipVia
FROM Orders
WHERE ShippedDate >= DATEFROMPARTS(1998, 05, 06) AND ShipVia >= 2;
/*Заказы, которые не имеют даты доставки не отображаются, 
так как операция сравнения NULL с числом возвращает значение UNKNOWN*/

/*
1.2 Написать запрос, который выводит только недоставленные заказы из таблицы Orders. 
В результатах запроса высвечивать для колонки ShippedDate вместо значений NULL строку ‘Not Shipped’ –
- использовать системную функцию CASЕ. Запрос должен высвечивать только колонки OrderID и ShippedDate.
*/
SELECT OrderID,
	CASE 
		WHEN ShippedDate IS NULL
			THEN 'Not Shipped'
		END ShippedDate
FROM Orders
WHERE ShippedDate IS NULL;

/*
1.3 Выбрать в таблице Orders заказы, которые были доставлены после 6 мая 1998 года (ShippedDate)
 не включая эту дату или которые еще не доставлены. 
 В запросе должны высвечиваться только колонки OrderID (переименовать в Order Number) 
 и ShippedDate (переименовать в Shipped Date). 
 В результатах запроса высвечивать для колонки ShippedDate вместо значений NULL строку ‘Not Shipped’,
  для остальных значений высвечивать дату в формате по умолчанию.
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
2.1 Выбрать из таблицы Customers всех заказчиков, проживающих в USA и Canada. 
Запрос сделать с только помощью оператора IN. 
Высвечивать колонки с именем пользователя и названием страны в результатах запроса. 
Упорядочить результаты запроса по имени заказчиков и по месту проживания.
*/
SELECT ContactName, Country
FROM Customers
WHERE Country IN ('USA', 'Canada')
ORDER BY ContactName, Country;


/*
2.2 Выбрать из таблицы Customers всех заказчиков, не проживающих в USA и Canada. 
Запрос сделать с помощью оператора IN. 
Высвечивать колонки с именем пользователя и названием страны в результатах запроса. 
Упорядочить результаты запроса по имени заказчиков.
*/
SELECT ContactName, Country
FROM Customers
WHERE Country NOT IN ('USA', 'Canada')
ORDER BY ContactName;

/*
2.3 Выбрать из таблицы Customers все страны, в которых проживают заказчики. 
Страна должна быть упомянута только один раз и список отсортирован по убыванию. 
Не использовать предложение GROUP BY. Высвечивать только одну колонку в результатах запроса.
*/
SELECT DISTINCT Country
FROM Customers
ORDER BY Country DESC;

/*
3.1 Выбрать все заказы (OrderID) из таблицы Order Details (заказы не должны повторяться), 
где встречаются продукты с количеством от 3 до 10 включительно – это колонка Quantity в таблице Order Details. 
Использовать оператор BETWEEN. 
Запрос должен высвечивать только колонку OrderID.
*/
SELECT DISTINCT OrderID
FROM [Order Details]
WHERE Quantity BETWEEN 3 AND 10;

/*
3.2 Выбрать всех заказчиков из таблицы Customers, 
у которых название страны начинается на буквы из диапазона b и g. 
Использовать оператор BETWEEN. 
Проверить, что в результаты запроса попадает Germany. 
Запрос должен высвечивать только колонки CustomerID и Country и отсортирован по Country.
*/
SELECT CustomerID, Country
FROM Customers
/* WHERE Country LIKE '[b-g]%'*/
WHERE LEFT(Country, 1) BETWEEN 'b' AND 'g'
ORDER BY Country;

/*
3.3 Выбрать всех заказчиков из таблицы Customers, 
у которых название страны начинается на буквы из диапазона b и g, 
не используя оператор BETWEEN. 
С помощью опции “Execution Plan” определить 
какой запрос предпочтительнее 3.2 или 3.3 – для этого надо ввести в скрипт
 выполнение текстового Execution Plan-a для двух этих запросов, 
 результаты выполнения Execution Plan надо ввести в скрипт в виде комментария 
 и по их результатам дать ответ на вопрос – по какому параметру было проведено сравнение. 
 Запрос должен высвечивать только колонки CustomerID и Country и отсортирован по Country.
*/

SELECT CustomerID, Country
FROM Customers
WHERE Country LIKE '[b-g]%'
ORDER BY Country;

/* 
туть не разобралась, как план в текстовом формате подключать, 
но если просто включать план выполнения запросов, то он покажет, что 3.2 и 3.3 равнозначны
*/

/*
4.1 В таблице Products найти все продукты (колонка ProductName), где встречается подстрока 'chocolade'. 
Известно, что в подстроке 'chocolade' может быть изменена одна буква 'c' в середине - 
найти все продукты, которые удовлетворяют этому условию. 
Подсказка: результаты запроса должны высвечивать 2 строки.
*/
SELECT ProductName
FROM Products
WHERE ProductName LIKE '%cho_olade%';

/*
5.1 Найти общую сумму всех заказов из таблицы Order Details 
с учетом количества закупленных товаров и скидок по ним. 
Результат округлить до сотых и высветить в стиле 1 для типа данных money. 
Скидка (колонка Discount) составляет процент из стоимости для данного товара. 
Для определения действительной цены на проданный продукт надо вычесть скидку 
из указанной в колонке UnitPrice цены. 
Результатом запроса должна быть одна запись с одной колонкой с названием колонки 'Totals'.
*/
/*
SELECT CONVERT(money, ROUND(SUM(Quantity*UnitPrice*(1 - (Discount / 100))), 2), 1) [5.1] -- у меня другая цифра получается
FROM [Order Details];*/

--В случае, если Discount всё-таки не в процентах:
SELECT CONVERT(money, ROUND(SUM(Quantity*UnitPrice*(1 - Discount)), 2), 1) [5.1]
FROM [Order Details]; 



/*
5.2 По таблице Orders найти количество заказов, которые еще не были доставлены 
(т.е. в колонке ShippedDate нет значения даты доставки). 
Использовать при этом запросе только оператор COUNT. 
Не использовать предложения WHERE и GROUP.
*/
SELECT COUNT(*) - COUNT(ShippedDate) [5.2]
FROM Orders;

/*
5.3 По таблице Orders найти количество различных покупателей (CustomerID), сделавших заказы. 
Использовать функцию COUNT и не использовать предложения WHERE и GROUP.
*/
SELECT COUNT(DISTINCT ShippedDate) [5.3]
FROM Orders;


/*
6.1 По таблице Orders найти количество заказов с группировкой по годам. 
В результатах запроса надо высвечивать две колонки c названиями Year и Total. 
Написать проверочный запрос, который вычисляет количество всех заказов.
*/
SELECT YEAR(OrderDate) [Year], COUNT(*) [Total]
FROM Orders
GROUP BY YEAR(OrderDate);

/*чек*/
SELECT COUNT(*) [Total]
From Orders;


/*
6.2 По таблице Orders найти количество заказов, cделанных каждым продавцом. 
Заказ для указанного продавца – это любая запись в таблице Orders, 
где в колонке EmployeeID задано значение для данного продавца. 
В результатах запроса надо высвечивать колонку с именем продавца 
(Должно высвечиваться имя полученное конкатенацией LastName & FirstName. 
Эта строка LastName & FirstName должна быть получена отдельным запросом в колонке основного запроса. 
Также основной запрос должен использовать группировку по EmployeeID.) 
с названием колонки ‘Seller’ и колонку c количеством заказов высвечивать с названием 'Amount'. 
Результаты запроса должны быть упорядочены по убыванию количества заказов.
*/
SELECT --сделай, используя один select
	(SELECT CONCAT(FirstName, ' ', LastName)
	FROM Employees 
	WHERE Employees.EmployeeID = Orders.EmployeeID) [Seller],
	COUNT(*) [Amount]
FROM Orders
GROUP BY EmployeeID
ORDER BY [Amount] DESC;

--с одним select:
SELECT CONCAT(E.FirstName, ' ', E.LastName), COUNT(*) [Amount]
FROM Employees as E JOIN Orders as O ON
E.EmployeeID = O.EmployeeID
GROUP BY O.EmployeeID, E.FirstName, E.LastName
ORDER BY [Amount] DESC;


/*
6.3 По таблице Orders найти количество заказов, cделанных каждым продавцом и для каждого покупателя. 
Необходимо определить это только для заказов сделанных в 1998 году. 
В результатах запроса надо высвечивать колонку с именем продавца (название колонки ‘Seller’), 
колонку с именем покупателя (название колонки ‘Customer’) 
и колонку c количеством заказов высвечивать с названием 'Amount'. 
В запросе необходимо использовать специальный оператор языка T-SQL для работы с выражением GROUP 
(Этот же оператор поможет выводить строку “ALL” в результатах запроса). 
Группировки должны быть сделаны по ID продавца и покупателя. 
Результаты запроса должны быть упорядочены по продавцу, покупателю и по убыванию количества продаж. 
В результатах должна быть сводная информация по продажам.
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
6.4 Найти покупателей и продавцов, которые живут в одном городе. 
Если в городе живут только один или несколько продавцов 
или только один или несколько покупателей, 
то информация о таких покупателя и продавцах не должна попадать в результирующий набор. 
Не использовать конструкцию JOIN. 
В результатах запроса необходимо вывести следующие заголовки для результатов запроса:
 ‘Person’, ‘Type’ (здесь надо выводить строку ‘Customer’ или ‘Seller’ в завимости от типа записи), ‘City’. 
 Отсортировать результаты запроса по колонке ‘City’ и по ‘Person’.
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
6.5 Найти всех покупателей, которые живут в одном городе. 
В запросе использовать соединение таблицы Customers c собой - самосоединение. 
Высветить колонки CustomerID и City. 
Запрос не должен высвечивать дублируемые записи. 
Для проверки написать запрос, который высвечивает города, 
которые встречаются более одного раза в таблице Customers. 
Это позволит проверить правильность запроса.
*/
SELECT DISTINCT Cust1.CustomerID, Cust2.City
FROM Customers as Cust1 JOIN Customers as Cust2
ON Cust1.CustomerID <> Cust2.CustomerID AND Cust1.City = Cust2.City;

--чек
SELECT City
FROM Customers
GROUP BY City
HAVING COUNT(*) > 1;

/*
6.6 По таблице Employees найти для каждого продавца его руководителя, 
т.е. кому он делает репорты. 
Высветить колонки с именами 'User Name' (LastName) и 'Boss'. 
В колонках должны быть высвечены имена из колонки LastName. 
Высвечены ли все продавцы в этом запросе?
*/
SELECT Emp1.LastName [User Name], Emp2.LastName [Boss]
FROM Employees as Emp1 JOIN Employees as Emp2
ON Emp1.ReportsTo = Emp2.EmployeeID;
--выведены не все продавцы. похоже, что Fuller босс всех боссов

/*
7.1 Определить продавцов, которые обслуживают регион 'Western' (таблица Region). 
Результаты запроса должны высвечивать два поля: 
'LastName' продавца и название обслуживаемой территории ('TerritoryDescription' из таблицы Territories). 
Запрос должен использовать JOIN в предложении FROM. 
Для определения связей между таблицами Employees и Territories надо использовать графические диаграммы для базы Northwind.
*/
SELECT LastName, TerritoryDescription
FROM Employees as Emp INNER JOIN EmployeeTerritories as EmpTerr
ON Emp.EmployeeID = EmpTerr.EmployeeID INNER JOIN Territories as Terr
ON EmpTerr.TerritoryID = Terr.TerritoryID INNER JOIN Region as Reg
ON Terr.RegionID = Reg.RegionID
WHERE Reg.RegionDescription = 'Western';

/*
8.1 Высветить в результатах запроса имена всех заказчиков из таблицы Customers
 и суммарное количество их заказов из таблицы Orders. 
 Принять во внимание, что у некоторых заказчиков нет заказов, 
 но они также должны быть выведены в результатах запроса. 
 Упорядочить результаты запроса по возрастанию количества заказов.
*/
SELECT C.CompanyName, COUNT(O.CustomerID) [Count]
FROM Customers as C LEFT JOIN  Orders as O
ON C.CustomerID = O.CustomerID
GROUP BY C.CompanyName
ORDER BY [Count];

/*
9.1 Высветить всех поставщиков (колонка CompanyName в таблице Suppliers),
 у которых нет хотя бы одного продукта на складе (UnitsInStock в таблице Products равно 0). 
 Использовать вложенный SELECT для этого запроса с использованием оператора IN. 
 Можно ли использовать вместо оператора IN оператор '=' ?
*/
SELECT CompanyName
FROM Suppliers 
WHERE Suppliers.SupplierID IN 
	(SELECT SupplierID
	FROM Products
	WHERE UnitsInStock = 0);

/*
10.1 Высветить всех продавцов, которые имеют более 150 заказов. 
Использовать вложенный коррелированный SELECT.
*/
SELECT LastName
FROM Employees
WHERE EmployeeID IN
	(SELECT EmployeeID
	FROM Orders	
	GROUP BY EmployeeID
	HAVING COUNT(*) > 150);

/*
11.1 Высветить всех заказчиков (таблица Customers), которые не имеют ни одного заказа (подзапрос по таблице Orders). 
Использовать коррелированный SELECT и оператор EXISTS.
*/
SELECT CompanyName
FROM Customers as C
WHERE  NOT EXISTS 
	(SELECT *
	FROM Orders as O
	WHERE C.CustomerID = O.CustomerID);

/*
12.2 Для формирования алфавитного указателя Employees
 высветить из таблицы Employees список только тех букв алфавита, 
 с которых начинаются фамилии Employees (колонка LastName ) из этой таблицы. 
 Алфавитный список должен быть отсортирован по возрастанию.
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
Написать функцию, которая определяет, есть ли у продавца подчиненные. 
Возвращает тип данных BIT. 
В качестве входного параметра функции используется EmployeeID. 
Название функции IsBoss. 
Продемонстрировать использование функции для всех продавцов из таблицы Employees.
*/
SELECT CONCAT(FirstName, ' ', LastName) as EmployeeName,
CASE WHEN dbo.IsBoss(EmployeeID) = 1
THEN 'True'
ELSE 'False'
END as [IsBoss]
FROM Employees;
