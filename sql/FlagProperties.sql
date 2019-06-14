-- Creating table 'FlagProperties'
CREATE TABLE [dbo].[FlagProperties] (
        [Id] int IDENTITY(1,1) NOT NULL,
        [Name] nvarchar(max)  NOT NULL,
        [Description] nvarchar(max)  NOT NULL,
        [PropertyKey] nvarchar(max)  NOT NULL,
        [PropertyValue] nvarchar(max)  NOT NULL,
        [FlagId] int  NOT NULL
    );
GO

-- Creating primary key on [Id] in table 'FlagProperties'
ALTER TABLE [dbo].[FlagProperties]
ADD CONSTRAINT [PK_FlagProperties]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating foreign key on [FlagId] in table 'FlagProperties'
ALTER TABLE [dbo].[FlagProperties]
ADD CONSTRAINT [FK_FlagToFlagProperty]
    FOREIGN KEY ([FlagId])
    REFERENCES [dbo].[Flags]
    ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FlagToFlagProperty'
CREATE INDEX [IX_FK_FlagToFlagProperty]
ON [dbo].[FlagProperties]
    ([FlagId]);
GO

