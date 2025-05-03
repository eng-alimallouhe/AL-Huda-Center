go 

use HudaCenterDB

go 

-- Roles Rable
create table [Roles](
	[RoleId] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
	[RoleType] VARCHAR(100) NOT NULL
);

go 

-- Users Table
create table [Users](
	[UserId] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
	[RoleId] UNIQUEIDENTIFIER NOT NULL,
	[FullName] VARCHAR(100) NOT NULL,
	[UserName] VARCHAR(100) NOT NULL UNIQUE,
	[Email] VARCHAR(100) NOT NULL UNIQUE,
	[Password] VARCHAR(60) NOT NULL,
	[FailedLoginAttempts] INT DEFAULT 0,
	[IsLocked] BIT DEFAULT 0,
	[IsVerified] BIT DEFAULT 1,
	[IsDeleted] BIT DEFAULT 0,
	[CreatedAt] DATETIME DEFAULT GETDATE(),
	[UpdatedAt] DATETIME DEFAULT GETDATE(),
	[LastLogin] DATETIME DEFAULT GETDATE(),
	[LockedUntil] DATETIME,
	FOREIGN KEY ([RoleId]) REFERENCES [Roles]([RoleId])
);

go 


-- Level Table
create table [Levels](
	[LevelId] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
	[LevelName] VARCHAR(100) NOT NULL UNIQUE,
	[RequiredPoints] INT NOT NULL,
	[DiscountPercentage] DECIMAL(5,3),
	[LevelDescription] VARCHAR(512),
	[PointPerDolar] DECIMAL(4,2),
	[IsActive] BIT DEFAULT 1,
	[CreatedAt] DATETIME DEFAULT GETDATE(),
	[UpdatedAt] DATETIME DEFAULT GETDATE(),
);

go 

-- Customers Table
create table [Customers](
	[UserId] UNIQUEIDENTIFIER PRIMARY KEY,
	[LevelId] UNIQUEIDENTIFIER NOT NULL,
	[Points] DECIMAL(10,3)
	FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId]),
	FOREIGN KEY ([LevelId]) REFERENCES [Levels]([LevelId])
);

go 

--Departments Table
CREATE TABLE [Departments] (
    [DepartmentId] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [DepartmentName] NVARCHAR(100) NOT NULL,
    [Description] NVARCHAR(MAX) NOT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1,
    [CreatedAt] DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    [UpdatedAt] DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);

go 

-- Employees Table
CREATE TABLE [Employees] (
    [UserId] UNIQUEIDENTIFIER PRIMARY KEY,
    [HireDate] DATETIME2 NOT NULL,
    [BaseSalary] DECIMAL(18, 2) NOT NULL,
    CONSTRAINT [FK_Employees_Users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId])
);

go 

-- EmployeesDepartments Table
CREATE TABLE [EmployeeDepartments] (
    [EmployeeDepartmentId] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [EmployeeId] UNIQUEIDENTIFIER NOT NULL,
    [DepartmentId] UNIQUEIDENTIFIER NOT NULL,
    [AppointmentDecisionUrl] NVARCHAR(MAX) NOT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1,
    [StartDate] DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    [EndDate] DATETIME2 NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT [FK_EmployeeDepartments_Employees] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees]([UserId]),
    CONSTRAINT [FK_EmployeeDepartments_Departments] FOREIGN KEY ([DepartmentId]) REFERENCES [Departments]([DepartmentId])
);

go 

--RefreshTokens Table
CREATE TABLE [RefreshTokens] (
    [RefreshTokenId] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    [Token] NVARCHAR(MAX) NOT NULL,
    [Expiration] DATETIME2 NOT NULL,
    [CreatedAt] DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    [IsRevoked] BIT NOT NULL DEFAULT 0,
    CONSTRAINT [FK_RefreshTokens_Users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId])
);

go 

-- OtpCodes Table:
CREATE TABLE [OtpCodes] (
    [OtpCodeId] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    [HashedValue] NVARCHAR(MAX) NOT NULL,
    [IsUsed] BIT NOT NULL DEFAULT 0,
    [FailedAttempts] INT NOT NULL,
    [CodeType] INT NOT NULL,
    [CreatedAt] DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    [ExpiredAt] DATETIME2 NOT NULL,
    CONSTRAINT [FK_OtpCodes_Users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId])
);

go 

-- Notifications Table
CREATE TABLE [Notifications] (
    [NotificationId] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    [Title] NVARCHAR(MAX) NOT NULL,
    [Message] NVARCHAR(MAX) NOT NULL,
    [CreatedAt] DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    [IsRead] BIT NOT NULL DEFAULT 0,
    [ReadAt] DATETIME2 NULL,
    [RedirectUrl] NVARCHAR(MAX) NOT NULL,
    CONSTRAINT [FK_Notifications_Users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId])
);


go 

-- Addresses Table
CREATE TABLE [Addresses] (
    [AddressId] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [CustomerId] UNIQUEIDENTIFIER NULL,
    [Latitude] NVARCHAR(MAX) NOT NULL,
    [Longitude] NVARCHAR(MAX) NOT NULL,
    [GZipCode] NVARCHAR(MAX) NOT NULL,
    [PhoneNumber] NVARCHAR(MAX) NOT NULL,
    CONSTRAINT [FK_Addresses_Customers] FOREIGN KEY ([CustomerId]) REFERENCES [Customers]([UserId])
);
