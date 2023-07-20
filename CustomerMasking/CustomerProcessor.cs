
using CustomerMasking;
using System.Data.SqlClient;

public partial class CustomerProcessor
{
    private readonly int _batchSize = App.Default.BatchSize;
    private readonly string _connectionString = App.Default.ConnectionString;
    private readonly string _tableName = App.Default.TableName;

    public async Task ProcessNonProcessedCustomersAsync()
    {
        // Connect to SQL and fetch top 1000 non-processed rows
        var customers = FetchNonProcessedCustomers();

        // Loop through all rows
        foreach (var customer in customers)
        {
            // Update CustomerXML
            await UpdateCustomerXMLAsync(customer);
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
        return new Customer
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
    }

    public async Task UpdateCustomerXMLAsync(Customer customer)
    {
        // Perform the necessary operations on the CustomerApplicationXML property of the Customer object to update it accordingly
        ApplicationXMLHandler handler = new ApplicationXMLHandler(_connectionString);
        await handler.UpdateCustomerApplicationXMLAsync(customer);

        var userid = MarkAsProcessed(customer);
    }

    public int MarkAsProcessed(Customer customerData)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            var userId = customerData.UserID; // Assuming there's a primary key column named "CustomerID"
            var query = $"UPDATE {_tableName} SET Exported = 1, ExportedDate = @Date WHERE UserID = @UserID";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Date", DateTime.Now);
                command.Parameters.AddWithValue("@UserID", userId);
                command.ExecuteNonQuery();
            }

            Console.WriteLine(Constants.CustomerProcessed + userId);

            connection.Close();

            return userId;
        }
    }
}