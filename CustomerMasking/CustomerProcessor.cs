
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
            // Map data into Customer object
            var customerObject = MapToCustomerObject(customer);

            // Update CustomerXML
            UpdateCustomerXML(customerObject);

            // Mark as processed
            MarkAsProcessed(customer);
        }
    }

    private List<DataRow> FetchNonProcessedCustomers()
    {
        //List<DataRow> customers = new List<DataRow>();

        //using (var connection = new SqlConnection(_connectionString))
        //{
        //    connection.Open();

        //    var query = $"SELECT TOP {_batchSize} * FROM {_tableName} WHERE Processed = 0";

        //    using (var command = new SqlCommand(query, connection))
        //    {
        //        using (var reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                customers.Add(reader); // Add the fetched row to the list of customers
        //            }
        //        }
        //    }

        //    connection.Close();
        //}

        //return customers;
        return null;
    }

    private Customer MapToCustomerObject(DataRow customerData)
    {
        // Map data from DataRow to a Customer object
        Customer customer = new Customer
        {
            // Map properties from customerData to corresponding properties of the Customer object
        };

        return customer;
    }

    public void UpdateCustomerXML(Customer customer)
    {
        // Perform the necessary operations on the CustomerApplicationXML property of the Customer object to update it accordingly
    }

    private void MarkAsProcessed(DataRow customerData)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            var customerId = customerData["CustomerID"]; // Assuming there's a primary key column named "CustomerID"
            var query = $"UPDATE {_tableName} SET Processed = 1 WHERE CustomerID = @CustomerId";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@CustomerId", customerId);
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }
}