using System;
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
            var splittedXml = customer.CustomerApplicationXML.Split("<AWT>");
            var replaceStringFirstName = splittedXml.FirstOrDefault(s => s.Contains("CustomerAndInsuredPersonFirstNameTag"));
            var replaceStringLastName = splittedXml.FirstOrDefault(s => s.Contains("CustomerAndInsuredPersonLastNameTag"));
            var replaceStringSSN = splittedXml.FirstOrDefault(s => s.Contains("CustomerAndInsuredPersonSocialSecurityTag"));

            if (replaceStringFirstName != null)
            {
                if (replaceStringFirstName.Contains("</AnswersDefinition>"))
                {
                    customer.CustomerApplicationXML = customer.CustomerApplicationXML.Replace(replaceStringFirstName, "<VAL>true</VAL><DVL /><VL /><QT>CustomerAndInsuredPersonFirstNameTag</QT></AWT></AL></APS></AL></AP></PL></AnswersDefinition>");
                }
                else
                {
                    customer.CustomerApplicationXML = customer.CustomerApplicationXML.Replace(replaceStringFirstName, "<VAL>true</VAL><DVL><DV>" + customer.FirstName + "</DV></DVL><VL><V>"+ customer.FirstName +"</V></VL><QT>CustomerAndInsuredPersonFirstNameTag</QT></AWT>");
                }

                await UpdateAsync(customer.CustomerApplicationXML, customer.PersonID);
                Console.WriteLine($"Cusotmer {customer.UserID}, updated CustomerAndInsuredPersonFirstNameTag!");
            }
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

    }
}
