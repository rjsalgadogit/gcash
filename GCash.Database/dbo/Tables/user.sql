CREATE TABLE [dbo].[user] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [User]        NVARCHAR (128) NULL,
    [CreatedDate] DATETIME       NULL,
    [CreatedBy]   NVARCHAR (128) NULL
);

