USE [saAction_chatapi]
GO
/****** Object:  Table [dbo].[Attachment]    Script Date: 3/27/2020 7:09:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attachment](
	[AttachmentId] [bigint] NULL,
	[MessageId] [bigint] NULL,
	[AttachmentType] [tinyint] NULL,
	[FilePath] [nvarchar](500) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ChatSession]    Script Date: 3/27/2020 7:09:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ChatSession](
	[ChatSessionId] [bigint] IDENTITY(1,1) NOT NULL,
	[OperatorId] [int] NULL,
	[UserId] [int] NOT NULL,
	[InviteCodeId] [int] NULL,
	[IPAddress] [varchar](20) NULL,
	[OSVersion] [varchar](50) NULL,
	[OtherDetail] [varchar](600) NULL,
	[ExeType] [tinyint] NULL,
	[ChatStartDate] [datetime] NULL,
	[ChatEndDate] [datetime] NULL,
 CONSTRAINT [PK_ChatSession] PRIMARY KEY CLUSTERED 
(
	[ChatSessionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[InviteCode]    Script Date: 3/27/2020 7:09:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InviteCode](
	[InviteCodeId] [int] IDENTITY(1,1) NOT NULL,
	[InviteCode] [nvarchar](50) NOT NULL,
	[InviteCodeType] [tinyint] NOT NULL,
	[GeneratedBy] [int] NOT NULL,
	[GeneratedOn] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_tblInviteCode_IsActive]  DEFAULT ((1)),
	[DeactivatedOn] [datetime] NULL,
	[DeactivatedBy] [int] NULL,
 CONSTRAINT [PK_InviteCode] PRIMARY KEY CLUSTERED 
(
	[InviteCodeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Message]    Script Date: 3/27/2020 7:09:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Message](
	[MessageId] [bigint] IDENTITY(1,1) NOT NULL,
	[ChatSessionId] [bigint] NOT NULL,
	[MessageSentBy] [tinyint] NULL,
	[Message] [nvarchar](1000) NULL,
	[MessageType] [tinyint] NULL,
	[OperatorId] [int] NULL,
	[UserId] [int] NULL,
	[SentAt] [datetime] NULL,
 CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED 
(
	[MessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Operator]    Script Date: 3/27/2020 7:09:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Operator](
	[OperatorId] [int] IDENTITY(1,1) NOT NULL,
	[DisplayName] [nvarchar](50) NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[EmailId] [nvarchar](50) NULL,
	[Phone] [nvarchar](20) NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_tblOperator_IsActive]  DEFAULT ((1)),
	[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_tblOperator_CreatedOn]  DEFAULT (getdate()),
	[ModifiedOn] [datetime] NOT NULL CONSTRAINT [DF_tblOperator_ModifiedOn]  DEFAULT (getdate()),
	[ManagerId] [int] NULL,
 CONSTRAINT [PK_Operator] PRIMARY KEY CLUSTERED 
(
	[OperatorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SessionHistory]    Script Date: 3/27/2020 7:09:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SessionHistory](
	[SessionHistoryId] [bigint] NOT NULL,
	[ChatSessionId] [bigint] NOT NULL,
	[OldOperatorId] [int] NOT NULL,
	[NewOperatorId] [int] NULL,
	[UseOldOperatorName] [bit] NOT NULL,
	[ReasonForTransfer] [int] NULL,
	[TransferTime] [datetime] NULL,
	[OperatorComments] [nvarchar](500) NULL,
 CONSTRAINT [PK_SessionHistory] PRIMARY KEY CLUSTERED 
(
	[SessionHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Settings]    Script Date: 3/27/2020 7:09:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Settings](
	[SettingID] [int] IDENTITY(1,1) NOT NULL,
	[SettingDescription] [varchar](100) NULL,
	[SettingValue] [varchar](450) NULL,
 CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED 
(
	[SettingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TransferReason]    Script Date: 3/27/2020 7:09:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransferReason](
	[TransferReasonId] [int] NULL,
	[Description] [nvarchar](500) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedOn] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 3/27/2020 7:09:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](50) NULL,
	[EmailId] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_User_IsActive]  DEFAULT ((1)),
	[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_User_CreatedOn]  DEFAULT (getdate()),
	[ModifiedOn] [datetime] NOT NULL CONSTRAINT [DF_User_ModifiedOn]  DEFAULT (getdate()),
	[UserToken] [varchar](50) NULL,
	[Exes] [varchar](200) NULL,
	[LatestVersion] [varchar](50) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserCode]    Script Date: 3/27/2020 7:09:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserCode](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[InviteCodeId] [int] NULL,
 CONSTRAINT [PK_UserCode] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserLoginHistory]    Script Date: 3/27/2020 7:09:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserLoginHistory](
	[HistoryId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[UserToken] [uniqueidentifier] NULL,
	[LoginTime] [datetime] NULL,
	[IPAddress] [varchar](20) NULL,
	[OSVersion] [varchar](50) NULL,
	[OtherDetail] [varchar](600) NULL,
	[ExeType] [tinyint] NULL,
	[CurrExeVersion] [varchar](50) NULL,
 CONSTRAINT [PK_UserLoginHistory] PRIMARY KEY CLUSTERED 
(
	[HistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Attachment] ([AttachmentId], [MessageId], [AttachmentType], [FilePath]) VALUES (NULL, 20027, 1, N'/7/0001-20200320.pdf')
GO
INSERT [dbo].[Attachment] ([AttachmentId], [MessageId], [AttachmentType], [FilePath]) VALUES (NULL, 20030, 0, N'/7/Chat Application Specification v1.docx')
GO
SET IDENTITY_INSERT [dbo].[ChatSession] ON 

GO
INSERT [dbo].[ChatSession] ([ChatSessionId], [OperatorId], [UserId], [InviteCodeId], [IPAddress], [OSVersion], [OtherDetail], [ExeType], [ChatStartDate], [ChatEndDate]) VALUES (1, 1, 3, 1, N'10.0.122.128', N'windows 10.26.36', N'--test--otp--checking', 1, CAST(N'2020-03-02 10:36:09.760' AS DateTime), CAST(N'2020-03-15 03:01:59.047' AS DateTime))
GO
INSERT [dbo].[ChatSession] ([ChatSessionId], [OperatorId], [UserId], [InviteCodeId], [IPAddress], [OSVersion], [OtherDetail], [ExeType], [ChatStartDate], [ChatEndDate]) VALUES (2, NULL, 2, 2, N'192.168.0.105', N'Windows 8', N'', 1, NULL, NULL)
GO
INSERT [dbo].[ChatSession] ([ChatSessionId], [OperatorId], [UserId], [InviteCodeId], [IPAddress], [OSVersion], [OtherDetail], [ExeType], [ChatStartDate], [ChatEndDate]) VALUES (3, NULL, 2, 2, N'192.168.0.105', N'Windows 8', N'', 1, NULL, NULL)
GO
INSERT [dbo].[ChatSession] ([ChatSessionId], [OperatorId], [UserId], [InviteCodeId], [IPAddress], [OSVersion], [OtherDetail], [ExeType], [ChatStartDate], [ChatEndDate]) VALUES (4, NULL, 2, 1, N'192.168.0.105', N'Windows 8', N'', 1, NULL, NULL)
GO
INSERT [dbo].[ChatSession] ([ChatSessionId], [OperatorId], [UserId], [InviteCodeId], [IPAddress], [OSVersion], [OtherDetail], [ExeType], [ChatStartDate], [ChatEndDate]) VALUES (5, 2, 2, 4, N'10.0.122.128', N'windows 10.26.36', N'--test--otp--checking', 1, CAST(N'2020-03-13 10:00:07.447' AS DateTime), NULL)
GO
INSERT [dbo].[ChatSession] ([ChatSessionId], [OperatorId], [UserId], [InviteCodeId], [IPAddress], [OSVersion], [OtherDetail], [ExeType], [ChatStartDate], [ChatEndDate]) VALUES (6, 2, 2, 5, N'10.0.122.128', N'windows 10.26.36', N'--test--otp--checking', 1, CAST(N'2020-03-14 22:13:56.633' AS DateTime), NULL)
GO
INSERT [dbo].[ChatSession] ([ChatSessionId], [OperatorId], [UserId], [InviteCodeId], [IPAddress], [OSVersion], [OtherDetail], [ExeType], [ChatStartDate], [ChatEndDate]) VALUES (7, 2, 2, 6, N'10.0.122.128', N'windows 10.26.36', N'--test--otp--checking', 1, CAST(N'2020-03-15 02:27:29.557' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[ChatSession] OFF
GO
SET IDENTITY_INSERT [dbo].[InviteCode] ON 

GO
INSERT [dbo].[InviteCode] ([InviteCodeId], [InviteCode], [InviteCodeType], [GeneratedBy], [GeneratedOn], [IsActive], [DeactivatedOn], [DeactivatedBy]) VALUES (1, N'671278', 1, 1, CAST(N'2020-02-27 09:35:04.313' AS DateTime), 1, NULL, NULL)
GO
INSERT [dbo].[InviteCode] ([InviteCodeId], [InviteCode], [InviteCodeType], [GeneratedBy], [GeneratedOn], [IsActive], [DeactivatedOn], [DeactivatedBy]) VALUES (2, N'286465', 1, 1, CAST(N'2020-02-27 09:35:23.197' AS DateTime), 1, CAST(N'2020-02-27 00:00:00.000' AS DateTime), 2)
GO
INSERT [dbo].[InviteCode] ([InviteCodeId], [InviteCode], [InviteCodeType], [GeneratedBy], [GeneratedOn], [IsActive], [DeactivatedOn], [DeactivatedBy]) VALUES (3, N'861846', 1, 2, CAST(N'2020-03-13 00:00:00.000' AS DateTime), 1, CAST(N'2020-03-13 00:00:00.000' AS DateTime), 3)
GO
INSERT [dbo].[InviteCode] ([InviteCodeId], [InviteCode], [InviteCodeType], [GeneratedBy], [GeneratedOn], [IsActive], [DeactivatedOn], [DeactivatedBy]) VALUES (4, N'701091', 1, 2, CAST(N'2020-03-13 09:59:55.967' AS DateTime), 1, CAST(N'2020-03-13 10:00:07.447' AS DateTime), 2)
GO
INSERT [dbo].[InviteCode] ([InviteCodeId], [InviteCode], [InviteCodeType], [GeneratedBy], [GeneratedOn], [IsActive], [DeactivatedOn], [DeactivatedBy]) VALUES (5, N'521358', 1, 2, CAST(N'2020-03-14 22:13:45.157' AS DateTime), 1, CAST(N'2020-03-14 22:13:56.633' AS DateTime), 2)
GO
INSERT [dbo].[InviteCode] ([InviteCodeId], [InviteCode], [InviteCodeType], [GeneratedBy], [GeneratedOn], [IsActive], [DeactivatedOn], [DeactivatedBy]) VALUES (6, N'798234', 1, 2, CAST(N'2020-03-15 02:27:22.213' AS DateTime), 1, CAST(N'2020-03-15 02:27:29.557' AS DateTime), 2)
GO
INSERT [dbo].[InviteCode] ([InviteCodeId], [InviteCode], [InviteCodeType], [GeneratedBy], [GeneratedOn], [IsActive], [DeactivatedOn], [DeactivatedBy]) VALUES (7, N'623638', 1, 2, CAST(N'2020-03-26 02:44:25.870' AS DateTime), 1, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[InviteCode] OFF
GO
SET IDENTITY_INSERT [dbo].[Message] ON 

GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (1, 1, 1, N'Welcome to Chat App....!!!', 0, NULL, NULL, CAST(N'2020-03-02 10:12:12.860' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (2, 1, 1, N'Welcome to Chat App....!!!', 0, NULL, NULL, CAST(N'2020-03-02 10:29:39.813' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (3, 1, 1, N'Welcome to Chat App....!!!', 0, NULL, NULL, CAST(N'2020-03-02 10:30:21.500' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (4, 1, 1, N'Welcome to new chat app', 0, NULL, NULL, CAST(N'2020-03-02 10:31:15.630' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (5, 1, 1, N'Welcome to Chat App....!!!', 0, NULL, NULL, CAST(N'2020-03-02 10:31:35.397' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (6, 1, 1, N'Welcome to Chat App....!!!', 0, NULL, NULL, CAST(N'2020-03-02 10:31:49.120' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (7, 1, 1, N'Welcome to Chat App....!!!', 0, NULL, NULL, CAST(N'2020-03-02 10:31:54.490' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (8, 1, 1, N'Welcome to Chat App....!!!', 0, NULL, NULL, CAST(N'2020-03-02 10:34:40.560' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (9, 1, 1, N'Welcome to Chat App....!!!', 0, NULL, NULL, CAST(N'2020-03-02 10:35:12.547' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (10, 1, 1, N'This is new for test..!', 0, NULL, NULL, CAST(N'2020-03-02 10:36:09.760' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (11, 1, 1, N'asdfsfasdfasdfsdf', 0, NULL, 1, CAST(N'2020-03-03 05:52:15.463' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (12, 1, 1, N'asdfsfasdfasdfsdf', 0, 1, 0, CAST(N'2020-03-03 05:52:31.230' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (13, 1, 0, N'Welcome to new chat..', 0, 0, 0, CAST(N'2020-03-03 05:54:52.700' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (14, 1, 0, N'r', 0, 0, 1, CAST(N'2020-03-03 05:55:25.400' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (15, 1, 0, N'Let''s start out talk', 0, 0, 1, CAST(N'2020-03-03 06:06:26.563' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (16, 1, 0, N'Let''s start out talk', 0, 0, 1, CAST(N'2020-03-03 06:07:00.270' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (17, 1, 0, N'Lets start out talk', 0, 0, 1, CAST(N'2020-03-03 06:07:40.980' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (18, 1, 0, N'Lets start out talk', 0, 0, 1, CAST(N'2020-03-03 06:08:29.820' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (19, 1, 0, N'Lets start out talk', 0, 0, 1, CAST(N'2020-03-03 06:09:04.410' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (20, 1, 0, N'Lets start out talk', 0, 0, 1, CAST(N'2020-03-03 06:09:23.270' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (21, 1, 0, N'rasdfasdf asdfsdfds', 0, 0, 1, CAST(N'2020-03-03 06:09:46.287' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (22, 1, 0, N'talking about nice thing........', 0, 0, 1, CAST(N'2020-03-03 06:11:00.857' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (23, 1, 0, N'rasdfasdf asdfsdfds', 0, 0, 1, CAST(N'2020-03-03 06:13:03.733' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (24, 1, 0, N'talking about nice thing 2342342424........', 0, 0, 1, CAST(N'2020-03-03 06:15:04.957' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (10011, 1, 0, N'talking about nice thing 2342342424........', 0, 0, 1, CAST(N'2020-03-04 18:38:23.390' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (20011, 0, 0, N'this is how we do', 0, 0, 2, CAST(N'2020-03-10 19:32:49.890' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (20012, 0, 0, N'this is how we do', 0, 0, 2, CAST(N'2020-03-10 19:33:53.700' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (20013, 0, 0, N'this is how we do', 0, 0, 2, CAST(N'2020-03-10 19:33:54.930' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (20014, 0, 0, N'this is how we do', 0, 0, 2, CAST(N'2020-03-10 19:34:08.080' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (20015, 0, 0, N'this is how we do', 0, 0, 2, CAST(N'2020-03-10 19:34:09.320' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (20016, 0, 0, N'fszfsCZCSA', 0, 0, 2, CAST(N'2020-03-11 09:12:26.307' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (20017, 0, 0, N'
qweqwe w wef  a', 0, 0, 2, CAST(N'2020-03-11 10:32:43.500' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (20018, 0, 0, N'Hi there, My name is Manish...
', 0, 0, 2, CAST(N'2020-03-11 10:37:20.470' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (20019, 0, 0, N'I am facing problem with my mouse, its not working...', 0, 0, 2, CAST(N'2020-03-11 10:37:39.233' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (20020, 0, 0, N'A', 0, 0, 2, CAST(N'2020-03-11 10:38:41.700' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (20021, 0, 0, N'B', 0, 0, 2, CAST(N'2020-03-11 10:38:44.343' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (20022, 0, 0, N'C', 0, 0, 2, CAST(N'2020-03-11 10:45:37.230' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (20023, 0, 0, N'Operator Reply 1', 0, 0, 2, CAST(N'2020-03-11 10:46:34.847' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (20024, 0, 0, N'Operator Reply 2', 0, 0, 2, CAST(N'2020-03-11 10:46:38.920' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (20025, 0, 0, N'Operator Reply 3', 0, 0, 2, CAST(N'2020-03-11 10:46:43.067' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (20026, 0, 0, N'jlasd fldsakjf ;ldasfj asd;lfj dsa;lfsad fadsf sadf', 0, 0, 2, CAST(N'2020-03-11 10:49:26.470' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (20027, 7, 1, N'''HI..this is new File Attachement message.....''', 1, 2, 2, CAST(N'2020-03-22 07:31:38.667' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (20028, 7, 1, N'''HI..this is  Without Attachment.....''', 0, 2, 2, CAST(N'2020-03-22 07:55:42.110' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (20029, 7, 1, N'''HI..this is ..without..... Attachment.....''', 0, 2, 2, CAST(N'2020-03-22 09:48:21.853' AS DateTime))
GO
INSERT [dbo].[Message] ([MessageId], [ChatSessionId], [MessageSentBy], [Message], [MessageType], [OperatorId], [UserId], [SentAt]) VALUES (20030, 7, 1, N'''HI..this is ..with..... Attachment.....''', 1, 2, 2, CAST(N'2020-03-22 09:54:08.107' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Message] OFF
GO
SET IDENTITY_INSERT [dbo].[Operator] ON 

GO
INSERT [dbo].[Operator] ([OperatorId], [DisplayName], [FirstName], [LastName], [EmailId], [Phone], [UserName], [Password], [IsActive], [CreatedOn], [ModifiedOn], [ManagerId]) VALUES (1, N'SMR', N'samir', N'mehta', N'mehta@gmail.com', N'9089808', N'samir', N'samir', 1, CAST(N'2020-02-27 00:00:00.000' AS DateTime), CAST(N'2020-02-27 00:00:00.000' AS DateTime), 100)
GO
INSERT [dbo].[Operator] ([OperatorId], [DisplayName], [FirstName], [LastName], [EmailId], [Phone], [UserName], [Password], [IsActive], [CreatedOn], [ModifiedOn], [ManagerId]) VALUES (2, N'JOGANI', N'bhavesh', N'jogani', N'bhavesh.jogani@gmail.com', N'48484848', N'bhavesh', N'bhavesh', 1, CAST(N'2020-02-28 00:00:00.000' AS DateTime), CAST(N'2020-02-28 00:00:00.000' AS DateTime), 101)
GO
SET IDENTITY_INSERT [dbo].[Operator] OFF
GO
SET IDENTITY_INSERT [dbo].[Settings] ON 

GO
INSERT [dbo].[Settings] ([SettingID], [SettingDescription], [SettingValue]) VALUES (1, N'Exe1', N'30')
GO
INSERT [dbo].[Settings] ([SettingID], [SettingDescription], [SettingValue]) VALUES (2, N'Exe2', N'30')
GO
INSERT [dbo].[Settings] ([SettingID], [SettingDescription], [SettingValue]) VALUES (3, N'ApiUrl', N'http://chatapi.saaction.in/api/')
GO
INSERT [dbo].[Settings] ([SettingID], [SettingDescription], [SettingValue]) VALUES (4, N'UpdateUrl', N'http://chatapi.saaction.in/')
GO
SET IDENTITY_INSERT [dbo].[Settings] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

GO
INSERT [dbo].[User] ([UserId], [UserName], [FirstName], [LastName], [Password], [Phone], [EmailId], [City], [State], [IsActive], [CreatedOn], [ModifiedOn], [UserToken], [Exes], [LatestVersion]) VALUES (1, N'samir', N'samir', N'mehta', N'samir', N'samir', N'samir@gmail.com', N'surat', N'gujarat', 1, CAST(N'2020-02-27 00:00:00.000' AS DateTime), CAST(N'2020-02-27 00:00:00.000' AS DateTime), NULL, N'MessageMe.exe', N'1')
GO
INSERT [dbo].[User] ([UserId], [UserName], [FirstName], [LastName], [Password], [Phone], [EmailId], [City], [State], [IsActive], [CreatedOn], [ModifiedOn], [UserToken], [Exes], [LatestVersion]) VALUES (2, N'manish', N'manish', N'patel', N'manish', N'09601292221', N'sahanpriyaswami@gmail.com', N'Surat', N'Gujarat', 1, CAST(N'2020-02-27 00:00:00.000' AS DateTime), CAST(N'2020-02-27 00:00:00.000' AS DateTime), N'BA7641B5-294F-4AAF-A974-B882AD3B1165', N'MessageMe.exe', N'1')
GO
INSERT [dbo].[User] ([UserId], [UserName], [FirstName], [LastName], [Password], [Phone], [EmailId], [City], [State], [IsActive], [CreatedOn], [ModifiedOn], [UserToken], [Exes], [LatestVersion]) VALUES (3, N'bhavesh', N'bhavesh', N'jogani', N'bhavesh', N'985858585', N'bhavesh@gmail.com', N'surat', N'gujarat', 1, CAST(N'2020-02-25 00:00:00.000' AS DateTime), CAST(N'2020-02-26 00:00:00.000' AS DateTime), NULL, N'MessageMe.exe', N'1')
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[UserCode] ON 

GO
INSERT [dbo].[UserCode] ([Id], [UserId], [InviteCodeId]) VALUES (10, 3, 1)
GO
INSERT [dbo].[UserCode] ([Id], [UserId], [InviteCodeId]) VALUES (11, 2, 2)
GO
INSERT [dbo].[UserCode] ([Id], [UserId], [InviteCodeId]) VALUES (12, 2, 2)
GO
INSERT [dbo].[UserCode] ([Id], [UserId], [InviteCodeId]) VALUES (13, 2, 1)
GO
INSERT [dbo].[UserCode] ([Id], [UserId], [InviteCodeId]) VALUES (14, 3, NULL)
GO
INSERT [dbo].[UserCode] ([Id], [UserId], [InviteCodeId]) VALUES (15, 2, 4)
GO
INSERT [dbo].[UserCode] ([Id], [UserId], [InviteCodeId]) VALUES (16, 2, 5)
GO
INSERT [dbo].[UserCode] ([Id], [UserId], [InviteCodeId]) VALUES (17, 2, 6)
GO
SET IDENTITY_INSERT [dbo].[UserCode] OFF
GO
SET IDENTITY_INSERT [dbo].[UserLoginHistory] ON 

GO
INSERT [dbo].[UserLoginHistory] ([HistoryId], [UserId], [UserToken], [LoginTime], [IPAddress], [OSVersion], [OtherDetail], [ExeType], [CurrExeVersion]) VALUES (1, 2, N'8b803c9a-dd57-43a0-ba13-48bc71ea0060', CAST(N'2020-03-27 05:59:23.747' AS DateTime), N'192.168.43.68', N'Windows 8', N'', 1, N'0.00')
GO
INSERT [dbo].[UserLoginHistory] ([HistoryId], [UserId], [UserToken], [LoginTime], [IPAddress], [OSVersion], [OtherDetail], [ExeType], [CurrExeVersion]) VALUES (2, 2, N'ba7641b5-294f-4aaf-a974-b882ad3b1165', CAST(N'2020-03-27 06:36:47.440' AS DateTime), N'192.168.43.68', N'Windows 8', N'', 1, N'0.00')
GO
SET IDENTITY_INSERT [dbo].[UserLoginHistory] OFF
GO
ALTER TABLE [dbo].[SessionHistory] ADD  CONSTRAINT [DF_SessionHistory_UseOldOperatorName]  DEFAULT ((1)) FOR [UseOldOperatorName]
GO
ALTER TABLE [dbo].[TransferReason] ADD  CONSTRAINT [DF_TransferReason_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[TransferReason] ADD  CONSTRAINT [DF_TransferReason_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
/****** Object:  StoredProcedure [dbo].[Chat_SignOUT]    Script Date: 3/27/2020 7:09:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Sign Out

--OperatorId : '1',
--UserId : '3',
--ChatSessionId : '1',

CREATE PROC [dbo].[Chat_SignOUT]
(
	@flag int = null output,
	@OperatorId int,
	@ChatSessionId int
)
AS
Begin
	--select * from chatsession
	if exists (select  1 from chatsession where ChatSessionId = @ChatSessionId and ChatEndDate is null)
	Begin
		Update chatsession 
		set ChatEndDate = getdate()
		where chatsessionId = @ChatSessionId and OperatorId = @OperatorId
		select @flag = 1
	end
	else 
	Begin
		select @flag = -1
	end
End
GO
/****** Object:  StoredProcedure [dbo].[ChatReceivedByTime]    Script Date: 3/27/2020 7:09:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--[dbo].[ChatReceivedByTime] 1,1,1,3,'2020-03-03 13:00:12'
CREATE PROC [dbo].[ChatReceivedByTime]
(
	@flag int  = null output,
	@OperatorId  int, --: '1',
	@UserId int, --: '3',
	@ChatSessionId int,--: '1',
	@lastChatReadTime datetime --:’ date time’
)
AS
Begin
	if exists(select 1 from ChatSession where ChatSessionId = @ChatSessionId and ChatEndDate is null)
	Begin
		--select * from ChatSession
		--select * from [Message]
		Select [Message].MessageId,
			   [Message].MessageSentBy,
			   [Message],
			   [Message].MessageType,
			   Operator.DisplayName as OperatorName,
			   [User].UserName,
			   [Message].SentAt
		From [Message] left join Operator on [Message].OperatorId = Operator.OperatorId
			 left join [User] on [Message].UserId = [User].UserId
		Where [Message].ChatSessionId = @ChatSessionId and 
			 -- [Message].OperatorId = @OperatorId and 
			 -- [Message].UserId = @UserId	and
			  [Message].SentAt > @lastChatReadTime
		set @flag = 1
	End
	else
	Begin
		select NULL
		set @flag = -1
	End
End
GO
/****** Object:  StoredProcedure [dbo].[ChatSend]    Script Date: 3/27/2020 7:09:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--[ChatSend] 0,0,1,1,'rasdfasdf asdfsdfds'
CREATE PROC [dbo].[ChatSend]
(
	@flag int  output ,
	@OperatorId int = 0,
	@UserId int = 0,
	@ChatSessionId bigint,
	@Msg nvarchar(200) = null,

	-- new added parameter
	@MessageSentBy tinyint, -- 1:Operator, 2:Client
	@MessageType tinyint, -- 0:No Attachment,1:Attachment
	@AttachmentType tinyint, -- 0:Doc,1:image,2:video,3:other
	@filePath nvarchar(500) = '' -- /ChessionId/fileName.ext ( path format )
)
AS
Begin
	Begin Try
			-- MessageSentBy = 1 Operator,2 Client
			-- [MessageType] = 0 without Attachment, 1 with Attachment
			if exists(select 1 from ChatSession where  ChatsessionId = @ChatSessionId and OperatorId is null and UserId = @UserId)
			Begin
				update ChatSession
				set OperatorId = @OperatorId,
					ChatStartDate = GetDate()
				where UserId = @UserId and ChatsessionId = @ChatSessionId
			End

			--select * from [Message]
			Declare @message_id int = 0;
			insert into [Message]
			(
				ChatSessionId,MessageSentBy,[Message],[MessageType],SentAt,OperatorID,UserID
			)
			Values
			(
				@ChatSessionId,@MessageSentBy,@Msg,@MessageType,GETDATE(),@OperatorId,@UserId
			)
			select @message_id = scope_identity()
			--select * from [Attachment]
			if @MessageType = 1
			Begin
				insert into Attachment
				(
					MessageId,AttachmentType,FilePath
				)
				values(
					@message_id,@AttachmentType,@FilePath
				)
			End
			select @flag = 1
			print @flag
	End Try
	Begin Catch
		
		select @flag = 0
		
	End Catch
End

--select * from [Message]
GO
/****** Object:  StoredProcedure [dbo].[GetLatestVersionByUser]    Script Date: 3/27/2020 7:09:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetLatestVersionByUser]
(
	@UserId int,
	@Token Uniqueidentifier
	
)
AS
BEGIN
	IF EXISTS(SELECT 1 FROM dbo.[user] WHERE UserId = @UserId AND UserToken = @Token AND IsActive = 1)
	BEGIN
		SELECT LatestVersion,1 AS IsValid FROM dbo.[user] 
	END
	ELSE 
	BEGIN
		-- Not Valid
		SELECT '' AS LatestVersion, 0 AS IsValid
	END
	
END

GO
/****** Object:  StoredProcedure [dbo].[LoginCheck]    Script Date: 3/27/2020 7:09:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[LoginCheck]
(
	@UserName VARCHAR(50),
	@Password VARCHAR(20),
	@ExeType TINYINT,
	@IPAddress VARCHAR(20)=NULL,
	@OSVersion VARCHAR(50)=NULL,
	@OtherDetail VARCHAR(600)=NULL,
	@CurrExeVersion VARCHAR(50)=NULL
)
AS
BEGIN
	IF EXISTS(SELECT 1 FROM dbo.[user] WHERE UserName = @UserName AND [Password] = @Password AND IsActive = 1)
	BEGIN
		
		DECLARE @token AS VARCHAR(50)
		SET @token = NEWID()
		UPDATE [user] SET UserToken = @token WHERE  UserName = @UserName AND [Password] = @Password AND IsActive = 1

		DECLARE @settingVal AS VARCHAR(150)
		SELECT @settingVal = SettingValue FROM [Settings] WHERE SettingId = @ExeType

		DECLARE @ApiUrl AS VARCHAR(500)
		SELECT @ApiUrl = SettingValue FROM [Settings] WHERE SettingId = 3  -- 'ApiUrl'

		DECLARE @UpdateUrl AS VARCHAR(500)
		SELECT @UpdateUrl = SettingValue FROM [Settings] WHERE SettingId = 4  -- 'UpdateUrl'

		SELECT UserID,UserName,UserToken AS Token,FirstName,LastName,@settingVal AS UpdateDuration,@ApiUrl AS ApiUrl,@UpdateUrl AS UpdateUrl,Exes,LatestVersion
		FROM dbo.[user] WHERE UserName = @UserName AND [Password] = @Password AND IsActive = 1

		DECLARE @UserId INT
		SELECT @UserId = UserID FROM dbo.[user] WHERE UserName = @UserName AND [Password] = @Password AND IsActive = 1
		
		INSERT INTO [UserLoginHistory] 
		(
           UserId,UserToken,LoginTime,IPAddress,OSVersion,OtherDetail,ExeType,CurrExeVersion
		)
		VALUES (@UserId,@token,Getdate(),@IPAddress,@OSVersion,@OtherDetail,@ExeType,@CurrExeVersion)


	END
	ELSE IF EXISTS(SELECT 1 FROM dbo.[user] WHERE IsActive = 0)
	BEGIN
		-- INACTIVE User
		SELECT -1 AS UserID,'' AS UserName, '' AS Token
	END
	ELSE 
	BEGIN
		-- not exists in our system
		SELECT -2 AS UserID,'' AS UserName, '' AS Token
	END
END

GO
/****** Object:  StoredProcedure [dbo].[OperatorRegistration]    Script Date: 3/27/2020 7:09:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[OperatorRegistration]
(
	 @OperatorID	int	output
	,@OperatorName	varchar(50)
	,@EmailAddress	varchar(50) = NULL
	,@ContactNo	varchar(15)= NULL
	,@UserName	varchar(20)
	,@Password	varchar(20)
	,@IsActive	bit = 1
)
AS
Begin Try
	Begin Tran
		if exists(select 1 from tblOperator where UserName= @OperatorName)
		Begin
			select  -1
		End
		else if (@OperatorID = 0)
		Begin 
			Insert Into tblOperator
			select @OperatorName,@EmailAddress,@ContactNo,@UserName,@Password,@IsActive,GETDATE()
			select @OperatorID = scope_identity()
		End
		else if(@OperatorID > 0)
		Begin
			update tblOperator
			set
				EmailAddress = @EmailAddress,
				Password = @Password,
				ContactNo = @ContactNo,
				UserName = @UserName,
				IsActive = @IsActive,
				ChangeDate = GetDate()
		   where OperatorID = @OperatorID
		   select @OperatorID
		End
	commit tran
End Try
Begin Catch
	select @OperatorID = 0
End Catch
GO
/****** Object:  StoredProcedure [dbo].[SetOtpByOperator]    Script Date: 3/27/2020 7:09:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SetOtpByOperator]
(
	@operatorId int
)
AS
Begin
	--SELECT ABS(CHECKSUM(NEWID()))%900000 + 100000
	--select * from InviteCode
	Declare @invitecode nvarchar(50)
	set @invitecode = ABS(CHECKSUM(NEWID()))%900000 + 100000
	
	insert into InviteCode
	select @invitecode,1,@operatorId,GETDATE(),1,NULL,NULL

	select @invitecode as otp
End
GO
/****** Object:  StoredProcedure [dbo].[UserRegistration]    Script Date: 3/27/2020 7:09:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[UserRegistration]
(
	@UserID int output,
	@UserFullName Varchar(30),
	@EmailID varchar(30) = NULL,
	@UserName varchar(50),
	@Password varchar(20),
	@ContactNo varchar(15),
	@City nvarchar(50) = NULL,
	@State nvarchar(50) = NULL,
	@Country nvarchar(50) = NULL,
	@IsActive bit = 1
)
AS
Begin Try
	Begin Tran
		if exists(select 1 from tblUser where UserName=@UserName)
		Begin
			select  -1
		End
		else if (@UserID = 0)
		Begin 
			Insert Into tblUser
			select @UserFullName,@EmailID,@UserName,@Password,@ContactNo,@City,@State,@Country,@IsActive,GETDATE()
			select @UserID = scope_identity()
		End
		else if(@UserID > 0)
		Begin
			update tblUser
			set UserFullName = @UserFullName,
				EmailID = @EmailID,
				Password = @Password,
				ContactNo = @ContactNo,
				City = @City,
				State = @State,
				Country =@Country,
				IsActive = @IsActive,
				ChangeDate = GetDate()
		   where UserID =@UserID
		   select @UserID
		End
	commit tran
End Try
Begin Catch
	select @UserID = 0
End Catch
GO
/****** Object:  StoredProcedure [dbo].[ValidateOTP]    Script Date: 3/27/2020 7:09:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--[dbo].[ValidateOTP] 0,'671278','3','10.0.122.128','windows 10.26.36','aa','1'
CREATE PROC [dbo].[ValidateOTP] 
(
	@flag int output,
	@otp nvarchar(10),
	@UserId int,
	@IPAddress varchar(20),
	@OSVersion varchar(20),
	@OtherDetail varchar(200),
	@ExeType tinyint
)
AS
Begin
	Begin Try
		Begin Tran
			
			if exists(select 1 from invitecode where invitecode = @otp and deactivatedon is null and IsActive = 1)
			Begin
					Declare @InviteCodeId as int,@operator_Id as int,@OperatorName as  nvarchar(50)
					
					select @InviteCodeId=InviteCodeId,
						   @operator_id = GeneratedBy,
						   @OperatorName = operator.DisplayName
					from InviteCode inner join operator on Operator.OperatorId = InviteCode.GeneratedBy
					where invitecode = @otp and deactivatedon is null and InviteCode.IsActive = 1
					
					update invitecode
					set deactivatedon = Getdate(),
						DeactivatedBy = @userid
					where invitecode = @otp and deactivatedon is null and IsActive = 1

					insert into UserCode select @UserId,@InviteCodeId
					--select * from ChatSession
					insert into ChatSession
					(
						OperatorID,
						UserId,
						InviteCodeId,
						IPAddress,
						OSVersion,
						OtherDetail,
						ExeType,
						ChatStartDate
					)
					 select 
							@operator_Id
						   ,InviteCode.DeactivatedBy
						   ,InviteCode.InviteCodeID
						   ,@IPAddress
						   ,@OSVersion
						   ,@OtherDetail
						   ,@ExeType
						   ,GetDate()
					from InviteCode
					where InviteCode.InviteCodeId = @InviteCodeID
						  and InviteCode.GeneratedBy = @operator_Id

					--select @UserId,@InviteCodeId,@IPAddress,@OSVersion,@OtherDetail,@ExeType

					select scope_identity() as ChatSessionID ,
						   @operator_id as OperatorID,
						   @OperatorName as OperatorName,
						   @InviteCodeId as InviteCodeID

					select @flag = 1
			End
			Else 
			Begin
					select @flag = 2
			End
		commit Tran
	End Try
	Begin Catch
		Rollback Tran
		select @flag = 3
		print Error_Message()
	End Catch
End
--declare @otp int = '671278'

GO
