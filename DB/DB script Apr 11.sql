USE [saAction_chatapi]
GO
/****** Object:  Table [dbo].[DownloadFileHistory]    Script Date: 11-04-2020 21:04:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DownloadFileHistory](
	[FileId] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [nvarchar](50) NULL,
	[FilePath] [nvarchar](500) NULL,
	[UserId] [int] NOT NULL,
	[DateInstalled] [datetime] NULL,
	[VersionId] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[DateAdded] [datetime] NOT NULL,
	[AddedBy] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Settings]    Script Date: 11-04-2020 21:04:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Settings](
	[SettingID] [int] IDENTITY(1,1) NOT NULL,
	[SettingDescription] [varchar](100) NULL,
	[SettingValue] [varchar](450) NULL,
	[UserId] [int] NULL,
	[Code] [nvarchar](50) NULL,
 CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED 
(
	[SettingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 11-04-2020 21:04:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
	[IsActive] [bit] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[UserToken] [varchar](50) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DownloadFileHistory] ADD  CONSTRAINT [DF_DownloadFileHistory_UserId]  DEFAULT ((-100)) FOR [UserId]
GO
ALTER TABLE [dbo].[DownloadFileHistory] ADD  CONSTRAINT [DF_DownloadFileHistory_VersionId]  DEFAULT ((0.0)) FOR [VersionId]
GO
ALTER TABLE [dbo].[DownloadFileHistory] ADD  CONSTRAINT [DF_DownloadFileHistory_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[DownloadFileHistory] ADD  CONSTRAINT [DF_DownloadFileHistory_DateAdded]  DEFAULT (getdate()) FOR [DateAdded]
GO
ALTER TABLE [dbo].[Settings] ADD  CONSTRAINT [DF_Settings_UserId]  DEFAULT ((-100)) FOR [UserId]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
/****** Object:  StoredProcedure [dbo].[CheckUpdateExists]    Script Date: 11-04-2020 21:04:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[CheckUpdateExists]
(
	@UserId int,
	@Token Uniqueidentifier,
	@CurrExeVersion int
	
)
AS
BEGIN
	IF EXISTS(SELECT 1 FROM dbo.[user] WHERE UserId = @UserId AND UserToken = @Token AND IsActive = 1 )
	BEGIN
		
		DECLARE @ApiUrl AS VARCHAR(500)
		SELECT @ApiUrl = SettingValue FROM [Settings] WHERE [Code] = 'ApiUrl'

		DECLARE @UpdateUrl AS VARCHAR(500)
		SELECT @UpdateUrl = SettingValue FROM [Settings] WHERE [Code] = 'UpdateUrl'

		IF EXISTS (SELECT 1 from DownloadfileHistory d where d.VersionId > @CurrExeVersion AND d.UserId = @UserId)
		BEGIN
			SELECT  
				1 As IsValidUser,
				1 As IsUpdateAvailable

		END
		ELSE	
			BEGIN
			SELECT  
				1 As IsValidUser,
				0 As IsUpdateAvailable
			END
				

	--	SELECT LatestVersion,1 AS IsValid,@ApiUrl AS ApiUrl,@UpdateUrl AS UpdateUrl,Exes FROM dbo.[user] where UserId=@UserId
	END
	ELSE 
	BEGIN
		-- Not Valid
		SELECT 
			0 As IsValidUser,
			0 As IsUpdateAvailable
			
	END
	
END

GO
/****** Object:  StoredProcedure [dbo].[GetLatestVersionByUser]    Script Date: 11-04-2020 21:04:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetLatestVersionByUser]
(
	@UserId int,
	@Token Uniqueidentifier,
	@CurrExeVersion int
	
)
AS
BEGIN
	IF EXISTS(SELECT 1 FROM dbo.[user] WHERE UserId = @UserId AND UserToken = @Token AND IsActive = 1 )
	BEGIN
		
		DECLARE @ApiUrl AS VARCHAR(500)
		SELECT @ApiUrl = SettingValue FROM [Settings] WHERE [Code] = 'ApiUrl'

		DECLARE @UpdateUrl AS VARCHAR(500)
		SELECT @UpdateUrl = SettingValue FROM [Settings] WHERE [Code] = 'UpdateUrl'

		IF EXISTS (SELECT 1 from DownloadfileHistory d where d.VersionId > @CurrExeVersion AND d.UserId = @UserId)
		BEGIN
			SELECT  Top(1)
				@ApiUrl AS ApiUrl,
				@UpdateUrl AS UpdateUrl,
				d2.FileName,
				d2.FilePath,
				d2.VersionId 
			 
			FROM dbo.[User] U
			LEFT JOIN DownloadfileHistory d1 ON U.UserId = d1.UserId  
			LEFT JOIN DownloadfileHistory d2 ON d1.UserId = d2.UserId
			LEFT JOIN DownloadFileHistory d3 ON d2.UserId = d3.UserId
			WHERE 
				d2.VersionId > d1.VersionId AND 
				d3.VersionId > d1.VersionId AND 
				d2.VersionId < = d3.VersionId AND
				d1.VersionId = @CurrExeVersion AND 
				U.UserId = @UserId AND 
				U.UserToken = @Token  AND 
				U.IsActive = 1
			ORDER BY d2.VersionId ASC
		END
		ELSE	
			BEGIN
			SELECT  
				@ApiUrl AS ApiUrl,
				@UpdateUrl AS UpdateUrl,
				d2.FileName,
				d2.FilePath,
				d2.VersionId 
			FROM dbo.[user] U
			INNER JOIN  DownloadfileHistory d2 ON U.UserId = d2.UserId 
			WHERE 
				d2.VersionId = @CurrExeVersion AND 
				U.UserId = @UserId AND 
				U.UserToken = @Token  AND 
				U.IsActive = 1
			END
				

	--	SELECT LatestVersion,1 AS IsValid,@ApiUrl AS ApiUrl,@UpdateUrl AS UpdateUrl,Exes FROM dbo.[user] where UserId=@UserId
	END
	ELSE 
	BEGIN
		-- Not Valid
		SELECT 
			'' AS ApiUrl,
			'' AS UpdateUrl,
			'' AS [FileName],
			'' AS FilePath,
			'' AS VersionId 
			
	END
	
END

GO
/****** Object:  StoredProcedure [dbo].[LoginCheck]    Script Date: 11-04-2020 21:04:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[LoginCheck]
(
	@UserName VARCHAR(50),
	@Password VARCHAR(20),
	@ExeType varchar(20),
	@IPAddress VARCHAR(20)=NULL,
	@OSVersion VARCHAR(50)=NULL,
	@OtherDetail VARCHAR(600)=NULL,
	@CurrExeVersion int=0
)
AS
BEGIN
	IF EXISTS(SELECT 1 FROM dbo.[user] WHERE UserName = @UserName AND [Password] = @Password AND IsActive = 1)
	BEGIN
		
		DECLARE @token AS VARCHAR(50)
		SET @token = NEWID()
		UPDATE [user] SET UserToken = @token WHERE  UserName = @UserName AND [Password] = @Password AND IsActive = 1

		DECLARE @settingVal AS VARCHAR(150)
		SELECT @settingVal = SettingValue FROM [Settings] WHERE [Code] = @ExeType

		DECLARE @ApiUrl AS VARCHAR(500)
		SELECT @ApiUrl = SettingValue FROM [Settings] WHERE [Code] = 'ApiUrl'   -- 'ApiUrl'

		DECLARE @UpdateUrl AS VARCHAR(500)
		SELECT @UpdateUrl = SettingValue FROM [Settings] WHERE [Code] = 'UpdateUrl'  -- 'UpdateUrl'

		IF EXISTS (SELECT 1 from DownloadfileHistory d where d.VersionId > @CurrExeVersion)
	BEGIN
		SELECT  Top(1)
			U.UserID,
			U.UserName,
			U.UserToken AS Token,
			U.FirstName,
			U.LastName,@settingVal AS UpdateDuration,
			@ApiUrl AS ApiUrl,
			@UpdateUrl AS UpdateUrl,
			d2.FileName,
			d2.FilePath,
			d2.VersionId 
			 
		FROM dbo.[user] U
		LEFT JOIN  DownloadfileHistory d1 ON U.UserId = d1.UserId 
		LEFT JOIN DownloadfileHistory d2 on d1.UserId = d2.UserId
		LEFT JOIN DownloadFileHistory d3 on d2.UserId = d3.UserId
		WHERE 
			d2.VersionId > d1.VersionId AND 
			d3.VersionId > d1.VersionId AND 
			d2.VersionId < = d3.VersionId AND
			d1.VersionId = @CurrExeVersion AND 
			U.UserName = @UserName AND 
			U.[Password] = @Password AND 
			U.IsActive = 1

 
		ORDER BY d2.VersionId ASC
	END
	ELSE	
		BEGIN
		SELECT  
			U.UserID,
			U.UserName,
			U.UserToken AS Token,
			U.FirstName,
			U.LastName,@settingVal AS UpdateDuration,
			@ApiUrl AS ApiUrl,
			@UpdateUrl AS UpdateUrl,
			d2.FileName,
			d2.FilePath,
			d2.VersionId 
			
			 
		FROM dbo.[user] U
		INNER JOIN  DownloadfileHistory d2 ON U.UserId = d2.UserId 
		
		WHERE 
			d2.VersionId = @CurrExeVersion AND 
			U.UserName = @UserName AND 
			U.[Password] = @Password AND 
			U.IsActive = 1
		END
				

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
