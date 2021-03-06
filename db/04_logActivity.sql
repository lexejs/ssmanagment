/*
   Saturday, March 14, 20091:23:09 PM
   User: 
   Server: SUZEV-A\SQLEXPRESS
   Database: E:\LOCAL\FREE\SSM\INTRUNK\SSMANAGMENT\APP_DATA\SM.MDF
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
ALTER TABLE dbo.logActivity
	DROP CONSTRAINT FK_logActivity_buyer
GO
COMMIT
select Has_Perms_By_Name(N'dbo.buyer', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.buyer', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.buyer', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.logActivity
	DROP CONSTRAINT DF_logActivity_date
GO
CREATE TABLE dbo.Tmp_logActivity
	(
	id int NOT NULL IDENTITY (1, 1),
	action varchar(150) NULL,
	date datetime NULL,
	buyerId int NULL,
	informAdmin bit NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_logActivity ADD CONSTRAINT
	DF_logActivity_date DEFAULT (getdate()) FOR date
GO
SET IDENTITY_INSERT dbo.Tmp_logActivity ON
GO
IF EXISTS(SELECT * FROM dbo.logActivity)
	 EXEC('INSERT INTO dbo.Tmp_logActivity (id, action, date, buyerId, informAdmin)
		SELECT id, action, date, buyerId, informAdmin FROM dbo.logActivity WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_logActivity OFF
GO
DROP TABLE dbo.logActivity
GO
EXECUTE sp_rename N'dbo.Tmp_logActivity', N'logActivity', 'OBJECT' 
GO
ALTER TABLE dbo.logActivity ADD CONSTRAINT
	PK_logActivity PRIMARY KEY CLUSTERED 
	(
	id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.logActivity ADD CONSTRAINT
	FK_logActivity_buyer FOREIGN KEY
	(
	buyerId
	) REFERENCES dbo.buyer
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.logActivity', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.logActivity', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.logActivity', 'Object', 'CONTROL') as Contr_Per 