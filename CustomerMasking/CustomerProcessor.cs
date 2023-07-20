
using System.Data;
using System.Data.SqlClient;


public partial class CustomerProcessor
{
    private readonly int _batchSize = 1000;
    private readonly string _connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=DV_UrzusNO_Main_Agency;Persist Security Info=True;User ID=sa;Password=Sql2005$";
    private readonly string _tableName = "TempCustomerRecordsForProfileMasking";

    public void ProcessNonProcessedCustomers()
    {
        // Connect to SQL and fetch top 1000 non-processed rows
        var customers = FetchNonProcessedCustomers();

        // Loop through all rows
        foreach (var customer in customers)
        {
            // Update CustomerXML
            UpdateCustomerXML(customer);

            // Mark as processed
            MarkAsProcessed(customer);
        }
    }

    private List<Customer> FetchNonProcessedCustomers()
    {
        Console.WriteLine(Constants.SqlConnect);
        Console.WriteLine(_connectionString);

        List<Customer> customers = new List<Customer>();

        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            Console.WriteLine(Constants.SqlConnected);

            var query = $"SELECT TOP {_batchSize} * FROM {_tableName} WHERE Exported is null";

            using (var command = new SqlCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var customer = MapToCustomerObject(reader);

                        customers.Add(customer);
                    }
                }
            }

            connection.Close();
        }

        Console.WriteLine(Constants.Customers + customers.Count);

        return customers;
    }

    private Customer MapToCustomerObject(SqlDataReader reader)
    {
        // Map data from DataRow to a Customer object
        Customer customer = new Customer
        {
            PersonID = Convert.ToInt32(reader["PersonID"]),
            SocialSecurity = reader["SocialSecurity"].ToString(),
            FirstName = reader["FirstName"].ToString(),
            MiddleNames = reader["MiddleNames"].ToString(),
            LastName = reader["LastName"].ToString(),
            DateOfBirth = Convert.IsDBNull(reader["DateOfBirth"]) ? DateTime.MinValue : Convert.ToDateTime(reader["DateOfBirth"]),
            AddressID = Convert.ToInt32(reader["AddressID"]),
            Line1 = reader["Line1"].ToString(),
            Line3 = reader["Line3"].ToString(),
            Postcode = reader["Postcode"].ToString(),
            UserID = Convert.ToInt32(reader["UserID"]),
            CustomerUserHistoryID = Convert.ToInt32(reader["CustomerUserHistoryID"]),
            VersionID = Convert.ToInt32(reader["VersionID"]),
            CustomerApplicationXML = reader["CustomerApplicationXML"].ToString(),
            Exported = Convert.IsDBNull(reader["Exported"]) ? false : Convert.ToBoolean(reader["Exported"]),
            ExportedDate = reader["ExportedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ExportedDate"]) : (DateTime?)null
        };

        return customer;
    }

    public void UpdateCustomerXML(Customer customer)
    {
        // Perform the necessary operations on the CustomerApplicationXML property of the Customer object to update it accordingly
    }

    private void MarkAsProcessed(Customer customerData)
    {
        //using (var connection = new SqlConnection(_connectionString))
        //{
        //    connection.Open();

        //    var customerId = customerData["CustomerID"]; // Assuming there's a primary key column named "CustomerID"
        //    var query = $"UPDATE {_tableName} SET Processed = 1 WHERE CustomerID = @CustomerId";

        //    using (var command = new SqlCommand(query, connection))
        //    {
        //        command.Parameters.AddWithValue("@CustomerId", customerId);
        //        command.ExecuteNonQuery();
        //    }

        //    connection.Close();
        //}
    }
}