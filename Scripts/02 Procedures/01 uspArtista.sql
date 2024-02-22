IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspArtista]') AND type in (N'P', N'PC'))
	DROP PROCEDURE dbo.uspArtista
GO

CREATE PROCEDURE uspArtista
(
	@IdArtista BIGINT = NULL
	, @Nombre VARCHAR(150) = NULL
	, @Pais VARCHAR(150) = NULL
	, @CasaDisquera VARCHAR(150) = NULL
	, @Delete BIT = 0
)

AS

BEGIN
	IF @Delete = 0
	BEGIN		
		IF ISNULL(@Nombre, '') = '' OR ISNULL(@Pais, '') = '' OR ISNULL(@CasaDisquera, '') = ''
		BEGIN
			SELECT 0 AS Estado, 100 AS Codigo, 'Debe enviar todos los campos del artista' AS Mensaje
			RETURN
		END

		IF ISNULL(@IdArtista, 0) = 0
		BEGIN
			INSERT tblArtista (Nombre, Pais, CasaDisquera)
			SELECT @Nombre, @Pais, @CasaDisquera

			IF @@ROWCOUNT >= 1
				SELECT 1 AS Estado, 200 AS Codigo, 'Artista creado exitosamente' AS Mensaje
			ELSE
				SELECT 0 AS Estado, 101 AS Codigo, 'No se pudo crear el artista' AS Mensaje			
		END
		ELSE
		BEGIN
			UPDATE tblArtista SET Nombre = @Nombre, Pais = @Pais, CasaDisquera = @CasaDisquera WHERE IdArtista = @IdArtista

			IF @@ROWCOUNT >= 1
				SELECT 1 AS Estado, 200 AS Codigo, 'Artista actualizado exitosamente' AS Mensaje
			ELSE
				SELECT 0 AS Estado, 102 AS Codigo, 'No se encontró artista para actualizar' AS Mensaje
		END
	END
	ELSE IF @Delete = 1
	BEGIN
		IF ISNULL(@IdArtista, 0) = 0
		BEGIN
			SELECT 0 AS Estado, 103 AS Codigo, 'Debe enviar el código del artista para eliminarlo' AS Mensaje
			RETURN
		END

		IF EXISTS(SELECT * FROM tblCancion WHERE IdArtista = @IdArtista)
		BEGIN
			SELECT 0 AS Estado, 104 AS Codigo, 'NO se puede eliminar el artista porque a está asociado a una canción' AS Mensaje
			RETURN
		END

		DELETE FROM tblArtista WHERE IdArtista = @IdArtista

		IF @@ROWCOUNT >= 1
			SELECT 1 AS Estado, 200 AS Codigo, 'Artista eliminado exitosamente' AS Mensaje
		ELSE
			SELECT 0 AS Estado, 105 AS Codigo, 'No se encontró artista para eliminar' AS Mensaje
	END
END