IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_logSales_item]') AND parent_object_id = OBJECT_ID(N'[dbo].[logSales]'))
ALTER TABLE [dbo].[logSales] DROP CONSTRAINT [FK_logSales_item]
GO

ALTER TABLE [dbo].[logSales]  WITH NOCHECK ADD  CONSTRAINT [FK_logSales_item] FOREIGN KEY([itemId])
REFERENCES [dbo].[item] ([id])
GO

ALTER TABLE [dbo].[logSales] NOCHECK CONSTRAINT [FK_logSales_item]
GO