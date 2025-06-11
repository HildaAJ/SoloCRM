USE [Project]¡@-- your database name
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Customer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Tel] [varchar](20) NULL,
	[State] [nvarchar](10) NULL,
	[Status] [int] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[Email] [varchar](50) NULL,
	[Note] [nvarchar](500) NULL,
	[MetWhere] [nvarchar](50) NULL,
	[CurrentTeamId] [int] NULL,
	[MetWhen] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[UpdatedBy] [nvarchar](50) NOT NULL,
	[UserId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Customer] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Following = 1,Inactive = 2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'COLUMN',@level2name=N'Status'
GO


CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Company] [nvarchar](50) NOT NULL,
	[Type] [nvarchar](20) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[TaxType] [nvarchar](20) NOT NULL,
	[ProductInfomation] [nvarchar](300) NOT NULL,
	[CreatedAt] [datetime] NULL,
	[UpdateDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Product] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO

CREATE TABLE [dbo].[FollowUpRecord](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[FollowUpType] [nvarchar](50) NOT NULL,
	[NextFollowUpDate] [datetime] NULL,
	[Note] [nvarchar](1000) NOT NULL,
	[CreatedAt] [datetime] NULL,
	[UpdateDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[UpdatedBy] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[FollowUpRecord] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO



CREATE TABLE [dbo].[PurchaseRecord](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[Status] [nvarchar](20) NOT NULL,
	[ApplyDate] [date] NOT NULL,
	[ProductId] [int] NOT NULL,
	[ProductName] [nvarchar](50) NOT NULL,
	[SumAssured] [decimal](18, 2) NULL,
	[Fees] [decimal](18, 2) NOT NULL,
	[Years] [int] NULL,
	[CreatedAt] [datetime] NULL,
	[UpdateDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[UpdatedBy] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[PurchaseRecord] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO


CREATE TABLE [dbo].[CancelProductRecord](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[CustomerName] [nvarchar](50) NOT NULL,
	[PurchaseId] [int] NOT NULL,
	[Status] [nvarchar](20) NOT NULL,
	[Reason] [nvarchar](500) NOT NULL,
	[ApplyDate] [date] NOT NULL,
	[CreatedAt] [datetime] NULL,
	[UpdateDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[UpdatedBy] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CancelProductRecord] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO


CREATE TABLE [dbo].[Team](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[LicensingStatus] [nvarchar](50) NULL,
	[Code] [varchar](10) NOT NULL,
	[State] [nvarchar](10) NULL,
	[BusinessEmail] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[DOB] [varchar](10) NULL,
	[Address] [nvarchar](100) NULL,
	[Status] [nvarchar](10) NOT NULL,
	[CreatedAt] [datetime] NULL,
	[UpdateDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[UpdatedBy] [nvarchar](50) NOT NULL,
	[UserId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Team] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO

CREATE TABLE [dbo].[AppUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Account] [nvarchar](50) NOT NULL,
	[PasswordHash] [nvarchar](256) NOT NULL,
	[FullName] [nvarchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedAt] [datetime] NULL,
	[AgentCode] [varchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AppUser] ADD  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[AppUser] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO