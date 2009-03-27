alter table logActivity add [sellerId] [int]  
Go
update logActivity set [sellerId]=[buyerId]
Go
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_logActivity_buyer]') AND parent_object_id = OBJECT_ID(N'[dbo].[logActivity]'))
ALTER TABLE [dbo].[logActivity] DROP CONSTRAINT [FK_logActivity_buyer]
Go
ALTER TABLE logActivity drop column [buyerId]
Go
