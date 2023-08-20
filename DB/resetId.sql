USE [dedb]
GO

DBCC CHECKIDENT ('customersLogs', RESEED, 0);

DBCC CHECKIDENT ('customers', RESEED, 0);

DBCC CHECKIDENT ('nationalities', RESEED, 0);

DBCC CHECKIDENT ('departments', RESEED, 0);

