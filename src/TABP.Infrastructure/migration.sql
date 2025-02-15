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
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250113174554_DatabaseSchemaCreation'
)
BEGIN
    ALTER TABLE [HotelVisit] DROP CONSTRAINT [FK_HotelVisit_Hotels_HotelId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250113174554_DatabaseSchemaCreation'
)
BEGIN
    ALTER TABLE [HotelVisit] DROP CONSTRAINT [FK_HotelVisit_Users_UserId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250113174554_DatabaseSchemaCreation'
)
BEGIN
    ALTER TABLE [HotelVisit] DROP CONSTRAINT [PK_HotelVisit];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250113174554_DatabaseSchemaCreation'
)
BEGIN
    EXEC sp_rename N'[HotelVisit]', N'HotelVisits', 'OBJECT';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250113174554_DatabaseSchemaCreation'
)
BEGIN
    EXEC sp_rename N'[HotelVisits].[IX_HotelVisit_UserId]', N'IX_HotelVisits_UserId', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250113174554_DatabaseSchemaCreation'
)
BEGIN
    EXEC sp_rename N'[HotelVisits].[IX_HotelVisit_HotelId]', N'IX_HotelVisits_HotelId', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250113174554_DatabaseSchemaCreation'
)
BEGIN
    ALTER TABLE [HotelVisits] ADD CONSTRAINT [PK_HotelVisits] PRIMARY KEY ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250113174554_DatabaseSchemaCreation'
)
BEGIN
    ALTER TABLE [HotelVisits] ADD CONSTRAINT [FK_HotelVisits_Hotels_HotelId] FOREIGN KEY ([HotelId]) REFERENCES [Hotels] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250113174554_DatabaseSchemaCreation'
)
BEGIN
    ALTER TABLE [HotelVisits] ADD CONSTRAINT [FK_HotelVisits_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250113174554_DatabaseSchemaCreation'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250113174554_DatabaseSchemaCreation', N'9.0.1');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250116133814_Add RoomBookings'
)
BEGIN
    ALTER TABLE [RoomBooking] DROP CONSTRAINT [FK_RoomBooking_Rooms_RoomId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250116133814_Add RoomBookings'
)
BEGIN
    ALTER TABLE [RoomBooking] DROP CONSTRAINT [FK_RoomBooking_Users_UserId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250116133814_Add RoomBookings'
)
BEGIN
    ALTER TABLE [RoomBooking] DROP CONSTRAINT [PK_RoomBooking];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250116133814_Add RoomBookings'
)
BEGIN
    EXEC sp_rename N'[RoomBooking]', N'RoomBookings', 'OBJECT';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250116133814_Add RoomBookings'
)
BEGIN
    EXEC sp_rename N'[HotelVisits].[VisitDate]', N'Date', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250116133814_Add RoomBookings'
)
BEGIN
    EXEC sp_rename N'[RoomBookings].[StartingDate]', N'CheckOutDate', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250116133814_Add RoomBookings'
)
BEGIN
    EXEC sp_rename N'[RoomBookings].[Price]', N'TotalPrice', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250116133814_Add RoomBookings'
)
BEGIN
    EXEC sp_rename N'[RoomBookings].[EndingDate]', N'CheckInDate', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250116133814_Add RoomBookings'
)
BEGIN
    EXEC sp_rename N'[RoomBookings].[IX_RoomBooking_UserId]', N'IX_RoomBookings_UserId', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250116133814_Add RoomBookings'
)
BEGIN
    EXEC sp_rename N'[RoomBookings].[IX_RoomBooking_RoomId]', N'IX_RoomBookings_RoomId', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250116133814_Add RoomBookings'
)
BEGIN
    DECLARE @var sysname;
    SELECT @var = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[HotelVisits]') AND [c].[name] = N'TimeSpent');
    IF @var IS NOT NULL EXEC(N'ALTER TABLE [HotelVisits] DROP CONSTRAINT [' + @var + '];');
    ALTER TABLE [HotelVisits] ALTER COLUMN [TimeSpent] time NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250116133814_Add RoomBookings'
)
BEGIN
    ALTER TABLE [Discounts] ADD [CreationDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250116133814_Add RoomBookings'
)
BEGIN
    ALTER TABLE [Discounts] ADD [ModificationDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250116133814_Add RoomBookings'
)
BEGIN
    ALTER TABLE [RoomBookings] ADD CONSTRAINT [PK_RoomBookings] PRIMARY KEY ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250116133814_Add RoomBookings'
)
BEGIN
    ALTER TABLE [RoomBookings] ADD CONSTRAINT [FK_RoomBookings_Rooms_RoomId] FOREIGN KEY ([RoomId]) REFERENCES [Rooms] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250116133814_Add RoomBookings'
)
BEGIN
    ALTER TABLE [RoomBookings] ADD CONSTRAINT [FK_RoomBookings_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250116133814_Add RoomBookings'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250116133814_Add RoomBookings', N'9.0.1');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250120124326_AddRoomType'
)
BEGIN
    ALTER TABLE [Discounts] ADD [roomType] int NOT NULL DEFAULT 0;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250120124326_AddRoomType'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250120124326_AddRoomType', N'9.0.1');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250121160738_AddCartEntity'
)
BEGIN
    ALTER TABLE [CartItems] DROP CONSTRAINT [FK_CartItems_Users_UserId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250121160738_AddCartEntity'
)
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Rooms]') AND [c].[name] = N'IsAvailable');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Rooms] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Rooms] DROP COLUMN [IsAvailable];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250121160738_AddCartEntity'
)
BEGIN
    EXEC sp_rename N'[CartItems].[CreationDate]', N'AddedAt', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250121160738_AddCartEntity'
)
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CartItems]') AND [c].[name] = N'UserId');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [CartItems] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [CartItems] ALTER COLUMN [UserId] uniqueidentifier NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250121160738_AddCartEntity'
)
BEGIN
    ALTER TABLE [CartItems] ADD [CartId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250121160738_AddCartEntity'
)
BEGIN
    CREATE TABLE [Carts] (
        [Id] uniqueidentifier NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        [TotalPrice] decimal(18,2) NOT NULL,
        [Status] int NOT NULL,
        [ModificationDate] datetime2 NOT NULL,
        [CheckOutDate] datetime2 NULL,
        CONSTRAINT [PK_Carts] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Carts_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250121160738_AddCartEntity'
)
BEGIN
    CREATE INDEX [IX_CartItems_CartId] ON [CartItems] ([CartId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250121160738_AddCartEntity'
)
BEGIN
    CREATE INDEX [IX_Carts_UserId] ON [Carts] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250121160738_AddCartEntity'
)
BEGIN
    ALTER TABLE [CartItems] ADD CONSTRAINT [FK_CartItems_Carts_CartId] FOREIGN KEY ([CartId]) REFERENCES [Carts] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250121160738_AddCartEntity'
)
BEGIN
    ALTER TABLE [CartItems] ADD CONSTRAINT [FK_CartItems_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250121160738_AddCartEntity'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250121160738_AddCartEntity', N'9.0.1');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250121164912_AddCartItemNewMetafields'
)
BEGIN
    EXEC sp_rename N'[CartItems].[AddedAt]', N'CreationDate', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250121164912_AddCartItemNewMetafields'
)
BEGIN
    ALTER TABLE [CartItems] ADD [CheckInDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250121164912_AddCartItemNewMetafields'
)
BEGIN
    ALTER TABLE [CartItems] ADD [CheckOutDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250121164912_AddCartItemNewMetafields'
)
BEGIN
    ALTER TABLE [CartItems] ADD [Notes] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250121164912_AddCartItemNewMetafields'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250121164912_AddCartItemNewMetafields', N'9.0.1');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250123161321_HotelVisitMetadataAlternation'
)
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[HotelVisits]') AND [c].[name] = N'Date');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [HotelVisits] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [HotelVisits] DROP COLUMN [Date];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250123161321_HotelVisitMetadataAlternation'
)
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[HotelVisits]') AND [c].[name] = N'ModificationDate');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [HotelVisits] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [HotelVisits] DROP COLUMN [ModificationDate];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250123161321_HotelVisitMetadataAlternation'
)
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[HotelVisits]') AND [c].[name] = N'TimeSpent');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [HotelVisits] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [HotelVisits] DROP COLUMN [TimeSpent];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250123161321_HotelVisitMetadataAlternation'
)
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[HotelReviews]') AND [c].[name] = N'IsVerifiedPurchase');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [HotelReviews] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [HotelReviews] DROP COLUMN [IsVerifiedPurchase];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250123161321_HotelVisitMetadataAlternation'
)
BEGIN
    ALTER TABLE [CartItems] ADD [Price] decimal(18,2) NOT NULL DEFAULT 0.0;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250123161321_HotelVisitMetadataAlternation'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250123161321_HotelVisitMetadataAlternation', N'9.0.1');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250125150231_AddImageEntity'
)
BEGIN
    CREATE TABLE [Images] (
        [Id] uniqueidentifier NOT NULL,
        [Path] nvarchar(max) NOT NULL,
        [EntityId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Images] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250125150231_AddImageEntity'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250125150231_AddImageEntity', N'9.0.1');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250126154046_FixRoleUser'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250126154046_FixRoleUser', N'9.0.1');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250206165118_EntityConfigurations'
)
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'Username');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [Users] ALTER COLUMN [Username] nvarchar(100) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250206165118_EntityConfigurations'
)
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'LastName');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [Users] ALTER COLUMN [LastName] nvarchar(50) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250206165118_EntityConfigurations'
)
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'FirstName');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [Users] ALTER COLUMN [FirstName] nvarchar(50) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250206165118_EntityConfigurations'
)
BEGIN
    DECLARE @var10 sysname;
    SELECT @var10 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Hotels]') AND [c].[name] = N'OwnerName');
    IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Hotels] DROP CONSTRAINT [' + @var10 + '];');
    ALTER TABLE [Hotels] ALTER COLUMN [OwnerName] nvarchar(100) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250206165118_EntityConfigurations'
)
BEGIN
    DECLARE @var11 sysname;
    SELECT @var11 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Hotels]') AND [c].[name] = N'Name');
    IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [Hotels] DROP CONSTRAINT [' + @var11 + '];');
    ALTER TABLE [Hotels] ALTER COLUMN [Name] nvarchar(100) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250206165118_EntityConfigurations'
)
BEGIN
    DECLARE @var12 sysname;
    SELECT @var12 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Hotels]') AND [c].[name] = N'DetailedDescription');
    IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [Hotels] DROP CONSTRAINT [' + @var12 + '];');
    ALTER TABLE [Hotels] ALTER COLUMN [DetailedDescription] nvarchar(1000) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250206165118_EntityConfigurations'
)
BEGIN
    DECLARE @var13 sysname;
    SELECT @var13 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Hotels]') AND [c].[name] = N'BriefDescription');
    IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [Hotels] DROP CONSTRAINT [' + @var13 + '];');
    ALTER TABLE [Hotels] ALTER COLUMN [BriefDescription] nvarchar(100) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250206165118_EntityConfigurations'
)
BEGIN
    DECLARE @var14 sysname;
    SELECT @var14 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[HotelReviews]') AND [c].[name] = N'Feedback');
    IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [HotelReviews] DROP CONSTRAINT [' + @var14 + '];');
    ALTER TABLE [HotelReviews] ALTER COLUMN [Feedback] nvarchar(1000) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250206165118_EntityConfigurations'
)
BEGIN
    DECLARE @var15 sysname;
    SELECT @var15 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Discounts]') AND [c].[name] = N'Reason');
    IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [Discounts] DROP CONSTRAINT [' + @var15 + '];');
    ALTER TABLE [Discounts] ALTER COLUMN [Reason] nvarchar(100) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250206165118_EntityConfigurations'
)
BEGIN
    DECLARE @var16 sysname;
    SELECT @var16 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Cities]') AND [c].[name] = N'Name');
    IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [Cities] DROP CONSTRAINT [' + @var16 + '];');
    ALTER TABLE [Cities] ALTER COLUMN [Name] nvarchar(50) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250206165118_EntityConfigurations'
)
BEGIN
    DECLARE @var17 sysname;
    SELECT @var17 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Cities]') AND [c].[name] = N'CountryName');
    IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [Cities] DROP CONSTRAINT [' + @var17 + '];');
    ALTER TABLE [Cities] ALTER COLUMN [CountryName] nvarchar(50) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250206165118_EntityConfigurations'
)
BEGIN
    CREATE INDEX [IX_Users_Username] ON [Users] ([Username]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250206165118_EntityConfigurations'
)
BEGIN
    CREATE INDEX [IX_HotelVisits_CreationDate] ON [HotelVisits] ([CreationDate]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250206165118_EntityConfigurations'
)
BEGIN
    CREATE INDEX [IX_Discounts_EndingDate] ON [Discounts] ([EndingDate]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250206165118_EntityConfigurations'
)
BEGIN
    CREATE INDEX [IX_Discounts_StartingDate] ON [Discounts] ([StartingDate]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250206165118_EntityConfigurations'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250206165118_EntityConfigurations', N'9.0.1');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250206165146_AddViews'
)
BEGIN
    CREATE VIEW vw_AvailableRooms AS
    SELECT
        R.Id
        R.Number,
        R.Type,
        R.AdultsCapacity,
        R.ChildrenCapacity,
        R.PricePerNight,
        R.HotelId,
        H.Name AS HotelName,
        H.StarRating,
        R.CreationDate,
        R.ModificationDate
    FROM Rooms R
    JOIN Hotels H ON R.HotelId = H.Id
    WHERE NOT EXISTS (
        SELECT 1
        FROM RoomBookings B
        WHERE B.RoomId = R.Id
        AND GETDATE() BETWEEN B.CheckInDate AND B.CheckOutDate
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250206165146_AddViews'
)
BEGIN
    CREATE VIEW vw_HotelRevenue AS
    SELECT 
        H.Id AS HotelId,
        H.Name,
        H.BriefDescription,
        H.DetailedDescription,
        H.StarRating,
        H.OwnerName,
        H.Geolocation,
        H.CreationDate,
        H.ModificationDate,
        H.CityId,
        C.Name AS CityName,
        C.CountryName AS CityCountryName,
        SUM(R.PricePerNight * DATEDIFF(DAY, B.StartingDate, B.EndingDate)) AS TotalRevenue
    FROM Hotels H
    JOIN Rooms R ON H.Id = R.HotelId
    JOIN Bookings B ON R.Id = B.RoomId
    JOIN Cities C ON H.CityId = C.Id
    WHERE B.EndingDate < GETDATE()
    GROUP BY 
        H.Id, 
        H.Name, 
        H.BriefDescription, 
        H.DetailedDescription, 
        H.StarRating, 
        H.OwnerName, 
        H.Geolocation, 
        H.CreationDate, 
        H.ModificationDate, 
        H.CityId,
        C.Name,
        C.CountryName;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250206165146_AddViews'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250206165146_AddViews', N'9.0.1');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250207113438_SeedTables'
)
BEGIN
    CREATE TABLE [Cities] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(50) NOT NULL,
        [CountryName] nvarchar(50) NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        [ModificationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Cities] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250207113438_SeedTables'
)
BEGIN
    CREATE TABLE [Images] (
        [Id] uniqueidentifier NOT NULL,
        [Path] nvarchar(max) NOT NULL,
        [EntityId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Images] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250207113438_SeedTables'
)
BEGIN
    CREATE TABLE [Roles] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250207113438_SeedTables'
)
BEGIN
    CREATE TABLE [Users] (
        [Id] uniqueidentifier NOT NULL,
        [Username] nvarchar(100) NOT NULL,
        [Password] nvarchar(max) NOT NULL,
        [FirstName] nvarchar(50) NOT NULL,
        [LastName] nvarchar(50) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [LastLogin] datetime2 NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250207113438_SeedTables'
)
BEGIN
    CREATE TABLE [Hotels] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(100) NOT NULL,
        [BriefDescription] nvarchar(100) NOT NULL,
        [DetailedDescription] nvarchar(1000) NOT NULL,
        [StarRating] decimal(18,2) NOT NULL,
        [OwnerName] nvarchar(100) NOT NULL,
        [Geolocation] nvarchar(max) NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        [ModificationDate] datetime2 NOT NULL,
        [CityId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Hotels] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Hotels_Cities_CityId] FOREIGN KEY ([CityId]) REFERENCES [Cities] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250207113438_SeedTables'
)
BEGIN
    CREATE TABLE [Carts] (
        [Id] uniqueidentifier NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        [TotalPrice] decimal(18,2) NOT NULL,
        [Status] int NOT NULL,
        [ModificationDate] datetime2 NOT NULL,
        [CheckOutDate] datetime2 NULL,
        CONSTRAINT [PK_Carts] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Carts_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250207113438_SeedTables'
)
BEGIN
    CREATE TABLE [RoleUser] (
        [RolesId] uniqueidentifier NOT NULL,
        [UsersId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_RoleUser] PRIMARY KEY ([RolesId], [UsersId]),
        CONSTRAINT [FK_RoleUser_Roles_RolesId] FOREIGN KEY ([RolesId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_RoleUser_Users_UsersId] FOREIGN KEY ([UsersId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250207113438_SeedTables'
)
BEGIN
    CREATE TABLE [Discounts] (
        [Id] uniqueidentifier NOT NULL,
        [Reason] nvarchar(100) NOT NULL,
        [StartingDate] datetime2 NOT NULL,
        [EndingDate] datetime2 NOT NULL,
        [AmountPercentage] decimal(18,2) NOT NULL,
        [roomType] int NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        [ModificationDate] datetime2 NOT NULL,
        [HotelId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Discounts] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Discounts_Hotels_HotelId] FOREIGN KEY ([HotelId]) REFERENCES [Hotels] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250207113438_SeedTables'
)
BEGIN
    CREATE TABLE [HotelReviews] (
        [Id] uniqueidentifier NOT NULL,
        [Feedback] nvarchar(1000) NOT NULL,
        [Rating] int NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        [ModificationDate] datetime2 NOT NULL,
        [HotelId] uniqueidentifier NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_HotelReviews] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_HotelReviews_Hotels_HotelId] FOREIGN KEY ([HotelId]) REFERENCES [Hotels] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_HotelReviews_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250207113438_SeedTables'
)
BEGIN
    CREATE TABLE [HotelVisits] (
        [Id] uniqueidentifier NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        [HotelId] uniqueidentifier NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_HotelVisits] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_HotelVisits_Hotels_HotelId] FOREIGN KEY ([HotelId]) REFERENCES [Hotels] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_HotelVisits_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250207113438_SeedTables'
)
BEGIN
    CREATE TABLE [Rooms] (
        [Id] uniqueidentifier NOT NULL,
        [Number] int NOT NULL,
        [Type] int NOT NULL,
        [AdultsCapacity] int NOT NULL,
        [ChildrenCapacity] int NOT NULL,
        [PricePerNight] int NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        [ModificationDate] datetime2 NOT NULL,
        [HotelId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Rooms] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Rooms_Hotels_HotelId] FOREIGN KEY ([HotelId]) REFERENCES [Hotels] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250207113438_SeedTables'
)
BEGIN
    CREATE TABLE [CartItems] (
        [Id] uniqueidentifier NOT NULL,
        [Price] decimal(18,2) NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        [Notes] nvarchar(max) NOT NULL,
        [CheckInDate] datetime2 NOT NULL,
        [CheckOutDate] datetime2 NOT NULL,
        [RoomId] uniqueidentifier NOT NULL,
        [CartId] uniqueidentifier NOT NULL,
        [UserId] uniqueidentifier NULL,
        CONSTRAINT [PK_CartItems] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_CartItems_Carts_CartId] FOREIGN KEY ([CartId]) REFERENCES [Carts] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_CartItems_Rooms_RoomId] FOREIGN KEY ([RoomId]) REFERENCES [Rooms] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_CartItems_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250207113438_SeedTables'
)
BEGIN
    CREATE TABLE [RoomBookings] (
        [Id] uniqueidentifier NOT NULL,
        [CheckInDate] datetime2 NOT NULL,
        [CheckOutDate] datetime2 NOT NULL,
        [TotalPrice] decimal(18,2) NOT NULL,
        [Status] int NOT NULL,
        [Notes] nvarchar(max) NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        [ModificationDate] datetime2 NOT NULL,
        [RoomId] uniqueidentifier NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_RoomBookings] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_RoomBookings_Rooms_RoomId] FOREIGN KEY ([RoomId]) REFERENCES [Rooms] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_RoomBookings_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250207113438_SeedTables'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CountryName', N'CreationDate', N'ModificationDate', N'Name') AND [object_id] = OBJECT_ID(N'[Cities]'))
        SET IDENTITY_INSERT [Cities] ON;
    EXEC(N'INSERT INTO [Cities] ([Id], [CountryName], [CreationDate], [ModificationDate], [Name])
    VALUES (''25eeca82-c189-4bbb-0209-08dd388dc7b9'', N''Jordan'', ''2025-01-19T13:34:10.1870000'', ''2025-01-19T13:34:10.1870000'', N''Amman''),
    (''45e0dcb1-62af-409a-8349-08dd4691b096'', N''UK'', ''2022-01-01T00:00:00.0000000Z'', ''2024-11-01T00:00:00.0000000Z'', N''London''),
    (''7ca4b1aa-fa9c-40a2-5601-08dd46916b70'', N''France'', ''2023-01-01T00:00:00.0000000Z'', ''2024-12-01T00:00:00.0000000Z'', N''Paris''),
    (''e6554375-0932-462c-0207-08dd388dc7b9'', N''Palestine'', ''2025-01-19T13:32:54.8310000'', ''2025-01-19T13:32:54.8310000'', N''Hebron''),
    (''f3ebb6de-2c8f-42fc-0208-08dd388dc7b9'', N''Palestine'', ''2025-01-19T13:33:39.4290000'', ''2025-01-19T13:33:39.4290000'', N''Safad''),
    (''fe33e674-6c47-49dc-c1f7-08dd46922384'', N''USA'', ''2024-01-01T00:00:00.0000000Z'', ''2024-10-01T00:00:00.0000000Z'', N''New York'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CountryName', N'CreationDate', N'ModificationDate', N'Name') AND [object_id] = OBJECT_ID(N'[Cities]'))
        SET IDENTITY_INSERT [Cities] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250207113438_SeedTables'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AdultsCapacity', N'ChildrenCapacity', N'CreationDate', N'HotelId', N'ModificationDate', N'Number', N'PricePerNight', N'Type') AND [object_id] = OBJECT_ID(N'[Rooms]'))
        SET IDENTITY_INSERT [Rooms] ON;
    EXEC(N'INSERT INTO [Rooms] ([Id], [AdultsCapacity], [ChildrenCapacity], [CreationDate], [HotelId], [ModificationDate], [Number], [PricePerNight], [Type])
    VALUES (''3914316b-1b87-46f7-294f-08dc29a4de37'', 2, 2, ''2025-01-19T14:34:10.1870000'', ''b7535eac-a5b4-49bb-1269-08dc29a09a1f'', ''2025-01-19T14:34:10.1870000'', 3, 75, 0),
    (''601a0d84-0435-4221-294e-08dc29a4de37'', 2, 0, ''2025-01-19T14:34:10.1870000'', ''b7535eac-a5b4-49bb-1269-08dc29a09a1f'', ''2025-01-19T14:34:10.1870000'', 2, 100, 0),
    (''b3f52184-3330-4750-294c-08dc29a4de37'', 2, 3, ''2024-02-09T19:26:58.9308602'', ''b7535eac-a5b4-49bb-1269-08dc29a09a1f'', ''2024-02-09T19:26:58.9308604'', 1, 50, 0),
    (''e2f3a4b5-c6d7-8901-e2f3-a4b5c6d78901'', 3, 1, ''2025-01-19T14:34:10.1870000'', ''b7535eac-a5b4-49bb-1269-08dc29a09a1f'', ''2025-01-19T14:34:10.1870000'', 4, 220, 1),
    (''f339b369-2a05-4eea-294d-08dc29a4de37'', 2, 3, ''2025-01-19T14:34:10.1870000'', ''b7535eac-a5b4-49bb-1269-08dc29a09a1f'', ''2025-01-19T14:34:10.1870000'', 1, 50, 0)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AdultsCapacity', N'ChildrenCapacity', N'CreationDate', N'HotelId', N'ModificationDate', N'Number', N'PricePerNight', N'Type') AND [object_id] = OBJECT_ID(N'[Rooms]'))
        SET IDENTITY_INSERT [Rooms] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250207113438_SeedTables'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'BriefDescription', N'CityId', N'CreationDate', N'DetailedDescription', N'Geolocation', N'ModificationDate', N'Name', N'OwnerName', N'StarRating') AND [object_id] = OBJECT_ID(N'[Hotels]'))
        SET IDENTITY_INSERT [Hotels] ON;
    EXEC(N'INSERT INTO [Hotels] ([Id], [BriefDescription], [CityId], [CreationDate], [DetailedDescription], [Geolocation], [ModificationDate], [Name], [OwnerName], [StarRating])
    VALUES (''1632e0bb-7aa4-45b3-8dc7-d48e8dc002ec'', N''A 4-star hotel in the heart of Times Square.'', ''fe33e674-6c47-49dc-c1f7-08dd46922384'', ''2024-02-09T19:03:10.5885700'', N''Stay in the heart of Times Square...'', N''40.7566480079687,-73.98881546193508'', ''2024-02-09T19:03:10.5885704'', N''Hilton New York Times Square'', N''Hilton'', 4.2),
    (''45e0dcb1-62af-409a-8349-08dd4691b096'', N''A 4-star hotel located above Tower Hill Underground Station.'', ''45e0dcb1-62af-409a-8349-08dd4691b096'', ''2024-02-09T18:54:53.6850569'', N''Experience luxury and convenience...'', N''51.510223410295524,-0.07644353237381915'', ''2024-02-09T20:45:21.0122021'', N''citizenM Tower Of London hotel'', N''citizenM'', 4.0),
    (''75ae0504-974c-4ff2-ab13-c30374ac8558'', N''A 4-star hotel'', ''e6554375-0932-462c-0207-08dd388dc7b9'', ''2025-01-19T13:34:10.1870000'', N''Stay in the heart of Hebron'', N''42.7566480079687,-74.98881546193508'', ''2025-01-19T13:34:10.1870000'', N''Abu Mazen'', N''Abu Mazen maybe'', 4.2),
    (''98123ca9-624e-4743-1268-08dc29a09a1f'', N''A 4-star hotel offering panoramic views of Paris.'', ''7ca4b1aa-fa9c-40a2-5601-08dd46916b70'', ''2024-02-09T18:56:58.1733565'', N''The 4-star Pullman Paris Tour Eiffel hotel...'', N''48.85567419020331,2.2928680490125637'', ''2024-02-09T18:56:58.1733570'', N''Pullman Paris Tour Eiffel'', N''Pullman'', 4.4),
    (''ceb2b836-3b8c-4ea9-93cc-f5c442d5d966'', N''A 4-star hotel in Hebron'', ''e6554375-0932-462c-0207-08dd388dc7b9'', ''2025-01-19T13:34:10.1870000'', N''Stay in the heart of Hebron'', N''20.7566480079687,-7.98881546193508'', ''2025-01-19T13:34:10.1870000'', N''Burj Herbawi 2'', N''Bashar'', 4.1),
    (''d9123022-25c0-4493-b5eb-b11cfd829554'', N''A 4-star hotel in Amman'', ''25eeca82-c189-4bbb-0209-08dd388dc7b9'', ''2025-01-19T13:34:10.1870000'', N''Stay in the heart of Amman'', N''40.7566480079687,-73.98881546193508'', ''2025-01-19T13:34:10.1870000'', N''Burj Herbawi'', N''Bashar'', 4.4)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'BriefDescription', N'CityId', N'CreationDate', N'DetailedDescription', N'Geolocation', N'ModificationDate', N'Name', N'OwnerName', N'StarRating') AND [object_id] = OBJECT_ID(N'[Hotels]'))
        SET IDENTITY_INSERT [Hotels] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250207113438_SeedTables'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CheckInDate', N'CheckOutDate', N'CreationDate', N'ModificationDate', N'Notes', N'RoomId', N'Status', N'TotalPrice', N'UserId') AND [object_id] = OBJECT_ID(N'[RoomBookings]'))
        SET IDENTITY_INSERT [RoomBookings] ON;
    EXEC(N'INSERT INTO [RoomBookings] ([Id], [CheckInDate], [CheckOutDate], [CreationDate], [ModificationDate], [Notes], [RoomId], [Status], [TotalPrice], [UserId])
    VALUES (''1a2b3c4d-e5f6-4c92-3031-323334353637'', ''2025-07-01T14:00:00.0000000'', ''2025-07-05T11:00:00.0000000'', ''2025-06-15T19:15:00.0000000'', ''2025-06-15T19:15:00.0000000'', N''Yoink'', ''3914316b-1b87-46f7-294f-08dc29a4de37'', 0, 0.0, ''f82bff97-d6d4-4d47-424e-08dd3e217c75''),
    (''4d5e6f7a-b8c9-4f2c-3940-414243444546'', ''2025-10-20T14:00:00.0000000'', ''2025-10-30T11:00:00.0000000'', ''2025-09-01T07:20:00.0000000'', ''2025-09-01T07:20:00.0000000'', N''Yoink'', ''e2f3a4b5-c6d7-8901-e2f3-a4b5c6d78901'', 0, 0.0, ''923043b5-e9c8-4ded-9cfa-08dd3e211f93''),
    (''61237e1a-928b-494a-be63-d9b562a65896'', ''2025-03-06T12:12:54.2950000'', ''2025-03-09T13:08:36.2950000'', ''2025-02-07T11:00:19.3890000'', ''2025-02-07T11:00:19.3890000'', N''Yoink'', ''e2f3a4b5-c6d7-8901-e2f3-a4b5c6d78901'', 0, 0.0, ''5e91fd72-53c3-43ee-3d87-08dd3498aaa5''),
    (''9c0d1e2f-a4b5-4b7b-5455-565758596061'', ''2026-03-15T14:00:00.0000000'', ''2026-03-20T11:00:00.0000000'', ''2026-02-20T17:45:00.0000000'', ''2026-02-20T17:45:00.0000000'', N''Yoink'', ''b3f52184-3330-4750-294c-08dc29a4de37'', 0, 0.0, ''f82bff97-d6d4-4d47-424e-08dd3e217c75''),
    (''e1f2a3b4-c5d6-4f7a-2324-252627282930'', ''2025-05-01T14:00:00.0000000'', ''2025-05-08T11:00:00.0000000'', ''2025-04-01T11:55:00.0000000'', ''2025-04-01T11:55:00.0000000'', N''Yoink'', ''b3f52184-3330-4750-294c-08dc29a4de37'', 0, 0.0, ''39ad172a-602e-4118-29d9-08dd398180c1''),
    (''f9a0b1c2-d3e4-418b-2627-282930313233'', ''2025-06-10T14:00:00.0000000'', ''2025-06-12T11:00:00.0000000'', ''2025-05-20T08:00:00.0000000'', ''2025-05-20T08:00:00.0000000'', N''Yoink'', ''601a0d84-0435-4221-294e-08dc29a4de37'', 0, 0.0, ''923043b5-e9c8-4ded-9cfa-08dd3e211f93'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CheckInDate', N'CheckOutDate', N'CreationDate', N'ModificationDate', N'Notes', N'RoomId', N'Status', N'TotalPrice', N'UserId') AND [object_id] = OBJECT_ID(N'[RoomBookings]'))
        SET IDENTITY_INSERT [RoomBookings] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250207113438_SeedTables'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AmountPercentage', N'CreationDate', N'EndingDate', N'HotelId', N'ModificationDate', N'Reason', N'StartingDate', N'roomType') AND [object_id] = OBJECT_ID(N'[Discounts]'))
        SET IDENTITY_INSERT [Discounts] ON;
    EXEC(N'INSERT INTO [Discounts] ([Id], [AmountPercentage], [CreationDate], [EndingDate], [HotelId], [ModificationDate], [Reason], [StartingDate], [roomType])
    VALUES (''027e91be-e337-4b48-ac33-a6ee6992d708'', 30.0, ''2025-01-19T13:34:10.1870000'', ''2025-04-19T13:34:10.1870000'', ''d9123022-25c0-4493-b5eb-b11cfd829554'', ''2025-02-09T19:06:19.6610000'', N''New season'', ''2025-01-19T13:34:10.1870000'', 1),
    (''3329cf25-8cc8-4034-b032-6e1db78e4dd9'', 17.0, ''2025-01-19T13:34:10.1870000'', ''2025-04-19T13:34:10.1870000'', ''1632e0bb-7aa4-45b3-8dc7-d48e8dc002ec'', ''2025-03-19T13:34:10.1870000'', N''New season'', ''2025-01-19T13:34:10.1870000'', 1),
    (''335126f5-ea9a-49a0-978a-9503d04449db'', 15.0, ''2025-01-19T13:34:10.1870000'', ''2025-04-19T13:34:10.1870000'', ''98123ca9-624e-4743-1268-08dc29a09a1f'', ''2025-01-19T13:34:10.1870000'', N''New season'', ''2025-01-19T13:34:10.1870000'', 1),
    (''892944e1-6c20-4530-a865-250989e23248'', 15.0, ''2025-01-19T13:34:10.1870000'', ''2025-04-19T13:34:10.1870000'', ''75ae0504-974c-4ff2-ab13-c30374ac8558'', ''2025-04-19T13:34:10.1870000'', N''New season'', ''2025-01-19T13:34:10.1870000'', 1),
    (''cda35c4b-d597-4186-afe7-26bd3af94397'', 30.0, ''2025-01-19T13:34:10.1870000'', ''2025-04-09T19:06:19.6610000'', ''d9123022-25c0-4493-b5eb-b11cfd829554'', ''2025-01-19T13:34:10.1870000'', N''New season'', ''2025-01-19T13:34:10.1870000'', 2),
    (''d7a473e6-ec2f-48d6-852a-2e68c9993f9b'', 30.0, ''2025-01-19T13:34:10.1870000'', ''2025-04-09T19:06:19.6610000'', ''d9123022-25c0-4493-b5eb-b11cfd829554'', ''2025-01-19T13:34:10.1870000'', N''Why not'', ''2025-01-19T13:34:10.1870000'', 0),
    (''f0a22dda-4769-4509-913a-1be8a8d5b88f'', 10.0, ''2025-01-19T13:34:10.1870000'', ''2025-04-19T13:34:10.1870000'', ''45e0dcb1-62af-409a-8349-08dd4691b096'', ''2025-04-19T13:34:10.1870000'', N''New season'', ''2025-01-19T13:34:10.1870000'', 1)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AmountPercentage', N'CreationDate', N'EndingDate', N'HotelId', N'ModificationDate', N'Reason', N'StartingDate', N'roomType') AND [object_id] = OBJECT_ID(N'[Discounts]'))
        SET IDENTITY_INSERT [Discounts] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250207113438_SeedTables'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreationDate', N'Feedback', N'HotelId', N'ModificationDate', N'Rating', N'UserId') AND [object_id] = OBJECT_ID(N'[HotelReviews]'))
        SET IDENTITY_INSERT [HotelReviews] ON;
    EXEC(N'INSERT INTO [HotelReviews] ([Id], [CreationDate], [Feedback], [HotelId], [ModificationDate], [Rating], [UserId])
    VALUES (''0d1e2f3a-b4c5-4d2c-6d7e-8f9a0b1c2d3e'', ''2025-09-02T18:20:00.0000000'', N''Location is not ideal for tourists, but okay for business.'', ''75ae0504-974c-4ff2-ab13-c30374ac8558'', ''2025-09-02T18:20:00.0000000'', 3, ''f82bff97-d6d4-4d47-424e-08dd3e217c75''),
    (''1a2b3c4d-e5f6-4def-7a8b-c9d0e1f2a3b4'', ''2025-06-18T11:30:00.0000000'', N''Service was top-notch, highly recommend.'', ''98123ca9-624e-4743-1268-08dc29a09a1f'', ''2025-06-18T11:30:00.0000000'', 5, ''923043b5-e9c8-4ded-9cfa-08dd3e211f93''),
    (''1e2f3a4b-c5d6-4d3d-7e8f-9a0b1c2d3e4f'', ''2025-07-01T11:15:00.0000000'', N''Great location in Amman, close to everything.'', ''d9123022-25c0-4493-b5eb-b11cfd829554'', ''2025-07-01T11:15:00.0000000'', 4, ''5e91fd72-53c3-43ee-3d87-08dd3498aaa5''),
    (''2b3c4d5e-f6a7-4fae-8b9c-0d1e2f3a4b5c'', ''2025-07-05T17:15:00.0000000'', N''A bit pricey but worth it for the view.'', ''98123ca9-624e-4743-1268-08dc29a09a1f'', ''2025-07-05T17:15:00.0000000'', 3, ''f82bff97-d6d4-4d47-424e-08dd3e217c75''),
    (''2f3a4b5c-d6e7-4d4e-8f9a-0b1c2d3e4f5a'', ''2025-08-10T15:55:00.0000000'', N''Helpful staff and comfortable rooms.'', ''d9123022-25c0-4493-b5eb-b11cfd829554'', ''2025-08-10T15:55:00.0000000'', 3, ''39ad172a-602e-4118-29d9-08dd398180c1''),
    (''3a4b5c6d-e7f8-4d5f-9a0b-1c2d3e4f5a6b'', ''2025-09-25T09:05:00.0000000'', N''Breakfast was okay, but overall a pleasant stay.'', ''d9123022-25c0-4493-b5eb-b11cfd829554'', ''2025-09-25T09:05:00.0000000'', 3, ''923043b5-e9c8-4ded-9cfa-08dd3e211f93''),
    (''3c4d5e6f-a7b8-4fbf-9c0d-1e2f3a4b5c6d'', ''2025-05-01T08:50:00.0000000'', N''Perfect location for exploring Times Square.'', ''1632e0bb-7aa4-45b3-8dc7-d48e8dc002ec'', ''2025-05-01T08:50:00.0000000'', 4, ''5e91fd72-53c3-43ee-3d87-08dd3498aaa5''),
    (''4b5c6d7e-f8a9-4d6a-0b1c-2d3e4f5a6b7c'', ''2025-10-05T14:30:00.0000000'', N''Could be cleaner, but decent for the price.'', ''d9123022-25c0-4493-b5eb-b11cfd829554'', ''2025-10-05T14:30:00.0000000'', 3, ''f82bff97-d6d4-4d47-424e-08dd3e217c75''),
    (''4d5e6f7a-b8c9-4cc0-0d1e-2f3a4b5c6d7e'', ''2025-06-12T13:25:00.0000000'', N''Comfortable and clean rooms, great amenities.'', ''1632e0bb-7aa4-45b3-8dc7-d48e8dc002ec'', ''2025-06-12T13:25:00.0000000'', 3, ''39ad172a-602e-4118-29d9-08dd398180c1''),
    (''5c6d7e8f-9a0b-4d7b-1c2d-3e4f5a6b7c8d'', ''2025-08-01T10:00:00.0000000'', N''Quiet location, good for relaxing.'', ''ceb2b836-3b8c-4ea9-93cc-f5c442d5d966'', ''2025-08-01T10:00:00.0000000'', 3, ''5e91fd72-53c3-43ee-3d87-08dd3498aaa5''),
    (''5e6f7a8b-c9d0-4cd1-1e2f-3a4b5c6d7e8f'', ''2025-07-20T10:10:00.0000000'', N''Good for a short stay, a bit busy though.'', ''1632e0bb-7aa4-45b3-8dc7-d48e8dc002ec'', ''2025-07-20T10:10:00.0000000'', 3, ''923043b5-e9c8-4ded-9cfa-08dd3e211f93''),
    (''6d7e8f9a-0b1c-4d8c-2d3e-4f5a6b7c8d9e'', ''2025-09-15T16:40:00.0000000'', N''Basic amenities, suitable for a short stay.'', ''ceb2b836-3b8c-4ea9-93cc-f5c442d5d966'', ''2025-09-15T16:40:00.0000000'', 3, ''39ad172a-602e-4118-29d9-08dd398180c1''),
    (''6f7a8b9c-d0e1-4ce2-2f3a-4b5c6d7e8f9a'', ''2025-08-01T19:55:00.0000000'', N''Location is amazing but hotel feels dated.'', ''1632e0bb-7aa4-45b3-8dc7-d48e8dc002ec'', ''2025-08-01T19:55:00.0000000'', 4, ''f82bff97-d6d4-4d47-424e-08dd3e217c75''),
    (''7a8b9c0d-e1f2-4cf3-3a4b-5c6d7e8f9a0b'', ''2025-06-01T15:40:00.0000000'', N''Authentic experience, great hospitality.'', ''75ae0504-974c-4ff2-ab13-c30374ac8558'', ''2025-06-01T15:40:00.0000000'', 4, ''5e91fd72-53c3-43ee-3d87-08dd3498aaa5''),
    (''7e8f9a0b-1c2d-4d9d-3e4f-5a6b7c8d9e0f'', ''2025-10-22T11:20:00.0000000'', N''Friendly staff, but facilities are limited.'', ''ceb2b836-3b8c-4ea9-93cc-f5c442d5d966'', ''2025-10-22T11:20:00.0000000'', 3, ''923043b5-e9c8-4ded-9cfa-08dd3e211f93''),
    (''8b9c0d1e-f2a3-4d0a-4b5c-6d7e8f9a0b1c'', ''2025-07-12T21:00:00.0000000'', N''Simple but clean, friendly staff.'', ''75ae0504-974c-4ff2-ab13-c30374ac8558'', ''2025-07-12T21:00:00.0000000'', 3, ''39ad172a-602e-4118-29d9-08dd398180c1''),
    (''8f9a0b1c-2d3e-4dae-4f5a-6b7c8d9e0f1a'', ''2025-11-01T20:00:00.0000000'', N''Not the best, needs improvement.'', ''ceb2b836-3b8c-4ea9-93cc-f5c442d5d966'', ''2025-11-01T20:00:00.0000000'', 2, ''f82bff97-d6d4-4d47-424e-08dd3e217c75''),
    (''9c0d1e2f-a3b4-4d1b-5c6d-7e8f9a0b1c2d'', ''2025-08-19T12:50:00.0000000'', N''Value for money, good for budget travelers.'', ''75ae0504-974c-4ff2-ab13-c30374ac8558'', ''2025-08-19T12:50:00.0000000'', 3, ''923043b5-e9c8-4ded-9cfa-08dd3e211f93''),
    (''a1b2c3d4-e5f6-4789-1a2b-c3d4e5f6a7b8'', ''2025-03-10T14:20:00.0000000'', N''Great location, modern and clean rooms.'', ''45e0dcb1-62af-409a-8349-08dd4691b096'', ''2025-03-10T14:20:00.0000000'', 5, ''5e91fd72-53c3-43ee-3d87-08dd3498aaa5''),
    (''b2c3d4e5-f6a7-489a-2b3c-d4e5f6a7b8c9'', ''2025-04-15T18:30:00.0000000'', N''Sleek hotel with friendly staff and amazing views.'', ''45e0dcb1-62af-409a-8349-08dd4691b096'', ''2025-04-15T18:30:00.0000000'', 4, ''39ad172a-602e-4118-29d9-08dd398180c1''),
    (''c3d4e5f6-a7b8-49ab-3c4d-e5f6a7b8c9d0'', ''2025-05-22T09:45:00.0000000'', N''Excellent stay, breakfast was delicious.'', ''45e0dcb1-62af-409a-8349-08dd4691b096'', ''2025-05-22T09:45:00.0000000'', 5, ''923043b5-e9c8-4ded-9cfa-08dd3e211f93''),
    (''d4e5f6a7-b8c9-4bcd-4d5e-f6a7b8c9d0e1'', ''2025-06-01T16:00:00.0000000'', N''A bit noisy but overall a good experience.'', ''45e0dcb1-62af-409a-8349-08dd4691b096'', ''2025-06-01T16:00:00.0000000'', 3, ''f82bff97-d6d4-4d47-424e-08dd3e217c75''),
    (''e5f6a7b8-c9d0-4cde-5e6f-a7b8c9d0e1f2'', ''2025-04-01T12:10:00.0000000'', N''Unbeatable views of the Eiffel Tower, luxurious.'', ''98123ca9-624e-4743-1268-08dc29a09a1f'', ''2025-04-01T12:10:00.0000000'', 5, ''5e91fd72-53c3-43ee-3d87-08dd3498aaa5''),
    (''f6a7b8c9-d0e1-4def-6f7a-b8c9d0e1f2a3'', ''2025-05-10T20:40:00.0000000'', N''Wonderful location, rooms are very comfortable.'', ''98123ca9-624e-4743-1268-08dc29a09a1f'', ''2025-05-10T20:40:00.0000000'', 4, ''39ad172a-602e-4118-29d9-08dd398180c1'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreationDate', N'Feedback', N'HotelId', N'ModificationDate', N'Rating', N'UserId') AND [object_id] = OBJECT_ID(N'[HotelReviews]'))
        SET IDENTITY_INSERT [HotelReviews] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250207113438_SeedTables'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreationDate', N'HotelId', N'UserId') AND [object_id] = OBJECT_ID(N'[HotelVisits]'))
        SET IDENTITY_INSERT [HotelVisits] ON;
    EXEC(N'INSERT INTO [HotelVisits] ([Id], [CreationDate], [HotelId], [UserId])
    VALUES (''0e1f2a3b-4c5d-466e-7f8a-9b0c1d2e3f4a'', ''2025-04-12T14:20:00.0000000'', ''1632e0bb-7aa4-45b3-8dc7-d48e8dc002ec'', ''923043b5-e9c8-4ded-9cfa-08dd3e211f93''),
    (''11111111-2222-3333-4444-555555555555'', ''2026-02-28T16:30:00.0000000'', ''1632e0bb-7aa4-45b3-8dc7-d48e8dc002ec'', ''923043b5-e9c8-4ded-9cfa-08dd3e211f93''),
    (''1b2c3d4e-f5a6-487b-8c9d-0e1f2a3b4c5d'', ''2025-05-10T19:50:00.0000000'', ''ceb2b836-3b8c-4ea9-93cc-f5c442d5d966'', ''5e91fd72-53c3-43ee-3d87-08dd3498aaa5''),
    (''1f2a3b4c-5d6e-477f-8a9b-0c1d2e3f4a5b'', ''2025-06-05T18:10:00.0000000'', ''75ae0504-974c-4ff2-ab13-c30374ac8558'', ''923043b5-e9c8-4ded-9cfa-08dd3e211f93''),
    (''2a3b4c5d-6e7f-488a-9b0c-1d2e3f4a5b6c'', ''2025-08-01T22:50:00.0000000'', ''d9123022-25c0-4493-b5eb-b11cfd829554'', ''923043b5-e9c8-4ded-9cfa-08dd3e211f93''),
    (''2c3d4e5f-6a7b-498c-9d0e-1f2a3b4c5d6e'', ''2024-12-19T12:30:00.0000000'', ''45e0dcb1-62af-409a-8349-08dd4691b096'', ''39ad172a-602e-4118-29d9-08dd398180c1''),
    (''3b4c5d6e-7f8a-499b-0c1d-2e3f4a5b6c7d'', ''2025-10-25T10:30:00.0000000'', ''ceb2b836-3b8c-4ea9-93cc-f5c442d5d966'', ''923043b5-e9c8-4ded-9cfa-08dd3e211f93''),
    (''3d4e5f6a-7b8c-4a9d-0e1f-2a3b4c5d6e7f'', ''2025-03-01T15:40:00.0000000'', ''98123ca9-624e-4743-1268-08dc29a09a1f'', ''39ad172a-602e-4118-29d9-08dd398180c1''),
    (''4c5d6e7f-8a9b-4a0c-1d2e-3f4a5b6c7d8e'', ''2025-02-20T13:55:00.0000000'', ''45e0dcb1-62af-409a-8349-08dd4691b096'', ''f82bff97-d6d4-4d47-424e-08dd3e217c75''),
    (''4e5f6a7b-8c9d-4b0e-1f2a-3b4c5d6e7f8a'', ''2025-05-02T17:00:00.0000000'', ''1632e0bb-7aa4-45b3-8dc7-d48e8dc002ec'', ''39ad172a-602e-4118-29d9-08dd398180c1''),
    (''5d6e7f8a-9b0c-4b1d-2e3f-4a5b6c7d8e9f'', ''2025-04-05T16:10:00.0000000'', ''98123ca9-624e-4743-1268-08dc29a09a1f'', ''f82bff97-d6d4-4d47-424e-08dd3e217c75''),
    (''5f6a7b8c-9d0e-4c1f-2a3b-4c5d6e7f8a9b'', ''2025-07-10T08:55:00.0000000'', ''75ae0504-974c-4ff2-ab13-c30374ac8558'', ''39ad172a-602e-4118-29d9-08dd398180c1''),
    (''6a7b8c9d-0e1f-4d2a-3b4c-5d6e7f8a9b0c'', ''2025-09-03T13:25:00.0000000'', ''d9123022-25c0-4493-b5eb-b11cfd829554'', ''39ad172a-602e-4118-29d9-08dd398180c1''),
    (''6e7f8a9b-0c1d-4c2e-3f4a-5b6c7d8e9f0a'', ''2025-06-12T09:45:00.0000000'', ''1632e0bb-7aa4-45b3-8dc7-d48e8dc002ec'', ''f82bff97-d6d4-4d47-424e-08dd3e217c75''),
    (''7b8c9d0e-1f2a-4e3b-4c5d-6e7f8a9b0c1d'', ''2025-11-18T20:10:00.0000000'', ''ceb2b836-3b8c-4ea9-93cc-f5c442d5d966'', ''39ad172a-602e-4118-29d9-08dd398180c1''),
    (''7f8a9b0c-1d2e-4d3f-4a5b-6c7d8e9f0a1b'', ''2025-08-18T15:30:00.0000000'', ''75ae0504-974c-4ff2-ab13-c30374ac8558'', ''f82bff97-d6d4-4d47-424e-08dd3e217c75''),
    (''8a9b0c1d-2e3f-4e4a-5b6c-7d8e9f0a1b2c'', ''2025-10-01T21:00:00.0000000'', ''d9123022-25c0-4493-b5eb-b11cfd829554'', ''f82bff97-d6d4-4d47-424e-08dd3e217c75''),
    (''8c9d0e1f-2a3b-4f4c-5d6e-7f8a9b0c1d2e'', ''2025-01-18T11:40:00.0000000'', ''45e0dcb1-62af-409a-8349-08dd4691b096'', ''923043b5-e9c8-4ded-9cfa-08dd3e211f93''),
    (''9d0e1f2a-3b4c-455d-6e7f-8a9b0c1d2e3f'', ''2025-02-25T09:00:00.0000000'', ''98123ca9-624e-4743-1268-08dc29a09a1f'', ''923043b5-e9c8-4ded-9cfa-08dd3e211f93''),
    (''a1a1a1a1-b2b2-c3c3-d4d4-e5e5e5e5e5e5'', ''2026-01-15T17:20:00.0000000'', ''45e0dcb1-62af-409a-8349-08dd4691b096'', ''39ad172a-602e-4118-29d9-08dd398180c1''),
    (''b1a2c3d4-e5f6-4789-9a0b-c1d2e3f4a5b6'', ''2024-12-18T10:00:00.0000000'', ''45e0dcb1-62af-409a-8349-08dd4691b096'', ''5e91fd72-53c3-43ee-3d87-08dd3498aaa5''),
    (''b1b1b1b1-c2c2-d3d3-e4e4-f5f5f5f5f5f5'', ''2026-02-10T08:30:00.0000000'', ''45e0dcb1-62af-409a-8349-08dd4691b096'', ''923043b5-e9c8-4ded-9cfa-08dd3e211f93''),
    (''c1c1c1c1-d2d2-e3e3-f4f4-a5a5a5a5a5a5'', ''2026-03-01T19:40:00.0000000'', ''45e0dcb1-62af-409a-8349-08dd4691b096'', ''f82bff97-d6d4-4d47-424e-08dd3e217c75''),
    (''c7d8e9f0-1a2b-4c5d-3e4f-5a6b7c8d9e0f'', ''2025-01-10T14:30:00.0000000'', ''98123ca9-624e-4743-1268-08dc29a09a1f'', ''5e91fd72-53c3-43ee-3d87-08dd3498aaa5''),
    (''d1d1d1d1-e2e2-f3f3-a4a4-b5b5b5b5b5b5'', ''2026-01-20T14:50:00.0000000'', ''98123ca9-624e-4743-1268-08dc29a09a1f'', ''5e91fd72-53c3-43ee-3d87-08dd3498aaa5''),
    (''d3e4f5a6-b7c8-4d9e-5f0a-1b2c3d4e5f6a'', ''2025-02-01T09:15:00.0000000'', ''1632e0bb-7aa4-45b3-8dc7-d48e8dc002ec'', ''5e91fd72-53c3-43ee-3d87-08dd3498aaa5''),
    (''e1e1e1e1-f2f2-a3a3-b4b4-c5c5c5c5c5c5'', ''2026-02-15T11:00:00.0000000'', ''98123ca9-624e-4743-1268-08dc29a09a1f'', ''f82bff97-d6d4-4d47-424e-08dd3e217c75''),
    (''e9f0a1b2-c3d4-4e5f-6a7b-8c9d0e1f2a3b'', ''2025-03-15T16:45:00.0000000'', ''75ae0504-974c-4ff2-ab13-c30374ac8558'', ''5e91fd72-53c3-43ee-3d87-08dd3498aaa5''),
    (''f1f1f1f1-a2a2-b3b3-c4c4-d5d5d5d5d5d5'', ''2026-01-25T09:10:00.0000000'', ''1632e0bb-7aa4-45b3-8dc7-d48e8dc002ec'', ''39ad172a-602e-4118-29d9-08dd398180c1''),
    (''f5a6b7c8-d9e0-4f1a-7b8c-9d0e1f2a3b4c'', ''2025-04-20T11:20:00.0000000'', ''d9123022-25c0-4493-b5eb-b11cfd829554'', ''5e91fd72-53c3-43ee-3d87-08dd3498aaa5'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreationDate', N'HotelId', N'UserId') AND [object_id] = OBJECT_ID(N'[HotelVisits]'))
        SET IDENTITY_INSERT [HotelVisits] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250207113438_SeedTables'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AdultsCapacity', N'ChildrenCapacity', N'CreationDate', N'HotelId', N'ModificationDate', N'Number', N'PricePerNight', N'Type') AND [object_id] = OBJECT_ID(N'[Rooms]'))
        SET IDENTITY_INSERT [Rooms] ON;
    EXEC(N'INSERT INTO [Rooms] ([Id], [AdultsCapacity], [ChildrenCapacity], [CreationDate], [HotelId], [ModificationDate], [Number], [PricePerNight], [Type])
    VALUES (''28e17d69-9e0e-44a3-2947-08dc29a4de37'', 2, 3, ''2024-03-09T18:54:53.6850569'', ''45e0dcb1-62af-409a-8349-08dd4691b096'', ''2024-03-09T19:25:25.9245743'', 1, 50, 0),
    (''a914be7e-c545-4784-2948-08dc29a4de37'', 2, 0, ''2024-02-09T19:25:46.0762941'', ''45e0dcb1-62af-409a-8349-08dd4691b096'', ''2024-02-09T19:25:46.0762945'', 2, 100, 1),
    (''b3f07752-2d97-4b2c-2949-08dc29a4de37'', 3, 2, ''2024-02-09T19:26:03.5942376'', ''45e0dcb1-62af-409a-8349-08dd4691b096'', ''2024-02-09T19:26:03.5942379'', 3, 75, 0),
    (''c9a9f0d0-1111-4444-8888-1234567890ab'', 4, 1, ''2025-01-09T20:00:00.0000000'', ''45e0dcb1-62af-409a-8349-08dd4691b096'', ''2025-01-09T20:00:00.0000000'', 4, 200, 1),
    (''d1e2f3a4-b5c6-7890-d1e2-f3a4b5c67890'', 2, 1, ''2025-01-19T14:34:10.1870000'', ''98123ca9-624e-4743-1268-08dc29a09a1f'', ''2025-01-19T14:34:10.1870000'', 3, 180, 1),
    (''e8a1a3e6-d8da-4928-294b-08dc29a4de37'', 2, 0, ''2024-02-09T19:26:39.9723640'', ''98123ca9-624e-4743-1268-08dc29a09a1f'', ''2024-02-09T19:26:39.9723645'', 2, 100, 1),
    (''f15bd95c-8746-4236-294a-08dc29a4de37'', 3, 2, ''2024-02-09T19:26:22.3839118'', ''98123ca9-624e-4743-1268-08dc29a09a1f'', ''2024-02-09T19:26:22.3839124'', 1, 75, 0)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AdultsCapacity', N'ChildrenCapacity', N'CreationDate', N'HotelId', N'ModificationDate', N'Number', N'PricePerNight', N'Type') AND [object_id] = OBJECT_ID(N'[Rooms]'))
        SET IDENTITY_INSERT [Rooms] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250207113438_SeedTables'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CheckInDate', N'CheckOutDate', N'CreationDate', N'ModificationDate', N'Notes', N'RoomId', N'Status', N'TotalPrice', N'UserId') AND [object_id] = OBJECT_ID(N'[RoomBookings]'))
        SET IDENTITY_INSERT [RoomBookings] ON;
    EXEC(N'INSERT INTO [RoomBookings] ([Id], [CheckInDate], [CheckOutDate], [CreationDate], [ModificationDate], [Notes], [RoomId], [Status], [TotalPrice], [UserId])
    VALUES (''2b3c4d5e-f6a7-4d0a-3334-353637383940'', ''2025-08-15T14:00:00.0000000'', ''2025-08-25T11:00:00.0000000'', ''2025-07-01T13:00:00.0000000'', ''2025-07-01T13:00:00.0000000'', N''Yoink'', ''c9a9f0d0-1111-4444-8888-1234567890ab'', 0, 0.0, ''5e91fd72-53c3-43ee-3d87-08dd3498aaa5''),
    (''3c4d5e6f-a7b8-4e1b-3637-383940414243'', ''2025-09-01T14:00:00.0000000'', ''2025-09-04T11:00:00.0000000'', ''2025-08-10T21:40:00.0000000'', ''2025-08-10T21:40:00.0000000'', N''Yoink'', ''d1e2f3a4-b5c6-7890-d1e2-f3a4b5c67890'', 0, 0.0, ''39ad172a-602e-4118-29d9-08dd398180c1''),
    (''5e6f7a8b-c9d0-4d3d-4243-444546474849'', ''2025-11-05T14:00:00.0000000'', ''2025-11-07T11:00:00.0000000'', ''2025-10-15T15:50:00.0000000'', ''2025-10-15T15:50:00.0000000'', N''Yoink'', ''28e17d69-9e0e-44a3-2947-08dc29a4de37'', 0, 0.0, ''f82bff97-d6d4-4d47-424e-08dd3e217c75''),
    (''6401b407-35d4-4fe6-a1ba-ecf945440fbf'', ''2025-01-06T12:12:54.2950000'', ''2025-01-09T13:08:36.2950000'', ''2025-02-07T11:00:19.3890000'', ''2025-02-07T11:00:19.3890000'', N''Yoink'', ''c9a9f0d0-1111-4444-8888-1234567890ab'', 0, 0.0, ''5e91fd72-53c3-43ee-3d87-08dd3498aaa5''),
    (''6f7a8b9c-d1e2-4e4e-4546-474849505152'', ''2025-12-22T14:00:00.0000000'', ''2025-12-25T11:00:00.0000000'', ''2025-11-01T18:30:00.0000000'', ''2025-11-01T18:30:00.0000000'', N''Yoink'', ''a914be7e-c545-4784-2948-08dc29a4de37'', 0, 0.0, ''5e91fd72-53c3-43ee-3d87-08dd3498aaa5''),
    (''7a8b9c0d-e2f3-4f5f-4849-505152535455'', ''2026-01-10T14:00:00.0000000'', ''2026-01-12T11:00:00.0000000'', ''2025-12-10T22:00:00.0000000'', ''2025-12-10T22:00:00.0000000'', N''Yoink'', ''f15bd95c-8746-4236-294a-08dc29a4de37'', 0, 0.0, ''39ad172a-602e-4118-29d9-08dd398180c1''),
    (''8b9c0d1e-f3a4-416a-5152-535455565758'', ''2026-02-01T14:00:00.0000000'', ''2026-02-05T11:00:00.0000000'', ''2026-01-05T11:11:00.0000000'', ''2026-01-05T11:11:00.0000000'', N''Yoink'', ''e8a1a3e6-d8da-4928-294b-08dc29a4de37'', 0, 0.0, ''923043b5-e9c8-4ded-9cfa-08dd3e211f93''),
    (''97d8a55f-ac24-4e02-87f7-48b2a101c0c1'', ''2025-04-06T12:12:54.2950000'', ''2025-05-09T13:08:36.2950000'', ''2025-02-07T11:00:19.3890000'', ''2025-02-07T11:00:19.3890000'', N''Yoink'', ''d1e2f3a4-b5c6-7890-d1e2-f3a4b5c67890'', 0, 0.0, ''5e91fd72-53c3-43ee-3d87-08dd3498aaa5''),
    (''a1b2c3d4-e5f6-4789-1011-121314151617'', ''2024-12-20T14:00:00.0000000'', ''2024-12-24T11:00:00.0000000'', ''2024-11-15T09:30:00.0000000'', ''2024-11-15T09:30:00.0000000'', N''Yoink'', ''28e17d69-9e0e-44a3-2947-08dc29a4de37'', 0, 0.0, ''39ad172a-602e-4118-29d9-08dd398180c1''),
    (''b7c8d9e0-f1a2-4b5c-1314-151617181920'', ''2025-01-15T14:00:00.0000000'', ''2025-01-22T11:00:00.0000000'', ''2024-12-01T14:45:00.0000000'', ''2024-12-01T14:45:00.0000000'', N''Yoink'', ''a914be7e-c545-4784-2948-08dc29a4de37'', 0, 0.0, ''923043b5-e9c8-4ded-9cfa-08dd3e211f93''),
    (''bdb25542-aad6-41fc-9c5d-61debda238e0'', ''2025-01-06T12:12:54.2950000'', ''2025-01-10T13:08:36.2950000'', ''2025-02-07T11:00:19.3890000'', ''2025-02-07T11:00:19.3890000'', N''Yoink'', ''c9a9f0d0-1111-4444-8888-1234567890ab'', 0, 0.0, ''5e91fd72-53c3-43ee-3d87-08dd3498aaa5''),
    (''c3d4e5f6-a7b8-4d9e-1617-181920212223'', ''2025-02-28T14:00:00.0000000'', ''2025-03-02T11:00:00.0000000'', ''2025-01-10T10:10:00.0000000'', ''2025-01-10T10:10:00.0000000'', N''Yoink'', ''f15bd95c-8746-4236-294a-08dc29a4de37'', 0, 0.0, ''f82bff97-d6d4-4d47-424e-08dd3e217c75''),
    (''d5e6f7a8-b9c0-4e1f-2021-222324252627'', ''2025-04-10T14:00:00.0000000'', ''2025-04-15T11:00:00.0000000'', ''2025-03-15T16:20:00.0000000'', ''2025-03-15T16:20:00.0000000'', N''Yoink'', ''e8a1a3e6-d8da-4928-294b-08dc29a4de37'', 0, 0.0, ''5e91fd72-53c3-43ee-3d87-08dd3498aaa5''),
    (''f7422ce9-5e89-4b49-08ca-08dd47669c80'', ''2025-02-10T12:12:54.2950000'', ''2025-02-19T13:08:36.2950000'', ''2025-02-07T11:00:19.3890000'', ''2025-02-07T11:00:19.3890000'', N''Yoink'', ''d1e2f3a4-b5c6-7890-d1e2-f3a4b5c67890'', 0, 0.0, ''5e91fd72-53c3-43ee-3d87-08dd3498aaa5'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CheckInDate', N'CheckOutDate', N'CreationDate', N'ModificationDate', N'Notes', N'RoomId', N'Status', N'TotalPrice', N'UserId') AND [object_id] = OBJECT_ID(N'[RoomBookings]'))
        SET IDENTITY_INSERT [RoomBookings] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250207113438_SeedTables'
)
BEGIN
    CREATE INDEX [IX_CartItems_CartId] ON [CartItems] ([CartId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250207113438_SeedTables'
)
BEGIN
    CREATE INDEX [IX_CartItems_RoomId] ON [CartItems] ([RoomId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250207113438_SeedTables'
)
BEGIN
    CREATE INDEX [IX_CartItems_UserId] ON [CartItems] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250207113438_SeedTables'
)
BEGIN
    CREATE INDEX [IX_Carts_UserId] ON [Carts] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250207113438_SeedTables'
)
BEGIN
    CREATE INDEX [IX_Discounts_EndingDate] ON [Discounts] ([EndingDate]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250207113438_SeedTables'
)
BEGIN
    CREATE INDEX [IX_Discounts_HotelId] ON [Discounts] ([HotelId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250207113438_SeedTables'
)
BEGIN
    CREATE INDEX [IX_Discounts_StartingDate] ON [Discounts] ([StartingDate]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250207113438_SeedTables'
)
BEGIN
    CREATE INDEX [IX_HotelReviews_HotelId] ON [HotelReviews] ([HotelId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250207113438_SeedTables'
)
BEGIN
    CREATE INDEX [IX_HotelReviews_UserId] ON [HotelReviews] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250207113438_SeedTables'
)
BEGIN
    CREATE INDEX [IX_Hotels_CityId] ON [Hotels] ([CityId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250207113438_SeedTables'
)
BEGIN
    CREATE INDEX [IX_HotelVisits_CreationDate] ON [HotelVisits] ([CreationDate]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250207113438_SeedTables'
)
BEGIN
    CREATE INDEX [IX_HotelVisits_HotelId] ON [HotelVisits] ([HotelId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250207113438_SeedTables'
)
BEGIN
    CREATE INDEX [IX_HotelVisits_UserId] ON [HotelVisits] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250207113438_SeedTables'
)
BEGIN
    CREATE INDEX [IX_RoleUser_UsersId] ON [RoleUser] ([UsersId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250207113438_SeedTables'
)
BEGIN
    CREATE INDEX [IX_RoomBookings_RoomId] ON [RoomBookings] ([RoomId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250207113438_SeedTables'
)
BEGIN
    CREATE INDEX [IX_RoomBookings_UserId] ON [RoomBookings] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250207113438_SeedTables'
)
BEGIN
    CREATE INDEX [IX_Rooms_HotelId] ON [Rooms] ([HotelId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250207113438_SeedTables'
)
BEGIN
    CREATE INDEX [IX_Users_Username] ON [Users] ([Username]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250207113438_SeedTables'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250207113438_SeedTables', N'9.0.1');
END;

COMMIT;
GO


CREATE FUNCTION dbo.fn_HotelRevenue(
        @StartDate DATETIME,
        @EndDate DATETIME
    )
    RETURN TABLE AS
    RETURN
    (
        SELECT Id AS [HotelId], Name, BriefDescription, DetailedDescription, 
        StarRating, OwnerName, Geolocation, CreationDate, ModificationDate,
        CityId, City.Name AS [CityName], City.CountryName [CountryName],
        SUM(Room.PricePerNight * DATEDIFF(DAY, Booking.CheckInDate, Booking.CheckOutDate)) AS [TotalRevenue]

        FROM Hotels Hotel
        JOIN Rooms Room
            ON Hotel.Id = Room.HotelId
        JOIN Bookings Booking
            ON Room.Id = Booking.RoomId
        JOIN Cities City
            ON Hotel.CityId = City.Id
        
        WHERE Booking.CheckOutDate BETWEEN @StartDate AND @EndDate

        GROUP BY
            Hotel.Id,
            Hotel.Name -- hmmm id should be enough already.
    );
