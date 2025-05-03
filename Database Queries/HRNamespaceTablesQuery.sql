use HudaCenterDB

go 


CREATE TABLE [Attendances] (
    [AttendanceId] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [EmployeeId] UNIQUEIDENTIFIER NOT NULL,
    [Date] DATETIME2 NOT NULL,
    [TimeIn] DATETIME2 NOT NULL,
    [TimeOut] DATETIME2 NOT NULL,
    [IsPresent] BIT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1,
    CONSTRAINT [FK_Attendances_Employees] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees]([UserId])
);

go 

CREATE TABLE [Incentives] (
    [IncentiveId] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [EmployeeId] UNIQUEIDENTIFIER NOT NULL,
    [Amount] DECIMAL(18, 2) NOT NULL,
    [Reason] NVARCHAR(MAX) NOT NULL,
    [DecisionFileUrl] NVARCHAR(MAX) NOT NULL,
    [Date] DATETIME2 NOT NULL,
    [IsDisbursed] BIT NOT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1,
    CONSTRAINT [FK_Incentives_Employees] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees]([UserId])
);

go 

CREATE TABLE [Leaves] (
    [LeaveId] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [EmployeeId] UNIQUEIDENTIFIER NOT NULL,
    [StartDate] DATETIME2 NOT NULL,
    [EndDate] DATETIME2 NOT NULL,
    [LeaveType] INT NOT NULL,
    [Reason] NVARCHAR(MAX) NOT NULL,
    [IsApproved] BIT NOT NULL,
    [IsPaid] BIT NOT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1,
    CONSTRAINT [FK_Leaves_Employees] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees]([UserId])
);

go 


CREATE TABLE [LeaveBalances] (
    [LeaveBalanceId] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [EmployeeId] UNIQUEIDENTIFIER NOT NULL,
    [RemainBalance] INT NOT NULL,
    [BaseBalance] INT NOT NULL,
    [TotalBalance] INT NOT NULL,
    [RoundedBalance] INT NOT NULL,
    [Year] INT NOT NULL,
    CONSTRAINT [FK_LeaveBalances_Employees] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees]([UserId])
);

go 


CREATE TABLE [Penalties] (
    [PenaltyId] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [EmployeeId] UNIQUEIDENTIFIER NOT NULL,
    [Amount] DECIMAL(18, 2) NOT NULL,
    [DecisionFileUrl] NVARCHAR(MAX) NOT NULL,
    [Reason] NVARCHAR(MAX) NOT NULL,
    [Date] DATETIME2 NOT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1,
    CONSTRAINT [FK_Penalties_Employees] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees]([UserId])
);

go 


CREATE TABLE [Salaries] (
    [SalaryId] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [EmployeeId] UNIQUEIDENTIFIER NOT NULL,
    [Month] INT NOT NULL,
    [Value] DECIMAL(18, 2) NOT NULL,
    [Year] INT NOT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1,
    CONSTRAINT [FK_Salaries_Employees] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees]([UserId])
);


go 

