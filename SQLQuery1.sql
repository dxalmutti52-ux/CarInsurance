CREATE TABLE [dbo].[Insurees] (
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [FirstName] NVARCHAR(50) NOT NULL,
    [LastName] NVARCHAR(50) NOT NULL,
    [EmailAddress] NVARCHAR(100) NOT NULL,
    [DateOfBirth] DATETIME NOT NULL,
    [CarYear] INT NOT NULL,
    [CarMake] NVARCHAR(50) NOT NULL,
    [CarModel] NVARCHAR(50) NOT NULL,
    [DUI] BIT NOT NULL,
    [SpeedingTickets] INT NOT NULL,
    [CoverageType] BIT NOT NULL,
    [Quote] DECIMAL(18,2) NOT NULL
);
