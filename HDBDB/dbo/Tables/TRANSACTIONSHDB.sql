CREATE TABLE [dbo].[TRANSACTIONSHDB](
	[TransactionNo] [varchar](15) NOT NULL,
	[ReceiptNo] [varchar](15) NULL,
	[TransactionType] [varchar](5) NULL,
	[HDBReferenceNo] [varchar](11) NOT NULL,
	[PolicyNo] [varchar](20) NULL,
	[CertificateNo] [varchar](20) NULL,
	[InsuranceEffectiveDate] [date] NULL,
	[InsuranceTerm] [int] NULL,
	[SumInsured] [numeric](10, 2) NULL,
	[PremiumAmount] [numeric](7, 2) NULL,
	[PremiumAmountGST] [numeric](6, 2) NULL,
	[PremiumDiscount] [numeric](6, 2) NULL,
	[TotalPremiumAmount] [numeric](7, 2) NULL,
	[PaymentMethod] [varchar](20) NULL,
	[PaymentChequeNo] [varchar](30) NULL,
	[PaymentCreditCardNo] [varchar](30) NULL,
	[PaymentReferenceNo] [varchar](30) NULL,
	[PaymentAmount] [numeric](7, 2) NULL,
	[EntryUserId] [uniqueidentifier] NULL,
	[EntryDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[TransactionNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO