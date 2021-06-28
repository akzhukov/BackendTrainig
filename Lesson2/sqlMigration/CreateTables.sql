CREATE TABLE [dbo].[Factory](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Name] [nvarchar](max) NULL,
    [Description] [nvarchar](max) NULL,
    CONSTRAINT [PK_Statuses] PRIMARY KEY CLUSTERED ([Id] ASC)
)

CREATE TABLE [dbo].[Unit](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Name] [nvarchar](max) NULL,
    [FactoryId] [int] NULL,
    CONSTRAINT [PK_Unit] PRIMARY KEY CLUSTERED ([Id] ASC)
)
ALTER TABLE [dbo].[Unit] WITH CHECK ADD CONSTRAINT [FK_Unit_Factory_FactoryId] FOREIGN KEY([FactoryId])
    REFERENCES [dbo].[Factory] ([Id])

CREATE TABLE [dbo].[Tank](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Name] [nvarchar](max) NULL,
    [Volume] [int] NOT NULL,
    [MaxVolume] [int] NOT NULL,
    [UnitId] [int] NULL,
    CONSTRAINT [PK_Tank] PRIMARY KEY CLUSTERED ([Id] ASC)
)
ALTER TABLE [dbo].[Tank] WITH CHECK ADD CONSTRAINT [FK_Tank_Unit_UnitId] FOREIGN KEY([UnitId])
    REFERENCES [dbo].[Unit] ([Id])


		
											

