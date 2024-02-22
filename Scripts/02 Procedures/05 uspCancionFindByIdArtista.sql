IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspCancionFindByIdArtista]') AND type in (N'P', N'PC'))
	DROP PROCEDURE dbo.uspCancionFindByIdArtista
GO

CREATE PROCEDURE uspCancionFindByIdArtista
(	
	@IdArtista BIGINT
)

AS

BEGIN
	SELECT C.IdCancion, C.IdArtista, A.Nombre AS NombreArtista, C.Nombre, C.Duracion
	FROM tblCancion C
	INNER JOIN tblArtista A ON A.IdArtista = C.IdArtista
	WHERE C.IdArtista = @IdArtista
END