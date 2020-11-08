CREATE TABLE [dbo].[tblImage]
(
	[ImageId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [AlbumImage] IMAGE NOT NULL, 
    [ImageName] NVARCHAR(50) NOT NULL, 
    [ImageURL] NVARCHAR(50) NOT NULL, 
    [ImageAlt] NCHAR(500) NOT NULL
)

GO

CREATE UNIQUE INDEX [IX_tblImage_ImageURL] ON [dbo].[tblImage] (ImageURL)

GO

CREATE UNIQUE INDEX [IX_tblImage_ImageName] ON [dbo].[tblImage] (ImageName)