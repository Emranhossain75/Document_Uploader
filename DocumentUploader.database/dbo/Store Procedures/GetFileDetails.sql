CREATE Procedure [dbo].[GetFileDetails]  
(  
@Id int=null 
)  
as  
begin  
  
select Id,FileName,FileContent,RelatedTo,RelatedRecordId,Note,CreatedBy,CreateDate from FileDetails  
where Id=isnull(@Id,Id) 
 
End 
 