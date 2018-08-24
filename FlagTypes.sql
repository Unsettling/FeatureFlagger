-- Creating table 'FlagTypes'
CREATE TABLE [dbo].[FlagTypes] (
        [Id] int IDENTITY(1,1) NOT NULL,
        [Name] nvarchar(max)  NOT NULL,
        [Description] nvarchar(max)  NOT NULL,
        [LastUpdatedBy] nvarchar(50)  NOT NULL,
        [LastUpdatedAt] datetime  NOT NULL,
        [UpdateVersion] ROWVERSION  NULL
    );
GO

-- Creating primary key on [Id] in table 'FlagTypes'
ALTER TABLE [dbo].[FlagTypes]
ADD CONSTRAINT [PK_FlagTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO
