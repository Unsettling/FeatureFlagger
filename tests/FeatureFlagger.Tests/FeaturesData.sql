USE [FeatureFlagger]
GO
ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [FK_UserRole_User]
GO
ALTER TABLE [dbo].[UserFeature] DROP CONSTRAINT [FK_UserFeature_User]
GO
ALTER TABLE [dbo].[UserFeature] DROP CONSTRAINT [FK_UserFeature_Feature]
GO
ALTER TABLE [dbo].[FeatureRole] DROP CONSTRAINT [FK_FeatureRole_Feature]
GO
ALTER TABLE [dbo].[Flags] DROP CONSTRAINT [FK_FeatureFlag]
GO
ALTER TABLE [dbo].[FlagProperties] DROP CONSTRAINT [FK_FlagToFlagProperty]
GO
TRUNCATE TABLE [dbo].[UserFeature]
TRUNCATE TABLE [dbo].[Users]
TRUNCATE TABLE [dbo].[Features]
TRUNCATE TABLE [dbo].[Flags]
TRUNCATE TABLE [dbo].[FlagProperties]
GO
SET IDENTITY_INSERT [dbo].[Features] ON
INSERT [dbo].[Features] ([Id], [Name], [Description], [LastUpdatedBy], [LastUpdatedAt], [UpdateVersion]) VALUES (1, N'Example', N'Example.', N'rbogle', CAST(N'2018-09-10 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Features] ([Id], [Name], [Description], [LastUpdatedBy], [LastUpdatedAt], [UpdateVersion]) VALUES (2, N'Disabled', N'Disabled.', N'rbogle', CAST(N'2018-09-10 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Features] ([Id], [Name], [Description], [LastUpdatedBy], [LastUpdatedAt], [UpdateVersion]) VALUES (3, N'From', N'From.', N'rbogle', CAST(N'2018-09-10 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Features] ([Id], [Name], [Description], [LastUpdatedBy], [LastUpdatedAt], [UpdateVersion]) VALUES (4, N'User', N'User.', N'rbogle', CAST(N'2018-09-10 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Features] ([Id], [Name], [Description], [LastUpdatedBy], [LastUpdatedAt], [UpdateVersion]) VALUES (5, N'Unuser', N'UnUser.', N'rbogle', CAST(N'2018-09-10 00:00:00.000' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Features] OFF
SET IDENTITY_INSERT [dbo].[Flags] ON
INSERT [dbo].[Flags] ([Id], [Name], [Description], [LastUpdatedBy], [LastUpdatedAt], [FeatureId]) VALUES (1, N'Enabled', N'Example.Enabled', N'rbogle', CAST(N'2018-09-10 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Flags] ([Id], [Name], [Description], [LastUpdatedBy], [LastUpdatedAt], [FeatureId]) VALUES (2, N'Enabled', N'Disabled.Enabled', N'rbogle', CAST(N'2018-09-10 00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Flags] ([Id], [Name], [Description], [LastUpdatedBy], [LastUpdatedAt], [FeatureId]) VALUES (3, N'Enabled', N'From.Enabled', N'rbogle', CAST(N'2018-09-10 00:00:00.000' AS DateTime), 3)
INSERT [dbo].[Flags] ([Id], [Name], [Description], [LastUpdatedBy], [LastUpdatedAt], [FeatureId]) VALUES (4, N'Enabled', N'User.Enabled', N'rbogle', CAST(N'2019-09-10 00:00:00.000' AS DateTime), 4)
INSERT [dbo].[Flags] ([Id], [Name], [Description], [LastUpdatedBy], [LastUpdatedAt], [FeatureId]) VALUES (5, N'Enabled', N'Unuser.Enabled', N'rbogle', CAST(N'2018-09-10 00:00:00.000' AS DateTime), 5)
INSERT [dbo].[Flags] ([Id], [Name], [Description], [LastUpdatedBy], [LastUpdatedAt], [FeatureId]) VALUES (6, N'From', N'From.Date', N'rbogle', CAST(N'2018-09-10 00:00:00.000' AS DateTime), 3)
INSERT [dbo].[Flags] ([Id], [Name], [Description], [LastUpdatedBy], [LastUpdatedAt], [FeatureId]) VALUES (7, N'User', N'User.User', N'rbogle', CAST(N'2018-09-10 00:00:00.000' AS DateTime), 4)
INSERT [dbo].[Flags] ([Id], [Name], [Description], [LastUpdatedBy], [LastUpdatedAt], [FeatureId]) VALUES (8, N'User', N'Ununser.User', N'rbogle', CAST(N'2018-09-10 00:00:00.000' AS DateTime), 5)
SET IDENTITY_INSERT [dbo].[Flags] OFF
SET IDENTITY_INSERT [dbo].[FlagProperties] ON
INSERT [dbo].[FlagProperties] ([Id], [Name], [Description], [LastUpdatedBy], [LastUpdatedAt], [PropertyKey], [PropertyValue], [FlagId]) VALUES (1, N'Enabled', N'Enabled.', N'rbogle', CAST(N'2018-09-10 00:00:00.000' AS DateTime), N'enabled', N'true', 1)
INSERT [dbo].[FlagProperties] ([Id], [Name], [Description], [LastUpdatedBy], [LastUpdatedAt], [PropertyKey], [PropertyValue], [FlagId]) VALUES (2, N'Enabled', N'Enabled.', N'rbogle', CAST(N'2018-09-10 00:00:00.000' AS DateTime), N'enabled', N'false', 2)
INSERT [dbo].[FlagProperties] ([Id], [Name], [Description], [LastUpdatedBy], [LastUpdatedAt], [PropertyKey], [PropertyValue], [FlagId]) VALUES (3, N'Enabled', N'Enabled.', N'rbogle', CAST(N'2018-09-10 00:00:00.000' AS DateTime), N'enabled', N'true', 3)
INSERT [dbo].[FlagProperties] ([Id], [Name], [Description], [LastUpdatedBy], [LastUpdatedAt], [PropertyKey], [PropertyValue], [FlagId]) VALUES (4, N'Enabled', N'Enabled.', N'rbogle', CAST(N'2018-09-10 00:00:00.000' AS DateTime), N'enabled', N'true', 4)
INSERT [dbo].[FlagProperties] ([Id], [Name], [Description], [LastUpdatedBy], [LastUpdatedAt], [PropertyKey], [PropertyValue], [FlagId]) VALUES (5, N'Enabled', N'Enabled.', N'rbogle', CAST(N'2018-09-10 00:00:00.000' AS DateTime), N'enabled', N'true', 5)
INSERT [dbo].[FlagProperties] ([Id], [Name], [Description], [LastUpdatedBy], [LastUpdatedAt], [PropertyKey], [PropertyValue], [FlagId]) VALUES (6, N'Date', N'Date.', N'rbogle', CAST(N'2018-09-10 00:00:00.000' AS DateTime), N'date', N'2018-06-21', 6)
INSERT [dbo].[FlagProperties] ([Id], [Name], [Description], [LastUpdatedBy], [LastUpdatedAt], [PropertyKey], [PropertyValue], [FlagId]) VALUES (8, N'Users', N'Users.', N'rbogle', CAST(N'2018-09-10 00:00:00.000' AS DateTime), N'users', N'dummy', 7)
INSERT [dbo].[FlagProperties] ([Id], [Name], [Description], [LastUpdatedBy], [LastUpdatedAt], [PropertyKey], [PropertyValue], [FlagId]) VALUES (10, N'Name', N'Name.', N'rbogle', CAST(N'2018-09-10 00:00:00.000' AS DateTime), N'name', N'jenny', 8)
INSERT [dbo].[FlagProperties] ([Id], [Name], [Description], [LastUpdatedBy], [LastUpdatedAt], [PropertyKey], [PropertyValue], [FlagId]) VALUES (12, N'Users', N'Users.', N'rbogle', CAST(N'2018-09-10 00:00:00.000' AS DateTime), N'users', N'tom,dick,harry', 8)
SET IDENTITY_INSERT [dbo].[FlagProperties] OFF
INSERT [dbo].[UserFeature] ([Users_Id], [Features_Id]) VALUES (1, 1)
INSERT [dbo].[UserFeature] ([Users_Id], [Features_Id]) VALUES (1, 2)
INSERT [dbo].[UserFeature] ([Users_Id], [Features_Id]) VALUES (1, 4)
SET IDENTITY_INSERT [dbo].[Users] ON
INSERT [dbo].[Users] ([Id], [Name], [LastUpdatedBy], [LastUpdatedAt]) VALUES (1, N'dummy', N'rbogle', CAST(N'2018-09-15 00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Users] OFF
GO


ALTER TABLE [dbo].[FlagProperties]  WITH CHECK ADD  CONSTRAINT [FK_FlagToFlagProperty] FOREIGN KEY([FlagId])
REFERENCES [dbo].[Flags] ([Id])
GO
ALTER TABLE [dbo].[FlagProperties] CHECK CONSTRAINT [FK_FlagToFlagProperty]
GO
ALTER TABLE [dbo].[Flags]  WITH CHECK ADD  CONSTRAINT [FK_FeatureFlag] FOREIGN KEY([FeatureId])
REFERENCES [dbo].[Features] ([Id])
GO
ALTER TABLE [dbo].[Flags] CHECK CONSTRAINT [FK_FeatureFlag]
GO
ALTER TABLE [dbo].[FeatureRole]  WITH CHECK ADD  CONSTRAINT [FK_FeatureRole_Feature] FOREIGN KEY([Features_Id])
REFERENCES [dbo].[Features] ([Id])
GO
ALTER TABLE [dbo].[FeatureRole] CHECK CONSTRAINT [FK_FeatureRole_Feature]
GO
ALTER TABLE [dbo].[UserFeature]  WITH CHECK ADD  CONSTRAINT [FK_UserFeature_Feature] FOREIGN KEY([Features_Id])
REFERENCES [dbo].[Features] ([Id])
GO
ALTER TABLE [dbo].[UserFeature] CHECK CONSTRAINT [FK_UserFeature_Feature]
GO
ALTER TABLE [dbo].[UserFeature]  WITH CHECK ADD  CONSTRAINT [FK_UserFeature_User] FOREIGN KEY([Users_Id])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserFeature] CHECK CONSTRAINT [FK_UserFeature_User]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_User] FOREIGN KEY([Users_Id])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_User]
GO

SELECT f.Name as 'Feature Name', fg.Name as "Flag Name", fp.PropertyKey, fp.PropertyValue
FROM features as f
left join Flags as fg on f.Id = fg.FeatureId
left join FlagProperties as fp on fp.FlagId = fg.Id
ORDER BY f.Name
GO

SELECT Users.Name as 'User Name', Features.Name as 'Feature Name'
FROM Users
JOIN UserFeature ON Users.Id = UserFeature.Users_Id
JOIN Features ON UserFeature.Features_Id = Features.Id
GO
