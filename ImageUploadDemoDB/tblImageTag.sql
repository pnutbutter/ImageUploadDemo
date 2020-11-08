CREATE TABLE [dbo].[tblImageTag]
(
	[ImageTagId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [ImageId] UNIQUEIDENTIFIER NOT NULL, 
    [TagId] UNIQUEIDENTIFIER NOT NULL, 
    CONSTRAINT [FK_tblImageTag_tblImage] FOREIGN KEY (ImageId) REFERENCES [tblImage]([ImageId]), 
    CONSTRAINT [FK_tblImageTag_tblTag] FOREIGN KEY (TagId) REFERENCES tblTag(TagId)
)

GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_tblImageTag_ImageId_TagId] ON [dbo].[tblImageTag] (ImageId,TagId)
