$(document).ready(function () {
    'use strict';
    
  
    
    
    
//-------------------------------------------1. Data from the input box is being collected here--------------------------------------------------- 
    var recordDesignID = function() {
        var designID = $("#entryBox").val()
       
        if (designID.length === 0){
            alert ("no Design ID entered.");
        }
            
        else {
            $("#loadingIndicator").show();
            designID = designID.trim();
            designID = "'" + designID + "'"; 
            console.log(designID);
            $.ajax({ 
                type: 'GET',
                cache: false,
                timeout: 119000,
                crossDomain: true,    
                url: 'http://localhost:59932/RestServiceImpl.svc/designID/' + designID, 
                success: function (response) {
                    $("#loadingIndicator").hide();
                        var jsonData = response.GetNoDiePckgResult;
                        console.log(jsonData);
                        $.each(JSON.parse(jsonData), function(k, v) {
                            $('#DieInPackage').append($("<option></option>").attr("value",k).text(v.Die_Count));
                            $('#DieInPackage').css("border","solid 2px yellowgreen");
                            $('#DieInPackage').attr('size',6);   //changing the size attribute will change the dropdown to a listbox (hence auto expand)
                        });
                },  
                error: function (jqXhr, statusText, errorThrown) { 
                    alert("readyState: " + jqXhr.readyState + "responseText: " + jqXhr.responseText + "StatusCode: " + jqXhr.status + "statusText: " +     jqXhr.statusText);
                    $("#loadingIndicator").hide();
                } 
            });
        }
    };
    
    $("#entryBox").keypress(function() {
        if (event.which === 13) recordDesignID();
    });
    
    $("#submitButton").click(function() {     
        recordDesignID();
    });
    
//----------------------------------------2. Once No Die in PKG selection is made-----------------------------------------------------------
      
        $("#DieInPackage").change(function() {     
            
            $('#DieInPackage').attr('size',1);
            $("#loadingIndicator").show();
            var designID = $("#entryBox").val()
            designID = designID.trim();
            designID = "'" + designID + "'"; 
            var NoDieInPackage = $('#DieInPackage :selected').text();
            NoDieInPackage = "'" + NoDieInPackage + "'"; 
            $.ajax({ 
                type: 'GET',
                cache: false,
                timeout: 119000,
                crossDomain: true,    
                url: 'http://localhost:59932/RestServiceImpl.svc/noDiePckg/' + designID + '/' + NoDieInPackage,
                success: function (response) {
                    $("#loadingIndicator").hide();
                        var jsonData = response.GetPackageTypeResult;
                        console.log(jsonData);
                        $.each(JSON.parse(jsonData), function(k, v) {
                            $('#PackageType').append($("<option></option>").attr("value",k).text(v.Package_Type));
                            $('#PackageType').css("border","solid 2px yellowgreen");
                            $('#PackageType').attr('size',6); 
                        });
                },  
                error: function (jqXhr, statusText, errorThrown) { 
                    alert("readyState: " + jqXhr.readyState + "responseText: " + jqXhr.responseText + "StatusCode: " + jqXhr.status + "statusText: " +     jqXhr.statusText);
                    $("#loadingIndicator").hide();
                } 
            });
    });

                                       

//----------------------------------------3. Once Package_Type Selection is made-----------------------------------------------------------
      
        $("#PackageType").change(function() {     
       
            $('#PackageType').attr('size',1); 
            $("#loadingIndicator").show();
            var designID = $("#entryBox").val()
            designID = designID.trim();
            designID = "'" + designID + "'"; 
            var NoDieInPackage = $('#DieInPackage :selected').text();
            NoDieInPackage = "'" + NoDieInPackage + "'"; 
            var PackageType = $('#PackageType :selected').text();
            PackageType = "'" + PackageType + "'"; 
            $.ajax({ 
                type: 'GET',
                cache: false,
                timeout: 119000,
                crossDomain: true,    
                url: 'http://localhost:59932/RestServiceImpl.svc/packageType/' + designID + '/' + NoDieInPackage + '/' + PackageType,
                success: function (response) {
                    $("#loadingIndicator").hide();
                        var jsonData = response.GetLeadCountResult;
                        console.log(jsonData);
                        $.each(JSON.parse(jsonData), function(k, v) {
                            $('#LeadCount').append($("<option></option>").attr("value",k).text(v.Ball_Count));
                            $('#LeadCount').css("border","solid 2px yellowgreen");
                            $('#LeadCount').attr('size',6); 
                        });
                },  
                error: function (jqXhr, statusText, errorThrown) { 
                    alert("readyState: " + jqXhr.readyState + "responseText: " + jqXhr.responseText + "StatusCode: " + jqXhr.status + "statusText: " +     jqXhr.statusText);
                    $("#loadingIndicator").hide();
                } 
            });
    });

    

    //----------------------------------------4. Once Lead Count Selection is made, find MST DRY WEIGHT --------------------------------------------------
      
        $("#LeadCount").change(function() {     
            
            $('#LeadCount').attr('size', 1); 
            $("#loadingIndicator").show();
            var count = 0;
            var DryWeightVal = 0.0; 
            var averageVal = 0.0;
            var designID = $("#entryBox").val()
            designID = designID.trim();
            designID = "'" + designID + "'"; 
            var NoDieInPackage = $('#DieInPackage :selected').text();
            NoDieInPackage = "'" + NoDieInPackage + "'"; 
            var PackageType = $('#PackageType :selected').text();
            PackageType = "'" + PackageType + "'"; 
            var LeadCount = $('#LeadCount :selected').text();
            LeadCount = LeadCount.replace("/", "a");  //In case the value of Lead count is in fraction eg. 96/110
            LeadCount = "'" + LeadCount + "'";
            

            $.ajax({ 
                type: 'GET',
                cache: false,
                timeout: 119000,
                crossDomain: true,    
                url: 'http://localhost:59932/RestServiceImpl.svc/leadCount/' + designID + '/' + NoDieInPackage + '/' + PackageType + '/' + LeadCount,
                success: function (response) {
                    $("#loadingIndicator").hide();
                        var jsonData = response.GetDryWeightResult; 
                        console.log(jsonData);
                        $.each(JSON.parse(jsonData), function(k, v) {
                            var resultRow = "<tr class=\"text-center\"><td>" + 
							v.Design_ID + "</td><td>" +
							v.Die_Count + "</td><td>" + 
							v.Package_Type + "</td><td>" + 
							v.Ball_Count + "</td><td>" + 
				 			v.MST_Dry_Weight + "</td></tr>";
                            $('#ResultTable tbody').append($(resultRow));
                            $('#demotable').show();
                            count = count + 1;
                            console.log("count value " + count);
                            DryWeightVal = DryWeightVal + parseFloat(v.MST_Dry_Weight);
                            console.log("dry weight val " + DryWeightVal);
                        });   
                },  
                error: function (jqXhr, statusText, errorThrown) { 
                    alert("readyState: " + jqXhr.readyState + "responseText: " + jqXhr.responseText + "StatusCode: " + jqXhr.status + "statusText: " +     jqXhr.statusText);
                    $("#loadingIndicator").hide();
                } 
            });
            
            averageVal = DryWeightVal/count;
            $("#resultBox").val('answer');
            console.log("dry weight val " + DryWeightVal);

    });

    


    //---------------------------------------- Click event for the Reset button to refresh the page ------------------------------------------------
    $("#ClearResults").click(function(){
        location.reload(true);   //refreshes the page and 'true' parameter is to release the cache
    });

    
    
    
    //----------------------------------------------------------------------------------------------------------------------------------------------
    
    
    
});