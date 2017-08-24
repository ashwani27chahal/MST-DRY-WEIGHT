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
                timeout: 600000,
                crossDomain: true,    
                url: 'http://localhost:59932/RestServiceImpl.svc/Expand/' + designID, 
                success: function (response) {
                    $("#loadingIndicator").hide();
                        var jsonData = response.expandedResultResult;
                        if(response.expandedResultResult.length <=2 ){alert('Design ID not found');}
                        console.log(jsonData);
                        $.each(JSON.parse(jsonData), function(k, v) {
                            var resultRow = "<tr class=\"text-center\"><td>" + 
                            v.DESIGN_ID + "</td><td>" +
                            v.MA_ID + "</td><td>" +
                            v.LEAD_COUNT + "</td><td>" + 
                            v.PACKAGE_TYPE + "</td><td>" +
							v.NUMBER_OF_DIE_IN_PKG + "</td><td>" +
				 			v.DRY_WEIGHT+ "</td></tr>";
                            $('#ResultTable tbody').append($(resultRow));
                        });
                    $('#demotable').show();
                    //script for generating filters
                    var filtersConfig = {
                        base_path: 'tablefilter/',
                        col_0: 'none',
                        col_1: 'select',
                        col_2: 'select',
                        col_3: 'select',
                        col_4: 'select',
                        col_5: 'select',
                        alternate_rows: true,
                        rows_counter: false,
                        clear_filter_text: "Select",
                        sort_select: true,
                        linked_filters: true,
                        btn_reset: false
                    };
                    var tf = new TableFilter('ResultTable', filtersConfig);
                    tf.init(); //this is an object of table filter library
   
                    
                },  
                error: function (jqXhr, statusText, errorThrown) { 
                    alert("readyState: " + jqXhr.readyState + "responseText: " + jqXhr.responseText + "StatusCode: " + jqXhr.status + "statusText: " +     jqXhr.statusText);
                    $("#loadingIndicator").hide();
                } 
            });
        }
    };
    
    $("#entryBox").keypress(function() {
        if (event.which === 13) recordDesignID();  //event 13 is for the 'enter' key
    });
    
    $("#submitButton").click(function() {     
        recordDesignID();
    });
    


    //---------------------------------------- Click event for the Reset button to refresh the page ------------------------------------------------
    $("#ClearResults").click(function(){
        location.reload(true);   //refreshes the page and 'true' parameter is to release the cache
    });

    
    
    
    //----------------------------------------------------------------------------------------------------------------------------------------------
    
    
    
});