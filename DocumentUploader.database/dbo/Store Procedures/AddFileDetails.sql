CREATE Procedure [dbo].[AddFileDetails]  
(  
@FileName varchar(60),  
@FileContent varBinary(Max),
@RelatedTo varchar(255),
@RelatedRecordId int,
@Note varchar(255),
@CreatedBy varchar(255),
@CreateDate datetime
)  
as  
begin  

Set NoCount off
  
DECLARE @CurrentBDTime DateTime
	SET @CurrentBDTime = CONVERT(datetime,SWITCHOFFSET(CONVERT(datetimeoffset,GetUTCDate()),'+06:00'))
	
	begin

Insert into FileDetails values(@FileName,@FileContent,@RelatedTo,@RelatedRecordId,@Note,@CreatedBy,@CurrentBDTime)  
  
    end
End 