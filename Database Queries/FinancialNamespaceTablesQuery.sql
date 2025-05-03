USE HudaCenterDB;
GO


CREATE TABLE [FinancialRevenues] (
    [FinancialRevenueId] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [CustomerId] UNIQUEIDENTIFIER NOT NULL,
    [EmployeeId] UNIQUEIDENTIFIER NOT NULL,
    [Amount] DECIMAL(18, 2) NOT NULL,
    [PaymentMethod] INT NOT NULL,     -- Enum: PaymentMethod
    [PaymentStatus] INT NOT NULL,     -- Enum: PaymentStatus
    [Service] INT NOT NULL,           -- Enum: Service
    [IsActive] BIT NOT NULL DEFAULT 1,
    [CreatedAt] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    [UpdatedAt] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT [FK_FinancialRevenues_Customers] FOREIGN KEY ([CustomerId]) REFERENCES [Customers]([UserId]),
    CONSTRAINT [FK_FinancialRevenues_Employees] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees]([UserId])
);
GO


CREATE TABLE [Payments] (
    [PaymentId] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [Date] DATETIME2 NOT NULL,
    [Amount] DECIMAL(18, 2) NOT NULL,
    [Reasone] NVARCHAR(MAX) NOT NULL,
    [Details] NVARCHAR(MAX) NOT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1
);
GO
