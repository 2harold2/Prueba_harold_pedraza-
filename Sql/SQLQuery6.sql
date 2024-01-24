USE [prueba_harold_pedraza]
GO
/****** Object:  StoredProcedure [dbo].[CrearPaciente]    Script Date: 1/24/2024 11:22:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[CrearPaciente] (
	@numeroDocumento VARCHAR(15),
	@nombres VARCHAR(120),
	@apellidos VARCHAR(120),
	@email VARCHAR(150),
	@telefono VARCHAR(15),
	@fechaNacimiento DATETIME,
	@idTipoDocumento INT,
	@idTipoAfiliacion INT,
	@success BIT OUTPUT,
	@message VARCHAR(100) OUTPUT
)
AS
BEGIN

	IF NOT EXISTS (SELECT 1 FROM Paciente WHERE numeroDocumento = @numeroDocumento)
	BEGIN
		INSERT INTO Paciente(numeroDocumento, nombres, apellidos, email, telefono, fechaNacimiento, idTipoDocumento, idTipoAfiliacion)
		VALUES (@numeroDocumento, @nombres, @apellidos, @email, @telefono, @fechaNacimiento, @idTipoDocumento, @idTipoAfiliacion);

		SET @success = 1;
		SET @message = 'Se ha creado el usuario correctamente';
		SELECT SCOPE_IDENTITY() AS IdPatient;
	END
	ELSE
		BEGIN
			SET @success = 0;
			SET @message = 'Ya existe el usuario';
		END
END