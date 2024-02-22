IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspCancionFind]') AND type in (N'P', N'PC'))
	DROP PROCEDURE dbo.uspCancionFind
GO

CREATE PROCEDURE uspCancionFind
(	
	@Nombre VARCHAR(150) = NULL
)

AS

BEGIN
	SELECT C.IdCancion, C.IdArtista, A.Nombre AS NombreArtista, C.Nombre, C.Duracion
	FROM tblCancion C
	INNER JOIN tblArtista A ON A.IdArtista = C.IdArtista
	WHERE C.Nombre LIKE '%' + @Nombre + '%' OR ISNULL(@Nombre, '') = ''
END