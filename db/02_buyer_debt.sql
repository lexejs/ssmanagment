/*
   Tuesday, March 03, 20095:17:11 PM
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
ALTER TABLE dbo.buyer ADD
	debt float(53) NULL
GO
COMMIT
select Has_Perms_By_Name(N'dbo.buyer', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.buyer', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.buyer', 'Object', 'CONTROL') as Contr_Per 