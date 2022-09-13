// See https://aka.ms/new-console-template for more information
using ApplicationXMLTool;
using ApplicationXMLTool.Properties;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Runtime.Intrinsics.Arm;
using System.Xml;
using System.Xml.Linq;


const string TableName = "InsurancePolicyHistory";

string connectionString = Settings.Default.ConnectionString;

SqlXml xml = Run.GetXmlData(connectionString);

var s = xml.Value.ToString();

var splitted = s.Split("<AWT>");

var single = splitted.FirstOrDefault(s => s.Contains("MultiLineTextTag"));

s = s.Replace(single, "<VAL>true</VAL><DVL /><VL /><QT>MultiLineTextTag</QT></AWT>");

using (SqlConnection connection = new SqlConnection(connectionString))
{
    connection.Open();

    string commandText =
        "UPDATE InsurancePolicyHistory SET ApplicationXML = '" + s + "' WHERE InsurancePolicyID = 1016";

    SqlCommand updateCommand = new SqlCommand(commandText, connection);

    
    updateCommand.ExecuteNonQuery();
    



}

    var test = "";
