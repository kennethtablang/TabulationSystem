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
CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(128) NOT NULL,
    [ProviderKey] nvarchar(128) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(128) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250206125523_InitialCreate', N'9.0.0');

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUserTokens]') AND [c].[name] = N'Name');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUserTokens] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [AspNetUserTokens] ALTER COLUMN [Name] nvarchar(450) NOT NULL;

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUserTokens]') AND [c].[name] = N'LoginProvider');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUserTokens] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [AspNetUserTokens] ALTER COLUMN [LoginProvider] nvarchar(450) NOT NULL;

ALTER TABLE [AspNetUsers] ADD [DateTimeCreated] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

ALTER TABLE [AspNetUsers] ADD [DateTimeUpdated] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

ALTER TABLE [AspNetUsers] ADD [FirstName] nvarchar(50) NOT NULL DEFAULT N'';

ALTER TABLE [AspNetUsers] ADD [LastName] nvarchar(50) NOT NULL DEFAULT N'';

ALTER TABLE [AspNetUsers] ADD [MiddleName] nvarchar(50) NOT NULL DEFAULT N'';

ALTER TABLE [AspNetUsers] ADD [ProfilePicture] varbinary(max) NOT NULL DEFAULT 0x;

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUserLogins]') AND [c].[name] = N'ProviderKey');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUserLogins] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [AspNetUserLogins] ALTER COLUMN [ProviderKey] nvarchar(450) NOT NULL;

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUserLogins]') AND [c].[name] = N'LoginProvider');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUserLogins] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [AspNetUserLogins] ALTER COLUMN [LoginProvider] nvarchar(450) NOT NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250209031303_UpdateApplicationUser', N'9.0.0');

ALTER TABLE [AspNetUserRoles] ADD [ApplicationUserId] nvarchar(450) NULL;

CREATE INDEX [IX_AspNetUserRoles_ApplicationUserId] ON [AspNetUserRoles] ([ApplicationUserId]);

ALTER TABLE [AspNetUserRoles] ADD CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [AspNetUsers] ([Id]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250306014147_UpdateApplicationUserWithRoles', N'9.0.0');

ALTER TABLE [AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_ApplicationUserId];

DROP INDEX [IX_AspNetUserRoles_ApplicationUserId] ON [AspNetUserRoles];

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUserRoles]') AND [c].[name] = N'ApplicationUserId');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUserRoles] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [AspNetUserRoles] DROP COLUMN [ApplicationUserId];

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250306044437_RemoveUserRole', N'9.0.0');

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'ProfilePicture');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [AspNetUsers] ALTER COLUMN [ProfilePicture] varbinary(max) NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250306103519_UpdateProfilePictureToNull', N'9.0.0');

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'MiddleName');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [AspNetUsers] ALTER COLUMN [MiddleName] nvarchar(50) NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250306112900_UpdateNullableMiddleName', N'9.0.0');

CREATE TABLE [AuditLogs] (
    [AuditLogId] int NOT NULL IDENTITY,
    [ApplicationUserId] nvarchar(450) NOT NULL,
    [Action] nvarchar(500) NOT NULL,
    [Timestamp] datetime2 NOT NULL,
    [Details] nvarchar(1000) NOT NULL,
    [Severity] int NOT NULL,
    [DateCreated] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
    CONSTRAINT [PK_AuditLogs] PRIMARY KEY ([AuditLogId]),
    CONSTRAINT [FK_AuditLogs_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Events] (
    [EventId] int NOT NULL IDENTITY,
    [EventName] nvarchar(100) NOT NULL,
    [Description] nvarchar(500) NULL,
    [Status] int NOT NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateUpdated] datetime2 NOT NULL,
    [ApplicationUserId] nvarchar(450) NULL,
    [Banner] nvarchar(500) NULL,
    CONSTRAINT [PK_Events] PRIMARY KEY ([EventId]),
    CONSTRAINT [FK_Events_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [AspNetUsers] ([Id])
);

CREATE TABLE [Notifications] (
    [NotificationId] int NOT NULL IDENTITY,
    [Title] nvarchar(100) NOT NULL,
    [ApplicationUserId] nvarchar(450) NOT NULL,
    [Message] nvarchar(500) NOT NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateUpdated] datetime2 NOT NULL,
    [IsRead] bit NOT NULL,
    [NotificationType] int NOT NULL,
    CONSTRAINT [PK_Notifications] PRIMARY KEY ([NotificationId]),
    CONSTRAINT [FK_Notifications_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);

CREATE TABLE [Years] (
    [YearId] int NOT NULL IDENTITY,
    [YearNumber] int NOT NULL,
    [Status] bit NOT NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateUpdated] datetime2 NOT NULL,
    CONSTRAINT [PK_Years] PRIMARY KEY ([YearId])
);

