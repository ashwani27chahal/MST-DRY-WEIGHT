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


        public string expandedResult(string DesignID)
        {
            TeraDataRetriever tds = new TeraDataRetriever("BOTERAPROD09", "TERADATAREADER", "TeradataReader", "DataTera01");
            if (!tds.connectionError)
            {
                string stringSQL;
                stringSQL = "";
                stringSQL = stringSQL + "SELECT E.MA_ID, \n";
                stringSQL = stringSQL + "max(case when attr_values.attr_ID = 'DESIGN ID' then attr_values.attr_value else '' end) as DESIGN_ID, \n";
                stringSQL = stringSQL + "max(case when attr_values.attr_ID = 'LEAD COUNT' then attr_values.attr_value else '' end) as LEAD_COUNT, \n";
                stringSQL = stringSQL + "max(case when attr_values.attr_ID = 'NUMBER OF DIE IN PKG' then attr_values.attr_value else '' end) as NUMBER_OF_DIE_IN_PKG, \n";
                stringSQL = stringSQL + "max(case when attr_values.attr_ID = 'PACKAGE TYPE' then attr_values.attr_value else '' end) as PACKAGE_TYPE, \n";
                stringSQL = stringSQL + "E.attr_value as DRY_WEIGHT \n";

                stringSQL = stringSQL + "FROM       WW_BE_DM.ENG_DATA_RECORDED E \n";

                stringSQL = stringSQL + "INNER JOIN WW_BE_DM.MA_attr \n";
                stringSQL = stringSQL + "ON         WW_BE_DM.MA_attr.MA_ID = E.MA_ID \n";
                stringSQL = stringSQL + "AND        WW_BE_DM.MA_attr.system_name = E.system_name \n";
                stringSQL = stringSQL + "AND        WW_BE_DM.MA_attr.attr_ID = 'DESIGN ID' \n";
                stringSQL = stringSQL + "AND        WW_BE_DM.MA_attr.attr_value = " + DesignID + " \n";

                stringSQL = stringSQL + "LEFT OUTER JOIN WW_BE_DM.MA_attr attr_values \n";
                stringSQL = stringSQL + "ON              attr_values.MA_ID = E.MA_ID  \n";
                stringSQL = stringSQL + "AND             attr_values.system_name = E.system_name  \n";
                stringSQL = stringSQL + "AND             attr_values.attr_ID IN('LEAD COUNT', 'NUMBER OF DIE IN PKG', 'PACKAGE TYPE', 'DESIGN ID') \n";

                stringSQL = stringSQL + "WHERE E.attr_ID = 'DRY WEIGHT' \n";
                stringSQL = stringSQL + "AND   E.system_name = 'MAMQA' \n";
                stringSQL = stringSQL + "AND   E.attr_value IS NOT NULL \n";
                stringSQL = stringSQL + "AND   E.attr_value > '0.0' \n";

                stringSQL = stringSQL + "GROUP BY E.MA_ID, \n";
                stringSQL = stringSQL + "E.attr_ID, \n";
                stringSQL = stringSQL + "E.attr_value; \n";


                string jsonResult = tds.GetQueryResult(stringSQL);
                tds.Dispose();
                return jsonResult;
            }
            else
                return "{\"Exception\":\"" + tds.connectionErrorMessage + "\"}";
        }



        public string fetchResults(string DesignID)
        {
            TeraDataRetriever tds = new TeraDataRetriever("BOTERAPROD09", "TERADATAREADER", "TeradataReader", "DataTera01");
            if (!tds.connectionError)
            {
                string stringSQL;
                stringSQL = "WITH ResultSet AS( \n";
                stringSQL = stringSQL + "SELECT E.MA_ID, \n";
                stringSQL = stringSQL + "max(case when attr_values.attr_ID = 'DESIGN ID' then attr_values.attr_value else '' end) as DESIGN_ID, \n";
                stringSQL = stringSQL + "max(case when attr_values.attr_ID = 'LEAD COUNT' then attr_values.attr_value else '' end) as LEAD_COUNT, \n";
                stringSQL = stringSQL + "max(case when attr_values.attr_ID = 'NUMBER OF DIE IN PKG' then attr_values.attr_value else '' end) as NUMBER_OF_DIE_IN_PKG, \n";
                stringSQL = stringSQL + "max(case when attr_values.attr_ID = 'PACKAGE TYPE' then attr_values.attr_value else '' end) as PACKAGE_TYPE, \n";
                stringSQL = stringSQL + "E.attr_value as DRY_WEIGHT \n";

                stringSQL = stringSQL + "FROM       WW_BE_DM.ENG_DATA_RECORDED E \n";

                stringSQL = stringSQL + "INNER JOIN WW_BE_DM.MA_attr \n";
                stringSQL = stringSQL + "ON         WW_BE_DM.MA_attr.MA_ID = E.MA_ID \n";
                stringSQL = stringSQL + "AND        WW_BE_DM.MA_attr.system_name = E.system_name \n";
                stringSQL = stringSQL + "AND        WW_BE_DM.MA_attr.attr_ID = 'DESIGN ID' \n";
                stringSQL = stringSQL + "AND        WW_BE_DM.MA_attr.attr_value = " + DesignID + " \n";

                stringSQL = stringSQL + "LEFT OUTER JOIN WW_BE_DM.MA_attr attr_values \n";
                stringSQL = stringSQL + "ON              attr_values.MA_ID = E.MA_ID  \n";
                stringSQL = stringSQL + "AND             attr_values.system_name = E.system_name  \n";
                stringSQL = stringSQL + "AND             attr_values.attr_ID IN('LEAD COUNT', 'NUMBER OF DIE IN PKG', 'PACKAGE TYPE', 'DESIGN ID') \n";

                stringSQL = stringSQL + "WHERE E.attr_ID = 'DRY WEIGHT' \n";
                stringSQL = stringSQL + "AND   E.system_name = 'MAMQA' \n";
                stringSQL = stringSQL + "AND   E.attr_value IS NOT NULL \n";
                stringSQL = stringSQL + "AND   E.attr_value > '0.0' \n";

                stringSQL = stringSQL + "GROUP BY E.MA_ID, \n";
                stringSQL = stringSQL + "E.attr_ID, \n";
                stringSQL = stringSQL + "E.attr_value \n";
                stringSQL = stringSQL + ") \n";

                stringSQL = stringSQL + "SELECT DESIGN_ID, NUMBER_OF_DIE_IN_PKG, PACKAGE_TYPE, LEAD_COUNT, \n";
                stringSQL = stringSQL + "average(DRY_WEIGHT)AS DRY_WEIGHT \n";
                stringSQL = stringSQL + "FROM     ResultSet \n";
                stringSQL = stringSQL + "GROUP BY DESIGN_ID, LEAD_COUNT, NUMBER_OF_DIE_IN_PKG, PACKAGE_TYPE; \n";

                string jsonResult = tds.GetQueryResult(stringSQL);
                tds.Dispose();
                return jsonResult;
            }
            else
                return "{\"Exception\":\"" + tds.connectionErrorMessage + "\"}";
        }




        public string GetNoDiePckg(string DesignID)
        {
            string connectionString;
            connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;";
            connectionString = connectionString + @"Data Source=\\na.micron.com\root\micronweb\MTI\NT\webdata.micron.com\webapps\mfg\QA\packrel\Qual_Data\MST_Dry_Weight.mdb;";
            connectionString = connectionString + "Persist Security Info=True";
            MSAccessDataRetriever mds = new MSAccessDataRetriever(connectionString);
            if (!mds.connectionError)
            {
                string stringSQL;
                stringSQL = "SELECT DISTINCT Die_Count \n";
                stringSQL = stringSQL + "FROM Package_Weight \n";
                stringSQL = stringSQL + "WHERE Design_ID =" + DesignID + " \n";
                stringSQL = stringSQL + "ORDER BY Die_Count";
                string jsonResult = mds.GetQueryResult(stringSQL);
                mds.Dispose();
                return jsonResult;
            }
            else
                return "{\"ConnectionException\":\"" + mds.connectionErrorMessage + "\"}";
        }



        public string GetPackageType(string DesignID, string NoDiePackage)
        {
            string connectionString;
            connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;";
            connectionString = connectionString + @"Data Source=\\na.micron.com\root\micronweb\MTI\NT\webdata.micron.com\webapps\mfg\QA\packrel\Qual_Data\MST_Dry_Weight.mdb;";
            connectionString = connectionString + "Persist Security Info=True";
            MSAccessDataRetriever mds = new MSAccessDataRetriever(connectionString);
            if (!mds.connectionError)
            {
                string stringSQL;
                stringSQL = "SELECT DISTINCT Package_Type \n";
                stringSQL = stringSQL + "FROM Package_Weight \n";
                stringSQL = stringSQL + "WHERE Design_ID =" + DesignID + " \n";
                stringSQL = stringSQL + "AND Die_Count =" + NoDiePackage + " \n";
                stringSQL = stringSQL + "ORDER BY Package_Type";
                string jsonResult = mds.GetQueryResult(stringSQL);
                mds.Dispose();
                return jsonResult;
            }
            else
                return "{\"ConnectionException\":\"" + mds.connectionErrorMessage + "\"}";
        }

        public string GetLeadCount(string DesignID, string NoDiePackage, string PackageType)
        {
            string connectionString;
            connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;";
            connectionString = connectionString + @"Data Source=\\na.micron.com\root\micronweb\MTI\NT\webdata.micron.com\webapps\mfg\QA\packrel\Qual_Data\MST_Dry_Weight.mdb;";
            connectionString = connectionString + "Persist Security Info=True";
            MSAccessDataRetriever mds = new MSAccessDataRetriever(connectionString);
            if (!mds.connectionError)
            {
                string stringSQL;
                stringSQL = "SELECT DISTINCT Ball_Count \n";
                stringSQL = stringSQL + "FROM Package_Weight \n";
                stringSQL = stringSQL + "WHERE Design_ID =" + DesignID + " \n";
                stringSQL = stringSQL + "AND Die_Count =" + NoDiePackage + " \n";
                stringSQL = stringSQL + "AND Package_Type =" + PackageType + " \n";
                stringSQL = stringSQL + "ORDER BY Ball_Count";
                string jsonResult = mds.GetQueryResult(stringSQL);
                mds.Dispose();
                return jsonResult;
            }
            else
                return "{\"ConnectionException\":\"" + mds.connectionErrorMessage + "\"}";
        }


        public string GetDryWeight(string DesignID, string NoDiePackage, string PackageType, string LeadCount)
        {
            LeadCount = LeadCount.Replace("a","/");
            string connectionString;
            connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;";
            connectionString = connectionString + @"Data Source=\\na.micron.com\root\micronweb\MTI\NT\webdata.micron.com\webapps\mfg\QA\packrel\Qual_Data\MST_Dry_Weight.mdb;";
            connectionString = connectionString + "Persist Security Info=True";
            MSAccessDataRetriever mds = new MSAccessDataRetriever(connectionString);
            if (!mds.connectionError)
            {
                string stringSQL;
                stringSQL = "SELECT DISTINCT Design_ID, Die_Count, Package_Type, Ball_Count, MST_Dry_Weight \n";
                stringSQL = stringSQL + "FROM Package_Weight \n";
                stringSQL = stringSQL + "WHERE Design_ID =" + DesignID + " \n";
                stringSQL = stringSQL + "AND Die_Count =" + NoDiePackage + " \n";
                stringSQL = stringSQL + "AND Package_Type =" + PackageType + " \n";
                stringSQL = stringSQL + "AND Ball_Count =" + LeadCount + " \n";
                stringSQL = stringSQL + "ORDER BY Package_Type";
                string jsonResult = mds.GetQueryResult(stringSQL);
                mds.Dispose();
                return jsonResult;
            }
            else
                return "{\"ConnectionException\":\"" + mds.connectionErrorMessage + "\"}";
        }


    }
}
