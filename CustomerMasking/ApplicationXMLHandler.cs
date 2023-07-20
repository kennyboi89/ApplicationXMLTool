using System;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace CustomerMasking
{
    public class ApplicationXMLHandler
    {
        private string _connectionString;

        private List<string> qnas = new List<string>
        {
            "CustomerAndInsuredPersonFirstNameTag",
            "CustomerAndInsuredPersonLastNameTag",
            "AddrLine1Tag",
            "AddrPostCodeTag",
            "AddrLine3Tag"
        };

        public ApplicationXMLHandler(string conn)
        {
            _connectionString = conn;
        }
        internal async Task UpdateCustomerApplicationXMLAsync(Customer customer)
        {
            var splittedXml = customer.CustomerApplicationXML.Split("<AWT>");
            //var replaceStringFirstName = splittedXml.FirstOrDefault(s => s.Contains("CustomerAndInsuredPersonFirstNameTag"));
            //var replaceStringLastName = splittedXml.FirstOrDefault(s => s.Contains("CustomerAndInsuredPersonLastNameTag"));
            //var replaceStringSSN = splittedXml.FirstOrDefault(s => s.Contains("CustomerAndInsuredPersonSocialSecurityTag"));


            foreach (var qnaTagName in qnas)
            {

                var stringToReplace = splittedXml.FirstOrDefault(s => s.Contains(qnaTagName));
                if (stringToReplace != null)
                {
                    if (stringToReplace.Contains("</AnswersDefinition>"))
                    {
                        customer.CustomerApplicationXML = customer.CustomerApplicationXML.Replace(stringToReplace, $"<VAL>true</VAL><DVL /><VL /><QT>{qnaTagName}</QT></AWT></AL></APS></AL></AP></PL></AnswersDefinition>");
                    }
                    else
                    {
                        customer.CustomerApplicationXML = customer.CustomerApplicationXML.Replace(stringToReplace, TemplateString(customer.FirstName, qnaTagName));
                    }


                }
                await UpdateAsync(customer.CustomerApplicationXML, customer.PersonID);
                Console.WriteLine($"Cusotmer {customer.UserID}, updated {qnaTagName}!");
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

        private string TemplateString(string qnaTag, string value) => $"<VAL>true</VAL><DVL><DV> {value} </DV></DVL><VL><V> {value} </V></VL><QT>{qnaTag}</QT></AWT>";
    }
}
