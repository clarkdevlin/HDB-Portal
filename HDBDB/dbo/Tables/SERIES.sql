CREATE TABLE [dbo].[SERIES]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Module] VARCHAR(30) NULL, 
    [Prefix] VARCHAR(5) NULL, 
    [CurrentYear] INT NULL,
	[CurrentNo] INT NULL
)
