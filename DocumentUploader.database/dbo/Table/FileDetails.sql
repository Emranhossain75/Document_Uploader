CREATE TABLE [dbo].[FileDetails](  
    [Id] [int] IDENTITY(1,1) NOT NULL,  
    [FileName] [varchar](60) NULL,  
    [FileContent] [varbinary](max) NULL ,
    [RelatedTo] VARCHAR(60) NULL,
    RelatedRecordId INT NULL,
    Note VARCHAR(60) NULL,
    CreatedBy VARCHAR(60) NULL,
    CreateDate DATETIME
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] 
