CREATE TABLE [dbo].[HCPLANPREMIUMS](
	[PlanName] [varchar](20) NOT NULL,
	[Section1] [numeric](10, 2) NULL,
	[FreeAddOnsToSection1] [bit] NULL,
	[Section2] [bit] NULL,
	[Section3] [bit] NULL,
	[TotalSumInsured] [numeric](10, 2) NULL,
	[Premium1YearAmount] [numeric](7, 2) NULL,
	[Premium1YearAmountGST] [numeric](6, 2) NULL,
	[Premium1YearAmountPerDay] [numeric](6, 2) NULL,
	[Premium5YearAmount] [numeric](7, 2) NULL,
	[Premium5YearAmountGST] [numeric](6, 2) NULL,
	[Premium5YearAmountPerDay] [numeric](6, 2) NULL
 CONSTRAINT [PK_HCPLANPREMIUMS] PRIMARY KEY CLUSTERED 
(
	[PlanName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO