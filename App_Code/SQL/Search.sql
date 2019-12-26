WITH SearchData AS
(
/*--order by 請在程式覆蓋 ORDER BY ModulePublish.initDate --*/
select  ROW_NUMBER() OVER(ORDER BY id desc) AS RowNumber
,id,sent,name,joingroup,batch,initdate,status,email,tel,pdf,introduction,why,essay,gonsakon_score1,gonsakon_score2,gonsakon_score3,gonsakon_love,justin_score1,justin_score2,justin_score3,justin_love,(gonsakon_memo+'<br/>'+justin_memo)AS memo,followup
/*--new colums--*/ 
,(gonsakon_score1*2+gonsakon_score2*5+gonsakon_score3*2+justin_score1*2+justin_score2*5+justin_score3*2)AS total
from registform where 1=1

/*--where begin --*/

/*--where End--*/
)

select * from SearchData WHERE RowNumber >=@start  and RowNumber <=@end
