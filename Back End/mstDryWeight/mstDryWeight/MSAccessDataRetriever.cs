using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Xml.Serialization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;


namespace mstDryWeight
{
    public class MSAccessDataRetriever
    {
        //This is the MSACCESS connection object named con
        OleDbConnection con;
        public Boolean connectionError;
        public String connectionErrorMessage;
        //dataSource is provided here as an argument
        public MSAccessDataRetriever(String connectionString)
        {
            try
            {
                con = new OleDbConnection(connectionString);
                con.Open();
                connectionError = false;
                connectionErrorMessage = "Error: Failed to create a database connection";
            }
            catch (Exception e)
            {
                connectionError = true;
                connectionErrorMessage = e.Message;
            }
        }

        public String GetQueryResult(String query)
        {
            try
            {
                var queryData = new DataTable();
                var command = new OleDbCommand(query, con);
                command.CommandTimeout = 600;
                var adapter = new OleDbDataAdapter(command);
                adapter.Fill(queryData);

                //USIING JsonConvert from Newtonsoft.Json
                string myString = DataTableToJSONWithJSONNet(queryData);
                return myString;
            }
            catch (Exception e)
            {
                return "{\"DataException\":\"" + e.Message + "\"}";
            }

        }

        public string DataTableToJSONWithJSONNet(DataTable table)
        {
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;
        }



        public void Dispose()
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }
}