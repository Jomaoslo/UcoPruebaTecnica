IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[viewCancionxArtista]'))
	DROP VIEW [dbo].[viewCancionxArtista]
GO


CREATE VIEW [dbo].[viewCancionxArtista]

AS

SELECT C.IdCancion, C.IdArtista, A.Nombre AS NombreArtista, C.Nombre, C.Duracion, A.Pais, A.CasaDisquera
FROM dbo.tblCancion AS C 
INNER JOIN dbo.tblArtista AS A ON A.IdArtista = C.IdArtista