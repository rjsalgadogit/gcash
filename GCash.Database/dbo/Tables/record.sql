CREATE TABLE [dbo].[record] (
    [Id]              INT             IDENTITY (1, 1) NOT NULL,
    [ReferenceNumber] NVARCHAR (50)   NULL,
    [Amount]          DECIMAL (18, 2) NULL,
    [IsClaimed]       BIT             NULL,
    [CreatedDate]     DATETIME        NULL,
    [CreatedBy]       INT             NULL,
    [ModifiedDate]    DATETIME        NULL,
    [ModifiedBy]      INT             NULL
);

