using System;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

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

            foreach(var d in data)
            {
                var replaceString= splittedXml.FirstOrDefault(s => s.Contains(d.Key));

                if (replaceString != null)
                {
                    customer.CustomerApplicationXML = customer.CustomerApplicationXML.Replace(replaceString, GetApplicationXMLBlockForReplace(d.Key, d.Value));

                }
            }

            await UpdateAsync(customer.CustomerApplicationXML, customer.PersonID);
            Console.WriteLine($"Cusotmer {customer.UserID}, updated!");
        }

        public IDictionary<string, string> GetMaskedData(Customer customer)
        {
            IDictionary<string, string> data = new Dictionary<string, string>();
            data.Add("CustomerAndInsuredPersonFirstNameTag", customer.FirstName);
            data.Add("CustomerAndInsuredPersonLastNameTag", customer.LastName);
            data.Add("CustomerAndInsuredPersonSocialSecurityTag", customer.SocialSecurity);
            data.Add("CustomerAndInsuredPersonDOBTag", customer.DateOfBirth.ToString());
            data.Add("AddrLine1Tag", customer.Line1);
            data.Add("AddrLine3Tag", customer.Line3);
            data.Add("AddrPostCodeTag", customer.Postcode);

            return data;
        }

        public string GetApplicationXMLBlockForReplace(string tagName, string maskedValue)
        {
            return "<VAL>true</VAL><DVL><DV>" + maskedValue + "</DV></DVL><VL><V>" + maskedValue + "</V></VL><QT>" + tagName + "</QT></AWT>";
        }

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

        private string TemplateString(string qnaTag, string value) => $"<VAL>true</VAL><DVL><DV> {value} </DV></DVL><VL><V> {value} </V></VL><QT>{qnaTag}</QT></AWT>";
    }
}
