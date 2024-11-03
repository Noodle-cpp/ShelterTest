IF OBJECT_ID('dbo.Companies') IS NOT NULL
BEGIN
  PRINT 'Object Companies already exists.';
END
ELSE
BEGIN
  CREATE TABLE Companies (
    Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    ParentCompanyId UNIQUEIDENTIFIER NULL,
    Name VARCHAR(255) NOT NULL,
    Inn VARCHAR(10) NOT NULL,
    Phone VARCHAR(30) NULL,
    CONSTRAINT FK_Companies_ParentCompany FOREIGN KEY (ParentCompanyId) REFERENCES Companies(Id)
  );
  PRINT 'Object Companies created.';
END