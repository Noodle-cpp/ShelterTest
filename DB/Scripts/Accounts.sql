IF OBJECT_ID('dbo.Accounts') IS NOT NULL
BEGIN
  PRINT 'Object Accounts already exists.';
END
ELSE
BEGIN
  CREATE TABLE Accounts (
    Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    CompanyId UNIQUEIDENTIFIER NOT NULL,
    Type INT NOT NULL,
    Name VARCHAR(255) NOT NULL,
    Email VARCHAR(255) NOT NULL,
    Password VARCHAR(255) NOT NULL,
    ApiKey VARCHAR(255) NULL,
    Attribute BIT NOT NULL DEFAULT 0,
    CONSTRAINT FK_Companies_CompanyId FOREIGN KEY (CompanyId) REFERENCES Companies(Id)
  );
  PRINT 'Object Accounts created.';
END
