IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspArtistaFind]') AND type in (N'P', N'PC'))
	DROP PROCEDURE dbo.uspArtistaFind
GO

CREATE PROCEDURE uspArtistaFind
(	
	@Nombre VARCHAR(150) = NULL
)

AS

BEGIN
	SELECT * FROM tblArtista
	WHERE Nombre LIKE '%' + @Nombre + '%' OR ISNULL(@Nombre, '') = ''
END