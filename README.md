# Introduction 
Sample .NET Framework project using ASP.NET WebForms, Autofac IOC Container, MSTest and YAML Pipeline...

# Getting Started

1.	Setup database
	- Download [SQL Server 2022 Express](https://www.microsoft.com/en-us/download/details.aspx?id=104781)  
	- Login the SQL Server Instance and create a database name **Northwind**
	- Run scripts below to to setup the **BankAccount** table
	~~~~sql
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
	~~~~
2.	Install [Visual Studio Community Edition](https://visualstudio.microsoft.com/vs/community/) and create a new Solution

3.	Create Repository project  
  
  Install Nuget package:  
  - EntityFramework  

  Create the following files:   
  - ADO.NET Entity Data Model: **EntityDataModel.edmx**
  - Interface file: **IBankAccountRepository.cs**
  - Implementation file: **BankAccountRepository.cs**  
	- **Note**: Implementation class has constructor *"depends"* on Entities Data Model **class**.

  Note for App.Config file:
  - Has connection string point to the SQL Server database


4.	Create a Service project

	Create the following files:
	- Interface file: **IBankAccountService.cs**
	- Implementation file: **BankAccountService.cs**
		- Note: Implementation class has constructor *"depends"* on Repository **interface**.

5.  Create WebForms project
	
	Install Nuget packages:
	- Autofac 6.4.0 **(IMPORTANT: don't install the latest version)**
	- Autofac.Web 6.1.0
	- EntityFramework

	Add reference to Repository and Service projects.


	Update web.config to enable Property Injection  [Autofac for WebForms](https://docs.autofac.org/en/stable/integration/webforms.html)

6. Create MSTest.Test project

	Install Nuget packages:
	- 

# Downloads:
1. [Visual Studio Community Edition](https://visualstudio.microsoft.com/vs/community/)
2. [MSTest Cheat Sheet](https://www.automatetheplanet.com/mstest-cheat-sheet/)
3. [SQL Server 2022 Express](https://www.microsoft.com/en-us/download/details.aspx?id=104781)
