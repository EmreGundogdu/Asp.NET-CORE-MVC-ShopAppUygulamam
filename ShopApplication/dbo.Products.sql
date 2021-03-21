CREATE TABLE [dbo].[Products] (
    [ProductId]   INT           IDENTITY (1, 1) NOT NULL,
    [name]        NVARCHAR (50) NOT NULL,
    [price]       INT           NOT NULL,
    [Description] NVARCHAR (50) NOT NULL,
    [ImageUrl]    NCHAR (10)    NOT NULL,
    [IsApproved]  BIT           NOT NULL,
    [Url] NVARCHAR(50) NOT NULL, 
    PRIMARY KEY CLUSTERED ([ProductId] ASC)
);

