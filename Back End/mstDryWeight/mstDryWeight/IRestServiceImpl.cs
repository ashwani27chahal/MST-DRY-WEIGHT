using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


namespace mstDryWeight
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRestServiceImpl" in both code and config file together.
    [ServiceContract]
    public interface IRestServiceImpl
    {

        [OperationContract]
        [WebInvoke(
        Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Wrapped,
        UriTemplate = "Expand/{id}"
        )]
        string expandedResult(string id);

        [OperationContract]
        [WebInvoke(
         Method = "GET",
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Wrapped,
         UriTemplate = "Teradata/{id}"
         )]
        string fetchResults(string id);



        [OperationContract]
        [WebInvoke(
                 Method = "GET",
                 ResponseFormat = WebMessageFormat.Json,
                 BodyStyle = WebMessageBodyStyle.Wrapped,
                 UriTemplate = "designID/{id}"
                 )]
        string GetNoDiePckg(string id);

        [OperationContract]
        [WebInvoke(
                 Method = "GET",
                 ResponseFormat = WebMessageFormat.Json,
                 BodyStyle = WebMessageBodyStyle.Wrapped,
                 UriTemplate = "noDiePckg/{id1}/{id2}"
                 )]
        string GetPackageType(string id1, string id2);


        [OperationContract]
        [WebInvoke(
         Method = "GET",
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Wrapped,
         UriTemplate = "packageType/{id1}/{id2}/{id3}"
         )]
        string GetLeadCount(string id1, string id2, string id3);


        [OperationContract]
        [WebInvoke(
         Method = "GET",
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Wrapped,
         UriTemplate = "leadCount/{id1}/{id2}/{id3}/{id4}"
         )]
        string GetDryWeight(string id1, string id2, string id3, string id4);



    }
}
