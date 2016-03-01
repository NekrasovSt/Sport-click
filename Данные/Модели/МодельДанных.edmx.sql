
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/01/2016 14:58:17
-- Generated from EDMX file: D:\Code\СпортКлик\Данные\Модели\МодельДанных.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [data];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Товар_Категории]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Товар] DROP CONSTRAINT [FK_Товар_Категории];
GO
IF OBJECT_ID(N'[dbo].[FK_Заказ_СтатусЗаказа]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Заказ] DROP CONSTRAINT [FK_Заказ_СтатусЗаказа];
GO
IF OBJECT_ID(N'[dbo].[FK_СвойствоЗаказа_Заказ]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[СвойствоЗаказа] DROP CONSTRAINT [FK_СвойствоЗаказа_Заказ];
GO
IF OBJECT_ID(N'[dbo].[FK_Товар_Производители]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Товар] DROP CONSTRAINT [FK_Товар_Производители];
GO
IF OBJECT_ID(N'[dbo].[FK_ИсторияЗаказа_Заказ]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ИсторияЗаказа] DROP CONSTRAINT [FK_ИсторияЗаказа_Заказ];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Категории]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Категории];
GO
IF OBJECT_ID(N'[dbo].[Товар]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Товар];
GO
IF OBJECT_ID(N'[dbo].[Производители]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Производители];
GO
IF OBJECT_ID(N'[dbo].[СтатусЗаказа]', 'U') IS NOT NULL
    DROP TABLE [dbo].[СтатусЗаказа];
GO
IF OBJECT_ID(N'[dbo].[СвойствоЗаказа]', 'U') IS NOT NULL
    DROP TABLE [dbo].[СвойствоЗаказа];
GO
IF OBJECT_ID(N'[dbo].[Заказ]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Заказ];
GO
IF OBJECT_ID(N'[dbo].[ИсторияЗаказа]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ИсторияЗаказа];
GO
IF OBJECT_ID(N'[dbo].[aspnet_Profile]', 'U') IS NOT NULL
    DROP TABLE [dbo].[aspnet_Profile];
GO
IF OBJECT_ID(N'[dbo].[Отзыв]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Отзыв];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Категории'
CREATE TABLE [dbo].[Категории] (
    [Ид] int IDENTITY(1,1) NOT NULL,
    [Имя] nvarchar(max)  NOT NULL,
    [Описание] nvarchar(max)  NULL,
    [ИмяФайлаИзображения] nvarchar(50)  NULL
);
GO

-- Creating table 'Товар'
CREATE TABLE [dbo].[Товар] (
    [Ид] int IDENTITY(1,1) NOT NULL,
    [Имя] nvarchar(max)  NOT NULL,
    [Описание] nvarchar(max)  NULL,
    [ИмяФайлаИзображения] nvarchar(50)  NULL,
    [КатегорияИд] int  NOT NULL,
    [ПроизводительИд] int  NOT NULL,
    [Цена] decimal(10,2)  NOT NULL,
    [Скидка] int  NULL
);
GO

-- Creating table 'Производители'
CREATE TABLE [dbo].[Производители] (
    [Ид] int IDENTITY(1,1) NOT NULL,
    [Имя] nvarchar(max)  NOT NULL,
    [Описание] nvarchar(max)  NULL,
    [ИмяФайлаИзображения] nvarchar(50)  NULL
);
GO

-- Creating table 'СтатусЗаказа'
CREATE TABLE [dbo].[СтатусЗаказа] (
    [Ид] int IDENTITY(1,1) NOT NULL,
    [Имя] nvarchar(50)  NOT NULL,
    [Описание] nvarchar(max)  NULL
);
GO

-- Creating table 'СвойствоЗаказа'
CREATE TABLE [dbo].[СвойствоЗаказа] (
    [Ид] int IDENTITY(1,1) NOT NULL,
    [ИмяТовара] nvarchar(max)  NOT NULL,
    [ИдТовара] int  NOT NULL,
    [ИмяПроизводителя] nvarchar(max)  NULL,
    [Цена] decimal(18,2)  NOT NULL,
    [Скидка] int  NULL,
    [ИдЗаказа] int  NOT NULL,
    [Количество] int  NOT NULL
);
GO

-- Creating table 'Заказ'
CREATE TABLE [dbo].[Заказ] (
    [Ид] int IDENTITY(1,1) NOT NULL,
    [ДатаСоздания] datetime  NOT NULL,
    [ИмяКлиента] nvarchar(100)  NULL,
    [ФамилияКлиента] nvarchar(100)  NULL,
    [ОтчествоКлиента] nvarchar(100)  NULL,
    [Телефон] nvarchar(50)  NULL,
    [Почта] nvarchar(50)  NULL,
    [ИдКлиента] uniqueidentifier  NOT NULL,
    [ИдСотрудника] uniqueidentifier  NOT NULL,
    [ДатаИзменения] datetime  NOT NULL,
    [ИдСтатуса] int  NOT NULL
);
GO

-- Creating table 'ИсторияЗаказа'
CREATE TABLE [dbo].[ИсторияЗаказа] (
    [Ид] int IDENTITY(1,1) NOT NULL,
    [ИдЗаказ] int  NOT NULL,
    [ДатаИзменения] datetime  NOT NULL,
    [ИдСотрудника] uniqueidentifier  NOT NULL,
    [Коментарий] nvarchar(max)  NULL,
    [СтатусИд] int  NOT NULL
);
GO

-- Creating table 'aspnet_Profile'
CREATE TABLE [dbo].[aspnet_Profile] (
    [UserId] uniqueidentifier  NOT NULL,
    [PropertyNames] nvarchar(max)  NOT NULL,
    [PropertyValuesString] nvarchar(max)  NOT NULL,
    [PropertyValuesBinary] varbinary(max)  NOT NULL,
    [LastUpdatedDate] datetime  NOT NULL
);
GO

-- Creating table 'Отзыв'
CREATE TABLE [dbo].[Отзыв] (
    [Ид] int IDENTITY(1,1) NOT NULL,
    [Текст] nvarchar(max)  NOT NULL,
    [Проверен] bit  NOT NULL,
    [Дата] datetime  NOT NULL,
    [Пользователь] nvarchar(50)  NOT NULL,
    [ПользовательИд] uniqueidentifier  NOT NULL,
    [Сущность] nvarchar(50)  NOT NULL,
    [СущностьИд] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Ид] in table 'Категории'
ALTER TABLE [dbo].[Категории]
ADD CONSTRAINT [PK_Категории]
    PRIMARY KEY CLUSTERED ([Ид] ASC);
GO

-- Creating primary key on [Ид] in table 'Товар'
ALTER TABLE [dbo].[Товар]
ADD CONSTRAINT [PK_Товар]
    PRIMARY KEY CLUSTERED ([Ид] ASC);
GO

-- Creating primary key on [Ид] in table 'Производители'
ALTER TABLE [dbo].[Производители]
ADD CONSTRAINT [PK_Производители]
    PRIMARY KEY CLUSTERED ([Ид] ASC);
GO

-- Creating primary key on [Ид] in table 'СтатусЗаказа'
ALTER TABLE [dbo].[СтатусЗаказа]
ADD CONSTRAINT [PK_СтатусЗаказа]
    PRIMARY KEY CLUSTERED ([Ид] ASC);
GO

-- Creating primary key on [Ид] in table 'СвойствоЗаказа'
ALTER TABLE [dbo].[СвойствоЗаказа]
ADD CONSTRAINT [PK_СвойствоЗаказа]
    PRIMARY KEY CLUSTERED ([Ид] ASC);
GO

-- Creating primary key on [Ид] in table 'Заказ'
ALTER TABLE [dbo].[Заказ]
ADD CONSTRAINT [PK_Заказ]
    PRIMARY KEY CLUSTERED ([Ид] ASC);
GO

-- Creating primary key on [Ид] in table 'ИсторияЗаказа'
ALTER TABLE [dbo].[ИсторияЗаказа]
ADD CONSTRAINT [PK_ИсторияЗаказа]
    PRIMARY KEY CLUSTERED ([Ид] ASC);
GO

-- Creating primary key on [UserId] in table 'aspnet_Profile'
ALTER TABLE [dbo].[aspnet_Profile]
ADD CONSTRAINT [PK_aspnet_Profile]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [Ид] in table 'Отзыв'
ALTER TABLE [dbo].[Отзыв]
ADD CONSTRAINT [PK_Отзыв]
    PRIMARY KEY CLUSTERED ([Ид] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [КатегорияИд] in table 'Товар'
ALTER TABLE [dbo].[Товар]
ADD CONSTRAINT [FK_Товар_Категории]
    FOREIGN KEY ([КатегорияИд])
    REFERENCES [dbo].[Категории]
        ([Ид])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Товар_Категории'
CREATE INDEX [IX_FK_Товар_Категории]
ON [dbo].[Товар]
    ([КатегорияИд]);
GO

-- Creating foreign key on [ИдСтатуса] in table 'Заказ'
ALTER TABLE [dbo].[Заказ]
ADD CONSTRAINT [FK_Заказ_СтатусЗаказа]
    FOREIGN KEY ([ИдСтатуса])
    REFERENCES [dbo].[СтатусЗаказа]
        ([Ид])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Заказ_СтатусЗаказа'
CREATE INDEX [IX_FK_Заказ_СтатусЗаказа]
ON [dbo].[Заказ]
    ([ИдСтатуса]);
GO

-- Creating foreign key on [ИдЗаказа] in table 'СвойствоЗаказа'
ALTER TABLE [dbo].[СвойствоЗаказа]
ADD CONSTRAINT [FK_СвойствоЗаказа_Заказ]
    FOREIGN KEY ([ИдЗаказа])
    REFERENCES [dbo].[Заказ]
        ([Ид])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_СвойствоЗаказа_Заказ'
CREATE INDEX [IX_FK_СвойствоЗаказа_Заказ]
ON [dbo].[СвойствоЗаказа]
    ([ИдЗаказа]);
GO

-- Creating foreign key on [ПроизводительИд] in table 'Товар'
ALTER TABLE [dbo].[Товар]
ADD CONSTRAINT [FK_Товар_Производители]
    FOREIGN KEY ([ПроизводительИд])
    REFERENCES [dbo].[Производители]
        ([Ид])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Товар_Производители'
CREATE INDEX [IX_FK_Товар_Производители]
ON [dbo].[Товар]
    ([ПроизводительИд]);
GO

-- Creating foreign key on [ИдЗаказ] in table 'ИсторияЗаказа'
ALTER TABLE [dbo].[ИсторияЗаказа]
ADD CONSTRAINT [FK_ИсторияЗаказа_Заказ]
    FOREIGN KEY ([ИдЗаказ])
    REFERENCES [dbo].[Заказ]
        ([Ид])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ИсторияЗаказа_Заказ'
CREATE INDEX [IX_FK_ИсторияЗаказа_Заказ]
ON [dbo].[ИсторияЗаказа]
    ([ИдЗаказ]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------