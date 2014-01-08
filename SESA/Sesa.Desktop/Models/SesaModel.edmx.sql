
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 12/13/2013 23:20:23
-- Generated from EDMX file: D:\REZA\Project\SESA\Sesa.Desktop\Models\SesaModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [OrgSesaDb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_BaseUnit_DerivedUnit]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Unit] DROP CONSTRAINT [FK_BaseUnit_DerivedUnit];
GO
IF OBJECT_ID(N'[dbo].[FK_InternalMaterialProductMaterial]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InternalProductMaterial] DROP CONSTRAINT [FK_InternalMaterialProductMaterial];
GO
IF OBJECT_ID(N'[dbo].[FK_InternalProductProductMaterial]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InternalProductMaterial] DROP CONSTRAINT [FK_InternalProductProductMaterial];
GO
IF OBJECT_ID(N'[dbo].[FK_MaterialExternalProductMaterial]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExternalProductMaterial] DROP CONSTRAINT [FK_MaterialExternalProductMaterial];
GO
IF OBJECT_ID(N'[dbo].[FK_MaterialTestimonyDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TestimonyDetail] DROP CONSTRAINT [FK_MaterialTestimonyDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_MaterialWarehouseBillDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WarehouseBillDetail] DROP CONSTRAINT [FK_MaterialWarehouseBillDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductExternalProductMaterial]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExternalProductMaterial] DROP CONSTRAINT [FK_ProductExternalProductMaterial];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductProductionTestimony]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Testimony] DROP CONSTRAINT [FK_ProductProductionTestimony];
GO
IF OBJECT_ID(N'[dbo].[FK_TestimonyTestimonyDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TestimonyDetail] DROP CONSTRAINT [FK_TestimonyTestimonyDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_UnitMaterial]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Material] DROP CONSTRAINT [FK_UnitMaterial];
GO
IF OBJECT_ID(N'[dbo].[FK_UnitWarehouseBillDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WarehouseBillDetail] DROP CONSTRAINT [FK_UnitWarehouseBillDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_WarehouseBillTestimonyDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TestimonyDetail] DROP CONSTRAINT [FK_WarehouseBillTestimonyDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_WarehouseBillWarehouseBillDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WarehouseBillDetail] DROP CONSTRAINT [FK_WarehouseBillWarehouseBillDetail];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ExternalProductMaterial]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExternalProductMaterial];
GO
IF OBJECT_ID(N'[dbo].[InternalProductMaterial]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InternalProductMaterial];
GO
IF OBJECT_ID(N'[dbo].[Material]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Material];
GO
IF OBJECT_ID(N'[dbo].[Product]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Product];
GO
IF OBJECT_ID(N'[dbo].[Testimony]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Testimony];
GO
IF OBJECT_ID(N'[dbo].[TestimonyDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TestimonyDetail];
GO
IF OBJECT_ID(N'[dbo].[Unit]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Unit];
GO
IF OBJECT_ID(N'[dbo].[WarehouseBill]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WarehouseBill];
GO
IF OBJECT_ID(N'[dbo].[WarehouseBillDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WarehouseBillDetail];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Product'
CREATE TABLE [dbo].[Product] (
    [Id] uniqueidentifier  NOT NULL,
    [Caption] nvarchar(max)  NOT NULL,
    [ValidInputPercent] decimal(18,0)  NOT NULL,
    [AddedValuePercent] decimal(18,0)  NOT NULL
);
GO

-- Creating table 'Material'
CREATE TABLE [dbo].[Material] (
    [Id] uniqueidentifier  NOT NULL,
    [Caption] nvarchar(max)  NOT NULL,
    [GramFormula] nvarchar(max)  NULL,
    [IsFloatValue] bit  NOT NULL,
    [IsPackingType] bit  NOT NULL,
    [Unit_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'InternalProductMaterial'
CREATE TABLE [dbo].[InternalProductMaterial] (
    [Id] uniqueidentifier  NOT NULL,
    [Value] decimal(18,0)  NOT NULL,
    [Pert] decimal(18,0)  NOT NULL,
    [Material_Id] uniqueidentifier  NOT NULL,
    [Product_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Unit'
CREATE TABLE [dbo].[Unit] (
    [Id] uniqueidentifier  NOT NULL,
    [Symbol] nvarchar(max)  NOT NULL,
    [Caption] nvarchar(max)  NOT NULL,
    [BaseCoefficient] nvarchar(max)  NULL,
    [BaseUnit_Id] uniqueidentifier  NULL
);
GO

-- Creating table 'WarehouseBill'
CREATE TABLE [dbo].[WarehouseBill] (
    [Id] uniqueidentifier  NOT NULL,
    [RowNumber] nvarchar(max)  NOT NULL,
    [EmissionDate] nvarchar(max)  NOT NULL,
    [IsInternal] bit  NOT NULL
);
GO

-- Creating table 'WarehouseBillDetail'
CREATE TABLE [dbo].[WarehouseBillDetail] (
    [Id] uniqueidentifier  NOT NULL,
    [Value] decimal(18,0)  NOT NULL,
    [Weight] decimal(18,0)  NOT NULL,
    [Material_Id] uniqueidentifier  NOT NULL,
    [WarehouseBill_Id] uniqueidentifier  NOT NULL,
    [Unit_Id] uniqueidentifier  NULL
);
GO

-- Creating table 'Testimony'
CREATE TABLE [dbo].[Testimony] (
    [Id] uniqueidentifier  NOT NULL,
    [ProductCount] int  NOT NULL,
    [PackingType] nvarchar(max)  NOT NULL,
    [PureWeight] decimal(18,0)  NOT NULL,
    [RealWeight] decimal(18,0)  NOT NULL,
    [RequestNumber] nvarchar(max)  NOT NULL,
    [RequestDate] nvarchar(max)  NOT NULL,
    [HeaderNumber] nvarchar(max)  NOT NULL,
    [HeaderDate] nvarchar(max)  NOT NULL,
    [Product_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'TestimonyDetail'
CREATE TABLE [dbo].[TestimonyDetail] (
    [Id] uniqueidentifier  NOT NULL,
    [Value] decimal(18,0)  NOT NULL,
    [IsInternal] bit  NOT NULL,
    [Weight] decimal(18,0)  NOT NULL,
    [Testimony_Id] uniqueidentifier  NOT NULL,
    [WarehouseBill_Id] uniqueidentifier  NOT NULL,
    [Material_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'ExternalProductMaterial'
CREATE TABLE [dbo].[ExternalProductMaterial] (
    [Id] uniqueidentifier  NOT NULL,
    [Value] decimal(18,0)  NOT NULL,
    [Pert] decimal(18,0)  NOT NULL,
    [Product_Id] uniqueidentifier  NOT NULL,
    [Material_Id] uniqueidentifier  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Product'
ALTER TABLE [dbo].[Product]
ADD CONSTRAINT [PK_Product]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Material'
ALTER TABLE [dbo].[Material]
ADD CONSTRAINT [PK_Material]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'InternalProductMaterial'
ALTER TABLE [dbo].[InternalProductMaterial]
ADD CONSTRAINT [PK_InternalProductMaterial]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Unit'
ALTER TABLE [dbo].[Unit]
ADD CONSTRAINT [PK_Unit]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WarehouseBill'
ALTER TABLE [dbo].[WarehouseBill]
ADD CONSTRAINT [PK_WarehouseBill]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WarehouseBillDetail'
ALTER TABLE [dbo].[WarehouseBillDetail]
ADD CONSTRAINT [PK_WarehouseBillDetail]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Testimony'
ALTER TABLE [dbo].[Testimony]
ADD CONSTRAINT [PK_Testimony]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TestimonyDetail'
ALTER TABLE [dbo].[TestimonyDetail]
ADD CONSTRAINT [PK_TestimonyDetail]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ExternalProductMaterial'
ALTER TABLE [dbo].[ExternalProductMaterial]
ADD CONSTRAINT [PK_ExternalProductMaterial]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Material_Id] in table 'InternalProductMaterial'
ALTER TABLE [dbo].[InternalProductMaterial]
ADD CONSTRAINT [FK_InternalMaterialProductMaterial]
    FOREIGN KEY ([Material_Id])
    REFERENCES [dbo].[Material]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_InternalMaterialProductMaterial'
CREATE INDEX [IX_FK_InternalMaterialProductMaterial]
ON [dbo].[InternalProductMaterial]
    ([Material_Id]);
GO

-- Creating foreign key on [Product_Id] in table 'InternalProductMaterial'
ALTER TABLE [dbo].[InternalProductMaterial]
ADD CONSTRAINT [FK_InternalProductProductMaterial]
    FOREIGN KEY ([Product_Id])
    REFERENCES [dbo].[Product]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_InternalProductProductMaterial'
CREATE INDEX [IX_FK_InternalProductProductMaterial]
ON [dbo].[InternalProductMaterial]
    ([Product_Id]);
GO

-- Creating foreign key on [Unit_Id] in table 'Material'
ALTER TABLE [dbo].[Material]
ADD CONSTRAINT [FK_UnitMaterial]
    FOREIGN KEY ([Unit_Id])
    REFERENCES [dbo].[Unit]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UnitMaterial'
CREATE INDEX [IX_FK_UnitMaterial]
ON [dbo].[Material]
    ([Unit_Id]);
GO

-- Creating foreign key on [Material_Id] in table 'WarehouseBillDetail'
ALTER TABLE [dbo].[WarehouseBillDetail]
ADD CONSTRAINT [FK_MaterialWarehouseBillDetail]
    FOREIGN KEY ([Material_Id])
    REFERENCES [dbo].[Material]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MaterialWarehouseBillDetail'
CREATE INDEX [IX_FK_MaterialWarehouseBillDetail]
ON [dbo].[WarehouseBillDetail]
    ([Material_Id]);
GO

-- Creating foreign key on [WarehouseBill_Id] in table 'WarehouseBillDetail'
ALTER TABLE [dbo].[WarehouseBillDetail]
ADD CONSTRAINT [FK_WarehouseBillWarehouseBillDetail]
    FOREIGN KEY ([WarehouseBill_Id])
    REFERENCES [dbo].[WarehouseBill]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_WarehouseBillWarehouseBillDetail'
CREATE INDEX [IX_FK_WarehouseBillWarehouseBillDetail]
ON [dbo].[WarehouseBillDetail]
    ([WarehouseBill_Id]);
GO

-- Creating foreign key on [Unit_Id] in table 'WarehouseBillDetail'
ALTER TABLE [dbo].[WarehouseBillDetail]
ADD CONSTRAINT [FK_UnitWarehouseBillDetail]
    FOREIGN KEY ([Unit_Id])
    REFERENCES [dbo].[Unit]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UnitWarehouseBillDetail'
CREATE INDEX [IX_FK_UnitWarehouseBillDetail]
ON [dbo].[WarehouseBillDetail]
    ([Unit_Id]);
GO

-- Creating foreign key on [Product_Id] in table 'Testimony'
ALTER TABLE [dbo].[Testimony]
ADD CONSTRAINT [FK_ProductProductionTestimony]
    FOREIGN KEY ([Product_Id])
    REFERENCES [dbo].[Product]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductProductionTestimony'
CREATE INDEX [IX_FK_ProductProductionTestimony]
ON [dbo].[Testimony]
    ([Product_Id]);
GO

-- Creating foreign key on [Testimony_Id] in table 'TestimonyDetail'
ALTER TABLE [dbo].[TestimonyDetail]
ADD CONSTRAINT [FK_TestimonyTestimonyDetail]
    FOREIGN KEY ([Testimony_Id])
    REFERENCES [dbo].[Testimony]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TestimonyTestimonyDetail'
CREATE INDEX [IX_FK_TestimonyTestimonyDetail]
ON [dbo].[TestimonyDetail]
    ([Testimony_Id]);
GO

-- Creating foreign key on [WarehouseBill_Id] in table 'TestimonyDetail'
ALTER TABLE [dbo].[TestimonyDetail]
ADD CONSTRAINT [FK_WarehouseBillTestimonyDetail]
    FOREIGN KEY ([WarehouseBill_Id])
    REFERENCES [dbo].[WarehouseBill]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_WarehouseBillTestimonyDetail'
CREATE INDEX [IX_FK_WarehouseBillTestimonyDetail]
ON [dbo].[TestimonyDetail]
    ([WarehouseBill_Id]);
GO

-- Creating foreign key on [Material_Id] in table 'TestimonyDetail'
ALTER TABLE [dbo].[TestimonyDetail]
ADD CONSTRAINT [FK_MaterialTestimonyDetail]
    FOREIGN KEY ([Material_Id])
    REFERENCES [dbo].[Material]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MaterialTestimonyDetail'
CREATE INDEX [IX_FK_MaterialTestimonyDetail]
ON [dbo].[TestimonyDetail]
    ([Material_Id]);
GO

-- Creating foreign key on [Product_Id] in table 'ExternalProductMaterial'
ALTER TABLE [dbo].[ExternalProductMaterial]
ADD CONSTRAINT [FK_ProductExternalProductMaterial]
    FOREIGN KEY ([Product_Id])
    REFERENCES [dbo].[Product]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductExternalProductMaterial'
CREATE INDEX [IX_FK_ProductExternalProductMaterial]
ON [dbo].[ExternalProductMaterial]
    ([Product_Id]);
GO

-- Creating foreign key on [Material_Id] in table 'ExternalProductMaterial'
ALTER TABLE [dbo].[ExternalProductMaterial]
ADD CONSTRAINT [FK_MaterialExternalProductMaterial]
    FOREIGN KEY ([Material_Id])
    REFERENCES [dbo].[Material]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MaterialExternalProductMaterial'
CREATE INDEX [IX_FK_MaterialExternalProductMaterial]
ON [dbo].[ExternalProductMaterial]
    ([Material_Id]);
GO

-- Creating foreign key on [BaseUnit_Id] in table 'Unit'
ALTER TABLE [dbo].[Unit]
ADD CONSTRAINT [FK_BaseUnit_DerivedUnit]
    FOREIGN KEY ([BaseUnit_Id])
    REFERENCES [dbo].[Unit]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BaseUnit_DerivedUnit'
CREATE INDEX [IX_FK_BaseUnit_DerivedUnit]
ON [dbo].[Unit]
    ([BaseUnit_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------