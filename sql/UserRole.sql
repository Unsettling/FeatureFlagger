-- Creating table 'UserRole'
CREATE TABLE [dbo].[UserRole] (
    [Users_Id] int  NOT NULL,
    [Roles_Id] int  NOT NULL
);
GO
-- Creating primary key on [Users_Id], [Roles_Id] in table 'UserRole'
ALTER TABLE [dbo].[UserRole]
ADD CONSTRAINT [PK_UserRole]
    PRIMARY KEY CLUSTERED ([Users_Id], [Roles_Id] ASC);
GO
-- Creating foreign key on [Users_Id] in table 'UserRole'
ALTER TABLE [dbo].[UserRole]
ADD CONSTRAINT [FK_UserRole_User]
    FOREIGN KEY ([Users_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [Roles_Id] in table 'UserRole'
ALTER TABLE [dbo].[UserRole]
ADD CONSTRAINT [FK_UserRole_Role]
    FOREIGN KEY ([Roles_Id])
    REFERENCES [dbo].[Roles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_UserRole_Role'
CREATE INDEX [IX_FK_UserRole_Role]
ON [dbo].[UserRole]
    ([Roles_Id]);
GO