create database [SqlWorkshop]
GO

USE [SqlWorkshop]
GO

CREATE TABLE [dbo].[Emails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	CONSTRAINT [PK_Emails] PRIMARY KEY CLUSTERED ([Id] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Locations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Street] [nvarchar](85) NULL,
	[City] [nvarchar](85) NOT NULL,
	[State] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NOT NULL,
	[Postcode] [nvarchar](15) NULL,
	[Coordinates] [nvarchar](50) NULL,
	[Timezone] [nvarchar](50) NULL,
	CONSTRAINT [PK_Locations] PRIMARY KEY CLUSTERED ([Id] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Logins](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Uuid] [nchar](100) NOT NULL,
	[UserID] [int] NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](30) NOT NULL,
	[Salt] [nvarchar](50) NULL,
	[Md5] [nvarchar](32) NULL,
	[Sha1] [nvarchar](40) NULL,
	[Sha256] [nvarchar](64) NULL,
	[Registerdate] [datetime] NOT NULL,
	[Registered_duration] [int] NOT NULL,
	CONSTRAINT [PK_Logins] PRIMARY KEY CLUSTERED ([ID] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Phones](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[Phone] [nvarchar](50) NOT NULL,
	[Cell] [nvarchar](50) NOT NULL,
	CONSTRAINT [PK_Phones] PRIMARY KEY CLUSTERED ([ID] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Pictures](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Large] [nvarchar](255) NULL,
	[Medium] [nvarchar](255) NULL,
	[Thumbnail] [nvarchar](255) NULL,
	CONSTRAINT [PK_Pictures] PRIMARY KEY CLUSTERED ([Id] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Firstname] [nvarchar](50) NOT NULL,
	[Lastname] [nvarchar](50) NOT NULL,
	[BirthDate] [datetime] NOT NULL,
	[Age] [int] NOT NULL,
	[Nationality] [nvarchar](50) NOT NULL,
	[Gender] [nvarchar](50) NOT NULL,
	[Salary] [money] NOT NULL,
	[CreditCard] [nvarchar](19) NOT NULL,
	CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Emails] ADD CONSTRAINT [FK_Emails_Users] FOREIGN KEY([UserId]) REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[Locations] ADD CONSTRAINT [FK_Locations_Users] FOREIGN KEY([UserId]) REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[Logins] ADD CONSTRAINT [FK_Logins_Users] FOREIGN KEY([UserID]) REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[Phones] ADD CONSTRAINT [FK_Phones_Users] FOREIGN KEY([UserID]) REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[Pictures] ADD CONSTRAINT [FK_Pictures_Users] FOREIGN KEY([UserId]) REFERENCES [dbo].[Users] ([Id])
GO
