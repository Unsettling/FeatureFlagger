-- Creating table 'FeatureRole'
CREATE TABLE [dbo].[FeatureRole] (
    [Features_Id] int  NOT NULL,
    [Roles_Id] int  NOT NULL
);
GO
-- Creating primary key on [Features_Id], [Roles_Id] in table 'FeatureRole'
ALTER TABLE [dbo].[FeatureRole]
ADD CONSTRAINT [PK_FeatureRole]
    PRIMARY KEY CLUSTERED ([Features_Id], [Roles_Id] ASC);
GO
-- Creating foreign key on [Features_Id] in table 'FeatureRole'
ALTER TABLE [dbo].[FeatureRole]
ADD CONSTRAINT [FK_FeatureRole_Feature]
    FOREIGN KEY ([Features_Id])
    REFERENCES [dbo].[Features]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [Roles_Id] in table 'FeatureRole'
ALTER TABLE [dbo].[FeatureRole]
ADD CONSTRAINT [FK_FeatureRole_Role]
    FOREIGN KEY ([Roles_Id])
    REFERENCES [dbo].[Roles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_FeatureRole_Role'
CREATE INDEX [IX_FK_FeatureRole_Role]
ON [dbo].[FeatureRole]
    ([Roles_Id]);
GO