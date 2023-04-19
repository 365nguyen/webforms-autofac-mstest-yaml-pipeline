CREATE TABLE [dbo].[BankAccount](
	[AccountId] [int] IDENTITY(1,1) NOT NULL,
	[AccountNumber] [nchar](10) NOT NULL,
	[Balance] [decimal](18, 2) NOT NULL,
	[Frozen] [tinyint] NOT NULL,
 CONSTRAINT [PK_BankAccount] PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[BankAccount] ADD  CONSTRAINT [DF_BankAccount_Frozen]  DEFAULT ((0)) FOR [Frozen]
GO

INSERT INTO BankAccount(AccountNumber, Balance) VALUES('01-12344',10.00);
GO
SELECT * FROM BankAccount
