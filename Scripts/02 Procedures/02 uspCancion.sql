IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspCancion]') AND type in (N'P', N'PC'))
	DROP PROCEDURE dbo.uspCancion
GO

CREATE PROCEDURE uspCancion
(
	@IdCancion BIGINT = NULL
	, @IdArtista BIGINT = NULL
	, @Nombre VARCHAR(150) = NULL
	, @Duracion VARCHAR(20) = NULL
	, @Delete BIT = 0
)

AS

BEGIN
	IF @Delete = 0
	BEGIN		
		IF ISNULL(@IdArtista, 0) = 0 OR ISNULL(@Nombre, '') = '' OR ISNULL(@Duracion, '') = ''
		BEGIN
			SELECT 0 AS Estado, 100 AS Codigo, 'Debe enviar todos los campos de la Cancion' AS Mensaje
			RETURN
		END

		IF ISNULL(@IdCancion, 0) = 0
		BEGIN
			INSERT tblCancion (IdArtista, Nombre, Duracion)
			SELECT @IdArtista, @Nombre, @Duracion

			IF @@ROWCOUNT >= 1
				SELECT 1 AS Estado, 200 AS Codigo, 'Canción creada exitosamente' AS Mensaje
			ELSE
				SELECT 0 AS Estado, 101 AS Codigo, 'No se pudo crear la canción' AS Mensaje			
		END
		ELSE
		BEGIN
			UPDATE tblCancion SET IdArtista = @IdArtista, Nombre = @Nombre, Duracion = @Duracion WHERE IdCancion = @IdCancion

			IF @@ROWCOUNT >= 1
				SELECT 1 AS Estado, 200 AS Codigo, 'Canción actualizada exitosamente' AS Mensaje
			ELSE
				SELECT 0 AS Estado, 102 AS Codigo, 'No se encontró canción para actualizar' AS Mensaje
		END
	END
	ELSE IF @Delete = 1
	BEGIN
		IF ISNULL(@IdCancion, 0) = 0
		BEGIN
			SELECT 0 AS Estado, 103 AS Codigo, 'Debe enviar el código de la canción para eliminarlo' AS Mensaje
			RETURN
		END

		DELETE FROM tblCancion WHERE IdCancion = @IdCancion

		IF @@ROWCOUNT >= 1
			SELECT 1 AS Estado, 200 AS Codigo, 'Canción eliminada exitosamente' AS Mensaje
		ELSE
			SELECT 0 AS Estado, 105 AS Codigo, 'No se encontró canción para eliminar' AS Mensaje
	END
END