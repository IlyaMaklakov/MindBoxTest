SELECT Product_Id, COUNT(Product_Id) AS CountOfSalesAsFirst
FROM [testSconome3].[dbo].[Sales] P
INNER JOIN
(SELECT Customer_Id,
MIN(Date) As FirstDate
  FROM [dbo].[Sales]
  GROUP BY Customer_Id) X
  ON P.Customer_Id = X.Customer_Id AND P.Date = X.FirstDate
  GROUP BY Product_Id
  ORDER BY CountOfSalesAsFirst DESC