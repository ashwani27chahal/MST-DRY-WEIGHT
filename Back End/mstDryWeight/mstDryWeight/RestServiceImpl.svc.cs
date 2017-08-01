using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Teradata.Client.Provider;

namespace mstDryWeight
{

    public class RestServiceImpl : IRestServiceImpl
    {
        public string GetInventoryData(string DesignIDs)
        {
            string connectionString;
            connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;";
            connectionString = connectionString + @"Data Source=\\na.micron.com\root\micronweb\MTI\NT\webdata.micron.com\webapps\mfg\QA\packrel\Qual_Data\MST_Dry_Weight.mdb;";
            connectionString = connectionString + "Persist Security Info=True";
            //return connectionString;
            MSAccessDataRetriever mds = new MSAccessDataRetriever(connectionString);
            if (!mds.connectionError)
            {
                string stringSQL;
                stringSQL = "SELECT Design_ID, Die_Count, Package_Type, Ball_Count, MST_Dry_Weight \n";
                stringSQL = stringSQL + "FROM Package_Weight \n";
                stringSQL = stringSQL + "WHERE Design_ID IN(" + DesignIDs + ") \n";
                stringSQL = stringSQL + "ORDER BY Design_ID";

                string jsonResult = mds.GetQueryResult(stringSQL);
                mds.Dispose();
                return jsonResult;
            }
            else
                return "{\"ConnectionException\":\"" + mds.connectionErrorMessage + "\"}";
        }
    }
}
