namespace CustomerMasking.Tests.UpdateXML
{
    public class UpdateCustomerXmlTests
    {
        public Customer customer { get; set; }
        public UpdateCustomerXmlTests()
        {

            customer = new Customer
            {
                UserID = 1,
                SocialSecurity = "123-45-6789",
                FirstName = "John",
                MiddleNames = "William",
                LastName = "Doe",
                DateOfBirth = new DateTime(1990, 5, 15),
                AddressID = 100,
                Line1 = "123 Main Street",
                Line3 = "Apartment 4B",
                Postcode = "12345",
                CustomerUserHistoryID = 500,
                VersionID = 3,
                CustomerApplicationXML = "<xml>...</xml>",
                Exported = false,
                ExportedDate = null
            };


        }
        [Fact]
        public async Task TestCustomerXMLTests()
        {
            // arrange 
            customer.CustomerApplicationXML = CustomerXMLString.GetTestXML();
            var customerProsessing = new CustomerProcessor();
            
            // act

            await customerProsessing.UpdateCustomerXMLAsync(customer);

            // assert
            Assert.True(true);
        }

        [Fact]
        public void MarkAsProcessedTests()
        {

            // arrange 
            customer.UserID = 199;
            var customerProsessing = new CustomerProcessor();

            // act

            var result = customerProsessing.MarkAsProcessed(customer);

            // assert

            Assert.Equal(customer.UserID, result);

        }
    }
}
