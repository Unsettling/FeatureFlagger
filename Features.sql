﻿-- Creating table 'Features'
CREATE TABLE [dbo].[Features] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [LastUpdatedBy] nvarchar(50)  NOT NULL,
    [LastUpdatedAt] datetime  NOT NULL,
    [UpdateVersion] binary(8)  NULL
);
GO
-- Creating primary key on [Id] in table 'Features'
ALTER TABLE [dbo].[Features]
ADD CONSTRAINT [PK_Features]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO