CREATE TABLE [dbo].[TRANSACTIONSHC](
	[TransactionNo] [varchar](15) NOT NULL,
	[ReceiptNo] [varchar](15) NULL,
	[HDBReferenceNo] [varchar](11) NULL,
	[PolicyNo] [varchar](20) NULL,
	[AddressBlk] [varchar](5) NOT NULL,
	[Addresslevel] [varchar](2) NULL,
	[AddressUnit] [varchar](7) NULL,
	[AddressStreetName] [varchar](65) NULL,
	[AddressPostalCode] [varchar](6) NULL,
	[PlanName] [varchar](10) NULL,
	[Section1SumInsured] [numeric](7, 2) NULL,
	[Section2SumInsured] [numeric](7, 2) NULL,
	[Section3SumInsured] [numeric](7, 2) NULL,
	[TotalSumInsured] [numeric](7, 2) NULL,
	[PremiumAmount] [numeric](7, 2) NULL,
	[PremiumAmountGST] [numeric](5, 2) NULL,
	[PremiumDiscountAmount] [numeric](7, 2) NULL,
	[TotalPremiumAmount] [numeric](7, 2) NULL,
	[PromoCode] [varchar](30) NULL,
	[InsuranceEffectiveDate] [date] NULL,
	[InusranceTerm] [int] NULL,
	[PaymentMethod] [varchar](20) NULL,
	[PaymentChequeNo] [varchar](30) NULL,
	[PaymentCreditCardNo] [varchar](30) NULL,
	[PaymentReferenceNo] [varchar](30) NULL,
	[PaymentAmount] [numeric](7, 2) NULL,
	[EntryUserId] [uniqueidentifier] NULL,
	[EntryDate] [datetime] NULL,
 CONSTRAINT [PK__TRANSACT__554342D8258AC22D] PRIMARY KEY CLUSTERED 
(
	[TransactionNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
