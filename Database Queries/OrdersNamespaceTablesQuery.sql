USE HudaCenterDB;
GO

-- 1. Orders (Base Table)
CREATE TABLE [Orders] (
    [OrderId] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [CustomerId] UNIQUEIDENTIFIER NOT NULL,
    [EmployeeId] UNIQUEIDENTIFIER NULL,
    [DepartmentId] UNIQUEIDENTIFIER NULL,
    [Status] INT NOT NULL,
    [TotalPrice] DECIMAL(18, 2) NOT NULL,
    [PaidAmount] DECIMAL(18, 2) NOT NULL,
    [RemainingAmount] AS ([TotalPrice] - [PaidAmount]) PERSISTED,
    [OrderDate] DATETIME2 NOT NULL,
    [Notes] NVARCHAR(MAX) NULL,
    [IsActive] BIT NOT NULL DEFAULT 1
);
GO

-- 2. SellOrders
CREATE TABLE [SellOrders] (
    [OrderId] UNIQUEIDENTIFIER PRIMARY KEY,
    CONSTRAINT [FK_SellOrders_Orders] FOREIGN KEY ([OrderId]) REFERENCES [Orders]([OrderId]) ON DELETE CASCADE
);
GO

-- 3. PrintOrders
CREATE TABLE [PrintOrders] (
    [OrderId] UNIQUEIDENTIFIER PRIMARY KEY,
    [FileUrl] NVARCHAR(MAX) NOT NULL,
    [PageCount] INT NOT NULL,
    [PrintType] INT NOT NULL,
    [ColorType] INT NOT NULL,
    [PrintSize] NVARCHAR(100) NOT NULL,
    [PaperType] NVARCHAR(100) NOT NULL,
    [CopyCount] INT NOT NULL,
    CONSTRAINT [FK_PrintOrders_Orders] FOREIGN KEY ([OrderId]) REFERENCES [Orders]([OrderId]) ON DELETE CASCADE
);
GO

-- 4. OrderItems
CREATE TABLE [OrderItems] (
    [OrderItemId] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [SellOrderId] UNIQUEIDENTIFIER NOT NULL,
    [ProductId] UNIQUEIDENTIFIER NOT NULL,
    [Quantity] INT NOT NULL,
    [UnitPrice] DECIMAL(18, 2) NOT NULL,
    [TotalPrice] AS ([Quantity] * [UnitPrice]) PERSISTED,
    CONSTRAINT [FK_OrderItems_SellOrders] FOREIGN KEY ([SellOrderId]) REFERENCES [SellOrders]([OrderId]) ON DELETE CASCADE,
    CONSTRAINT [FK_OrderItems_Products] FOREIGN KEY ([ProductId]) REFERENCES [Products]([ProductId])
);
GO

-- 5. Carts
CREATE TABLE [Carts] (
    [CartId] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [CustomerId] UNIQUEIDENTIFIER NOT NULL UNIQUE,
    [CreatedDate] DATETIME2 NOT NULL DEFAULT GETDATE(),
    [IsActive] BIT NOT NULL DEFAULT 1,
    CONSTRAINT [FK_Carts_Customers] FOREIGN KEY ([CustomerId]) REFERENCES [Customers]([UserId]) ON DELETE CASCADE
);
GO

-- 6. CartItems
CREATE TABLE [CartItems] (
    [CartItemId] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [CartId] UNIQUEIDENTIFIER NOT NULL,
    [ProductId] UNIQUEIDENTIFIER NOT NULL,
    [Quantity] INT NOT NULL,
    [UnitPrice] DECIMAL(18, 2) NOT NULL,
    [TotalPrice] AS ([Quantity] * [UnitPrice]) PERSISTED,
    CONSTRAINT [FK_CartItems_Carts] FOREIGN KEY ([CartId]) REFERENCES [Carts]([CartId]) ON DELETE CASCADE,
    CONSTRAINT [FK_CartItems_Products] FOREIGN KEY ([ProductId]) REFERENCES [Products]([ProductId])
);
GO


ALTER TABLE [Orders] ADD CONSTRAINT [FK_Orders_Customers] FOREIGN KEY ([CustomerId]) REFERENCES [Customers]([UserId]);
ALTER TABLE [Orders] ADD CONSTRAINT [FK_Orders_Employees] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees]([UserId]);
ALTER TABLE [Orders] ADD CONSTRAINT [FK_Orders_Departments] FOREIGN KEY ([DepartmentId]) REFERENCES [Departments]([DepartmentId]);
GO
