

private readonly int _batchSize = 1000;
private readonly string _connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=DV_UrzusNO_Main_Agency;Persist Security Info=True;User ID=sa;Password=Sql2005$";
private readonly string tableName = "TempCustomerRecordsForProfileMasking";

/*
 
 * 1. Set the batch size and connection string constants:
   - _batchSize = 1000
   - _connectionString = "Data Source=.\SQLEXPRESS;Initial Catalog=DV_UrzusNO_Main_Agency;Persist Security Info=True;User ID=sa;Password=Sql2005$"
   - tableName = "TempCustomerRecordsForProfileMasking"

2. Connect to SQL and fetch top 1000 non-processed rows:
   - Open a SQL connection using the _connectionString
   - Prepare a SQL query to fetch top 1000 non-processed rows from the tableName
   - Execute the query to retrieve the data
   - Close the SQL connection

3. Loop through all rows:
   - For each row in the fetched data:
     - Map the data into a Customer object:
       - Create a new instance of the Customer class
       - Assign the values from the fetched row to the corresponding properties of the Customer object

     - Update CustomerXML:
       - Perform the necessary operations on the CustomerApplicationXML property of the Customer object to update it accordingly

     - Mark as processed:
       - Open a new SQL connection using the _connectionString
       - Prepare a SQL update query to mark the current row as processed in the tableName
       - Execute the update query to update the row's status as processed
       - Close the SQL connection

*/




Console.WriteLine("Hello, World!");
