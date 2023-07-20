using System.Data.SqlClient;

namespace CustomerMasking
{
    public class ApplicationXMLHandler
    {
        private string _connectionString;

        public ApplicationXMLHandler(string conn)
        {
            _connectionString = conn;
        }

        internal async Task UpdateCustomerApplicationXMLAsync(Customer customer)
        {
            var data = GetMaskedData(customer);

            var splittedXml = customer.CustomerApplicationXML.Split("<AWT>");

            foreach(var qnaTag in data)
            {
                var replaceString= splittedXml.FirstOrDefault(s => s.Contains(qnaTag.Key));

                if (replaceString != null)
                {
                    customer.CustomerApplicationXML = customer.CustomerApplicationXML.Replace(replaceString, GetApplicationXMLBlockForReplace(qnaTag.Key, qnaTag.Value));

                }
            }

            await UpdateAsync(customer.CustomerApplicationXML, customer.PersonID);
            Console.WriteLine($"Cusotmer {customer.UserID}, updated!");
        }

        public Dictionary<string, string> GetMaskedData(Customer customer)
        {
            return 
                new Dictionary<string, string>()
                {
                    {"CustomerAndInsuredPersonFirstNameTag", customer.FirstName },
                    {"CustomerAndInsuredPersonLastNameTag", customer.LastName },
                    {"CustomerAndInsuredPersonSocialSecurityTag", customer.SocialSecurity },
                    {"CustomerAndInsuredPersonDOBTag", customer.DateOfBirth.ToString() },
                    {"AddrLine1Tag", customer.Line1 },  
                    {"AddrLine3Tag", customer.Line3 },                                     
                    {"AddrPostCodeTag", customer.Postcode },                                 
                };
        }

        public string GetApplicationXMLBlockForReplace(string tagName, string maskedValue) => "<VAL>true</VAL><DVL><DV>" + maskedValue + "</DV></DVL><VL><V>" + maskedValue + "</V></VL><QT>" + tagName + "</QT></AWT>";

        public async Task UpdateAsync(string xml, int personID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string commandText =
                    "UPDATE TempCustomerRecordsForProfileMasking SET CustomerApplicationXML = '" + xml + "' WHERE PersonID = " + personID;

                SqlCommand updateCommand = new SqlCommand(commandText, connection);

                await updateCommand.ExecuteNonQueryAsync();
            }
        }
    }
}
