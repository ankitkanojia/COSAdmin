/****** Object:  Database [COSAdmin]    Script Date: 21-Oct-19 7:56:03 PM ******/
CREATE DATABASE [COSAdmin] ON  PRIMARY 
( NAME = N'COSAdmin', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\COSAdmin.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'COSAdmin_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\COSAdmin_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [COSAdmin].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [COSAdmin] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [COSAdmin] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [COSAdmin] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [COSAdmin] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [COSAdmin] SET ARITHABORT OFF 
GO
ALTER DATABASE [COSAdmin] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [COSAdmin] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [COSAdmin] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [COSAdmin] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [COSAdmin] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [COSAdmin] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [COSAdmin] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [COSAdmin] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [COSAdmin] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [COSAdmin] SET  DISABLE_BROKER 
GO
ALTER DATABASE [COSAdmin] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [COSAdmin] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [COSAdmin] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [COSAdmin] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [COSAdmin] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [COSAdmin] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [COSAdmin] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [COSAdmin] SET RECOVERY FULL 
GO
ALTER DATABASE [COSAdmin] SET  MULTI_USER 
GO
ALTER DATABASE [COSAdmin] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [COSAdmin] SET DB_CHAINING OFF 
GO
EXEC sys.sp_db_vardecimal_storage_format N'COSAdmin', N'ON'
GO
/****** Object:  Table [dbo].[CityMasters]    Script Date: 21-Oct-19 7:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CityMasters](
	[CityMasterID] [bigint] IDENTITY(1,1) NOT NULL,
	[CityName] [nvarchar](50) NULL,
	[StateMasterID] [bigint] NOT NULL,
 CONSTRAINT [PK_CityMasters] PRIMARY KEY CLUSTERED 
(
	[CityMasterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ClubMasters]    Script Date: 21-Oct-19 7:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClubMasters](
	[ClubMasterID] [bigint] IDENTITY(1,1) NOT NULL,
	[ClubName] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
	[ClubImage] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_ClubMasters] PRIMARY KEY CLUSTERED 
(
	[ClubMasterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ClubServices]    Script Date: 21-Oct-19 7:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClubServices](
	[ClubServiceID] [bigint] IDENTITY(1,1) NOT NULL,
	[ServiceName] [nvarchar](50) NOT NULL,
	[ServiceCategoryID] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
	[IsPaid] [bit] NOT NULL,
	[ClubMasterID] [bigint] NULL,
 CONSTRAINT [PK_ClubServices] PRIMARY KEY CLUSTERED 
(
	[ClubServiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CoachMasters]    Script Date: 21-Oct-19 7:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CoachMasters](
	[CouchMasterID] [bigint] IDENTITY(1,1) NOT NULL,
	[TraineeCharge] [decimal](9, 2) NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[CityMasterID] [bigint] NOT NULL,
	[ClubMasterID] [bigint] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDtae] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
	[Address] [nvarchar](50) NULL,
	[PinCode] [nvarchar](6) NULL,
	[Email] [nvarchar](50) NULL,
	[Mobile] [nvarchar](10) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[CoachCertificate] [nvarchar](max) NULL,
	[Experience] [decimal](5, 2) NULL,
 CONSTRAINT [PK_CoachMasters] PRIMARY KEY CLUSTERED 
(
	[CouchMasterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CoachSchedules]    Script Date: 21-Oct-19 7:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CoachSchedules](
	[CoachScheduleID] [bigint] IDENTITY(1,1) NOT NULL,
	[CoachMasterID] [bigint] NOT NULL,
	[StartTime] [time](7) NOT NULL,
	[EndTime] [time](7) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_CoachSchedules] PRIMARY KEY CLUSTERED 
(
	[CoachScheduleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CoachServices]    Script Date: 21-Oct-19 7:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CoachServices](
	[CoachServiceID] [bigint] NOT NULL,
	[CoachService1] [nvarchar](50) NOT NULL,
	[CoachMasterID] [bigint] NOT NULL,
	[ServiceCategoryID] [bigint] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_CoachServices] PRIMARY KEY CLUSTERED 
(
	[CoachServiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Enquiries]    Script Date: 21-Oct-19 7:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Enquiries](
	[EnquiryId] [bigint] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[IsResponce] [bit] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Enquiries] PRIMARY KEY CLUSTERED 
(
	[EnquiryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Feedbacks]    Script Date: 21-Oct-19 7:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedbacks](
	[FeedbackID] [bigint] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[ProductID] [bigint] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Feedbacks] PRIMARY KEY CLUSTERED 
(
	[FeedbackID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GallaryMasters]    Script Date: 21-Oct-19 7:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GallaryMasters](
	[GallaryMasterID] [bigint] IDENTITY(1,1) NOT NULL,
	[Image] [nvarchar](50) NOT NULL,
	[ClubServiceID] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_GallaryMasters] PRIMARY KEY CLUSTERED 
(
	[GallaryMasterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OTPs]    Script Date: 21-Oct-19 7:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OTPs](
	[OTPID] [bigint] IDENTITY(1,1) NOT NULL,
	[UniqueCode] [nvarchar](50) NOT NULL,
	[OTPCode] [nvarchar](6) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_OTPs] PRIMARY KEY CLUSTERED 
(
	[OTPID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PaymentTypes]    Script Date: 21-Oct-19 7:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentTypes](
	[PaymentTypeID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_PaymentTypes] PRIMARY KEY CLUSTERED 
(
	[PaymentTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Roles]    Script Date: 21-Oct-19 7:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleID] [bigint] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ServiceCategories]    Script Date: 21-Oct-19 7:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceCategories](
	[ServiceCategoryID] [bigint] IDENTITY(1,1) NOT NULL,
	[CategoryType] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_ServiceCategories] PRIMARY KEY CLUSTERED 
(
	[ServiceCategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StateMasters]    Script Date: 21-Oct-19 7:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StateMasters](
	[StateMasterID] [bigint] IDENTITY(1,1) NOT NULL,
	[StateName] [nvarchar](50) NULL,
 CONSTRAINT [PK_StateMasters] PRIMARY KEY CLUSTERED 
(
	[StateMasterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserMasters]    Script Date: 21-Oct-19 7:56:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserMasters](
	[UserMasterID] [bigint] IDENTITY(1,1) NOT NULL,
	[Mobile] [nvarchar](10) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[DOB] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[Profile] [nvarchar](max) NULL,
	[Address] [nvarchar](500) NULL,
	[Email] [nvarchar](50) NULL,
	[RoleID] [bigint] NOT NULL,
	[IsPaid] [bit] NOT NULL,
 CONSTRAINT [PK_UserMasters] PRIMARY KEY CLUSTERED 
(
	[UserMasterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

GO
INSERT [dbo].[Roles] ([RoleID], [RoleName]) VALUES (1, N'Admin')
GO
INSERT [dbo].[Roles] ([RoleID], [RoleName]) VALUES (2, N'Coach')
GO
INSERT [dbo].[Roles] ([RoleID], [RoleName]) VALUES (3, N'User')
GO
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[UserMasters] ON 

GO
INSERT [dbo].[UserMasters] ([UserMasterID], [Mobile], [Name], [Password], [DOB], [IsActive], [CreatedDate], [UpdatedDate], [Profile], [Address], [Email], [RoleID], [IsPaid]) VALUES (1, N'9904415123', N'Suchit', N'123456', CAST(N'1994-04-16 00:00:00.000' AS DateTime), 1, CAST(N'2019-09-09 00:00:00.000' AS DateTime), NULL, N'123', N'123', N'khuntsr@gmail.com', 1, 0)
GO
SET IDENTITY_INSERT [dbo].[UserMasters] OFF
GO
ALTER TABLE [dbo].[CityMasters]  WITH CHECK ADD  CONSTRAINT [FK_StateMaster] FOREIGN KEY([CityMasterID])
REFERENCES [dbo].[StateMasters] ([StateMasterID])
GO
ALTER TABLE [dbo].[CityMasters] CHECK CONSTRAINT [FK_StateMaster]
GO
ALTER TABLE [dbo].[CoachServices]  WITH CHECK ADD  CONSTRAINT [FK_CoachService_CoachService] FOREIGN KEY([CoachServiceID])
REFERENCES [dbo].[ServiceCategories] ([ServiceCategoryID])
GO
ALTER TABLE [dbo].[CoachServices] CHECK CONSTRAINT [FK_CoachService_CoachService]
GO
ALTER DATABASE [COSAdmin] SET  READ_WRITE 
GO