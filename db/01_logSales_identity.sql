/*
   Monday, March 02, 20093:11:58 PM
   User: 
   Server: SUZEV-A\SQLEXPRESS
   Database: SM
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.logSales
	DROP CONSTRAINT FK_logSales_buyer
GO
COMMIT
select Has_Perms_By_Name(N'dbo.buyer', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.buyer', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.buyer', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.logSales
	DROP CONSTRAINT FK_logSales_seller
GO
COMMIT
select Has_Perms_By_Name(N'dbo.seller', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.seller', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.seller', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.logSales
	DROP CONSTRAINT FK_logSales_item
GO
COMMIT
select Has_Perms_By_Name(N'dbo.item', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.item', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.item', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.logSales
	DROP CONSTRAINT DF_logSales_date
GO
CREATE TABLE dbo.Tmp_logSales
	(
	id int NOT NULL IDENTITY (1, 1),
	itemId int NULL,
	itemsCount int NULL,
	buyerId int NULL,
	sellerId int NULL,
	date datetime NULL,
	isGiveBack bit NULL,
	cash float(53) NULL,
	sid int NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_logSales ADD CONSTRAINT
	DF_logSales_date DEFAULT (getdate()) FOR date
GO
SET IDENTITY_INSERT dbo.Tmp_logSales ON
GO
IF EXISTS(SELECT * FROM dbo.logSales)
	 EXEC('INSERT INTO dbo.Tmp_logSales (id, itemId, itemsCount, buyerId, sellerId, date, isGiveBack, cash, sid)
		SELECT id, itemId, itemsCount, buyerId, sellerId, date, isGiveBack, cash, sid FROM dbo.logSales WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_logSales OFF
GO
DROP TABLE dbo.logSales
GO
EXECUTE sp_rename N'dbo.Tmp_logSales', N'logSales', 'OBJECT' 
GO
ALTER TABLE dbo.logSales ADD CONSTRAINT
	PK_logSales PRIMARY KEY CLUSTERED 
	(
	id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.logSales ADD CONSTRAINT
	FK_logSales_item FOREIGN KEY
	(
	itemId
	) REFERENCES dbo.item
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.logSales ADD CONSTRAINT
	FK_logSales_seller FOREIGN KEY
	(
	sellerId
	) REFERENCES dbo.seller
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.logSales ADD CONSTRAINT
	FK_logSales_buyer FOREIGN KEY
	(
	buyerId
	) REFERENCES dbo.buyer
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.logSales', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.logSales', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.logSales', 'Object', 'CONTROL') as Contr_Per 