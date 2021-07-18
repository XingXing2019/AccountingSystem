IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Sources] (
    [SourceId] int NOT NULL IDENTITY,
    [Code] nvarchar(max) NULL,
    CONSTRAINT [PK_Sources] PRIMARY KEY ([SourceId])
);
GO

CREATE TABLE [Vendors] (
    [VendorId] int NOT NULL IDENTITY,
    [VendorCode] nvarchar(max) NULL,
    [VendorName] nvarchar(max) NULL,
    CONSTRAINT [PK_Vendors] PRIMARY KEY ([VendorId])
);
GO

CREATE TABLE [Transactions] (
    [TransactionId] int NOT NULL IDENTITY,
    [BatchEntry] nvarchar(max) NULL,
    [GLCode] nvarchar(max) NULL,
    [GLDescription] nvarchar(max) NULL,
    [PostingSeq] nvarchar(max) NULL,
    [InvoiceNo] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [Debit] decimal(18,2) NULL,
    [Credit] decimal(18,2) NULL,
    [DocDate] datetime2 NOT NULL,
    [TransactionDate] datetime2 NOT NULL,
    [InvoiceReceiveDate] datetime2 NULL,
    [VendorId] int NULL,
    [SourceId] int NULL,
    CONSTRAINT [PK_Transactions] PRIMARY KEY ([TransactionId]),
    CONSTRAINT [FK_Transactions_Sources_SourceId] FOREIGN KEY ([SourceId]) REFERENCES [Sources] ([SourceId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Transactions_Vendors_VendorId] FOREIGN KEY ([VendorId]) REFERENCES [Vendors] ([VendorId]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_Transactions_SourceId] ON [Transactions] ([SourceId]);
GO

CREATE INDEX [IX_Transactions_VendorId] ON [Transactions] ([VendorId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210718074723_init', N'5.0.8');
GO

COMMIT;
GO

