-- Creating table 'UserFeature'
CREATE TABLE [dbo].[UserFeature] (
    [Users_Id] int  NOT NULL,
    [Features_Id] int  NOT NULL
);
GO
-- Creating primary key on [Users_Id], [Features_Id] in table 'UserFeature'
ALTER TABLE [dbo].[UserFeature]
ADD CONSTRAINT [PK_UserFeature]
    PRIMARY KEY CLUSTERED ([Users_Id], [Features_Id] ASC);
GO
-- Creating foreign key on [Users_Id] in table 'UserFeature'
ALTER TABLE [dbo].[UserFeature]
ADD CONSTRAINT [FK_UserFeature_User]
    FOREIGN KEY ([Users_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [Features_Id] in table 'UserFeature'
ALTER TABLE [dbo].[UserFeature]
ADD CONSTRAINT [FK_UserFeature_Feature]
    FOREIGN KEY ([Features_Id])
    REFERENCES [dbo].[Features]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_UserFeature_Feature'
CREATE INDEX [IX_FK_UserFeature_Feature]
ON [dbo].[UserFeature]
    ([Features_Id]);
GO