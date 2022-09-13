using ApplicationXMLTool.Properties;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Xml;

namespace ApplicationXMLTool
{
    public class Run
    {

        private readonly string _connectionString;
        private readonly List<string> _policiesIds;

        public Run()
        {
            _connectionString = Settings.Default.ConnectionString;
            _policiesIds = Settings.Default.Policies.Split(",").ToList();

        }

        public async Task Execute()
        {
            Console.WriteLine("Excecuting...");
            foreach (var policy in _policiesIds)
            {
                SqlXml xml = await GetXmlDataAsync(policy);

                var xmlStr = xml.Value;
                var splittedXml = xmlStr.Split("<AWT>");
                var replaceString = splittedXml.FirstOrDefault(s => s.Contains("MultiLineTextTag"));
                if (replaceString != null)
                {
                    xmlStr = xmlStr.Replace(replaceString, "<VAL>true</VAL><DVL /><VL /><QT>MultiLineTextTag</QT></AWT>");
                    await UpdateAsync(xmlStr, policy);
                    Console.WriteLine($"PolicyID {policy} updated!");
                }
            }

        }

        public async Task UpdateAsync(string xml, string policy)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string commandText =
                    "UPDATE InsurancePolicyHistory SET ApplicationXML = '" + xml + "' WHERE InsurancePolicyID = " + policy;

                SqlCommand updateCommand = new SqlCommand(commandText, connection);

                await updateCommand.ExecuteNonQueryAsync();
            }
        }


        public async Task<SqlXml> GetXmlDataAsync(string policyId)
        {

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlXml applicationXml = new SqlXml();

                string commandText =
                    "SELECT ApplicationXML,VersionID,InsurancePolicyID from InsurancePolicyHistory WHERE " +
                    "InsurancePolicyID = " + policyId;

                SqlCommand commandSales = new SqlCommand(commandText, connection);

                SqlDataReader applicationXmlReader = await commandSales.ExecuteReaderAsync();

                //  Multiple rows are returned by the SELECT, so each row
                //is read and an XmlReader (an xml data type) is set to the
                //value of its first(and only) column.

                while (await applicationXmlReader.ReadAsync())
                //  Must use GetSqlXml here to get a SqlXml type.
                //  GetValue returns a string instead of SqlXml.
                {
                    applicationXml = applicationXmlReader.GetSqlXml(0);

                }

                return applicationXml;
            }
        }
    }
}
