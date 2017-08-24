
WITH ResultSet AS (
	SELECT E.MA_ID, 
	       max(case when attr_values.attr_ID = 'DESIGN ID' then attr_values.attr_value else '' end ) as DESIGN_ID,
	       max(case when attr_values.attr_ID = 'LEAD COUNT' then attr_values.attr_value else '' end ) as LEAD_COUNT,
		   max(case when attr_values.attr_ID = 'NUMBER OF DIE IN PKG' then attr_values.attr_value else '' end ) as NUMBER_OF_DIE_IN_PKG,
	       max(case when attr_values.attr_ID = 'PACKAGE TYPE' then attr_values.attr_value else '' end ) as PACKAGE_TYPE,
	       E.attr_value as DRY_WEIGHT
	
	FROM       WW_BE_DM.ENG_DATA_RECORDED E
	INNER JOIN WW_BE_DM.MA_attr     
	ON         WW_BE_DM.MA_attr.MA_ID       = E.MA_ID 
	AND        WW_BE_DM.MA_attr.system_name = E.system_name 
	AND        WW_BE_DM.MA_attr.attr_ID     = 'DESIGN ID' 
	AND        WW_BE_DM.MA_attr.attr_value  = 'V80A'
	        
	LEFT OUTER JOIN WW_BE_DM.MA_attr attr_values 
	ON              attr_values.MA_ID       = E.MA_ID 
	AND             attr_values.system_name = E.system_name 
	AND             attr_values.attr_ID    IN ('LEAD COUNT', 'NUMBER OF DIE IN PKG', 'PACKAGE TYPE', 'DESIGN ID')
	
	WHERE E.attr_ID     = 'DRY WEIGHT' 
	AND   E.system_name = 'MAMQA' 
	AND   E.attr_value IS NOT NULL 
	AND   E.attr_value > '0.0'

	GROUP BY E.MA_ID,
	         E.attr_ID,
	         E.attr_value
)

SELECT   DESIGN_ID , NUMBER_OF_DIE_IN_PKG, PACKAGE_TYPE, LEAD_COUNT, 
         average(DRY_WEIGHT) AS DRY_WEIGHT
FROM     ResultSet 
GROUP BY DESIGN_ID , LEAD_COUNT, NUMBER_OF_DIE_IN_PKG, PACKAGE_TYPE;


-- *********************************************************** SHOWING ALL DETAILS *********************************************************


	SELECT E.MA_ID, 
	       max(case when attr_values.attr_ID = 'DESIGN ID' then attr_values.attr_value else '' end ) as DESIGN_ID,
	       max(case when attr_values.attr_ID = 'LEAD COUNT' then attr_values.attr_value else '' end ) as LEAD_COUNT,
		   max(case when attr_values.attr_ID = 'NUMBER OF DIE IN PKG' then attr_values.attr_value else '' end ) as NUMBER_OF_DIE_IN_PKG,
	       max(case when attr_values.attr_ID = 'PACKAGE TYPE' then attr_values.attr_value else '' end ) as PACKAGE_TYPE,
	       E.attr_value as DRY_WEIGHT
	
	FROM       WW_BE_DM.ENG_DATA_RECORDED E
	INNER JOIN WW_BE_DM.MA_attr     
	ON         WW_BE_DM.MA_attr.MA_ID       = E.MA_ID 
	AND        WW_BE_DM.MA_attr.system_name = E.system_name 
	AND        WW_BE_DM.MA_attr.attr_ID     = 'DESIGN ID' 
	AND        WW_BE_DM.MA_attr.attr_value  = 'V80A'
	        
	LEFT OUTER JOIN WW_BE_DM.MA_attr attr_values 
	ON              attr_values.MA_ID       = E.MA_ID 
	AND             attr_values.system_name = E.system_name 
	AND             attr_values.attr_ID    IN ('LEAD COUNT', 'NUMBER OF DIE IN PKG', 'PACKAGE TYPE', 'DESIGN ID')
	
	WHERE E.attr_ID     = 'DRY WEIGHT' 
	AND   E.system_name = 'MAMQA' 
	AND   E.attr_value IS NOT NULL 
	AND   E.attr_value > '0.0'

	GROUP BY E.MA_ID,
	         E.attr_ID,
	         E.attr_value;
	         
	         
	         