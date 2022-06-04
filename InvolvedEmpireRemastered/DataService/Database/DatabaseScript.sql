CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
 	CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
)


CREATE TABLE [dbo].[Game](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[SerializedGame] [nvarchar](max) NOT NULL,
 	CONSTRAINT [PK_Game] PRIMARY KEY CLUSTERED ([Id] ASC)
)

CREATE TABLE [dbo].[UserGame](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[GameId] [int] NOT NULL,
	[SerializedEmpire] [nvarchar](max) NOT NULL,
 	CONSTRAINT [PK_UserGame] PRIMARY KEY CLUSTERED ([Id] ASC)
)


ALTER TABLE [dbo].[UserGame]  WITH CHECK ADD  CONSTRAINT [FK_UserGame_GameId_Game_Id] FOREIGN KEY([GameId])
REFERENCES [dbo].[Game] ([Id])
GO

ALTER TABLE [dbo].[UserGame] CHECK CONSTRAINT [FK_UserGame_GameId_Game_Id]
GO

ALTER TABLE [dbo].[UserGame]  WITH CHECK ADD  CONSTRAINT [FK_UserGame_UserId_User_Id] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[UserGame] CHECK CONSTRAINT [FK_UserGame_UserId_User_Id]
GO