CREATE TABLE [EventCategories] (
    [EventCategoryId] int NOT NULL IDENTITY,
    [EventId] int NOT NULL,
    [CategoryName] nvarchar(100) NOT NULL,
    [Description] nvarchar(500) NULL,
    [ScoreType] int NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateUpdated] datetime2 NOT NULL,
    CONSTRAINT [PK_EventCategories] PRIMARY KEY ([EventCategoryId]),
    CONSTRAINT [FK_EventCategories_Events_EventId] FOREIGN KEY ([EventId]) REFERENCES [Events] ([EventId]) ON DELETE NO ACTION
);

CREATE TABLE [Candidates] (
    [CandidateId] int NOT NULL IDENTITY,
    [FirstName] nvarchar(50) NOT NULL,
    [MiddleName] nvarchar(50) NULL,
    [LastName] nvarchar(50) NOT NULL,
    [Gender] int NOT NULL,
    [Status] bit NOT NULL,
    [Picture] nvarchar(500) NULL,
    [Description] nvarchar(1000) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateUpdated] datetime2 NOT NULL,
    [YearId] int NOT NULL,
    [EventId] int NOT NULL,
    [ApplicationUserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_Candidates] PRIMARY KEY ([CandidateId]),
    CONSTRAINT [FK_Candidates_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Candidates_Events_EventId] FOREIGN KEY ([EventId]) REFERENCES [Events] ([EventId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Candidates_Years_YearId] FOREIGN KEY ([YearId]) REFERENCES [Years] ([YearId]) ON DELETE CASCADE
);

