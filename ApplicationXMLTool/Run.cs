using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Xml;

namespace ApplicationXMLTool
{
    public static class Run
    {
        public static SqlXml GetXmlData(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlXml applicationXml = new SqlXml();

                // The query includes two specific customers for simplicity's
                // sake. A more realistic approach would use a parameter
                // for the CustomerID criteria. The example selects two rows
                // in order to demonstrate reading first from one row to
                // another, then from one node to another within the xml column.
                string commandText =
                    "SELECT ApplicationXML,VersionID,InsurancePolicyID from InsurancePolicyHistory WHERE " +
                    "InsurancePolicyID = 1016";

                SqlCommand commandSales = new SqlCommand(commandText, connection);

                SqlDataReader applicationXmlReader = commandSales.ExecuteReader();

                //  Multiple rows are returned by the SELECT, so each row
                //  is read and an XmlReader (an xml data type) is set to the
                //  value of its first (and only) column.
                int countRow = 1;
                while (applicationXmlReader.Read())
                //  Must use GetSqlXml here to get a SqlXml type.
                //  GetValue returns a string instead of SqlXml.
                {
                    applicationXml = applicationXmlReader.GetSqlXml(0);
                    XmlReader applicationReaderXml = applicationXml.CreateReader();
                    Console.WriteLine("-----Row " + countRow + "-----");

                    //  Move to the root.
                    applicationReaderXml.MoveToContent();

                    //  We know each node type is either Element or Text.
                    //  All elements within the root are string values.
                    //  For this simple example, no elements are empty.
                    while (applicationReaderXml.Read())
                    {
                        if (applicationReaderXml.NodeType == XmlNodeType.Text)
                        {

                            string elementLocalName =
                            applicationReaderXml.LocalName;
                        //applicationReaderXml.Read();
                        Console.WriteLine(elementLocalName + ": " +
                            applicationReaderXml.Value);

                            if(applicationReaderXml.Value.Contains("MultiLineTextTag"))
                            {
                                var test = "";
                                var attr = applicationReaderXml.GetAttribute("AWS");
                                var hasAttr = applicationReaderXml.HasAttributes;
                                var quoteChar = applicationReaderXml.QuoteChar;
                            }
                           
                        }
                        if (applicationReaderXml.NodeType == XmlNodeType.Element)
                        {
                            var test = "";
                            var attr = applicationReaderXml.GetAttribute("AWS");
                            var hasAttr = applicationReaderXml.HasAttributes;
                            var quoteChar = applicationReaderXml.QuoteChar;
                        }


                    }
                    countRow = countRow + 1;
                }

                return applicationXml;
            }
        }
    }
}
