USE [dedb]
GO
ALTER TABLE [dbo].[userSetValues] DROP CONSTRAINT [FK_userSetValues_setValues]
GO
ALTER TABLE [dbo].[users] DROP CONSTRAINT [FK_users_countriesCodes]
GO
ALTER TABLE [dbo].[setValues] DROP CONSTRAINT [FK_setValues_setting]
GO
ALTER TABLE [dbo].[nationalities] DROP CONSTRAINT [FK_nationalities_users1]
GO
ALTER TABLE [dbo].[nationalities] DROP CONSTRAINT [FK_nationalities_users]
GO
ALTER TABLE [dbo].[error] DROP CONSTRAINT [FK_error_users]
GO
ALTER TABLE [dbo].[customers] DROP CONSTRAINT [FK_customers_users1]
GO
ALTER TABLE [dbo].[customers] DROP CONSTRAINT [FK_customers_users]
GO
ALTER TABLE [dbo].[customers] DROP CONSTRAINT [FK_customers_nationalities]
GO
ALTER TABLE [dbo].[customers] DROP CONSTRAINT [FK_customers_departments]
GO
ALTER TABLE [dbo].[cities] DROP CONSTRAINT [FK_cities_countriesCodes]
GO
ALTER TABLE [dbo].[users] DROP CONSTRAINT [DF_users_balance]
GO
ALTER TABLE [dbo].[countriesCodes] DROP CONSTRAINT [DF_countriesCodes_isDefault]
GO
/****** Object:  Table [dbo].[userSetValues]    Script Date: 12/8/2023 3:00:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[userSetValues]') AND type in (N'U'))
DROP TABLE [dbo].[userSetValues]
GO
/****** Object:  Table [dbo].[users]    Script Date: 12/8/2023 3:00:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[users]') AND type in (N'U'))
DROP TABLE [dbo].[users]
GO
/****** Object:  Table [dbo].[setValues]    Script Date: 12/8/2023 3:00:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[setValues]') AND type in (N'U'))
DROP TABLE [dbo].[setValues]
GO
/****** Object:  Table [dbo].[setting]    Script Date: 12/8/2023 3:00:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[setting]') AND type in (N'U'))
DROP TABLE [dbo].[setting]
GO
/****** Object:  Table [dbo].[nationalities]    Script Date: 12/8/2023 3:00:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nationalities]') AND type in (N'U'))
DROP TABLE [dbo].[nationalities]
GO
/****** Object:  Table [dbo].[error]    Script Date: 12/8/2023 3:00:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[error]') AND type in (N'U'))
DROP TABLE [dbo].[error]
GO
/****** Object:  Table [dbo].[departments]    Script Date: 12/8/2023 3:00:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[departments]') AND type in (N'U'))
DROP TABLE [dbo].[departments]
GO
/****** Object:  Table [dbo].[customers]    Script Date: 12/8/2023 3:00:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[customers]') AND type in (N'U'))
DROP TABLE [dbo].[customers]
GO
/****** Object:  Table [dbo].[countriesCodes]    Script Date: 12/8/2023 3:00:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[countriesCodes]') AND type in (N'U'))
DROP TABLE [dbo].[countriesCodes]
GO
/****** Object:  Table [dbo].[cities]    Script Date: 12/8/2023 3:00:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[cities]') AND type in (N'U'))
DROP TABLE [dbo].[cities]
GO
/****** Object:  Table [dbo].[cities]    Script Date: 12/8/2023 3:00:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cities](
	[cityId] [int] IDENTITY(1,1) NOT NULL,
	[cityCode] [nvarchar](50) NULL,
	[countryId] [int] NULL,
 CONSTRAINT [PK_cities] PRIMARY KEY CLUSTERED 
(
	[cityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[countriesCodes]    Script Date: 12/8/2023 3:00:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[countriesCodes](
	[countryId] [int] IDENTITY(1,1) NOT NULL,
	[code] [nvarchar](50) NOT NULL,
	[currency] [nvarchar](50) NULL,
	[name] [nvarchar](100) NULL,
	[isDefault] [tinyint] NOT NULL,
	[timeZoneName] [nvarchar](100) NULL,
	[timeZoneOffset] [nvarchar](100) NULL,
 CONSTRAINT [PK_countriesCodes] PRIMARY KEY CLUSTERED 
(
	[countryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[customers]    Script Date: 12/8/2023 3:00:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[customers](
	[custId] [bigint] IDENTITY(1,1) NOT NULL,
	[custname] [nvarchar](500) NULL,
	[lastName] [nvarchar](500) NULL,
	[mobile] [nvarchar](500) NULL,
	[department] [nvarchar](500) NULL,
	[barcode] [nvarchar](500) NULL,
	[printDate] [datetime2](7) NULL,
	[image] [nvarchar](max) NULL,
	[notes] [nvarchar](max) NULL,
	[createUserId] [bigint] NULL,
	[updateUserId] [bigint] NULL,
	[createDate] [datetime2](7) NULL,
	[updateDate] [datetime2](7) NULL,
	[isActive] [bit] NULL,
	[nationalityId] [bigint] NULL,
	[departmentId] [bigint] NULL,
 CONSTRAINT [PK_customers] PRIMARY KEY CLUSTERED 
(
	[custId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[departments]    Script Date: 12/8/2023 3:00:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[departments](
	[departmentId] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](500) NULL,
 CONSTRAINT [PK_departments] PRIMARY KEY CLUSTERED 
(
	[departmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[error]    Script Date: 12/8/2023 3:00:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[error](
	[errorId] [int] IDENTITY(1,1) NOT NULL,
	[num] [nvarchar](200) NULL,
	[msg] [ntext] NULL,
	[stackTrace] [ntext] NULL,
	[targetSite] [ntext] NULL,
	[createDate] [datetime2](7) NULL,
	[createUserId] [bigint] NULL,
 CONSTRAINT [PK_error] PRIMARY KEY CLUSTERED 
(
	[errorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[nationalities]    Script Date: 12/8/2023 3:00:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[nationalities](
	[nationalityId] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](500) NULL,
	[notes] [nvarchar](max) NULL,
	[isActive] [bit] NULL,
	[createUserId] [bigint] NULL,
	[updateUserId] [bigint] NULL,
	[createDate] [datetime2](7) NULL,
	[updateDate] [datetime2](7) NULL,
 CONSTRAINT [PK_nationalities] PRIMARY KEY CLUSTERED 
(
	[nationalityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[setting]    Script Date: 12/8/2023 3:00:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[setting](
	[settingId] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](200) NULL,
	[notes] [ntext] NULL,
 CONSTRAINT [PK_setting] PRIMARY KEY CLUSTERED 
(
	[settingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[setValues]    Script Date: 12/8/2023 3:00:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[setValues](
	[valId] [int] IDENTITY(1,1) NOT NULL,
	[value] [nvarchar](max) NULL,
	[isDefault] [int] NOT NULL,
	[isSystem] [int] NOT NULL,
	[notes] [nvarchar](max) NULL,
	[settingId] [int] NULL,
 CONSTRAINT [PK_setValues] PRIMARY KEY CLUSTERED 
(
	[valId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 12/8/2023 3:00:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[userId] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](500) NOT NULL,
	[AccountName] [nvarchar](500) NOT NULL,
	[lastName] [nvarchar](500) NULL,
	[company] [nvarchar](500) NULL,
	[email] [nvarchar](500) NULL,
	[phone] [nvarchar](500) NULL,
	[mobile] [nvarchar](500) NULL,
	[fax] [nvarchar](500) NULL,
	[address] [nvarchar](500) NULL,
	[agentLevel] [nvarchar](500) NULL,
	[createDate] [datetime2](7) NULL,
	[updateDate] [datetime2](7) NULL,
	[password] [nvarchar](500) NULL,
	[type] [nvarchar](10) NULL,
	[image] [ntext] NULL,
	[notes] [nvarchar](500) NULL,
	[balance] [decimal](20, 3) NOT NULL,
	[createUserId] [bigint] NULL,
	[updateUserId] [bigint] NULL,
	[isActive] [int] NULL,
	[code] [nvarchar](500) NULL,
	[isAdmin] [bit] NULL,
	[groupId] [int] NULL,
	[balanceType] [tinyint] NULL,
	[job] [nvarchar](100) NULL,
	[isOnline] [tinyint] NULL,
	[countryId] [int] NULL,
 CONSTRAINT [PK_agents] PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[userSetValues]    Script Date: 12/8/2023 3:00:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[userSetValues](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[userId] [bigint] NULL,
	[valId] [int] NULL,
	[note] [nvarchar](max) NULL,
	[createDate] [datetime2](7) NULL,
	[updateDate] [datetime2](7) NULL,
	[createUserId] [bigint] NULL,
	[updateUserId] [bigint] NULL,
 CONSTRAINT [PK_userSetValues] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[cities] ON 

INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (1, N'1', 2)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (2, N'2', 2)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (3, N'3', 2)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (4, N'4', 2)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (5, N'6', 2)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (6, N'7', 2)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (7, N'2', 4)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (8, N'3', 4)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (9, N'4', 4)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (10, N'6', 4)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (11, N'7', 4)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (12, N'9', 4)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (13, N'1', 7)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (14, N'21', 7)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (15, N'23', 7)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (16, N'24', 7)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (17, N'25', 7)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (18, N'30', 7)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (19, N'32', 7)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (20, N'33', 7)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (21, N'36', 7)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (22, N'37', 7)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (23, N'40', 7)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (24, N'42', 7)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (25, N'50', 7)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (26, N'53', 7)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (27, N'60', 7)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (28, N'62', 7)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (29, N'66', 7)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (30, N'1', 8)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (31, N'4', 8)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (32, N'5', 8)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (33, N'6', 8)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (34, N'7', 8)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (35, N'8', 8)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (36, N'9', 8)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (37, N'11', 9)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (38, N'21', 9)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (39, N'22', 9)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (40, N'23', 9)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (41, N'31', 9)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (42, N'33', 9)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (43, N'34', 9)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (44, N'41', 9)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (45, N'43', 9)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (46, N'51', 9)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (47, N'52', 9)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (48, N'14', 9)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (49, N'15', 9)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (50, N'16', 9)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (51, N'1', 10)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (52, N'2', 10)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (53, N'3', 10)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (54, N'4', 10)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (55, N'5', 10)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (56, N'6', 10)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (57, N'2', 11)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (58, N'5', 11)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (59, N'6', 11)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (60, N'8', 11)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (61, N'21', 12)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (62, N'24', 12)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (63, N'25', 12)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (64, N'26', 12)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (65, N'27', 12)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (66, N'29', 12)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (67, N'31', 12)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (68, N'32', 12)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (69, N'33', 12)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (70, N'34', 12)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (71, N'35', 12)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (72, N'36', 12)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (73, N'37', 12)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (74, N'38', 12)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (75, N'41', 12)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (76, N'43', 12)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (77, N'45', 12)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (78, N'46', 12)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (79, N'48', 12)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (80, N'49', 12)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (81, N'2', 13)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (82, N'3', 13)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (83, N'40', 13)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (84, N'45', 13)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (85, N'48', 13)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (86, N'50', 13)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (87, N'55', 13)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (88, N'62', 13)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (89, N'64', 13)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (90, N'66', 13)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (91, N'68', 13)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (92, N'82', 13)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (93, N'84', 13)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (94, N'86', 13)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (95, N'88', 13)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (96, N'93', 13)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (97, N'95', 13)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (98, N'96', 13)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (99, N'97', 13)
GO
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (100, N'71', 14)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (101, N'72', 14)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (103, N'73', 14)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (104, N'74', 14)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (105, N'75', 14)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (106, N'77', 14)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (107, N'21', 17)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (108, N'51', 17)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (109, N'57', 17)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (110, N'61', 17)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (111, N'87', 17)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (112, N'521', 17)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (113, N'652', 17)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (114, N'727', 17)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (115, N'212', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (116, N'216', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (117, N'222', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (118, N'224', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (119, N'232', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (120, N'236', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (121, N'242', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (122, N'246', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (123, N'256', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (124, N'258', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (125, N'266', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (126, N'272', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (127, N'274', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (128, N'276', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (129, N'284', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (130, N'312', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (131, N'322', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (132, N'332', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (134, N'338', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (135, N'342', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (136, N'346', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (137, N'352', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (138, N'358', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (139, N'362', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (140, N'364', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (141, N'366', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (142, N'382', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (143, N'412', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (144, N'414', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (145, N'416', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (146, N'422', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (147, N'424', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (148, N'432', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (149, N'442', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (150, N'452', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (151, N'462', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (152, N'472', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (153, N'482', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (154, N'488', 19)
SET IDENTITY_INSERT [dbo].[cities] OFF
GO
SET IDENTITY_INSERT [dbo].[countriesCodes] ON 

INSERT [dbo].[countriesCodes] ([countryId], [code], [currency], [name], [isDefault], [timeZoneName], [timeZoneOffset]) VALUES (1, N'+965', N'KWD', N'Kuwait', 1, N'Arab Standard Time', NULL)
INSERT [dbo].[countriesCodes] ([countryId], [code], [currency], [name], [isDefault], [timeZoneName], [timeZoneOffset]) VALUES (2, N'+966', N'SAR', N'Saudi Arabia', 0, N'Arab Standard Time', NULL)
INSERT [dbo].[countriesCodes] ([countryId], [code], [currency], [name], [isDefault], [timeZoneName], [timeZoneOffset]) VALUES (3, N'+968', N'OMR', N'Oman', 0, N'Arabian Standard Time', NULL)
INSERT [dbo].[countriesCodes] ([countryId], [code], [currency], [name], [isDefault], [timeZoneName], [timeZoneOffset]) VALUES (4, N'+971', N'AED', N'United Arab Emirates', 0, N'Arabian Standard Time', NULL)
INSERT [dbo].[countriesCodes] ([countryId], [code], [currency], [name], [isDefault], [timeZoneName], [timeZoneOffset]) VALUES (5, N'+974', N'QAR', N'Qatar', 0, N'Arabian Standard Time', NULL)
INSERT [dbo].[countriesCodes] ([countryId], [code], [currency], [name], [isDefault], [timeZoneName], [timeZoneOffset]) VALUES (6, N'+973', N'BHD', N'Bahrain', 0, N'Arabian Standard Time', NULL)
INSERT [dbo].[countriesCodes] ([countryId], [code], [currency], [name], [isDefault], [timeZoneName], [timeZoneOffset]) VALUES (7, N'+964', N'IQD', N'Iraq', 0, N'Arabic Standard Time', NULL)
INSERT [dbo].[countriesCodes] ([countryId], [code], [currency], [name], [isDefault], [timeZoneName], [timeZoneOffset]) VALUES (8, N'+961', N'LBP', N'Lebanon', 0, N'Middle East Standard Time', NULL)
INSERT [dbo].[countriesCodes] ([countryId], [code], [currency], [name], [isDefault], [timeZoneName], [timeZoneOffset]) VALUES (9, N'+963', N'SYP', N'Syria', 0, N'Syria Standard Time', NULL)
INSERT [dbo].[countriesCodes] ([countryId], [code], [currency], [name], [isDefault], [timeZoneName], [timeZoneOffset]) VALUES (10, N'+967', N'YER', N'Yemen', 0, N'Arab Standard Time', NULL)
INSERT [dbo].[countriesCodes] ([countryId], [code], [currency], [name], [isDefault], [timeZoneName], [timeZoneOffset]) VALUES (11, N'+962', N'JOD', N'Jordan', 0, N'Jordan Standard Time', NULL)
INSERT [dbo].[countriesCodes] ([countryId], [code], [currency], [name], [isDefault], [timeZoneName], [timeZoneOffset]) VALUES (12, N'+213', N'DZD', N'Algeria', 0, N'W. Central Africa Standard Time', NULL)
INSERT [dbo].[countriesCodes] ([countryId], [code], [currency], [name], [isDefault], [timeZoneName], [timeZoneOffset]) VALUES (13, N'+20', N'EGP', N'Egypt', 0, N'Egypt Standard Time', NULL)
INSERT [dbo].[countriesCodes] ([countryId], [code], [currency], [name], [isDefault], [timeZoneName], [timeZoneOffset]) VALUES (14, N'+216', N'TND', N'Tunisia', 0, N'W. Central Africa Standard Time', NULL)
INSERT [dbo].[countriesCodes] ([countryId], [code], [currency], [name], [isDefault], [timeZoneName], [timeZoneOffset]) VALUES (15, N'+249', N'SDG', N'Sudan', 0, N'Sudan Standard Time', NULL)
INSERT [dbo].[countriesCodes] ([countryId], [code], [currency], [name], [isDefault], [timeZoneName], [timeZoneOffset]) VALUES (16, N'+212', N'MAD', N'Morocco', 0, N'Morocco Standard Time', NULL)
INSERT [dbo].[countriesCodes] ([countryId], [code], [currency], [name], [isDefault], [timeZoneName], [timeZoneOffset]) VALUES (17, N'+218', N'LYD', N'Libya', 0, N'Libya Standard Time', NULL)
INSERT [dbo].[countriesCodes] ([countryId], [code], [currency], [name], [isDefault], [timeZoneName], [timeZoneOffset]) VALUES (18, N'+252', N'SOS', N'Somalia', 0, N'Libya Standard Time', NULL)
INSERT [dbo].[countriesCodes] ([countryId], [code], [currency], [name], [isDefault], [timeZoneName], [timeZoneOffset]) VALUES (19, N'+90', N'TRY', N'Turkey', 0, N'Turkey Standard Time', NULL)
SET IDENTITY_INSERT [dbo].[countriesCodes] OFF
GO
SET IDENTITY_INSERT [dbo].[error] ON 

INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (1, N'-2147467261', N'Value cannot be null.
Parameter name: source', N'   at System.Linq.Enumerable.Where[TSource](IEnumerable`1 source, Func`2 predicate)
   at BookAccountApp.View.applications.uc_programs.<Search>d__22.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\applications\us_programs.xaml.cs:line 425
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at BookAccountApp.View.applications.uc_programs.<Tgl_isActive_Checked>d__14.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\applications\us_programs.xaml.cs:line 181', N'System.Collections.Generic.IEnumerable`1[TSource] Where[TSource](System.Collections.Generic.IEnumerable`1[TSource], System.Func`2[TSource,System.Boolean])', CAST(N'2023-07-10T13:17:24.0868739' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2, N'-2147467261', N'Value cannot be null.
Parameter name: source', N'   at System.Linq.Enumerable.Where[TSource](IEnumerable`1 source, Func`2 predicate)
   at BookAccountApp.View.applications.uc_programs.<Search>d__22.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\applications\us_programs.xaml.cs:line 425
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at BookAccountApp.View.applications.uc_programs.<UserControl_Loaded>d__10.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\applications\us_programs.xaml.cs:line 83', N'System.Collections.Generic.IEnumerable`1[TSource] Where[TSource](System.Collections.Generic.IEnumerable`1[TSource], System.Func`2[TSource,System.Boolean])', CAST(N'2023-07-10T13:17:24.3945897' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (3, N'-2147467261', N'Object reference not set to an instance of an object.', N'   at BookAccountApp.View.sectionData.uc_users.RefreshUsersView() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 512
   at BookAccountApp.View.sectionData.uc_users.<Search>d__22.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 502
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at BookAccountApp.View.sectionData.uc_users.<Tgl_isActive_Checked>d__17.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 384', N'Void RefreshUsersView()', CAST(N'2023-07-10T13:38:27.2398677' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (4, N'-2147467261', N'Value cannot be null.
Parameter name: source', N'   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at BookAccountApp.Classes.FillCombo.<fillCountries>d__37.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\Classes\FillCombo.cs:line 232
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at BookAccountApp.View.sectionData.uc_users.<UserControl_Loaded>d__10.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 82', N'System.Collections.Generic.List`1[TSource] ToList[TSource](System.Collections.Generic.IEnumerable`1[TSource])', CAST(N'2023-07-10T13:38:27.7021291' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (5, N'-2147467261', N'Object reference not set to an instance of an object.', N'   at BookAccountApp.View.sectionData.uc_users.RefreshUsersView() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 512
   at BookAccountApp.View.sectionData.uc_users.<Search>d__22.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 502
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at BookAccountApp.View.sectionData.uc_users.<Tgl_isActive_Checked>d__17.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 384', N'Void RefreshUsersView()', CAST(N'2023-07-10T13:43:15.1861230' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (6, N'-2147467261', N'Value cannot be null.
Parameter name: source', N'   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at BookAccountApp.Classes.FillCombo.<fillCountries>d__37.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\Classes\FillCombo.cs:line 232
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at BookAccountApp.View.sectionData.uc_users.<UserControl_Loaded>d__10.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 82', N'System.Collections.Generic.List`1[TSource] ToList[TSource](System.Collections.Generic.IEnumerable`1[TSource])', CAST(N'2023-07-10T13:43:15.6539315' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (7, N'-2147467261', N'Value cannot be null.
Parameter name: source', N'   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at BookAccountApp.Classes.FillCombo.<fillCountries>d__37.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\Classes\FillCombo.cs:line 232
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at BookAccountApp.View.sectionData.uc_users.<UserControl_Loaded>d__10.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 82', N'System.Collections.Generic.List`1[TSource] ToList[TSource](System.Collections.Generic.IEnumerable`1[TSource])', CAST(N'2023-07-10T13:43:22.0033953' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (8, N'-2147467261', N'Value cannot be null.
Parameter name: source', N'   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at BookAccountApp.Classes.FillCombo.<fillCountries>d__37.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\Classes\FillCombo.cs:line 232
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at BookAccountApp.View.sectionData.uc_users.<UserControl_Loaded>d__10.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 82', N'System.Collections.Generic.List`1[TSource] ToList[TSource](System.Collections.Generic.IEnumerable`1[TSource])', CAST(N'2023-07-10T13:43:29.8860350' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (9, N'-2147467261', N'Object reference not set to an instance of an object.', N'   at BookAccountApp.View.sectionData.uc_users.RefreshUsersView() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 512
   at BookAccountApp.View.sectionData.uc_users.<Search>d__22.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 502
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at BookAccountApp.View.sectionData.uc_users.<Tgl_isActive_Checked>d__17.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 384', N'Void RefreshUsersView()', CAST(N'2023-07-10T13:59:46.2070414' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (10, N'-2147467261', N'Object reference not set to an instance of an object.', N'   at BookAccountApp.View.sectionData.uc_users.RefreshUsersView() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 512
   at BookAccountApp.View.sectionData.uc_users.<Search>d__22.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 502
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at BookAccountApp.View.sectionData.uc_users.<Tgl_isActive_Checked>d__17.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 384', N'Void RefreshUsersView()', CAST(N'2023-07-10T14:07:40.0723543' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (11, N'-2147467261', N'Object reference not set to an instance of an object.', N'   at BookAccountApp.View.sectionData.uc_users.RefreshUsersView() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 512
   at BookAccountApp.View.sectionData.uc_users.<Search>d__22.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 502
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at BookAccountApp.View.sectionData.uc_users.<Tgl_isActive_Checked>d__17.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 384', N'Void RefreshUsersView()', CAST(N'2023-07-10T15:06:01.3457669' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (12, N'-2147467261', N'Object reference not set to an instance of an object.', N'   at BookAccountApp.View.sectionData.uc_users.RefreshUsersView() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 512
   at BookAccountApp.View.sectionData.uc_users.<Search>d__22.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 502
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at BookAccountApp.View.sectionData.uc_users.<Tgl_isActive_Checked>d__17.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 384', N'Void RefreshUsersView()', CAST(N'2023-07-10T15:11:24.6605400' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (13, N'-2147467261', N'Object reference not set to an instance of an object.', N'   at BookAccountApp.View.sectionData.uc_users.RefreshUsersView() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 512
   at BookAccountApp.View.sectionData.uc_users.<Search>d__22.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 502
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at BookAccountApp.View.sectionData.uc_users.<Tgl_isActive_Checked>d__17.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 384', N'Void RefreshUsersView()', CAST(N'2023-07-10T15:20:58.6021936' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (14, N'-2147467261', N'Object reference not set to an instance of an object.', N'   at BookAccountApp.View.sectionData.uc_users.RefreshUsersView() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 512
   at BookAccountApp.View.sectionData.uc_users.<Search>d__22.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 502
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at BookAccountApp.View.sectionData.uc_users.<Tgl_isActive_Checked>d__17.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 384', N'Void RefreshUsersView()', CAST(N'2023-07-10T15:22:48.1548919' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (15, N'-2147467261', N'Object reference not set to an instance of an object.', N'   at BookAccountApp.View.sectionData.uc_users.<Search>d__22.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 502
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at BookAccountApp.View.sectionData.uc_users.<Tgl_isActive_Checked>d__17.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 384', N'Void MoveNext()', CAST(N'2023-07-10T15:28:21.8662478' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (16, N'-2147467261', N'Object reference not set to an instance of an object.', N'   at BookAccountApp.View.sectionData.uc_users.RefreshUsersView() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 514
   at BookAccountApp.View.sectionData.uc_users.<Search>d__22.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 503
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at BookAccountApp.View.sectionData.uc_users.<Tgl_isActive_Checked>d__17.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 384', N'Void RefreshUsersView()', CAST(N'2023-07-10T15:30:32.0778769' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (17, N'-2147467261', N'Object reference not set to an instance of an object.', N'   at BookAccountApp.View.sectionData.uc_users.RefreshUsersView() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 514
   at BookAccountApp.View.sectionData.uc_users.<Search>d__22.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 503
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at BookAccountApp.View.sectionData.uc_users.<Tgl_isActive_Checked>d__17.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 384', N'Void RefreshUsersView()', CAST(N'2023-07-10T15:33:02.6654975' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (18, N'-2147467261', N'Object reference not set to an instance of an object.', N'   at BookAccountApp.View.sectionData.uc_users.RefreshUsersView() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 514
   at BookAccountApp.View.sectionData.uc_users.<Search>d__22.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 503
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at BookAccountApp.View.sectionData.uc_users.<Tgl_isActive_Checked>d__17.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 384', N'Void RefreshUsersView()', CAST(N'2023-07-10T15:36:36.9455168' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (19, N'-2147467261', N'Object reference not set to an instance of an object.', N'   at BookAccountApp.View.sectionData.uc_users.RefreshUsersView() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 513
   at BookAccountApp.View.sectionData.uc_users.<Search>d__22.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 503
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at BookAccountApp.View.sectionData.uc_users.<Tgl_isActive_Checked>d__17.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 384', N'Void RefreshUsersView()', CAST(N'2023-07-11T15:41:38.1758300' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (20, N'-2147467261', N'Object reference not set to an instance of an object.', N'   at BookAccountApp.View.sectionData.uc_users.RefreshUsersView() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 513
   at BookAccountApp.View.sectionData.uc_users.<Search>d__22.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 503
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at BookAccountApp.View.sectionData.uc_users.<Tgl_isActive_Checked>d__17.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\uc_users.xaml.cs:line 384', N'Void RefreshUsersView()', CAST(N'2023-07-11T16:51:13.4263341' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (21, N'-2147024809', N'The path is not of a legal form.', N'   at System.IO.Path.NewNormalizePath(String path, Int32 maxPathLength, Boolean expandShortPaths)
   at System.IO.Path.NormalizePath(String path, Boolean fullCheck, Int32 maxPathLength, Boolean expandShortPaths)
   at System.IO.Path.GetFullPathInternal(String path)
   at System.IO.Path.GetFullPath(String path)
   at System.Drawing.IntSecurity.UnsafeGetFullPath(String fileName)
   at System.Drawing.IntSecurity.DemandReadFileIO(String fileName)
   at System.Drawing.Image.FromFile(String filename, Boolean useEmbeddedColorManagement)
   at System.Drawing.Image.FromFile(String filename)
   at BookAccountApp.Classes.SetValues.<saveImage>d__35.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\Classes\ApiClasses\setValues.cs:line 456
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter`1.GetResult()
   at BookAccountApp.View.windows.wd_companyInfo.<Btn_save_Click>d__45.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\windows\wd_companyInfo.xaml.cs:line 434', N'System.String NewNormalizePath(System.String, Int32, Boolean)', CAST(N'2023-07-11T17:23:31.9971618' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (1019, N'-2146232062', N'The entry ''bookdbEntities'' has already been added.', N'   at System.Configuration.ConfigurationElementCollection.BaseAdd(ConfigurationElement element, Boolean throwIfExists, Boolean ignoreLocks)
   at System.Configuration.ConfigurationElementCollection.BaseAdd(ConfigurationElement element)
   at System.Configuration.ConnectionStringSettingsCollection.Add(ConnectionStringSettings settings)
   at BookAccountApp.View.windows.wd_logIn.AddNewConnectionString() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\windows\wd_logIn.xaml.cs:line 157
   at BookAccountApp.View.windows.wd_logIn.<btnLogIn_Click>d__9.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\windows\wd_logIn.xaml.cs:line 175', N'Void BaseAdd(System.Configuration.ConfigurationElement, Boolean, Boolean)', CAST(N'2023-07-29T19:27:01.4929051' AS DateTime2), NULL)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2019, N'-2146233038', N'Could not find any resources appropriate for the specified culture or the neutral culture.  Make sure "DataEntryApp.ar_file.resources" was correctly embedded or linked into assembly "BookAccountApp" at compile time, or that all the satellite assemblies required are loadable and fully signed.', N'   at System.Resources.ManifestBasedResourceGroveler.HandleResourceStreamMissing(String fileName)
   at System.Resources.ManifestBasedResourceGroveler.GrovelForResourceSet(CultureInfo culture, Dictionary`2 localResourceSets, Boolean tryParents, Boolean createIfNotExists, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo requestedCulture, Boolean createIfNotExists, Boolean tryParents, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo culture, Boolean createIfNotExists, Boolean tryParents)
   at System.Resources.ResourceManager.GetString(String name, CultureInfo culture)
   at System.Resources.ResourceManager.GetString(String name)
   at DataEntryApp.View.windows.wd_logIn.translate() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\windows\wd_logIn.xaml.cs:line 325
   at DataEntryApp.View.windows.wd_logIn.Window_Loaded(Object sender, RoutedEventArgs e) in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\windows\wd_logIn.xaml.cs:line 90', N'Void HandleResourceStreamMissing(System.String)', CAST(N'2023-08-08T15:27:16.8257470' AS DateTime2), NULL)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2020, N'-2146233038', N'Could not find any resources appropriate for the specified culture or the neutral culture.  Make sure "DataEntryApp.ar_file.resources" was correctly embedded or linked into assembly "BookAccountApp" at compile time, or that all the satellite assemblies required are loadable and fully signed.', N'   at System.Resources.ManifestBasedResourceGroveler.HandleResourceStreamMissing(String fileName)
   at System.Resources.ManifestBasedResourceGroveler.GrovelForResourceSet(CultureInfo culture, Dictionary`2 localResourceSets, Boolean tryParents, Boolean createIfNotExists, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo requestedCulture, Boolean createIfNotExists, Boolean tryParents, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo culture, Boolean createIfNotExists, Boolean tryParents)
   at System.Resources.ResourceManager.GetString(String name, CultureInfo culture)
   at System.Resources.ResourceManager.GetString(String name)
   at DataEntryApp.View.windows.wd_logIn.translate() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\windows\wd_logIn.xaml.cs:line 325
   at DataEntryApp.View.windows.wd_logIn.Window_Loaded(Object sender, RoutedEventArgs e) in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\windows\wd_logIn.xaml.cs:line 90', N'Void HandleResourceStreamMissing(System.String)', CAST(N'2023-08-08T15:28:33.6359289' AS DateTime2), NULL)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2021, N'-2146233038', N'Could not find any resources appropriate for the specified culture or the neutral culture.  Make sure "DataEntryApp.ar_file.resources" was correctly embedded or linked into assembly "BookAccountApp" at compile time, or that all the satellite assemblies required are loadable and fully signed.', N'   at System.Resources.ManifestBasedResourceGroveler.HandleResourceStreamMissing(String fileName)
   at System.Resources.ManifestBasedResourceGroveler.GrovelForResourceSet(CultureInfo culture, Dictionary`2 localResourceSets, Boolean tryParents, Boolean createIfNotExists, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo requestedCulture, Boolean createIfNotExists, Boolean tryParents, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo culture, Boolean createIfNotExists, Boolean tryParents)
   at System.Resources.ResourceManager.GetString(String name, CultureInfo culture)
   at System.Resources.ResourceManager.GetString(String name)
   at DataEntryApp.Classes.HelpClass.SetValidate(Path p_error, String tr) in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\Classes\HelpClass.cs:line 122
   at DataEntryApp.View.windows.wd_logIn.validateEmpty(Object sender, RoutedEventArgs e) in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\windows\wd_logIn.xaml.cs:line 276', N'Void HandleResourceStreamMissing(System.String)', CAST(N'2023-08-08T15:28:40.5477243' AS DateTime2), NULL)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2022, N'-2147467261', N'Object reference not set to an instance of an object.', N'   at DataEntryApp.View.badge.uc_customers.RefreshCustomersView() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\uc_customers.xaml.cs:line 472
   at DataEntryApp.View.badge.uc_customers.<Search>d__23.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\uc_customers.xaml.cs:line 463
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at DataEntryApp.View.badge.uc_customers.<Tgl_isActive_Checked>d__18.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\uc_customers.xaml.cs:line 347', N'Void RefreshCustomersView()', CAST(N'2023-08-09T00:20:57.5441920' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2023, N'-2147467261', N'Object reference not set to an instance of an object.', N'   at DataEntryApp.View.badge.uc_customers.RefreshCustomersView() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\uc_customers.xaml.cs:line 472
   at DataEntryApp.View.badge.uc_customers.<Search>d__23.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\uc_customers.xaml.cs:line 463
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at DataEntryApp.View.badge.uc_customers.<Tgl_isActive_Checked>d__18.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\uc_customers.xaml.cs:line 347', N'Void RefreshCustomersView()', CAST(N'2023-08-09T00:21:48.5783339' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2024, N'-2147467261', N'Object reference not set to an instance of an object.', N'   at DataEntryApp.View.badge.uc_customers.RefreshCustomersView() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\uc_customers.xaml.cs:line 472
   at DataEntryApp.View.badge.uc_customers.<Search>d__23.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\uc_customers.xaml.cs:line 463
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at DataEntryApp.View.badge.uc_customers.<Tgl_isActive_Checked>d__18.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\uc_customers.xaml.cs:line 347', N'Void RefreshCustomersView()', CAST(N'2023-08-09T00:24:11.9657561' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2025, N'-2147467261', N'Object reference not set to an instance of an object.', N'   at DataEntryApp.View.badge.uc_customers.RefreshCustomersView() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\uc_customers.xaml.cs:line 472
   at DataEntryApp.View.badge.uc_customers.<Search>d__23.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\uc_customers.xaml.cs:line 463
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at DataEntryApp.View.badge.uc_customers.<Tgl_isActive_Checked>d__18.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\uc_customers.xaml.cs:line 347', N'Void RefreshCustomersView()', CAST(N'2023-08-09T14:35:17.5402703' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2026, N'-2147467261', N'Object reference not set to an instance of an object.', N'   at DataEntryApp.View.badge.uc_customers.RefreshCustomersView() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\uc_customers.xaml.cs:line 472
   at DataEntryApp.View.badge.uc_customers.<Search>d__23.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\uc_customers.xaml.cs:line 463
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at DataEntryApp.View.badge.uc_customers.<Tgl_isActive_Checked>d__18.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\uc_customers.xaml.cs:line 347', N'Void RefreshCustomersView()', CAST(N'2023-08-09T15:06:34.9325645' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2027, N'-2147467261', N'Object reference not set to an instance of an object.', N'   at DataEntryApp.View.badge.uc_customers.RefreshCustomersView() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\uc_customers.xaml.cs:line 472
   at DataEntryApp.View.badge.uc_customers.<Search>d__23.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\uc_customers.xaml.cs:line 463
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at DataEntryApp.View.badge.uc_customers.<Tgl_isActive_Checked>d__18.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\uc_customers.xaml.cs:line 347', N'Void RefreshCustomersView()', CAST(N'2023-08-09T15:07:51.6049314' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2028, N'-2147467261', N'Object reference not set to an instance of an object.', N'   at DataEntryApp.View.badge.uc_customers.RefreshCustomersView() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\uc_customers.xaml.cs:line 472
   at DataEntryApp.View.badge.uc_customers.<Search>d__23.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\uc_customers.xaml.cs:line 463
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at DataEntryApp.View.badge.uc_customers.<Tgl_isActive_Checked>d__18.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\uc_customers.xaml.cs:line 347', N'Void RefreshCustomersView()', CAST(N'2023-08-09T15:09:39.2456933' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2029, N'-2147467261', N'Object reference not set to an instance of an object.', N'   at DataEntryApp.View.badge.uc_customers.RefreshCustomersView() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\uc_customers.xaml.cs:line 472
   at DataEntryApp.View.badge.uc_customers.<Search>d__23.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\uc_customers.xaml.cs:line 463
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at DataEntryApp.View.badge.uc_customers.<Tgl_isActive_Checked>d__18.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\uc_customers.xaml.cs:line 347', N'Void RefreshCustomersView()', CAST(N'2023-08-09T15:17:30.9753353' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2030, N'-2146233038', N'Could not find any resources appropriate for the specified culture or the neutral culture.  Make sure "BookAccountApp.ar_file.resources" was correctly embedded or linked into assembly "DataEntryApp" at compile time, or that all the satellite assemblies required are loadable and fully signed.', N'   at System.Resources.ManifestBasedResourceGroveler.HandleResourceStreamMissing(String fileName)
   at System.Resources.ManifestBasedResourceGroveler.GrovelForResourceSet(CultureInfo culture, Dictionary`2 localResourceSets, Boolean tryParents, Boolean createIfNotExists, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo requestedCulture, Boolean createIfNotExists, Boolean tryParents, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo culture, Boolean createIfNotExists, Boolean tryParents)
   at System.Resources.ResourceManager.GetString(String name, CultureInfo culture)
   at System.Resources.ResourceManager.GetString(String name)
   at DataEntryApp.View.settings.uc_settings.translate() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\settings\uc_settings.xaml.cs:line 77
   at DataEntryApp.View.settings.uc_settings.Window_Loaded(Object sender, RoutedEventArgs e) in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\settings\uc_settings.xaml.cs:line 56', N'Void HandleResourceStreamMissing(System.String)', CAST(N'2023-08-10T14:38:08.0057723' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2031, N'-2146233038', N'Could not find any resources appropriate for the specified culture or the neutral culture.  Make sure "BookAccountApp.ar_file.resources" was correctly embedded or linked into assembly "DataEntryApp" at compile time, or that all the satellite assemblies required are loadable and fully signed.', N'   at System.Resources.ManifestBasedResourceGroveler.HandleResourceStreamMissing(String fileName)
   at System.Resources.ManifestBasedResourceGroveler.GrovelForResourceSet(CultureInfo culture, Dictionary`2 localResourceSets, Boolean tryParents, Boolean createIfNotExists, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo requestedCulture, Boolean createIfNotExists, Boolean tryParents, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo culture, Boolean createIfNotExists, Boolean tryParents)
   at System.Resources.ResourceManager.GetString(String name, CultureInfo culture)
   at System.Resources.ResourceManager.GetString(String name)
   at DataEntryApp.View.applications.uc_programs.translate() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 99
   at DataEntryApp.View.applications.uc_programs.<UserControl_Loaded>d__7.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 81', N'Void HandleResourceStreamMissing(System.String)', CAST(N'2023-08-10T14:49:22.2078695' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2032, N'-2146233038', N'Could not find any resources appropriate for the specified culture or the neutral culture.  Make sure "BookAccountApp.ar_file.resources" was correctly embedded or linked into assembly "DataEntryApp" at compile time, or that all the satellite assemblies required are loadable and fully signed.', N'   at System.Resources.ManifestBasedResourceGroveler.HandleResourceStreamMissing(String fileName)
   at System.Resources.ManifestBasedResourceGroveler.GrovelForResourceSet(CultureInfo culture, Dictionary`2 localResourceSets, Boolean tryParents, Boolean createIfNotExists, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo requestedCulture, Boolean createIfNotExists, Boolean tryParents, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo culture, Boolean createIfNotExists, Boolean tryParents)
   at System.Resources.ResourceManager.GetString(String name, CultureInfo culture)
   at System.Resources.ResourceManager.GetString(String name)
   at DataEntryApp.View.applications.uc_programs.translate() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 99
   at DataEntryApp.View.applications.uc_programs.<UserControl_Loaded>d__7.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 81', N'Void HandleResourceStreamMissing(System.String)', CAST(N'2023-08-10T14:50:50.1142965' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2033, N'-2146233038', N'Could not find any resources appropriate for the specified culture or the neutral culture.  Make sure "BookAccountApp.ar_file.resources" was correctly embedded or linked into assembly "DataEntryApp" at compile time, or that all the satellite assemblies required are loadable and fully signed.', N'   at System.Resources.ManifestBasedResourceGroveler.HandleResourceStreamMissing(String fileName)
   at System.Resources.ManifestBasedResourceGroveler.GrovelForResourceSet(CultureInfo culture, Dictionary`2 localResourceSets, Boolean tryParents, Boolean createIfNotExists, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo requestedCulture, Boolean createIfNotExists, Boolean tryParents, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo culture, Boolean createIfNotExists, Boolean tryParents)
   at System.Resources.ResourceManager.GetString(String name, CultureInfo culture)
   at System.Resources.ResourceManager.GetString(String name)
   at DataEntryApp.View.applications.uc_programs.translate() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 99
   at DataEntryApp.View.applications.uc_programs.<UserControl_Loaded>d__7.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 81', N'Void HandleResourceStreamMissing(System.String)', CAST(N'2023-08-10T14:55:03.3196926' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2034, N'-2146233038', N'Could not find any resources appropriate for the specified culture or the neutral culture.  Make sure "BookAccountApp.ar_file.resources" was correctly embedded or linked into assembly "DataEntryApp" at compile time, or that all the satellite assemblies required are loadable and fully signed.', N'   at System.Resources.ManifestBasedResourceGroveler.HandleResourceStreamMissing(String fileName)
   at System.Resources.ManifestBasedResourceGroveler.GrovelForResourceSet(CultureInfo culture, Dictionary`2 localResourceSets, Boolean tryParents, Boolean createIfNotExists, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo requestedCulture, Boolean createIfNotExists, Boolean tryParents, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo culture, Boolean createIfNotExists, Boolean tryParents)
   at System.Resources.ResourceManager.GetString(String name, CultureInfo culture)
   at System.Resources.ResourceManager.GetString(String name)
   at DataEntryApp.View.applications.uc_programs.translate() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 99
   at DataEntryApp.View.applications.uc_programs.<UserControl_Loaded>d__7.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 81', N'Void HandleResourceStreamMissing(System.String)', CAST(N'2023-08-10T14:55:54.5478392' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2035, N'-2146233038', N'Could not find any resources appropriate for the specified culture or the neutral culture.  Make sure "BookAccountApp.ar_file.resources" was correctly embedded or linked into assembly "DataEntryApp" at compile time, or that all the satellite assemblies required are loadable and fully signed.', N'   at System.Resources.ManifestBasedResourceGroveler.HandleResourceStreamMissing(String fileName)
   at System.Resources.ManifestBasedResourceGroveler.GrovelForResourceSet(CultureInfo culture, Dictionary`2 localResourceSets, Boolean tryParents, Boolean createIfNotExists, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo requestedCulture, Boolean createIfNotExists, Boolean tryParents, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo culture, Boolean createIfNotExists, Boolean tryParents)
   at System.Resources.ResourceManager.GetString(String name, CultureInfo culture)
   at System.Resources.ResourceManager.GetString(String name)
   at DataEntryApp.View.applications.uc_programs.translate() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 99
   at DataEntryApp.View.applications.uc_programs.<UserControl_Loaded>d__7.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 81', N'Void HandleResourceStreamMissing(System.String)', CAST(N'2023-08-10T15:02:01.7251972' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2036, N'-2146233038', N'Could not find any resources appropriate for the specified culture or the neutral culture.  Make sure "BookAccountApp.ar_file.resources" was correctly embedded or linked into assembly "DataEntryApp" at compile time, or that all the satellite assemblies required are loadable and fully signed.', N'   at System.Resources.ManifestBasedResourceGroveler.HandleResourceStreamMissing(String fileName)
   at System.Resources.ManifestBasedResourceGroveler.GrovelForResourceSet(CultureInfo culture, Dictionary`2 localResourceSets, Boolean tryParents, Boolean createIfNotExists, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo requestedCulture, Boolean createIfNotExists, Boolean tryParents, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo culture, Boolean createIfNotExists, Boolean tryParents)
   at System.Resources.ResourceManager.GetString(String name, CultureInfo culture)
   at System.Resources.ResourceManager.GetString(String name)
   at DataEntryApp.View.settings.uc_settings.translate() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\settings\uc_settings.xaml.cs:line 77
   at DataEntryApp.View.settings.uc_settings.Window_Loaded(Object sender, RoutedEventArgs e) in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\settings\uc_settings.xaml.cs:line 56', N'Void HandleResourceStreamMissing(System.String)', CAST(N'2023-08-10T15:07:15.4183731' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2037, N'-2146233038', N'Could not find any resources appropriate for the specified culture or the neutral culture.  Make sure "BookAccountApp.ar_file.resources" was correctly embedded or linked into assembly "DataEntryApp" at compile time, or that all the satellite assemblies required are loadable and fully signed.', N'   at System.Resources.ManifestBasedResourceGroveler.HandleResourceStreamMissing(String fileName)
   at System.Resources.ManifestBasedResourceGroveler.GrovelForResourceSet(CultureInfo culture, Dictionary`2 localResourceSets, Boolean tryParents, Boolean createIfNotExists, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo requestedCulture, Boolean createIfNotExists, Boolean tryParents, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo culture, Boolean createIfNotExists, Boolean tryParents)
   at System.Resources.ResourceManager.GetString(String name, CultureInfo culture)
   at System.Resources.ResourceManager.GetString(String name)
   at DataEntryApp.View.settings.uc_settings.translate() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\settings\uc_settings.xaml.cs:line 77
   at DataEntryApp.View.settings.uc_settings.Window_Loaded(Object sender, RoutedEventArgs e) in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\settings\uc_settings.xaml.cs:line 56', N'Void HandleResourceStreamMissing(System.String)', CAST(N'2023-08-10T15:07:46.2927688' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2038, N'-2146233038', N'Could not find any resources appropriate for the specified culture or the neutral culture.  Make sure "BookAccountApp.ar_file.resources" was correctly embedded or linked into assembly "DataEntryApp" at compile time, or that all the satellite assemblies required are loadable and fully signed.', N'   at System.Resources.ManifestBasedResourceGroveler.HandleResourceStreamMissing(String fileName)
   at System.Resources.ManifestBasedResourceGroveler.GrovelForResourceSet(CultureInfo culture, Dictionary`2 localResourceSets, Boolean tryParents, Boolean createIfNotExists, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo requestedCulture, Boolean createIfNotExists, Boolean tryParents, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo culture, Boolean createIfNotExists, Boolean tryParents)
   at System.Resources.ResourceManager.GetString(String name, CultureInfo culture)
   at System.Resources.ResourceManager.GetString(String name)
   at DataEntryApp.View.settings.uc_settings.translate() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\settings\uc_settings.xaml.cs:line 77
   at DataEntryApp.View.settings.uc_settings.Window_Loaded(Object sender, RoutedEventArgs e) in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\settings\uc_settings.xaml.cs:line 56', N'Void HandleResourceStreamMissing(System.String)', CAST(N'2023-08-10T15:09:14.2380439' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2039, N'-2146233038', N'Could not find any resources appropriate for the specified culture or the neutral culture.  Make sure "BookAccountApp.ar_file.resources" was correctly embedded or linked into assembly "DataEntryApp" at compile time, or that all the satellite assemblies required are loadable and fully signed.', N'   at System.Resources.ManifestBasedResourceGroveler.HandleResourceStreamMissing(String fileName)
   at System.Resources.ManifestBasedResourceGroveler.GrovelForResourceSet(CultureInfo culture, Dictionary`2 localResourceSets, Boolean tryParents, Boolean createIfNotExists, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo requestedCulture, Boolean createIfNotExists, Boolean tryParents, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo culture, Boolean createIfNotExists, Boolean tryParents)
   at System.Resources.ResourceManager.GetString(String name, CultureInfo culture)
   at System.Resources.ResourceManager.GetString(String name)
   at DataEntryApp.View.settings.uc_settings.translate() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\settings\uc_settings.xaml.cs:line 77
   at DataEntryApp.View.settings.uc_settings.Window_Loaded(Object sender, RoutedEventArgs e) in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\settings\uc_settings.xaml.cs:line 56', N'Void HandleResourceStreamMissing(System.String)', CAST(N'2023-08-10T15:09:20.2386907' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2040, N'-2146233038', N'Could not find any resources appropriate for the specified culture or the neutral culture.  Make sure "BookAccountApp.ar_file.resources" was correctly embedded or linked into assembly "DataEntryApp" at compile time, or that all the satellite assemblies required are loadable and fully signed.', N'   at System.Resources.ManifestBasedResourceGroveler.HandleResourceStreamMissing(String fileName)
   at System.Resources.ManifestBasedResourceGroveler.GrovelForResourceSet(CultureInfo culture, Dictionary`2 localResourceSets, Boolean tryParents, Boolean createIfNotExists, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo requestedCulture, Boolean createIfNotExists, Boolean tryParents, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo culture, Boolean createIfNotExists, Boolean tryParents)
   at System.Resources.ResourceManager.GetString(String name, CultureInfo culture)
   at System.Resources.ResourceManager.GetString(String name)
   at DataEntryApp.View.settings.uc_settings.translate() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\settings\uc_settings.xaml.cs:line 77
   at DataEntryApp.View.settings.uc_settings.Window_Loaded(Object sender, RoutedEventArgs e) in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\settings\uc_settings.xaml.cs:line 56', N'Void HandleResourceStreamMissing(System.String)', CAST(N'2023-08-10T15:09:25.1215578' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2041, N'-2146233038', N'Could not find any resources appropriate for the specified culture or the neutral culture.  Make sure "BookAccountApp.ar_file.resources" was correctly embedded or linked into assembly "DataEntryApp" at compile time, or that all the satellite assemblies required are loadable and fully signed.', N'   at System.Resources.ManifestBasedResourceGroveler.HandleResourceStreamMissing(String fileName)
   at System.Resources.ManifestBasedResourceGroveler.GrovelForResourceSet(CultureInfo culture, Dictionary`2 localResourceSets, Boolean tryParents, Boolean createIfNotExists, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo requestedCulture, Boolean createIfNotExists, Boolean tryParents, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo culture, Boolean createIfNotExists, Boolean tryParents)
   at System.Resources.ResourceManager.GetString(String name, CultureInfo culture)
   at System.Resources.ResourceManager.GetString(String name)
   at DataEntryApp.View.settings.uc_settings.translate() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\settings\uc_settings.xaml.cs:line 77
   at DataEntryApp.View.settings.uc_settings.Window_Loaded(Object sender, RoutedEventArgs e) in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\settings\uc_settings.xaml.cs:line 56', N'Void HandleResourceStreamMissing(System.String)', CAST(N'2023-08-10T15:09:30.6703448' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2042, N'-2146233038', N'Could not find any resources appropriate for the specified culture or the neutral culture.  Make sure "BookAccountApp.ar_file.resources" was correctly embedded or linked into assembly "DataEntryApp" at compile time, or that all the satellite assemblies required are loadable and fully signed.', N'   at System.Resources.ManifestBasedResourceGroveler.HandleResourceStreamMissing(String fileName)
   at System.Resources.ManifestBasedResourceGroveler.GrovelForResourceSet(CultureInfo culture, Dictionary`2 localResourceSets, Boolean tryParents, Boolean createIfNotExists, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo requestedCulture, Boolean createIfNotExists, Boolean tryParents, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo culture, Boolean createIfNotExists, Boolean tryParents)
   at System.Resources.ResourceManager.GetString(String name, CultureInfo culture)
   at System.Resources.ResourceManager.GetString(String name)
   at DataEntryApp.View.settings.uc_settings.translate() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\settings\uc_settings.xaml.cs:line 77
   at DataEntryApp.View.settings.uc_settings.Window_Loaded(Object sender, RoutedEventArgs e) in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\settings\uc_settings.xaml.cs:line 56', N'Void HandleResourceStreamMissing(System.String)', CAST(N'2023-08-10T15:09:34.7045067' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2043, N'-2146233038', N'Could not find any resources appropriate for the specified culture or the neutral culture.  Make sure "BookAccountApp.ar_file.resources" was correctly embedded or linked into assembly "DataEntryApp" at compile time, or that all the satellite assemblies required are loadable and fully signed.', N'   at System.Resources.ManifestBasedResourceGroveler.HandleResourceStreamMissing(String fileName)
   at System.Resources.ManifestBasedResourceGroveler.GrovelForResourceSet(CultureInfo culture, Dictionary`2 localResourceSets, Boolean tryParents, Boolean createIfNotExists, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo requestedCulture, Boolean createIfNotExists, Boolean tryParents, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo culture, Boolean createIfNotExists, Boolean tryParents)
   at System.Resources.ResourceManager.GetString(String name, CultureInfo culture)
   at System.Resources.ResourceManager.GetString(String name)
   at DataEntryApp.View.settings.uc_settings.translate() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\settings\uc_settings.xaml.cs:line 77
   at DataEntryApp.View.settings.uc_settings.Window_Loaded(Object sender, RoutedEventArgs e) in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\settings\uc_settings.xaml.cs:line 56', N'Void HandleResourceStreamMissing(System.String)', CAST(N'2023-08-10T15:09:38.6215737' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2044, N'-2146233038', N'Could not find any resources appropriate for the specified culture or the neutral culture.  Make sure "BookAccountApp.ar_file.resources" was correctly embedded or linked into assembly "DataEntryApp" at compile time, or that all the satellite assemblies required are loadable and fully signed.', N'   at System.Resources.ManifestBasedResourceGroveler.HandleResourceStreamMissing(String fileName)
   at System.Resources.ManifestBasedResourceGroveler.GrovelForResourceSet(CultureInfo culture, Dictionary`2 localResourceSets, Boolean tryParents, Boolean createIfNotExists, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo requestedCulture, Boolean createIfNotExists, Boolean tryParents, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo culture, Boolean createIfNotExists, Boolean tryParents)
   at System.Resources.ResourceManager.GetString(String name, CultureInfo culture)
   at System.Resources.ResourceManager.GetString(String name)
   at DataEntryApp.View.settings.uc_settings.translate() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\settings\uc_settings.xaml.cs:line 77
   at DataEntryApp.View.settings.uc_settings.Window_Loaded(Object sender, RoutedEventArgs e) in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\settings\uc_settings.xaml.cs:line 56', N'Void HandleResourceStreamMissing(System.String)', CAST(N'2023-08-10T15:09:52.1360409' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2045, N'-2146233038', N'Could not find any resources appropriate for the specified culture or the neutral culture.  Make sure "BookAccountApp.ar_file.resources" was correctly embedded or linked into assembly "DataEntryApp" at compile time, or that all the satellite assemblies required are loadable and fully signed.', N'   at System.Resources.ManifestBasedResourceGroveler.HandleResourceStreamMissing(String fileName)
   at System.Resources.ManifestBasedResourceGroveler.GrovelForResourceSet(CultureInfo culture, Dictionary`2 localResourceSets, Boolean tryParents, Boolean createIfNotExists, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo requestedCulture, Boolean createIfNotExists, Boolean tryParents, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo culture, Boolean createIfNotExists, Boolean tryParents)
   at System.Resources.ResourceManager.GetString(String name, CultureInfo culture)
   at System.Resources.ResourceManager.GetString(String name)
   at DataEntryApp.View.settings.uc_settings.translate() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\settings\uc_settings.xaml.cs:line 77
   at DataEntryApp.View.settings.uc_settings.Window_Loaded(Object sender, RoutedEventArgs e) in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\settings\uc_settings.xaml.cs:line 56', N'Void HandleResourceStreamMissing(System.String)', CAST(N'2023-08-10T15:09:59.4848659' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2046, N'-2146233038', N'Could not find any resources appropriate for the specified culture or the neutral culture.  Make sure "BookAccountApp.ar_file.resources" was correctly embedded or linked into assembly "DataEntryApp" at compile time, or that all the satellite assemblies required are loadable and fully signed.', N'   at System.Resources.ManifestBasedResourceGroveler.HandleResourceStreamMissing(String fileName)
   at System.Resources.ManifestBasedResourceGroveler.GrovelForResourceSet(CultureInfo culture, Dictionary`2 localResourceSets, Boolean tryParents, Boolean createIfNotExists, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo requestedCulture, Boolean createIfNotExists, Boolean tryParents, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo culture, Boolean createIfNotExists, Boolean tryParents)
   at System.Resources.ResourceManager.GetString(String name, CultureInfo culture)
   at System.Resources.ResourceManager.GetString(String name)
   at DataEntryApp.View.settings.uc_settings.translate() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\settings\uc_settings.xaml.cs:line 77
   at DataEntryApp.View.settings.uc_settings.Window_Loaded(Object sender, RoutedEventArgs e) in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\settings\uc_settings.xaml.cs:line 56', N'Void HandleResourceStreamMissing(System.String)', CAST(N'2023-08-10T15:10:00.6190466' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2047, N'-2146233038', N'Could not find any resources appropriate for the specified culture or the neutral culture.  Make sure "BookAccountApp.ar_file.resources" was correctly embedded or linked into assembly "DataEntryApp" at compile time, or that all the satellite assemblies required are loadable and fully signed.', N'   at System.Resources.ManifestBasedResourceGroveler.HandleResourceStreamMissing(String fileName)
   at System.Resources.ManifestBasedResourceGroveler.GrovelForResourceSet(CultureInfo culture, Dictionary`2 localResourceSets, Boolean tryParents, Boolean createIfNotExists, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo requestedCulture, Boolean createIfNotExists, Boolean tryParents, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo culture, Boolean createIfNotExists, Boolean tryParents)
   at System.Resources.ResourceManager.GetString(String name, CultureInfo culture)
   at System.Resources.ResourceManager.GetString(String name)
   at DataEntryApp.View.settings.uc_settings.translate() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\settings\uc_settings.xaml.cs:line 77
   at DataEntryApp.View.settings.uc_settings.Window_Loaded(Object sender, RoutedEventArgs e) in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\settings\uc_settings.xaml.cs:line 56', N'Void HandleResourceStreamMissing(System.String)', CAST(N'2023-08-10T15:10:01.8842674' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2048, N'-2146233038', N'Could not find any resources appropriate for the specified culture or the neutral culture.  Make sure "BookAccountApp.ar_file.resources" was correctly embedded or linked into assembly "DataEntryApp" at compile time, or that all the satellite assemblies required are loadable and fully signed.', N'   at System.Resources.ManifestBasedResourceGroveler.HandleResourceStreamMissing(String fileName)
   at System.Resources.ManifestBasedResourceGroveler.GrovelForResourceSet(CultureInfo culture, Dictionary`2 localResourceSets, Boolean tryParents, Boolean createIfNotExists, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo requestedCulture, Boolean createIfNotExists, Boolean tryParents, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo culture, Boolean createIfNotExists, Boolean tryParents)
   at System.Resources.ResourceManager.GetString(String name, CultureInfo culture)
   at System.Resources.ResourceManager.GetString(String name)
   at DataEntryApp.View.settings.uc_settings.translate() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\settings\uc_settings.xaml.cs:line 77
   at DataEntryApp.View.settings.uc_settings.Window_Loaded(Object sender, RoutedEventArgs e) in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\settings\uc_settings.xaml.cs:line 56', N'Void HandleResourceStreamMissing(System.String)', CAST(N'2023-08-10T15:10:18.3338474' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2049, N'-2146233038', N'Could not find any resources appropriate for the specified culture or the neutral culture.  Make sure "BookAccountApp.ar_file.resources" was correctly embedded or linked into assembly "DataEntryApp" at compile time, or that all the satellite assemblies required are loadable and fully signed.', N'   at System.Resources.ManifestBasedResourceGroveler.HandleResourceStreamMissing(String fileName)
   at System.Resources.ManifestBasedResourceGroveler.GrovelForResourceSet(CultureInfo culture, Dictionary`2 localResourceSets, Boolean tryParents, Boolean createIfNotExists, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo requestedCulture, Boolean createIfNotExists, Boolean tryParents, StackCrawlMark& stackMark)
   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo culture, Boolean createIfNotExists, Boolean tryParents)
   at System.Resources.ResourceManager.GetString(String name, CultureInfo culture)
   at System.Resources.ResourceManager.GetString(String name)
   at DataEntryApp.View.settings.uc_settings.translate() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\settings\uc_settings.xaml.cs:line 77
   at DataEntryApp.View.settings.uc_settings.Window_Loaded(Object sender, RoutedEventArgs e) in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\settings\uc_settings.xaml.cs:line 56', N'Void HandleResourceStreamMissing(System.String)', CAST(N'2023-08-10T15:10:19.9681675' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2050, N'-2147467261', N'Object reference not set to an instance of an object.', N'   at DataEntryApp.View.applications.uc_programs.RefreshProgramsView() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 467
   at DataEntryApp.View.applications.uc_programs.<Search>d__22.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 458
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at DataEntryApp.View.applications.uc_programs.<Tgl_isActive_Checked>d__14.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 201', N'Void RefreshProgramsView()', CAST(N'2023-08-11T01:40:39.8545538' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2051, N'-2147467261', N'Object reference not set to an instance of an object.', N'   at DataEntryApp.View.applications.uc_programs.RefreshProgramsView() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 467
   at DataEntryApp.View.applications.uc_programs.<Search>d__22.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 458
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at DataEntryApp.View.applications.uc_programs.<Tgl_isActive_Checked>d__14.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 201', N'Void RefreshProgramsView()', CAST(N'2023-08-11T01:41:08.5692848' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2052, N'-2147467261', N'Object reference not set to an instance of an object.', N'   at DataEntryApp.View.applications.uc_programs.RefreshProgramsView() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 468
   at DataEntryApp.View.applications.uc_programs.<Search>d__22.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 458
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at DataEntryApp.View.applications.uc_programs.<Tgl_isActive_Checked>d__14.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 201', N'Void RefreshProgramsView()', CAST(N'2023-08-11T01:44:05.1804171' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2053, N'-2147467261', N'Object reference not set to an instance of an object.', N'   at DataEntryApp.View.applications.uc_programs.RefreshCustomerView() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 467
   at DataEntryApp.View.applications.uc_programs.<Search>d__22.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 458
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at DataEntryApp.View.applications.uc_programs.<Tgl_isActive_Checked>d__14.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 201', N'Void RefreshCustomerView()', CAST(N'2023-08-11T01:51:24.5286626' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2054, N'-2147467261', N'Object reference not set to an instance of an object.', N'   at DataEntryApp.View.applications.uc_programs.RefreshCustomerView() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 467
   at DataEntryApp.View.applications.uc_programs.<Search>d__22.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 458
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at DataEntryApp.View.applications.uc_programs.<Tgl_isActive_Checked>d__14.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 201', N'Void RefreshCustomerView()', CAST(N'2023-08-11T01:52:47.7278493' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2055, N'-2147467261', N'Object reference not set to an instance of an object.', N'   at DataEntryApp.View.applications.uc_programs.RefreshCustomerView() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 468
   at DataEntryApp.View.applications.uc_programs.<Search>d__22.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 459
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at DataEntryApp.View.applications.uc_programs.<Tgl_isActive_Checked>d__14.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 201', N'Void RefreshCustomerView()', CAST(N'2023-08-11T01:54:00.2389634' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2056, N'-2147467261', N'Object reference not set to an instance of an object.', N'   at DataEntryApp.View.applications.uc_programs.RefreshCustomerView() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 468
   at DataEntryApp.View.applications.uc_programs.<Search>d__22.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 459
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at DataEntryApp.View.applications.uc_programs.<Tgl_isActive_Checked>d__14.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 201', N'Void RefreshCustomerView()', CAST(N'2023-08-11T01:59:17.0804649' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2057, N'-2147467261', N'Object reference not set to an instance of an object.', N'   at DataEntryApp.View.applications.uc_programs.RefreshCustomerView() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 468
   at DataEntryApp.View.applications.uc_programs.<Search>d__22.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 459
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at DataEntryApp.View.applications.uc_programs.<Tgl_isActive_Checked>d__14.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 201', N'Void RefreshCustomerView()', CAST(N'2023-08-11T02:00:56.2407055' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2058, N'-2147467261', N'Object reference not set to an instance of an object.', N'   at DataEntryApp.View.applications.uc_programs.RefreshCustomerView() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 468
   at DataEntryApp.View.applications.uc_programs.<Search>d__22.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 459
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at DataEntryApp.View.applications.uc_programs.<Tgl_isActive_Checked>d__14.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 201', N'Void RefreshCustomerView()', CAST(N'2023-08-11T14:08:50.1973958' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2059, N'-2146233086', N'Index was out of range. Must be non-negative and less than the size of the collection.
Parameter name: index', N'   at System.ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument argument, ExceptionResource resource)
   at System.Collections.Generic.List`1.get_Item(Int32 index)
   at System.Collections.ObjectModel.Collection`1.get_Item(Int32 index)
   at DataEntryApp.View.applications.uc_programs.translate() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 113
   at DataEntryApp.View.applications.uc_programs.<UserControl_Loaded>d__10.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 81', N'Void ThrowArgumentOutOfRangeException(System.ExceptionArgument, System.ExceptionResource)', CAST(N'2023-08-11T14:08:50.5793318' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2060, N'-2147467261', N'Object reference not set to an instance of an object.', N'   at DataEntryApp.View.applications.uc_programs.RefreshCustomerView() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 468
   at DataEntryApp.View.applications.uc_programs.<Search>d__22.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 459
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at DataEntryApp.View.applications.uc_programs.<Tgl_isActive_Checked>d__14.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 201', N'Void RefreshCustomerView()', CAST(N'2023-08-11T14:10:35.3726142' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2061, N'-2147467261', N'Object reference not set to an instance of an object.', N'   at DataEntryApp.View.applications.uc_programs.RefreshCustomerView() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 468
   at DataEntryApp.View.applications.uc_programs.<Search>d__22.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 459
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at DataEntryApp.View.applications.uc_programs.<Tgl_isActive_Checked>d__14.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 201', N'Void RefreshCustomerView()', CAST(N'2023-08-11T14:11:29.8657137' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2062, N'-2147467261', N'Object reference not set to an instance of an object.', N'   at DataEntryApp.View.applications.uc_programs.<Search>b__22_0(Customers s) in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 455
   at System.Linq.Enumerable.WhereListIterator`1.MoveNext()
   at MS.Internal.Data.EnumerableCollectionView.LoadSnapshotCore(IEnumerable source)
   at MS.Internal.Data.EnumerableCollectionView..ctor(IEnumerable source)
   at MS.Internal.Data.ViewManager.GetViewRecord(Object collection, CollectionViewSource cvs, Type collectionViewType, Boolean createView, Func`2 GetSourceItem)
   at MS.Internal.Data.DataBindEngine.GetViewRecord(Object collection, CollectionViewSource key, Type collectionViewType, Boolean createView, Func`2 GetSourceItem)
   at System.Windows.Data.CollectionViewSource.GetDefaultCollectionView(Object source, Boolean createView, Func`2 GetSourceItem)
   at System.Windows.Data.CollectionViewSource.GetDefaultCollectionView(Object source, DependencyObject d, Func`2 GetSourceItem)
   at System.Windows.Controls.ItemCollection.SetItemsSource(IEnumerable value, Func`2 GetSourceItem)
   at System.Windows.Controls.ItemsControl.OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
   at System.Windows.DependencyObject.OnPropertyChanged(DependencyPropertyChangedEventArgs e)
   at System.Windows.FrameworkElement.OnPropertyChanged(DependencyPropertyChangedEventArgs e)
   at System.Windows.DependencyObject.NotifyPropertyChange(DependencyPropertyChangedEventArgs args)
   at System.Windows.DependencyObject.UpdateEffectiveValue(EntryIndex entryIndex, DependencyProperty dp, PropertyMetadata metadata, EffectiveValueEntry oldEntry, EffectiveValueEntry& newEntry, Boolean coerceWithDeferredReference, Boolean coerceWithCurrentValue, OperationType operationType)
   at System.Windows.DependencyObject.SetValueCommon(DependencyProperty dp, Object value, PropertyMetadata metadata, Boolean coerceWithDeferredReference, Boolean coerceWithCurrentValue, OperationType operationType, Boolean isInternal)
   at System.Windows.Controls.ItemsControl.set_ItemsSource(IEnumerable value)
   at DataEntryApp.View.applications.uc_programs.RefreshCustomerView() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 469
   at DataEntryApp.View.applications.uc_programs.<Search>d__22.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 460
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at DataEntryApp.View.applications.uc_programs.<Tb_search_TextChanged>d__13.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 185', N'Boolean <Search>b__22_0(DataEntryApp.ApiClasses.Customers)', CAST(N'2023-08-11T14:39:47.0715998' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2063, N'-2147467261', N'Object reference not set to an instance of an object.', N'   at DataEntryApp.View.applications.uc_programs.<Search>b__22_0(Customers s) in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 455
   at System.Linq.Enumerable.WhereListIterator`1.MoveNext()
   at MS.Internal.Data.EnumerableCollectionView.LoadSnapshotCore(IEnumerable source)
   at MS.Internal.Data.EnumerableCollectionView..ctor(IEnumerable source)
   at MS.Internal.Data.ViewManager.GetViewRecord(Object collection, CollectionViewSource cvs, Type collectionViewType, Boolean createView, Func`2 GetSourceItem)
   at MS.Internal.Data.DataBindEngine.GetViewRecord(Object collection, CollectionViewSource key, Type collectionViewType, Boolean createView, Func`2 GetSourceItem)
   at System.Windows.Data.CollectionViewSource.GetDefaultCollectionView(Object source, Boolean createView, Func`2 GetSourceItem)
   at System.Windows.Data.CollectionViewSource.GetDefaultCollectionView(Object source, DependencyObject d, Func`2 GetSourceItem)
   at System.Windows.Controls.ItemCollection.SetItemsSource(IEnumerable value, Func`2 GetSourceItem)
   at System.Windows.Controls.ItemsControl.OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
   at System.Windows.DependencyObject.OnPropertyChanged(DependencyPropertyChangedEventArgs e)
   at System.Windows.FrameworkElement.OnPropertyChanged(DependencyPropertyChangedEventArgs e)
   at System.Windows.DependencyObject.NotifyPropertyChange(DependencyPropertyChangedEventArgs args)
   at System.Windows.DependencyObject.UpdateEffectiveValue(EntryIndex entryIndex, DependencyProperty dp, PropertyMetadata metadata, EffectiveValueEntry oldEntry, EffectiveValueEntry& newEntry, Boolean coerceWithDeferredReference, Boolean coerceWithCurrentValue, OperationType operationType)
   at System.Windows.DependencyObject.SetValueCommon(DependencyProperty dp, Object value, PropertyMetadata metadata, Boolean coerceWithDeferredReference, Boolean coerceWithCurrentValue, OperationType operationType, Boolean isInternal)
   at System.Windows.Controls.ItemsControl.set_ItemsSource(IEnumerable value)
   at DataEntryApp.View.applications.uc_programs.RefreshCustomerView() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 469
   at DataEntryApp.View.applications.uc_programs.<Search>d__22.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 460
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at DataEntryApp.View.applications.uc_programs.<Tb_search_TextChanged>d__13.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 185', N'Boolean <Search>b__22_0(DataEntryApp.ApiClasses.Customers)', CAST(N'2023-08-11T14:39:51.6882933' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2064, N'-2147467261', N'Object reference not set to an instance of an object.', N'   at DataEntryApp.View.applications.uc_programs.<Search>b__22_0(Customers s) in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 455
   at System.Linq.Enumerable.WhereListIterator`1.MoveNext()
   at MS.Internal.Data.EnumerableCollectionView.LoadSnapshotCore(IEnumerable source)
   at MS.Internal.Data.EnumerableCollectionView..ctor(IEnumerable source)
   at MS.Internal.Data.ViewManager.GetViewRecord(Object collection, CollectionViewSource cvs, Type collectionViewType, Boolean createView, Func`2 GetSourceItem)
   at MS.Internal.Data.DataBindEngine.GetViewRecord(Object collection, CollectionViewSource key, Type collectionViewType, Boolean createView, Func`2 GetSourceItem)
   at System.Windows.Data.CollectionViewSource.GetDefaultCollectionView(Object source, Boolean createView, Func`2 GetSourceItem)
   at System.Windows.Data.CollectionViewSource.GetDefaultCollectionView(Object source, DependencyObject d, Func`2 GetSourceItem)
   at System.Windows.Controls.ItemCollection.SetItemsSource(IEnumerable value, Func`2 GetSourceItem)
   at System.Windows.Controls.ItemsControl.OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
   at System.Windows.DependencyObject.OnPropertyChanged(DependencyPropertyChangedEventArgs e)
   at System.Windows.FrameworkElement.OnPropertyChanged(DependencyPropertyChangedEventArgs e)
   at System.Windows.DependencyObject.NotifyPropertyChange(DependencyPropertyChangedEventArgs args)
   at System.Windows.DependencyObject.UpdateEffectiveValue(EntryIndex entryIndex, DependencyProperty dp, PropertyMetadata metadata, EffectiveValueEntry oldEntry, EffectiveValueEntry& newEntry, Boolean coerceWithDeferredReference, Boolean coerceWithCurrentValue, OperationType operationType)
   at System.Windows.DependencyObject.SetValueCommon(DependencyProperty dp, Object value, PropertyMetadata metadata, Boolean coerceWithDeferredReference, Boolean coerceWithCurrentValue, OperationType operationType, Boolean isInternal)
   at System.Windows.Controls.ItemsControl.set_ItemsSource(IEnumerable value)
   at DataEntryApp.View.applications.uc_programs.RefreshCustomerView() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 469
   at DataEntryApp.View.applications.uc_programs.<Search>d__22.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 460
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at DataEntryApp.View.applications.uc_programs.<Tb_search_TextChanged>d__13.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 185', N'Boolean <Search>b__22_0(DataEntryApp.ApiClasses.Customers)', CAST(N'2023-08-11T14:39:53.5550805' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2065, N'-2147467261', N'Object reference not set to an instance of an object.', N'   at DataEntryApp.View.applications.uc_programs.<Search>b__22_1(Customers s) in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 460
   at System.Linq.Enumerable.WhereListIterator`1.MoveNext()
   at MS.Internal.Data.EnumerableCollectionView.LoadSnapshotCore(IEnumerable source)
   at MS.Internal.Data.EnumerableCollectionView..ctor(IEnumerable source)
   at MS.Internal.Data.ViewManager.GetViewRecord(Object collection, CollectionViewSource cvs, Type collectionViewType, Boolean createView, Func`2 GetSourceItem)
   at MS.Internal.Data.DataBindEngine.GetViewRecord(Object collection, CollectionViewSource key, Type collectionViewType, Boolean createView, Func`2 GetSourceItem)
   at System.Windows.Data.CollectionViewSource.GetDefaultCollectionView(Object source, Boolean createView, Func`2 GetSourceItem)
   at System.Windows.Data.CollectionViewSource.GetDefaultCollectionView(Object source, DependencyObject d, Func`2 GetSourceItem)
   at System.Windows.Controls.ItemCollection.SetItemsSource(IEnumerable value, Func`2 GetSourceItem)
   at System.Windows.Controls.ItemsControl.OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
   at System.Windows.DependencyObject.OnPropertyChanged(DependencyPropertyChangedEventArgs e)
   at System.Windows.FrameworkElement.OnPropertyChanged(DependencyPropertyChangedEventArgs e)
   at System.Windows.DependencyObject.NotifyPropertyChange(DependencyPropertyChangedEventArgs args)
   at System.Windows.DependencyObject.UpdateEffectiveValue(EntryIndex entryIndex, DependencyProperty dp, PropertyMetadata metadata, EffectiveValueEntry oldEntry, EffectiveValueEntry& newEntry, Boolean coerceWithDeferredReference, Boolean coerceWithCurrentValue, OperationType operationType)
   at System.Windows.DependencyObject.SetValueCommon(DependencyProperty dp, Object value, PropertyMetadata metadata, Boolean coerceWithDeferredReference, Boolean coerceWithCurrentValue, OperationType operationType, Boolean isInternal)
   at System.Windows.Controls.ItemsControl.set_ItemsSource(IEnumerable value)
   at DataEntryApp.View.applications.uc_programs.RefreshCustomerView() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 476
   at DataEntryApp.View.applications.uc_programs.<Search>d__22.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 467
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at DataEntryApp.View.applications.uc_programs.<Tb_search_TextChanged>d__13.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 185', N'Boolean <Search>b__22_1(DataEntryApp.ApiClasses.Customers)', CAST(N'2023-08-11T14:48:20.4265656' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2066, N'-2147467261', N'Object reference not set to an instance of an object.', N'   at DataEntryApp.View.applications.uc_programs.<Search>b__22_1(Customers s) in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 460
   at System.Linq.Enumerable.WhereListIterator`1.MoveNext()
   at MS.Internal.Data.EnumerableCollectionView.LoadSnapshotCore(IEnumerable source)
   at MS.Internal.Data.EnumerableCollectionView..ctor(IEnumerable source)
   at MS.Internal.Data.ViewManager.GetViewRecord(Object collection, CollectionViewSource cvs, Type collectionViewType, Boolean createView, Func`2 GetSourceItem)
   at MS.Internal.Data.DataBindEngine.GetViewRecord(Object collection, CollectionViewSource key, Type collectionViewType, Boolean createView, Func`2 GetSourceItem)
   at System.Windows.Data.CollectionViewSource.GetDefaultCollectionView(Object source, Boolean createView, Func`2 GetSourceItem)
   at System.Windows.Data.CollectionViewSource.GetDefaultCollectionView(Object source, DependencyObject d, Func`2 GetSourceItem)
   at System.Windows.Controls.ItemCollection.SetItemsSource(IEnumerable value, Func`2 GetSourceItem)
   at System.Windows.Controls.ItemsControl.OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
   at System.Windows.DependencyObject.OnPropertyChanged(DependencyPropertyChangedEventArgs e)
   at System.Windows.FrameworkElement.OnPropertyChanged(DependencyPropertyChangedEventArgs e)
   at System.Windows.DependencyObject.NotifyPropertyChange(DependencyPropertyChangedEventArgs args)
   at System.Windows.DependencyObject.UpdateEffectiveValue(EntryIndex entryIndex, DependencyProperty dp, PropertyMetadata metadata, EffectiveValueEntry oldEntry, EffectiveValueEntry& newEntry, Boolean coerceWithDeferredReference, Boolean coerceWithCurrentValue, OperationType operationType)
   at System.Windows.DependencyObject.SetValueCommon(DependencyProperty dp, Object value, PropertyMetadata metadata, Boolean coerceWithDeferredReference, Boolean coerceWithCurrentValue, OperationType operationType, Boolean isInternal)
   at System.Windows.Controls.ItemsControl.set_ItemsSource(IEnumerable value)
   at DataEntryApp.View.applications.uc_programs.RefreshCustomerView() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 476
   at DataEntryApp.View.applications.uc_programs.<Search>d__22.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 467
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at DataEntryApp.View.applications.uc_programs.<Tb_search_TextChanged>d__13.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 185', N'Boolean <Search>b__22_1(DataEntryApp.ApiClasses.Customers)', CAST(N'2023-08-11T14:48:34.4636510' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2067, N'-2147467261', N'Object reference not set to an instance of an object.', N'   at DataEntryApp.View.applications.uc_programs.<Search>b__22_1(Customers s) in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 460
   at System.Linq.Enumerable.WhereListIterator`1.MoveNext()
   at MS.Internal.Data.EnumerableCollectionView.LoadSnapshotCore(IEnumerable source)
   at MS.Internal.Data.EnumerableCollectionView..ctor(IEnumerable source)
   at MS.Internal.Data.ViewManager.GetViewRecord(Object collection, CollectionViewSource cvs, Type collectionViewType, Boolean createView, Func`2 GetSourceItem)
   at MS.Internal.Data.DataBindEngine.GetViewRecord(Object collection, CollectionViewSource key, Type collectionViewType, Boolean createView, Func`2 GetSourceItem)
   at System.Windows.Data.CollectionViewSource.GetDefaultCollectionView(Object source, Boolean createView, Func`2 GetSourceItem)
   at System.Windows.Data.CollectionViewSource.GetDefaultCollectionView(Object source, DependencyObject d, Func`2 GetSourceItem)
   at System.Windows.Controls.ItemCollection.SetItemsSource(IEnumerable value, Func`2 GetSourceItem)
   at System.Windows.Controls.ItemsControl.OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
   at System.Windows.DependencyObject.OnPropertyChanged(DependencyPropertyChangedEventArgs e)
   at System.Windows.FrameworkElement.OnPropertyChanged(DependencyPropertyChangedEventArgs e)
   at System.Windows.DependencyObject.NotifyPropertyChange(DependencyPropertyChangedEventArgs args)
   at System.Windows.DependencyObject.UpdateEffectiveValue(EntryIndex entryIndex, DependencyProperty dp, PropertyMetadata metadata, EffectiveValueEntry oldEntry, EffectiveValueEntry& newEntry, Boolean coerceWithDeferredReference, Boolean coerceWithCurrentValue, OperationType operationType)
   at System.Windows.DependencyObject.SetValueCommon(DependencyProperty dp, Object value, PropertyMetadata metadata, Boolean coerceWithDeferredReference, Boolean coerceWithCurrentValue, OperationType operationType, Boolean isInternal)
   at System.Windows.Controls.ItemsControl.set_ItemsSource(IEnumerable value)
   at DataEntryApp.View.applications.uc_programs.RefreshCustomerView() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 476
   at DataEntryApp.View.applications.uc_programs.<Search>d__22.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 467
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at DataEntryApp.View.applications.uc_programs.<Tb_search_TextChanged>d__13.MoveNext() in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 185', N'Boolean <Search>b__22_1(DataEntryApp.ApiClasses.Customers)', CAST(N'2023-08-11T14:50:23.4250701' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2068, N'-2146233033', N'Input string was not in a correct format.', N'   at System.Number.StringToNumber(String str, NumberStyles options, NumberBuffer& number, NumberFormatInfo info, Boolean parseDecimal)
   at System.Number.ParseInt64(String value, NumberStyles options, NumberFormatInfo numfmt)
   at System.String.System.IConvertible.ToInt64(IFormatProvider provider)
   at System.Convert.ChangeType(Object value, Type conversionType, IFormatProvider provider)
   at MS.Internal.Data.SystemConvertConverter.Convert(Object o, Type type, Object parameter, CultureInfo culture)
   at MS.Internal.Data.DynamicValueConverter.Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
   at System.Windows.Controls.Primitives.Selector.VerifyEqual(Object knownValue, Type knownType, Object itemValue, DynamicValueConverter converter)
   at System.Windows.Controls.Primitives.Selector.FindItemWithValue(Object value, Int32& index)
   at System.Windows.Controls.Primitives.Selector.SelectItemWithValue(Object value, Boolean selectNow)
   at System.Windows.Controls.Primitives.Selector.CoerceSelectedValue(DependencyObject d, Object value)
   at System.Windows.DependencyObject.ProcessCoerceValue(DependencyProperty dp, PropertyMetadata metadata, EntryIndex& entryIndex, Int32& targetIndex, EffectiveValueEntry& newEntry, EffectiveValueEntry& oldEntry, Object& oldValue, Object baseValue, Object controlValue, CoerceValueCallback coerceValueCallback, Boolean coerceWithDeferredReference, Boolean coerceWithCurrentValue, Boolean skipBaseValueChecks)
   at System.Windows.DependencyObject.UpdateEffectiveValue(EntryIndex entryIndex, DependencyProperty dp, PropertyMetadata metadata, EffectiveValueEntry oldEntry, EffectiveValueEntry& newEntry, Boolean coerceWithDeferredReference, Boolean coerceWithCurrentValue, OperationType operationType)
   at System.Windows.DependencyObject.InvalidateProperty(DependencyProperty dp, Boolean preserveCurrentValue)
   at System.Windows.Data.BindingExpressionBase.Invalidate(Boolean isASubPropertyChange)
   at System.Windows.Data.BindingExpression.TransferValue(Object newValue, Boolean isASubPropertyChange)
   at System.Windows.Data.BindingExpression.Activate(Object item)
   at System.Windows.Data.BindingExpression.OnDataContextChanged(DependencyObject contextElement)
   at System.Windows.Data.BindingExpression.HandlePropertyInvalidation(DependencyObject d, DependencyPropertyChangedEventArgs args)
   at System.Windows.Data.BindingExpressionBase.OnPropertyInvalidation(DependencyObject d, DependencyPropertyChangedEventArgs args)
   at System.Windows.Data.BindingExpression.OnPropertyInvalidation(DependencyObject d, DependencyPropertyChangedEventArgs args)
   at System.Windows.DependentList.InvalidateDependents(DependencyObject source, DependencyPropertyChangedEventArgs sourceArgs)
   at System.Windows.DependencyObject.NotifyPropertyChange(DependencyPropertyChangedEventArgs args)
   at System.Windows.DependencyObject.UpdateEffectiveValue(EntryIndex entryIndex, DependencyProperty dp, PropertyMetadata metadata, EffectiveValueEntry oldEntry, EffectiveValueEntry& newEntry, Boolean coerceWithDeferredReference, Boolean coerceWithCurrentValue, OperationType operationType)
   at System.Windows.TreeWalkHelper.OnInheritablePropertyChanged(DependencyObject d, InheritablePropertyChangeInfo info, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1._VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(FrameworkElement fe, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.WalkLogicalChildren(FrameworkElement feParent, FrameworkContentElement fceParent, IEnumerator logicalChildren)
   at System.Windows.DescendentsWalker`1.WalkFrameworkElementLogicalThenVisualChildren(FrameworkElement feParent, Boolean hasLogicalChildren)
   at System.Windows.DescendentsWalker`1.IterateChildren(DependencyObject d)
   at System.Windows.DescendentsWalker`1._VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(FrameworkElement fe, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.WalkLogicalChildren(FrameworkElement feParent, FrameworkContentElement fceParent, IEnumerator logicalChildren)
   at System.Windows.DescendentsWalker`1.WalkFrameworkElementLogicalThenVisualChildren(FrameworkElement feParent, Boolean hasLogicalChildren)
   at System.Windows.DescendentsWalker`1.IterateChildren(DependencyObject d)
   at System.Windows.DescendentsWalker`1._VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(FrameworkElement fe, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.WalkLogicalChildren(FrameworkElement feParent, FrameworkContentElement fceParent, IEnumerator logicalChildren)
   at System.Windows.DescendentsWalker`1.WalkFrameworkElementLogicalThenVisualChildren(FrameworkElement feParent, Boolean hasLogicalChildren)
   at System.Windows.DescendentsWalker`1.IterateChildren(DependencyObject d)
   at System.Windows.DescendentsWalker`1._VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(FrameworkElement fe, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.WalkLogicalChildren(FrameworkElement feParent, FrameworkContentElement fceParent, IEnumerator logicalChildren)
   at System.Windows.DescendentsWalker`1.WalkFrameworkElementLogicalThenVisualChildren(FrameworkElement feParent, Boolean hasLogicalChildren)
   at System.Windows.DescendentsWalker`1.IterateChildren(DependencyObject d)
   at System.Windows.DescendentsWalker`1._VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(FrameworkElement fe, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.WalkLogicalChildren(FrameworkElement feParent, FrameworkContentElement fceParent, IEnumerator logicalChildren)
   at System.Windows.DescendentsWalker`1.WalkFrameworkElementLogicalThenVisualChildren(FrameworkElement feParent, Boolean hasLogicalChildren)
   at System.Windows.DescendentsWalker`1.IterateChildren(DependencyObject d)
   at System.Windows.DescendentsWalker`1._VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(FrameworkElement fe, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.WalkLogicalChildren(FrameworkElement feParent, FrameworkContentElement fceParent, IEnumerator logicalChildren)
   at System.Windows.DescendentsWalker`1.WalkFrameworkElementLogicalThenVisualChildren(FrameworkElement feParent, Boolean hasLogicalChildren)
   at System.Windows.DescendentsWalker`1.IterateChildren(DependencyObject d)
   at System.Windows.DescendentsWalker`1._VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(FrameworkElement fe, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.WalkLogicalChildren(FrameworkElement feParent, FrameworkContentElement fceParent, IEnumerator logicalChildren)
   at System.Windows.DescendentsWalker`1.WalkFrameworkElementLogicalThenVisualChildren(FrameworkElement feParent, Boolean hasLogicalChildren)
   at System.Windows.DescendentsWalker`1.IterateChildren(DependencyObject d)
   at System.Windows.DescendentsWalker`1.StartWalk(DependencyObject startNode, Boolean skipStartNode)
   at System.Windows.FrameworkElement.OnPropertyChanged(DependencyPropertyChangedEventArgs e)
   at System.Windows.DependencyObject.NotifyPropertyChange(DependencyPropertyChangedEventArgs args)
   at System.Windows.DependencyObject.UpdateEffectiveValue(EntryIndex entryIndex, DependencyProperty dp, PropertyMetadata metadata, EffectiveValueEntry oldEntry, EffectiveValueEntry& newEntry, Boolean coerceWithDeferredReference, Boolean coerceWithCurrentValue, OperationType operationType)
   at System.Windows.DependencyObject.SetValueCommon(DependencyProperty dp, Object value, PropertyMetadata metadata, Boolean coerceWithDeferredReference, Boolean coerceWithCurrentValue, OperationType operationType, Boolean isInternal)
   at System.Windows.FrameworkElement.set_DataContext(Object value)
   at DataEntryApp.View.applications.uc_programs.Dg_customer_SelectionChanged(Object sender, SelectionChangedEventArgs e) in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 787', N'Void StringToNumber(System.String, System.Globalization.NumberStyles, NumberBuffer ByRef, System.Globalization.NumberFormatInfo, Boolean)', CAST(N'2023-08-12T02:14:46.1224293' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2069, N'-2146233033', N'Input string was not in a correct format.', N'   at System.Number.StringToNumber(String str, NumberStyles options, NumberBuffer& number, NumberFormatInfo info, Boolean parseDecimal)
   at System.Number.ParseInt64(String value, NumberStyles options, NumberFormatInfo numfmt)
   at System.String.System.IConvertible.ToInt64(IFormatProvider provider)
   at System.Convert.ChangeType(Object value, Type conversionType, IFormatProvider provider)
   at MS.Internal.Data.SystemConvertConverter.Convert(Object o, Type type, Object parameter, CultureInfo culture)
   at MS.Internal.Data.DynamicValueConverter.Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
   at System.Windows.Controls.Primitives.Selector.VerifyEqual(Object knownValue, Type knownType, Object itemValue, DynamicValueConverter converter)
   at System.Windows.Controls.Primitives.Selector.FindItemWithValue(Object value, Int32& index)
   at System.Windows.Controls.Primitives.Selector.SelectItemWithValue(Object value, Boolean selectNow)
   at System.Windows.Controls.Primitives.Selector.CoerceSelectedValue(DependencyObject d, Object value)
   at System.Windows.DependencyObject.ProcessCoerceValue(DependencyProperty dp, PropertyMetadata metadata, EntryIndex& entryIndex, Int32& targetIndex, EffectiveValueEntry& newEntry, EffectiveValueEntry& oldEntry, Object& oldValue, Object baseValue, Object controlValue, CoerceValueCallback coerceValueCallback, Boolean coerceWithDeferredReference, Boolean coerceWithCurrentValue, Boolean skipBaseValueChecks)
   at System.Windows.DependencyObject.UpdateEffectiveValue(EntryIndex entryIndex, DependencyProperty dp, PropertyMetadata metadata, EffectiveValueEntry oldEntry, EffectiveValueEntry& newEntry, Boolean coerceWithDeferredReference, Boolean coerceWithCurrentValue, OperationType operationType)
   at System.Windows.DependencyObject.InvalidateProperty(DependencyProperty dp, Boolean preserveCurrentValue)
   at System.Windows.Data.BindingExpressionBase.Invalidate(Boolean isASubPropertyChange)
   at System.Windows.Data.BindingExpression.TransferValue(Object newValue, Boolean isASubPropertyChange)
   at System.Windows.Data.BindingExpression.Activate(Object item)
   at System.Windows.Data.BindingExpression.OnDataContextChanged(DependencyObject contextElement)
   at System.Windows.Data.BindingExpression.HandlePropertyInvalidation(DependencyObject d, DependencyPropertyChangedEventArgs args)
   at System.Windows.Data.BindingExpressionBase.OnPropertyInvalidation(DependencyObject d, DependencyPropertyChangedEventArgs args)
   at System.Windows.Data.BindingExpression.OnPropertyInvalidation(DependencyObject d, DependencyPropertyChangedEventArgs args)
   at System.Windows.DependentList.InvalidateDependents(DependencyObject source, DependencyPropertyChangedEventArgs sourceArgs)
   at System.Windows.DependencyObject.NotifyPropertyChange(DependencyPropertyChangedEventArgs args)
   at System.Windows.DependencyObject.UpdateEffectiveValue(EntryIndex entryIndex, DependencyProperty dp, PropertyMetadata metadata, EffectiveValueEntry oldEntry, EffectiveValueEntry& newEntry, Boolean coerceWithDeferredReference, Boolean coerceWithCurrentValue, OperationType operationType)
   at System.Windows.TreeWalkHelper.OnInheritablePropertyChanged(DependencyObject d, InheritablePropertyChangeInfo info, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1._VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(FrameworkElement fe, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.WalkLogicalChildren(FrameworkElement feParent, FrameworkContentElement fceParent, IEnumerator logicalChildren)
   at System.Windows.DescendentsWalker`1.WalkFrameworkElementLogicalThenVisualChildren(FrameworkElement feParent, Boolean hasLogicalChildren)
   at System.Windows.DescendentsWalker`1.IterateChildren(DependencyObject d)
   at System.Windows.DescendentsWalker`1._VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(FrameworkElement fe, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.WalkLogicalChildren(FrameworkElement feParent, FrameworkContentElement fceParent, IEnumerator logicalChildren)
   at System.Windows.DescendentsWalker`1.WalkFrameworkElementLogicalThenVisualChildren(FrameworkElement feParent, Boolean hasLogicalChildren)
   at System.Windows.DescendentsWalker`1.IterateChildren(DependencyObject d)
   at System.Windows.DescendentsWalker`1._VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(FrameworkElement fe, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.WalkLogicalChildren(FrameworkElement feParent, FrameworkContentElement fceParent, IEnumerator logicalChildren)
   at System.Windows.DescendentsWalker`1.WalkFrameworkElementLogicalThenVisualChildren(FrameworkElement feParent, Boolean hasLogicalChildren)
   at System.Windows.DescendentsWalker`1.IterateChildren(DependencyObject d)
   at System.Windows.DescendentsWalker`1._VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(FrameworkElement fe, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.WalkLogicalChildren(FrameworkElement feParent, FrameworkContentElement fceParent, IEnumerator logicalChildren)
   at System.Windows.DescendentsWalker`1.WalkFrameworkElementLogicalThenVisualChildren(FrameworkElement feParent, Boolean hasLogicalChildren)
   at System.Windows.DescendentsWalker`1.IterateChildren(DependencyObject d)
   at System.Windows.DescendentsWalker`1._VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(FrameworkElement fe, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.WalkLogicalChildren(FrameworkElement feParent, FrameworkContentElement fceParent, IEnumerator logicalChildren)
   at System.Windows.DescendentsWalker`1.WalkFrameworkElementLogicalThenVisualChildren(FrameworkElement feParent, Boolean hasLogicalChildren)
   at System.Windows.DescendentsWalker`1.IterateChildren(DependencyObject d)
   at System.Windows.DescendentsWalker`1._VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(FrameworkElement fe, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.WalkLogicalChildren(FrameworkElement feParent, FrameworkContentElement fceParent, IEnumerator logicalChildren)
   at System.Windows.DescendentsWalker`1.WalkFrameworkElementLogicalThenVisualChildren(FrameworkElement feParent, Boolean hasLogicalChildren)
   at System.Windows.DescendentsWalker`1.IterateChildren(DependencyObject d)
   at System.Windows.DescendentsWalker`1._VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(FrameworkElement fe, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.WalkLogicalChildren(FrameworkElement feParent, FrameworkContentElement fceParent, IEnumerator logicalChildren)
   at System.Windows.DescendentsWalker`1.WalkFrameworkElementLogicalThenVisualChildren(FrameworkElement feParent, Boolean hasLogicalChildren)
   at System.Windows.DescendentsWalker`1.IterateChildren(DependencyObject d)
   at System.Windows.DescendentsWalker`1.StartWalk(DependencyObject startNode, Boolean skipStartNode)
   at System.Windows.FrameworkElement.OnPropertyChanged(DependencyPropertyChangedEventArgs e)
   at System.Windows.DependencyObject.NotifyPropertyChange(DependencyPropertyChangedEventArgs args)
   at System.Windows.DependencyObject.UpdateEffectiveValue(EntryIndex entryIndex, DependencyProperty dp, PropertyMetadata metadata, EffectiveValueEntry oldEntry, EffectiveValueEntry& newEntry, Boolean coerceWithDeferredReference, Boolean coerceWithCurrentValue, OperationType operationType)
   at System.Windows.DependencyObject.SetValueCommon(DependencyProperty dp, Object value, PropertyMetadata metadata, Boolean coerceWithDeferredReference, Boolean coerceWithCurrentValue, OperationType operationType, Boolean isInternal)
   at System.Windows.FrameworkElement.set_DataContext(Object value)
   at DataEntryApp.View.applications.uc_programs.Dg_customer_SelectionChanged(Object sender, SelectionChangedEventArgs e) in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 787', N'Void StringToNumber(System.String, System.Globalization.NumberStyles, NumberBuffer ByRef, System.Globalization.NumberFormatInfo, Boolean)', CAST(N'2023-08-12T02:14:51.1752255' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2070, N'-2146233033', N'Input string was not in a correct format.', N'   at System.Number.StringToNumber(String str, NumberStyles options, NumberBuffer& number, NumberFormatInfo info, Boolean parseDecimal)
   at System.Number.ParseInt64(String value, NumberStyles options, NumberFormatInfo numfmt)
   at System.String.System.IConvertible.ToInt64(IFormatProvider provider)
   at System.Convert.ChangeType(Object value, Type conversionType, IFormatProvider provider)
   at MS.Internal.Data.SystemConvertConverter.Convert(Object o, Type type, Object parameter, CultureInfo culture)
   at MS.Internal.Data.DynamicValueConverter.Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
   at System.Windows.Controls.Primitives.Selector.VerifyEqual(Object knownValue, Type knownType, Object itemValue, DynamicValueConverter converter)
   at System.Windows.Controls.Primitives.Selector.FindItemWithValue(Object value, Int32& index)
   at System.Windows.Controls.Primitives.Selector.SelectItemWithValue(Object value, Boolean selectNow)
   at System.Windows.Controls.Primitives.Selector.CoerceSelectedValue(DependencyObject d, Object value)
   at System.Windows.DependencyObject.ProcessCoerceValue(DependencyProperty dp, PropertyMetadata metadata, EntryIndex& entryIndex, Int32& targetIndex, EffectiveValueEntry& newEntry, EffectiveValueEntry& oldEntry, Object& oldValue, Object baseValue, Object controlValue, CoerceValueCallback coerceValueCallback, Boolean coerceWithDeferredReference, Boolean coerceWithCurrentValue, Boolean skipBaseValueChecks)
   at System.Windows.DependencyObject.UpdateEffectiveValue(EntryIndex entryIndex, DependencyProperty dp, PropertyMetadata metadata, EffectiveValueEntry oldEntry, EffectiveValueEntry& newEntry, Boolean coerceWithDeferredReference, Boolean coerceWithCurrentValue, OperationType operationType)
   at System.Windows.DependencyObject.InvalidateProperty(DependencyProperty dp, Boolean preserveCurrentValue)
   at System.Windows.Data.BindingExpressionBase.Invalidate(Boolean isASubPropertyChange)
   at System.Windows.Data.BindingExpression.TransferValue(Object newValue, Boolean isASubPropertyChange)
   at System.Windows.Data.BindingExpression.Activate(Object item)
   at System.Windows.Data.BindingExpression.OnDataContextChanged(DependencyObject contextElement)
   at System.Windows.Data.BindingExpression.HandlePropertyInvalidation(DependencyObject d, DependencyPropertyChangedEventArgs args)
   at System.Windows.Data.BindingExpressionBase.OnPropertyInvalidation(DependencyObject d, DependencyPropertyChangedEventArgs args)
   at System.Windows.Data.BindingExpression.OnPropertyInvalidation(DependencyObject d, DependencyPropertyChangedEventArgs args)
   at System.Windows.DependentList.InvalidateDependents(DependencyObject source, DependencyPropertyChangedEventArgs sourceArgs)
   at System.Windows.DependencyObject.NotifyPropertyChange(DependencyPropertyChangedEventArgs args)
   at System.Windows.DependencyObject.UpdateEffectiveValue(EntryIndex entryIndex, DependencyProperty dp, PropertyMetadata metadata, EffectiveValueEntry oldEntry, EffectiveValueEntry& newEntry, Boolean coerceWithDeferredReference, Boolean coerceWithCurrentValue, OperationType operationType)
   at System.Windows.TreeWalkHelper.OnInheritablePropertyChanged(DependencyObject d, InheritablePropertyChangeInfo info, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1._VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(FrameworkElement fe, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.WalkLogicalChildren(FrameworkElement feParent, FrameworkContentElement fceParent, IEnumerator logicalChildren)
   at System.Windows.DescendentsWalker`1.WalkFrameworkElementLogicalThenVisualChildren(FrameworkElement feParent, Boolean hasLogicalChildren)
   at System.Windows.DescendentsWalker`1.IterateChildren(DependencyObject d)
   at System.Windows.DescendentsWalker`1._VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(FrameworkElement fe, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.WalkLogicalChildren(FrameworkElement feParent, FrameworkContentElement fceParent, IEnumerator logicalChildren)
   at System.Windows.DescendentsWalker`1.WalkFrameworkElementLogicalThenVisualChildren(FrameworkElement feParent, Boolean hasLogicalChildren)
   at System.Windows.DescendentsWalker`1.IterateChildren(DependencyObject d)
   at System.Windows.DescendentsWalker`1._VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(FrameworkElement fe, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.WalkLogicalChildren(FrameworkElement feParent, FrameworkContentElement fceParent, IEnumerator logicalChildren)
   at System.Windows.DescendentsWalker`1.WalkFrameworkElementLogicalThenVisualChildren(FrameworkElement feParent, Boolean hasLogicalChildren)
   at System.Windows.DescendentsWalker`1.IterateChildren(DependencyObject d)
   at System.Windows.DescendentsWalker`1._VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(FrameworkElement fe, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.WalkLogicalChildren(FrameworkElement feParent, FrameworkContentElement fceParent, IEnumerator logicalChildren)
   at System.Windows.DescendentsWalker`1.WalkFrameworkElementLogicalThenVisualChildren(FrameworkElement feParent, Boolean hasLogicalChildren)
   at System.Windows.DescendentsWalker`1.IterateChildren(DependencyObject d)
   at System.Windows.DescendentsWalker`1._VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(FrameworkElement fe, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.WalkLogicalChildren(FrameworkElement feParent, FrameworkContentElement fceParent, IEnumerator logicalChildren)
   at System.Windows.DescendentsWalker`1.WalkFrameworkElementLogicalThenVisualChildren(FrameworkElement feParent, Boolean hasLogicalChildren)
   at System.Windows.DescendentsWalker`1.IterateChildren(DependencyObject d)
   at System.Windows.DescendentsWalker`1._VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(FrameworkElement fe, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.WalkLogicalChildren(FrameworkElement feParent, FrameworkContentElement fceParent, IEnumerator logicalChildren)
   at System.Windows.DescendentsWalker`1.WalkFrameworkElementLogicalThenVisualChildren(FrameworkElement feParent, Boolean hasLogicalChildren)
   at System.Windows.DescendentsWalker`1.IterateChildren(DependencyObject d)
   at System.Windows.DescendentsWalker`1._VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(FrameworkElement fe, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.WalkLogicalChildren(FrameworkElement feParent, FrameworkContentElement fceParent, IEnumerator logicalChildren)
   at System.Windows.DescendentsWalker`1.WalkFrameworkElementLogicalThenVisualChildren(FrameworkElement feParent, Boolean hasLogicalChildren)
   at System.Windows.DescendentsWalker`1.IterateChildren(DependencyObject d)
   at System.Windows.DescendentsWalker`1.StartWalk(DependencyObject startNode, Boolean skipStartNode)
   at System.Windows.FrameworkElement.OnPropertyChanged(DependencyPropertyChangedEventArgs e)
   at System.Windows.DependencyObject.NotifyPropertyChange(DependencyPropertyChangedEventArgs args)
   at System.Windows.DependencyObject.UpdateEffectiveValue(EntryIndex entryIndex, DependencyProperty dp, PropertyMetadata metadata, EffectiveValueEntry oldEntry, EffectiveValueEntry& newEntry, Boolean coerceWithDeferredReference, Boolean coerceWithCurrentValue, OperationType operationType)
   at System.Windows.DependencyObject.SetValueCommon(DependencyProperty dp, Object value, PropertyMetadata metadata, Boolean coerceWithDeferredReference, Boolean coerceWithCurrentValue, OperationType operationType, Boolean isInternal)
   at System.Windows.FrameworkElement.set_DataContext(Object value)
   at DataEntryApp.View.applications.uc_programs.Dg_customer_SelectionChanged(Object sender, SelectionChangedEventArgs e) in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 787', N'Void StringToNumber(System.String, System.Globalization.NumberStyles, NumberBuffer ByRef, System.Globalization.NumberFormatInfo, Boolean)', CAST(N'2023-08-12T02:14:53.2584967' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2071, N'-2146233033', N'Input string was not in a correct format.', N'   at System.Number.StringToNumber(String str, NumberStyles options, NumberBuffer& number, NumberFormatInfo info, Boolean parseDecimal)
   at System.Number.ParseInt64(String value, NumberStyles options, NumberFormatInfo numfmt)
   at System.String.System.IConvertible.ToInt64(IFormatProvider provider)
   at System.Convert.ChangeType(Object value, Type conversionType, IFormatProvider provider)
   at MS.Internal.Data.SystemConvertConverter.Convert(Object o, Type type, Object parameter, CultureInfo culture)
   at MS.Internal.Data.DynamicValueConverter.Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
   at System.Windows.Controls.Primitives.Selector.VerifyEqual(Object knownValue, Type knownType, Object itemValue, DynamicValueConverter converter)
   at System.Windows.Controls.Primitives.Selector.FindItemWithValue(Object value, Int32& index)
   at System.Windows.Controls.Primitives.Selector.SelectItemWithValue(Object value, Boolean selectNow)
   at System.Windows.Controls.Primitives.Selector.CoerceSelectedValue(DependencyObject d, Object value)
   at System.Windows.DependencyObject.ProcessCoerceValue(DependencyProperty dp, PropertyMetadata metadata, EntryIndex& entryIndex, Int32& targetIndex, EffectiveValueEntry& newEntry, EffectiveValueEntry& oldEntry, Object& oldValue, Object baseValue, Object controlValue, CoerceValueCallback coerceValueCallback, Boolean coerceWithDeferredReference, Boolean coerceWithCurrentValue, Boolean skipBaseValueChecks)
   at System.Windows.DependencyObject.UpdateEffectiveValue(EntryIndex entryIndex, DependencyProperty dp, PropertyMetadata metadata, EffectiveValueEntry oldEntry, EffectiveValueEntry& newEntry, Boolean coerceWithDeferredReference, Boolean coerceWithCurrentValue, OperationType operationType)
   at System.Windows.DependencyObject.InvalidateProperty(DependencyProperty dp, Boolean preserveCurrentValue)
   at System.Windows.Data.BindingExpressionBase.Invalidate(Boolean isASubPropertyChange)
   at System.Windows.Data.BindingExpression.TransferValue(Object newValue, Boolean isASubPropertyChange)
   at System.Windows.Data.BindingExpression.Activate(Object item)
   at System.Windows.Data.BindingExpression.OnDataContextChanged(DependencyObject contextElement)
   at System.Windows.Data.BindingExpression.HandlePropertyInvalidation(DependencyObject d, DependencyPropertyChangedEventArgs args)
   at System.Windows.Data.BindingExpressionBase.OnPropertyInvalidation(DependencyObject d, DependencyPropertyChangedEventArgs args)
   at System.Windows.Data.BindingExpression.OnPropertyInvalidation(DependencyObject d, DependencyPropertyChangedEventArgs args)
   at System.Windows.DependentList.InvalidateDependents(DependencyObject source, DependencyPropertyChangedEventArgs sourceArgs)
   at System.Windows.DependencyObject.NotifyPropertyChange(DependencyPropertyChangedEventArgs args)
   at System.Windows.DependencyObject.UpdateEffectiveValue(EntryIndex entryIndex, DependencyProperty dp, PropertyMetadata metadata, EffectiveValueEntry oldEntry, EffectiveValueEntry& newEntry, Boolean coerceWithDeferredReference, Boolean coerceWithCurrentValue, OperationType operationType)
   at System.Windows.TreeWalkHelper.OnInheritablePropertyChanged(DependencyObject d, InheritablePropertyChangeInfo info, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1._VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(FrameworkElement fe, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.WalkLogicalChildren(FrameworkElement feParent, FrameworkContentElement fceParent, IEnumerator logicalChildren)
   at System.Windows.DescendentsWalker`1.WalkFrameworkElementLogicalThenVisualChildren(FrameworkElement feParent, Boolean hasLogicalChildren)
   at System.Windows.DescendentsWalker`1.IterateChildren(DependencyObject d)
   at System.Windows.DescendentsWalker`1._VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(FrameworkElement fe, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.WalkLogicalChildren(FrameworkElement feParent, FrameworkContentElement fceParent, IEnumerator logicalChildren)
   at System.Windows.DescendentsWalker`1.WalkFrameworkElementLogicalThenVisualChildren(FrameworkElement feParent, Boolean hasLogicalChildren)
   at System.Windows.DescendentsWalker`1.IterateChildren(DependencyObject d)
   at System.Windows.DescendentsWalker`1._VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(FrameworkElement fe, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.WalkLogicalChildren(FrameworkElement feParent, FrameworkContentElement fceParent, IEnumerator logicalChildren)
   at System.Windows.DescendentsWalker`1.WalkFrameworkElementLogicalThenVisualChildren(FrameworkElement feParent, Boolean hasLogicalChildren)
   at System.Windows.DescendentsWalker`1.IterateChildren(DependencyObject d)
   at System.Windows.DescendentsWalker`1._VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(FrameworkElement fe, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.WalkLogicalChildren(FrameworkElement feParent, FrameworkContentElement fceParent, IEnumerator logicalChildren)
   at System.Windows.DescendentsWalker`1.WalkFrameworkElementLogicalThenVisualChildren(FrameworkElement feParent, Boolean hasLogicalChildren)
   at System.Windows.DescendentsWalker`1.IterateChildren(DependencyObject d)
   at System.Windows.DescendentsWalker`1._VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(FrameworkElement fe, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.WalkLogicalChildren(FrameworkElement feParent, FrameworkContentElement fceParent, IEnumerator logicalChildren)
   at System.Windows.DescendentsWalker`1.WalkFrameworkElementLogicalThenVisualChildren(FrameworkElement feParent, Boolean hasLogicalChildren)
   at System.Windows.DescendentsWalker`1.IterateChildren(DependencyObject d)
   at System.Windows.DescendentsWalker`1._VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(FrameworkElement fe, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.WalkLogicalChildren(FrameworkElement feParent, FrameworkContentElement fceParent, IEnumerator logicalChildren)
   at System.Windows.DescendentsWalker`1.WalkFrameworkElementLogicalThenVisualChildren(FrameworkElement feParent, Boolean hasLogicalChildren)
   at System.Windows.DescendentsWalker`1.IterateChildren(DependencyObject d)
   at System.Windows.DescendentsWalker`1._VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(FrameworkElement fe, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.WalkLogicalChildren(FrameworkElement feParent, FrameworkContentElement fceParent, IEnumerator logicalChildren)
   at System.Windows.DescendentsWalker`1.WalkFrameworkElementLogicalThenVisualChildren(FrameworkElement feParent, Boolean hasLogicalChildren)
   at System.Windows.DescendentsWalker`1.IterateChildren(DependencyObject d)
   at System.Windows.DescendentsWalker`1.StartWalk(DependencyObject startNode, Boolean skipStartNode)
   at System.Windows.FrameworkElement.OnPropertyChanged(DependencyPropertyChangedEventArgs e)
   at System.Windows.DependencyObject.NotifyPropertyChange(DependencyPropertyChangedEventArgs args)
   at System.Windows.DependencyObject.UpdateEffectiveValue(EntryIndex entryIndex, DependencyProperty dp, PropertyMetadata metadata, EffectiveValueEntry oldEntry, EffectiveValueEntry& newEntry, Boolean coerceWithDeferredReference, Boolean coerceWithCurrentValue, OperationType operationType)
   at System.Windows.DependencyObject.SetValueCommon(DependencyProperty dp, Object value, PropertyMetadata metadata, Boolean coerceWithDeferredReference, Boolean coerceWithCurrentValue, OperationType operationType, Boolean isInternal)
   at System.Windows.FrameworkElement.set_DataContext(Object value)
   at DataEntryApp.View.applications.uc_programs.Dg_customer_SelectionChanged(Object sender, SelectionChangedEventArgs e) in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 787', N'Void StringToNumber(System.String, System.Globalization.NumberStyles, NumberBuffer ByRef, System.Globalization.NumberFormatInfo, Boolean)', CAST(N'2023-08-12T02:14:54.2923949' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2072, N'-2146233033', N'Input string was not in a correct format.', N'   at System.Number.StringToNumber(String str, NumberStyles options, NumberBuffer& number, NumberFormatInfo info, Boolean parseDecimal)
   at System.Number.ParseInt64(String value, NumberStyles options, NumberFormatInfo numfmt)
   at System.String.System.IConvertible.ToInt64(IFormatProvider provider)
   at System.Convert.ChangeType(Object value, Type conversionType, IFormatProvider provider)
   at MS.Internal.Data.SystemConvertConverter.Convert(Object o, Type type, Object parameter, CultureInfo culture)
   at MS.Internal.Data.DynamicValueConverter.Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
   at System.Windows.Controls.Primitives.Selector.VerifyEqual(Object knownValue, Type knownType, Object itemValue, DynamicValueConverter converter)
   at System.Windows.Controls.Primitives.Selector.FindItemWithValue(Object value, Int32& index)
   at System.Windows.Controls.Primitives.Selector.SelectItemWithValue(Object value, Boolean selectNow)
   at System.Windows.Controls.Primitives.Selector.CoerceSelectedValue(DependencyObject d, Object value)
   at System.Windows.DependencyObject.ProcessCoerceValue(DependencyProperty dp, PropertyMetadata metadata, EntryIndex& entryIndex, Int32& targetIndex, EffectiveValueEntry& newEntry, EffectiveValueEntry& oldEntry, Object& oldValue, Object baseValue, Object controlValue, CoerceValueCallback coerceValueCallback, Boolean coerceWithDeferredReference, Boolean coerceWithCurrentValue, Boolean skipBaseValueChecks)
   at System.Windows.DependencyObject.UpdateEffectiveValue(EntryIndex entryIndex, DependencyProperty dp, PropertyMetadata metadata, EffectiveValueEntry oldEntry, EffectiveValueEntry& newEntry, Boolean coerceWithDeferredReference, Boolean coerceWithCurrentValue, OperationType operationType)
   at System.Windows.DependencyObject.InvalidateProperty(DependencyProperty dp, Boolean preserveCurrentValue)
   at System.Windows.Data.BindingExpressionBase.Invalidate(Boolean isASubPropertyChange)
   at System.Windows.Data.BindingExpression.TransferValue(Object newValue, Boolean isASubPropertyChange)
   at System.Windows.Data.BindingExpression.Activate(Object item)
   at System.Windows.Data.BindingExpression.OnDataContextChanged(DependencyObject contextElement)
   at System.Windows.Data.BindingExpression.HandlePropertyInvalidation(DependencyObject d, DependencyPropertyChangedEventArgs args)
   at System.Windows.Data.BindingExpressionBase.OnPropertyInvalidation(DependencyObject d, DependencyPropertyChangedEventArgs args)
   at System.Windows.Data.BindingExpression.OnPropertyInvalidation(DependencyObject d, DependencyPropertyChangedEventArgs args)
   at System.Windows.DependentList.InvalidateDependents(DependencyObject source, DependencyPropertyChangedEventArgs sourceArgs)
   at System.Windows.DependencyObject.NotifyPropertyChange(DependencyPropertyChangedEventArgs args)
   at System.Windows.DependencyObject.UpdateEffectiveValue(EntryIndex entryIndex, DependencyProperty dp, PropertyMetadata metadata, EffectiveValueEntry oldEntry, EffectiveValueEntry& newEntry, Boolean coerceWithDeferredReference, Boolean coerceWithCurrentValue, OperationType operationType)
   at System.Windows.TreeWalkHelper.OnInheritablePropertyChanged(DependencyObject d, InheritablePropertyChangeInfo info, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1._VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(FrameworkElement fe, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.WalkLogicalChildren(FrameworkElement feParent, FrameworkContentElement fceParent, IEnumerator logicalChildren)
   at System.Windows.DescendentsWalker`1.WalkFrameworkElementLogicalThenVisualChildren(FrameworkElement feParent, Boolean hasLogicalChildren)
   at System.Windows.DescendentsWalker`1.IterateChildren(DependencyObject d)
   at System.Windows.DescendentsWalker`1._VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(FrameworkElement fe, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.WalkLogicalChildren(FrameworkElement feParent, FrameworkContentElement fceParent, IEnumerator logicalChildren)
   at System.Windows.DescendentsWalker`1.WalkFrameworkElementLogicalThenVisualChildren(FrameworkElement feParent, Boolean hasLogicalChildren)
   at System.Windows.DescendentsWalker`1.IterateChildren(DependencyObject d)
   at System.Windows.DescendentsWalker`1._VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(FrameworkElement fe, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.WalkLogicalChildren(FrameworkElement feParent, FrameworkContentElement fceParent, IEnumerator logicalChildren)
   at System.Windows.DescendentsWalker`1.WalkFrameworkElementLogicalThenVisualChildren(FrameworkElement feParent, Boolean hasLogicalChildren)
   at System.Windows.DescendentsWalker`1.IterateChildren(DependencyObject d)
   at System.Windows.DescendentsWalker`1._VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(FrameworkElement fe, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.WalkLogicalChildren(FrameworkElement feParent, FrameworkContentElement fceParent, IEnumerator logicalChildren)
   at System.Windows.DescendentsWalker`1.WalkFrameworkElementLogicalThenVisualChildren(FrameworkElement feParent, Boolean hasLogicalChildren)
   at System.Windows.DescendentsWalker`1.IterateChildren(DependencyObject d)
   at System.Windows.DescendentsWalker`1._VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(FrameworkElement fe, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.WalkLogicalChildren(FrameworkElement feParent, FrameworkContentElement fceParent, IEnumerator logicalChildren)
   at System.Windows.DescendentsWalker`1.WalkFrameworkElementLogicalThenVisualChildren(FrameworkElement feParent, Boolean hasLogicalChildren)
   at System.Windows.DescendentsWalker`1.IterateChildren(DependencyObject d)
   at System.Windows.DescendentsWalker`1._VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(FrameworkElement fe, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.WalkLogicalChildren(FrameworkElement feParent, FrameworkContentElement fceParent, IEnumerator logicalChildren)
   at System.Windows.DescendentsWalker`1.WalkFrameworkElementLogicalThenVisualChildren(FrameworkElement feParent, Boolean hasLogicalChildren)
   at System.Windows.DescendentsWalker`1.IterateChildren(DependencyObject d)
   at System.Windows.DescendentsWalker`1._VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(FrameworkElement fe, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.WalkLogicalChildren(FrameworkElement feParent, FrameworkContentElement fceParent, IEnumerator logicalChildren)
   at System.Windows.DescendentsWalker`1.WalkFrameworkElementLogicalThenVisualChildren(FrameworkElement feParent, Boolean hasLogicalChildren)
   at System.Windows.DescendentsWalker`1.IterateChildren(DependencyObject d)
   at System.Windows.DescendentsWalker`1.StartWalk(DependencyObject startNode, Boolean skipStartNode)
   at System.Windows.FrameworkElement.OnPropertyChanged(DependencyPropertyChangedEventArgs e)
   at System.Windows.DependencyObject.NotifyPropertyChange(DependencyPropertyChangedEventArgs args)
   at System.Windows.DependencyObject.UpdateEffectiveValue(EntryIndex entryIndex, DependencyProperty dp, PropertyMetadata metadata, EffectiveValueEntry oldEntry, EffectiveValueEntry& newEntry, Boolean coerceWithDeferredReference, Boolean coerceWithCurrentValue, OperationType operationType)
   at System.Windows.DependencyObject.SetValueCommon(DependencyProperty dp, Object value, PropertyMetadata metadata, Boolean coerceWithDeferredReference, Boolean coerceWithCurrentValue, OperationType operationType, Boolean isInternal)
   at System.Windows.FrameworkElement.set_DataContext(Object value)
   at DataEntryApp.View.applications.uc_programs.Dg_customer_SelectionChanged(Object sender, SelectionChangedEventArgs e) in D:\Github\Data-Entry-App\DataEntryApp\DataEntryApp\View\badge\us_programs.xaml.cs:line 787', N'Void StringToNumber(System.String, System.Globalization.NumberStyles, NumberBuffer ByRef, System.Globalization.NumberFormatInfo, Boolean)', CAST(N'2023-08-12T02:14:55.0758014' AS DateTime2), 2)
SET IDENTITY_INSERT [dbo].[error] OFF
GO
SET IDENTITY_INSERT [dbo].[setting] ON 

INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (1, N'com_name', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (2, N'com_address', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (3, N'com_email', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (4, N'com_mobile', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (5, N'com_phone', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (6, N'com_fax', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (7, N'com_logo', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (8, N'region', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (9, N'language', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (10, N'currency', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (11, N'tax', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (12, N'storage_cost', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (13, N'pur_order_email_temp', N'emailtemp-no')
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (14, N'dateForm', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (15, N'sale_email_temp', N'emailtemp')
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (16, N'sale_order_email_temp', N'emailtemp-no')
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (17, N'quotation_email_temp', N'emailtemp-no')
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (18, N'required_email_temp', N'emailtemp-no')
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (19, N'sale_copy_count', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (20, N'pur_copy_count', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (21, N'print_on_save_sale', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (22, N'print_on_save_pur', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (23, N'email_on_save_sale', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (24, N'email_on_save_pur', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (25, N'menuIsOpen', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (26, N'report_lang', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (27, N'rep_copy_count', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (28, N'user_path', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (29, N'accuracy', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (30, N'pur_email_temp', N'emailtemp-no')
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (31, N'Pur_inv_avg_count', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (32, N'Allow_print_inv_count', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (33, N'item_cost', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (34, N'sale_note', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (35, N'upgrade_email_temp', N'emailtemp')
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (36, N'', N'')
SET IDENTITY_INSERT [dbo].[setting] OFF
GO
SET IDENTITY_INSERT [dbo].[setValues] ON 

INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (1, N'en', 0, 0, NULL, 9)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (2, N'ar', 0, 0, NULL, 9)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (12, N'0', 1, 1, NULL, 11)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (13, N'0', 1, 1, NULL, 12)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (14, N'3204215c19f25609034d24451f7e03d7.png', 1, 1, N'', 7)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (15, N'purchase order', 0, 1, N'title', 13)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (16, N'this is ', 0, 1, N'text1', 13)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (17, N'', 0, 1, N'text2', 13)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (18, N'', 0, 1, N'link1text', 13)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (19, N'', 0, 1, N'link2text', 13)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (20, N'', 0, 0, N'link3text', 13)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (22, N'', 0, 1, N'link1url', 13)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (23, N'', 0, 1, N'link2url', 13)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (24, N'', 0, 1, N'link3url', 13)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (25, N'LongDatePattern', 1, 1, NULL, 14)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (26, N'Sales email', 0, 1, N'title', 15)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (27, N'', 0, 1, N'text1', 15)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (28, N'', 0, 1, N'text2', 15)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (29, N'', 0, 1, N'link1text', 15)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (30, N'', 0, 1, N'link2text', 15)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (31, N'', 0, 0, N'link3text', 15)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (32, N'', 0, 1, N'link1url', 15)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (33, N'', 0, 1, N'link2url', 15)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (34, N'', 0, 1, N'link3url', 15)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (39, N'Sales Order', 0, 1, N'title', 16)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (40, N'', 0, 0, N'text1', 16)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (41, N'', 0, 1, N'text2', 16)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (42, N'', 0, 1, N'link1text', 16)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (43, N'', 0, 1, N'link2text', 16)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (44, N'', 0, 1, N'link3text', 16)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (45, N'', 0, 1, N'link1url', 16)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (46, N'', 0, 1, N'link2url', 16)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (47, N'', 0, 1, N'link3url', 16)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (48, N'Quotaion', 0, 1, N'title', 17)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (49, N'', 0, 0, N'text1', 17)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (50, N'', 0, 1, N'text2', 17)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (51, N'', 0, 1, N'link1text', 17)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (52, N'', 0, 1, N'link2text', 17)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (53, N'', 0, 0, N'link3text', 17)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (54, N'', 0, 1, N'link1url', 17)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (55, N'', 0, 1, N'link2url', 17)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (56, N'', 0, 1, N'link3url', 17)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (58, N'SupClouds', 1, 1, NULL, 1)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (59, N'Kuwait', 1, 1, NULL, 2)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (60, N'admin@SupClouds.com', 1, 1, NULL, 3)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (61, N'+965-998484189', 1, 1, NULL, 4)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (62, N'+965--', 1, 1, NULL, 5)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (63, N'+965--', 1, 1, NULL, 6)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (64, N'Req', 0, 1, N'title', 18)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (65, N'', 0, 0, N'text1', 18)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (66, N'', 0, 1, N'text2', 18)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (67, N'', 0, 1, N'link1text', 18)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (68, N'', 0, 1, N'link2text', 18)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (69, N'', 0, 0, N'link3text', 18)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (70, N'', 0, 1, N'link1url', 18)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (71, N'', 0, 1, N'link2url', 18)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (72, N'', 0, 1, N'link3url', 18)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (73, N'1', 0, 1, N'print', 19)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (74, N'1', 0, 1, N'print', 20)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (75, N'0', 0, 1, N'print', 21)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (76, N'0', 0, 1, N'print', 22)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (77, N'0', 0, 1, N'print', 23)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (78, N'0', 0, 1, N'print', 24)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (79, N'open', 0, 0, NULL, 25)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (80, N'close', 0, 0, NULL, 25)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (81, N'en', 1, 1, NULL, 26)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (82, N'ar', 0, 1, NULL, 26)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (83, N'1', 1, 1, N'print', 27)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (84, N'first', 0, 0, N'0', 28)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (85, N'second', 0, 0, N'0', 28)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (87, N'1', 1, 1, N'0', 29)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (88, N'Purchase', 0, 1, N'title', 30)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (89, N'', 0, 0, N'text1', 30)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (90, N'', 0, 1, N'text2', 30)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (91, N'', 0, 1, N'link1text', 30)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (92, N'', 0, 1, N'link2text', 30)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (93, N'', 0, 1, N'link3text', 30)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (94, N'', 0, 1, N'link1url', 30)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (95, N'', 0, 1, N'link2url', 30)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (96, N'', 0, 1, N'link3url', 30)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (109, N'5', 1, 1, N'0', 31)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (110, N'5', 1, 1, N'print', 32)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (111, N'0', 1, 1, N'0', 33)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (112, N'', 1, 1, N'sale_note', 34)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (113, N'Booking email', 0, 1, N'title', 35)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (114, N'', 0, 1, N'text1', 35)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (115, N'', 0, 1, N'text2', 35)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (116, N'', 0, 1, N'link1text', 35)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (117, N'', 0, 1, N'link2text', 35)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (118, N'', 0, 1, N'link3text', 35)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (119, N'', 0, 1, N'link1url', 35)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (120, N'', 0, 1, N'link2url', 35)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (121, N'', 0, 1, N'link3url', 35)
SET IDENTITY_INSERT [dbo].[setValues] OFF
GO
SET IDENTITY_INSERT [dbo].[users] ON 

INSERT [dbo].[users] ([userId], [name], [AccountName], [lastName], [company], [email], [phone], [mobile], [fax], [address], [agentLevel], [createDate], [updateDate], [password], [type], [image], [notes], [balance], [createUserId], [updateUserId], [isActive], [code], [isAdmin], [groupId], [balanceType], [job], [isOnline], [countryId]) VALUES (1, N'administrator', N'administrator', N'Support', NULL, N'', NULL, N'+966-095550226', NULL, N'', NULL, NULL, CAST(N'2023-07-28T15:51:31.4860429' AS DateTime2), N'8ea60f80800198548a9a81aa2e4a6115', N'ad', N'57440ff6b80f068efd50410ea24fd593.png', N'', CAST(0.000 AS Decimal(20, 3)), 1, 2, 1, N'Us-Admin', NULL, NULL, NULL, NULL, NULL, 9)
INSERT [dbo].[users] ([userId], [name], [AccountName], [lastName], [company], [email], [phone], [mobile], [fax], [address], [agentLevel], [createDate], [updateDate], [password], [type], [image], [notes], [balance], [createUserId], [updateUserId], [isActive], [code], [isAdmin], [groupId], [balanceType], [job], [isOnline], [countryId]) VALUES (2, N'admin', N'admin', N'admin', NULL, N'', NULL, N'+966-095550226', NULL, N'', NULL, NULL, CAST(N'2023-08-12T02:55:50.0102903' AS DateTime2), N'9b43a5e653134fc8350ca9944eadaf2f', N'ad', N'c37858823db0093766eee74d8ee1f1e5.png', N'', CAST(0.000 AS Decimal(20, 3)), 1, 2, 1, N'Us-adminuser', NULL, NULL, NULL, NULL, 1, 9)
INSERT [dbo].[users] ([userId], [name], [AccountName], [lastName], [company], [email], [phone], [mobile], [fax], [address], [agentLevel], [createDate], [updateDate], [password], [type], [image], [notes], [balance], [createUserId], [updateUserId], [isActive], [code], [isAdmin], [groupId], [balanceType], [job], [isOnline], [countryId]) VALUES (3, N'sysagent', N'sysagent', N'sysagent', NULL, N'', NULL, N'+966-095550226', NULL, N'', NULL, NULL, CAST(N'2021-12-08T12:25:13.5504988' AS DateTime2), N'e4959b2ae3b5256076a7b5e99f88b8ba', N'ag', NULL, N'', CAST(0.000 AS Decimal(20, 3)), 1, 1, 1, N'aaa', NULL, NULL, NULL, NULL, NULL, 9)
INSERT [dbo].[users] ([userId], [name], [AccountName], [lastName], [company], [email], [phone], [mobile], [fax], [address], [agentLevel], [createDate], [updateDate], [password], [type], [image], [notes], [balance], [createUserId], [updateUserId], [isActive], [code], [isAdmin], [groupId], [balanceType], [job], [isOnline], [countryId]) VALUES (4, N'ahmad', N'ahmad', N'ahmad', NULL, N'', NULL, N'+963-944444444', NULL, N'', NULL, CAST(N'2023-07-10T14:01:50.3551343' AS DateTime2), CAST(N'2023-07-28T15:50:36.0939643' AS DateTime2), N'9b43a5e653134fc8350ca9944eadaf2f', N'us', N'd2ec5f1ed83abfca0dfec76506b696b3.png', N'', CAST(0.000 AS Decimal(20, 3)), 2, 2, 1, N'gbm', NULL, NULL, NULL, NULL, NULL, 9)
INSERT [dbo].[users] ([userId], [name], [AccountName], [lastName], [company], [email], [phone], [mobile], [fax], [address], [agentLevel], [createDate], [updateDate], [password], [type], [image], [notes], [balance], [createUserId], [updateUserId], [isActive], [code], [isAdmin], [groupId], [balanceType], [job], [isOnline], [countryId]) VALUES (6, N'test', N'test', N'test', NULL, N'', NULL, N'+963-599994949', NULL, N'', NULL, CAST(N'2023-07-28T16:19:02.6861123' AS DateTime2), CAST(N'2023-07-28T16:19:29.0893130' AS DateTime2), N'9b43a5e653134fc8350ca9944eadaf2f', N'us', NULL, N'', CAST(0.000 AS Decimal(20, 3)), 2, 2, 1, N'jnm', NULL, NULL, NULL, NULL, 1, 9)
SET IDENTITY_INSERT [dbo].[users] OFF
GO
ALTER TABLE [dbo].[countriesCodes] ADD  CONSTRAINT [DF_countriesCodes_isDefault]  DEFAULT ((0)) FOR [isDefault]
GO
ALTER TABLE [dbo].[users] ADD  CONSTRAINT [DF_users_balance]  DEFAULT ((0)) FOR [balance]
GO
ALTER TABLE [dbo].[cities]  WITH CHECK ADD  CONSTRAINT [FK_cities_countriesCodes] FOREIGN KEY([countryId])
REFERENCES [dbo].[countriesCodes] ([countryId])
GO
ALTER TABLE [dbo].[cities] CHECK CONSTRAINT [FK_cities_countriesCodes]
GO
ALTER TABLE [dbo].[customers]  WITH CHECK ADD  CONSTRAINT [FK_customers_departments] FOREIGN KEY([departmentId])
REFERENCES [dbo].[departments] ([departmentId])
GO
ALTER TABLE [dbo].[customers] CHECK CONSTRAINT [FK_customers_departments]
GO
ALTER TABLE [dbo].[customers]  WITH CHECK ADD  CONSTRAINT [FK_customers_nationalities] FOREIGN KEY([nationalityId])
REFERENCES [dbo].[nationalities] ([nationalityId])
GO
ALTER TABLE [dbo].[customers] CHECK CONSTRAINT [FK_customers_nationalities]
GO
ALTER TABLE [dbo].[customers]  WITH CHECK ADD  CONSTRAINT [FK_customers_users] FOREIGN KEY([createUserId])
REFERENCES [dbo].[users] ([userId])
GO
ALTER TABLE [dbo].[customers] CHECK CONSTRAINT [FK_customers_users]
GO
ALTER TABLE [dbo].[customers]  WITH CHECK ADD  CONSTRAINT [FK_customers_users1] FOREIGN KEY([updateUserId])
REFERENCES [dbo].[users] ([userId])
GO
ALTER TABLE [dbo].[customers] CHECK CONSTRAINT [FK_customers_users1]
GO
ALTER TABLE [dbo].[error]  WITH CHECK ADD  CONSTRAINT [FK_error_users] FOREIGN KEY([createUserId])
REFERENCES [dbo].[users] ([userId])
GO
ALTER TABLE [dbo].[error] CHECK CONSTRAINT [FK_error_users]
GO
ALTER TABLE [dbo].[nationalities]  WITH CHECK ADD  CONSTRAINT [FK_nationalities_users] FOREIGN KEY([createUserId])
REFERENCES [dbo].[users] ([userId])
GO
ALTER TABLE [dbo].[nationalities] CHECK CONSTRAINT [FK_nationalities_users]
GO
ALTER TABLE [dbo].[nationalities]  WITH CHECK ADD  CONSTRAINT [FK_nationalities_users1] FOREIGN KEY([updateUserId])
REFERENCES [dbo].[users] ([userId])
GO
ALTER TABLE [dbo].[nationalities] CHECK CONSTRAINT [FK_nationalities_users1]
GO
ALTER TABLE [dbo].[setValues]  WITH CHECK ADD  CONSTRAINT [FK_setValues_setting] FOREIGN KEY([settingId])
REFERENCES [dbo].[setting] ([settingId])
GO
ALTER TABLE [dbo].[setValues] CHECK CONSTRAINT [FK_setValues_setting]
GO
ALTER TABLE [dbo].[users]  WITH CHECK ADD  CONSTRAINT [FK_users_countriesCodes] FOREIGN KEY([countryId])
REFERENCES [dbo].[countriesCodes] ([countryId])
GO
ALTER TABLE [dbo].[users] CHECK CONSTRAINT [FK_users_countriesCodes]
GO
ALTER TABLE [dbo].[userSetValues]  WITH CHECK ADD  CONSTRAINT [FK_userSetValues_setValues] FOREIGN KEY([valId])
REFERENCES [dbo].[setValues] ([valId])
GO
ALTER TABLE [dbo].[userSetValues] CHECK CONSTRAINT [FK_userSetValues_setValues]
GO