CREATE TABLE [Criteria] (
    [CriteriaId] int NOT NULL IDENTITY,
    [CriteriaName] nvarchar(200) NOT NULL,
    [Percentage] decimal(5,2) NOT NULL,
    [Description] nvarchar(500) NULL,
    [DateCreated] datetime2 NOT NULL,
    [DateUpdated] datetime2 NOT NULL,
    [ApplicationUserId] nvarchar(450) NULL,
    [EventCategoryId] int NOT NULL,
    [CategoryId] int NOT NULL,
    CONSTRAINT [PK_Criteria] PRIMARY KEY ([CriteriaId]),
    CONSTRAINT [FK_Criteria_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [AspNetUsers] ([Id]),
    CONSTRAINT [FK_Criteria_EventCategories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [EventCategories] ([EventCategoryId]) ON DELETE NO ACTION
);

CREATE TABLE [Scores] (
    [ScoreId] int NOT NULL IDENTITY,
    [EventId] int NOT NULL,
    [CriteriaId] int NOT NULL,
    [JudgeId] nvarchar(450) NOT NULL,
    [CandidateId] int NOT NULL,
    [CategoryId] int NOT NULL,
    [ScoreValue] decimal(5,2) NOT NULL,
    [IsFinalized] bit NOT NULL,
    [DateCreated] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
    [DateUpdated] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
    [ApplicationUserId] nvarchar(450) NULL,
    CONSTRAINT [PK_Scores] PRIMARY KEY ([ScoreId]),
    CONSTRAINT [FK_Scores_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [AspNetUsers] ([Id]),
    CONSTRAINT [FK_Scores_AspNetUsers_JudgeId] FOREIGN KEY ([JudgeId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Scores_Candidates_CandidateId] FOREIGN KEY ([CandidateId]) REFERENCES [Candidates] ([CandidateId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Scores_Criteria_CriteriaId] FOREIGN KEY ([CriteriaId]) REFERENCES [Criteria] ([CriteriaId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Scores_EventCategories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [EventCategories] ([EventCategoryId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Scores_Events_EventId] FOREIGN KEY ([EventId]) REFERENCES [Events] ([EventId]) ON DELETE NO ACTION
);

CREATE INDEX [IX_AuditLogs_ApplicationUserId] ON [AuditLogs] ([ApplicationUserId]);

CREATE INDEX [IX_Candidates_ApplicationUserId] ON [Candidates] ([ApplicationUserId]);

CREATE INDEX [IX_Candidates_EventId] ON [Candidates] ([EventId]);

CREATE INDEX [IX_Candidates_YearId] ON [Candidates] ([YearId]);

CREATE INDEX [IX_Criteria_ApplicationUserId] ON [Criteria] ([ApplicationUserId]);

CREATE INDEX [IX_Criteria_CategoryId] ON [Criteria] ([CategoryId]);

CREATE INDEX [IX_EventCategories_EventId] ON [EventCategories] ([EventId]);

CREATE INDEX [IX_Events_ApplicationUserId] ON [Events] ([ApplicationUserId]);

CREATE INDEX [IX_Notifications_ApplicationUserId] ON [Notifications] ([ApplicationUserId]);

CREATE INDEX [IX_Scores_ApplicationUserId] ON [Scores] ([ApplicationUserId]);

CREATE UNIQUE INDEX [IX_Scores_CandidateId_JudgeId_CriteriaId] ON [Scores] ([CandidateId], [JudgeId], [CriteriaId]);

CREATE INDEX [IX_Scores_CategoryId] ON [Scores] ([CategoryId]);

CREATE INDEX [IX_Scores_CriteriaId] ON [Scores] ([CriteriaId]);

CREATE INDEX [IX_Scores_EventId] ON [Scores] ([EventId]);

CREATE INDEX [IX_Scores_JudgeId] ON [Scores] ([JudgeId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250307133734_UpdateModel', N'9.0.0');

EXEC sp_rename N'[Events].[Status]', N'EventStatus', 'COLUMN';

ALTER TABLE [Events] ADD [EventDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

ALTER TABLE [Events] ADD [EventLocation] nvarchar(255) NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250308152307_UpdateEventModel', N'9.0.0');

CREATE TABLE [EventJudges] (
    [EventId] int NOT NULL,
    [ApplicationUserId] nvarchar(450) NOT NULL,
    [EventJudgeId] int NOT NULL,
    [ApplicationUserId1] nvarchar(450) NULL,
    CONSTRAINT [PK_EventJudges] PRIMARY KEY ([EventId], [ApplicationUserId]),
    CONSTRAINT [FK_EventJudges_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_EventJudges_AspNetUsers_ApplicationUserId1] FOREIGN KEY ([ApplicationUserId1]) REFERENCES [AspNetUsers] ([Id]),
    CONSTRAINT [FK_EventJudges_Events_EventId] FOREIGN KEY ([EventId]) REFERENCES [Events] ([EventId]) ON DELETE NO ACTION
);

CREATE TABLE [EventManager] (
    [EventId] int NOT NULL,
    [ApplicationUserId] nvarchar(450) NOT NULL,
    [EventManagerId] int NOT NULL,
    [ApplicationUserId1] nvarchar(450) NULL,
    CONSTRAINT [PK_EventManager] PRIMARY KEY ([EventId], [ApplicationUserId]),
    CONSTRAINT [FK_EventManager_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_EventManager_AspNetUsers_ApplicationUserId1] FOREIGN KEY ([ApplicationUserId1]) REFERENCES [AspNetUsers] ([Id]),
    CONSTRAINT [FK_EventManager_Events_EventId] FOREIGN KEY ([EventId]) REFERENCES [Events] ([EventId]) ON DELETE NO ACTION
);

CREATE UNIQUE INDEX [IX_EventJudge_UniqueAssignment] ON [EventJudges] ([EventId], [ApplicationUserId]);

CREATE INDEX [IX_EventJudges_ApplicationUserId] ON [EventJudges] ([ApplicationUserId]);

CREATE INDEX [IX_EventJudges_ApplicationUserId1] ON [EventJudges] ([ApplicationUserId1]);

CREATE INDEX [IX_EventManager_ApplicationUserId] ON [EventManager] ([ApplicationUserId]);

CREATE INDEX [IX_EventManager_ApplicationUserId1] ON [EventManager] ([ApplicationUserId1]);

CREATE UNIQUE INDEX [IX_EventManager_UniqueAssignment] ON [EventManager] ([EventId], [ApplicationUserId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250309125048_FixApplicationUserIdMapping', N'9.0.0');

COMMIT;
GO

