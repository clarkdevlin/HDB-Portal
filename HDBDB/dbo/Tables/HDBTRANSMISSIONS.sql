CREATE TABLE [dbo].[HDBTRANSMISSIONS] (
    [TransmissionType]         VARCHAR (10)   NOT NULL,
	[HeaderIdentifier]       VARCHAR (2)    NULL,
	[ProcessDate]            DATE           NULL,
	[HDBReferenceNo]         VARCHAR (11)   NOT NULL,
    [InsuranceEffectiveDate] DATE           NULL,
    [InsuranceTerm]          INT            NULL,
    [BranchCode]             VARCHAR (8)    NULL,
    [AddressBlk]             VARCHAR (5)    NULL,
    [AddressLevel]           VARCHAR (2)    NULL,
    [AddressUnit]            VARCHAR (7)    NULL,
    [AddressStreetName]      VARCHAR (65)   NULL,
    [AddressPostalCode]      VARCHAR (6)    NULL,
    [FlatTypeClassification] VARCHAR (10)   NULL,
    [CompulsoryStatus]       VARCHAR (1)    NULL,
    [TransactionType]        VARCHAR (1)    NULL,
    [PremiumAmount]          NUMERIC (7, 2) NULL,
    [PremiumAmountGST]       NUMERIC (6, 2) NULL,
    [SendDateToInsurer]      DATE           NULL,
    [NameOfInsurer]          VARCHAR (66)   NULL,
    [ReturnDateToHDB]        DATE           NULL, 
    [TrailerIndentifier] VARCHAR(2) NULL, 
    [TrailerNoOfCases] INT NULL, 
    [TrailerProcessDate] DATE NULL, 
    [TrailerInsurerName] NVARCHAR(66) NULL
);


GO
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20190521-172959]
    ON [dbo].[HDBTRANSMISSIONS]([InsuranceEffectiveDate] ASC, [AddressLevel] ASC, [AddressUnit] ASC, [AddressPostalCode] ASC, [CompulsoryStatus] ASC, [TransactionType] ASC);

