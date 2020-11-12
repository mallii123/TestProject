USE [master] 
GO 
CREATE DATABASE [CompanyDataDB] 
GO 
USE [CompanyDataDB] 
GO 
--DROP TABLE [dbo].[Employee]
--DROP TABLE [dbo].[Company]

CREATE TABLE [dbo].[Company](
   [ID] BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),    
   [Name] NVARCHAR(100) NOT NULL, 
   [EstablishmentYear] INT NOT NULL,  
) 

GO
CREATE TABLE [dbo].[Employee]( 
   [ID] BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1), 
   [CompanyID] BIGINT NOT NULL, 
   [FirstName] NVARCHAR(100) NOT NULL, 
   [LastName] NVARCHAR(100) NOT NULL, 
   [DateOfBirth] DATE NOT NULL, 
   [JobTitle] NVARCHAR(20) NOT NULL,
   FOREIGN KEY (CompanyID) REFERENCES Company([ID]),   
) 
GO




