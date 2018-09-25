-- Creating table 'Flags'
CREATE TABLE [dbo].[Flags] (
        [Id] int IDENTITY(1,1) NOT NULL,
        [Name] nvarchar(max)  NOT NULL,
        [Description] nvarchar(max)  NOT NULL,
        [FeatureId] int  NOT NULL
    );
GO

-- Creating primary key on [Id] in table 'Flags'
ALTER TABLE [dbo].[Flags]
ADD CONSTRAINT [PK_Flags]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating foreign key on [FeatureId] in table 'Flags'
ALTER TABLE [dbo].[Flags]
ADD CONSTRAINT [FK_FeatureFlag]
    FOREIGN KEY ([FeatureId])
    REFERENCES [dbo].[Features]
    ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FeatureFlag'
CREATE INDEX [IX_FK_FeatureFlag]
ON [dbo].[Flags]
    ([FeatureId]);
GO
