using ApplicationXMLTool.Properties;
using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Xml;

namespace ApplicationXMLTool
{
    public class Run
    {
        private readonly string _connectionString;
        private readonly List<string> _policyIds;
        private readonly bool _updateDocuments;

        public Run()
        {
            _connectionString = Settings.Default.ConnectionString;
            _policyIds = Settings.Default.Policies.Trim().Split(",").ToList();
            _updateDocuments = Settings.Default.UpdateDocuments;
        }

        public async Task Execute()
        {
            Console.WriteLine("Excecuting...");
            foreach (var policy in _policyIds)
            {
                List<SqlXml> xmls = await GetXmlDataAsync(policy);

                var version = 0;
                foreach (SqlXml xml in xmls)
                {
                    var xmlStr = xml.Value;
                    var splittedXml = xmlStr.Split("<AWT>");
                    var replaceString = splittedXml.FirstOrDefault(s => s.Contains("MultiLineTextTag"));
                    var replaceStringNotes = splittedXml.FirstOrDefault(s => s.Contains("InternalNotesTag"));


                    if (replaceString != null)
                    {
                        if (replaceString.Contains("</AnswersDefinition>"))
                        {
                            xmlStr = xmlStr.Replace(replaceString, "<VAL>true</VAL><DVL /><VL /><QT>MultiLineTextTag</QT></AWT></AL></APS></AL></AP></PL></AnswersDefinition>");
                        }
                        else
                        {
                            xmlStr = xmlStr.Replace(replaceString, "<VAL>true</VAL><DVL /><VL /><QT>MultiLineTextTag</QT></AWT>");
                        }
                        
                        await UpdateAsync(xmlStr, policy, version);
                        Console.WriteLine($"PolicyID {policy} Version: {version}, updated MultiLineTextTag!");
                    }
                    else
                    {
                        Console.WriteLine($"PolicyID {policy} Version: {version}, NOT updated MultiLineTextTag!");
                    }

                    if (replaceStringNotes != null)
                    {
                        if (replaceStringNotes.Contains("</AnswersDefinition>"))
                        {
                            xmlStr = xmlStr.Replace(replaceStringNotes, "<VAL>true</VAL><DVL /><VL /><QT>InternalNotesTag</QT></AWT></AL></APS></AL></AP></PL></AnswersDefinition>");
                        }
                        else
                        {
                            xmlStr = xmlStr.Replace(replaceStringNotes, "<VAL>true</VAL><DVL /><VL /><QT>InternalNotesTag</QT></AWT>");
                        }

                        await UpdateAsync(xmlStr, policy, version);
                        Console.WriteLine($"PolicyID {policy} Version: {version}, updated InternalNotesTag!");
                    }
                    else
                    {
                        Console.WriteLine($"PolicyID {policy} Version: {version}, NOT updated InternalNotesTag!");
                    }

                    version++;
                }

                if (_updateDocuments)
                {
                    var listOfDocumentIds = await GetDocumentIds(policy);

                    if (listOfDocumentIds != null)
                    {
                        foreach (var documentId in listOfDocumentIds)
                        {
                            await UpdateDocumentAsync(documentId);
                        }
                    }
                }

            }
        }

        public async Task<List<string>> GetDocumentIds(string policyId)
        {
            var listOfDocumentIds = new List<string>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var listOfDocuments = new List<string>();

                string commandText =
                    "SELECT pdm.DocumentID FROM PolicyToDocumentMapping pdm " +
                    "INNER JOIN DocumentStore_Document ds ON ds.DocumentID = pdm.DocumentID " +
                    "INNER JOIN DocumentStore_DocumentContent dsc ON dsc.DocumentID = pdm.DocumentID " +
                    "INNER JOIN InsurancePolicies ip ON pdm.InsurancePolicyID = ip.InsurancePolicyID " +
                    "INNER JOIN contracts c ON ip.ContractID = c.ContractID " +
                    "WHERE pdm.InsurancePolicyID = " + policyId;

                SqlCommand commandSales = new SqlCommand(commandText, connection);

                SqlDataReader reader = await commandSales.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    listOfDocumentIds.Add(reader[0].ToString());
                }
            }

            return listOfDocumentIds;
        }

        public async Task UpdateDocumentAsync(string documentId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string commandText =
                    "UPDATE DocumentProduction set DocumentProductionStatusID = 1 where DocumentID = " + documentId;

                string commandText2 =
                   "UPDATE DocumentStore_Document SET Deleted = 1 where DocumentID = " + documentId;

                SqlCommand updateCommand = new SqlCommand(commandText, connection);
                SqlCommand updateCommand2 = new SqlCommand(commandText2, connection);

                await updateCommand.ExecuteNonQueryAsync();
                await updateCommand2.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateAsync(string xml, string policy, int version)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string commandText =
                    "UPDATE InsurancePolicyHistory SET ApplicationXML = '" + xml + "' WHERE InsurancePolicyID = " + policy +" AND VersionID = " + version;

                SqlCommand updateCommand = new SqlCommand(commandText, connection);

                await updateCommand.ExecuteNonQueryAsync();
            }
        }

        public async Task<List<SqlXml>> GetXmlDataAsync(string policyId)
        {

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var listOfPolicies = new List<SqlXml>();
                SqlXml applicationXml = new SqlXml();

                string commandText =
                    "SELECT ApplicationXML,VersionID from InsurancePolicyHistory WHERE " +
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
                    listOfPolicies.Add(applicationXmlReader.GetSqlXml(0));

                }

                return listOfPolicies;
            }
        }
    }
}
