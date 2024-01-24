CREATE DATABASE prueba_harold_pedraza;

USE prueba_harold_pedraza;

-- Creacion de tablas necesarias

CREATE TABLE TipoDocumento (
	idTipoDocumento INT PRIMARY KEY IDENTITY(1, 1),
	sigla VARCHAR(10),
	nombre VARCHAR(50)
);

CREATE TABLE Afiliacion (
	idTipoAfiliacion INT PRIMARY KEY IDENTITY(1, 1),
	descripcion VARCHAR(100)
);

CREATE TABLE Paciente (
	idPaciente INT PRIMARY KEY IDENTITY(1, 1),
	numeroDocumento VARCHAR(15) NOT NULL,
	nombres VARCHAR(120) NOT NULL,
	apellidos VARCHAR(120),
	email VARCHAR(150) NOT NULL,
	telefono VARCHAR(15),
	fechaNacimiento DATETIME,
	idTipoDocumento INT FOREIGN KEY REFERENCES TipoDocumento(idTipoDocumento),
	idTipoAfiliacion INT FOREIGN KEY REFERENCES Afiliacion(idTipoAfiliacion)
);

-- Creacion de Procedimientos Tipo de documento
CREATE PROCEDURE CrearTipoDocumento (
	@sigla VARCHAR(10),
	@nombre VARCHAR(50)
)
AS
BEGIN
	INSERT INTO TipoDocumento(sigla,nombre) VALUES (@sigla, @nombre);
END

EXEC CrearTipoDocumento 'CC', 'Cedula de ciudadanía'
EXEC CrearTipoDocumento 'TI', 'Tarjeta de identidad'
EXEC CrearTipoDocumento 'PA', 'Pasaporte'


CREATE PROCEDURE ObtenerTodosLosDocumentos
AS
BEGIN
	SELECT idTipoDocumento, CONCAT_WS(' ', sigla, nombre) AS Document FROM TipoDocumento;
END

EXEC ObtenerTodosLosDocumentos


CREATE PROCEDURE ObtenerDocumento(
	@idTipoDocumento INT
)
AS
BEGIN
	SELECT idTipoDocumento, sigla, nombre FROM TipoDocumento
	WHERE idTipoDocumento = @idTipoDocumento
END

EXEC ObtenerDocumento 1


-- Creacion de Procedimientos para afiliacion
CREATE PROCEDURE CrearAfiliacion (
	@descripcion VARCHAR(100)
)
AS
BEGIN
	INSERT INTO Afiliacion(descripcion) VALUES (@descripcion);
END

EXEC CrearAfiliacion 'Activo'
EXEC CrearAfiliacion 'Inactivo'


CREATE PROCEDURE ObtenerTodasLasAfiliaciones
AS
BEGIN
	SELECT idTipoAfiliacion, descripcion FROM Afiliacion;
END

EXEC ObtenerTodasLasAfiliaciones


CREATE PROCEDURE ObtenerAfiliacion(
	@idTipoAfiliacion INT
)
AS
BEGIN
	SELECT idTipoAfiliacion, descripcion FROM Afiliacion
	WHERE idTipoAfiliacion = @idTipoAfiliacion
END

EXEC ObtenerAfiliacion 1

-- Creacion de Procedimientos para Pacientes

CREATE PROCEDURE CrearPaciente (
	@numeroDocumento VARCHAR(15),
	@nombres VARCHAR(120),
	@apellidos VARCHAR(120),
	@email VARCHAR(150),
	@telefono VARCHAR(15),
	@fechaNacimiento DATETIME,
	@idTipoDocumento INT,
	@idTipoAfiliacion INT
)
AS
BEGIN
	INSERT INTO Paciente(numeroDocumento, nombres, apellidos, email, telefono, fechaNacimiento, idTipoDocumento, idTipoAfiliacion)
	VALUES (@numeroDocumento, @nombres, @apellidos, @email, @telefono, @fechaNacimiento, @idTipoDocumento, @idTipoAfiliacion);

	SELECT SCOPE_IDENTITY() AS IdPatient;
END

DECLARE @realizado BIT;
DECLARE @mensaje VARCHAR(100);

--EXEC CrearPaciente '123456', 'Harold', 'Pedraza', 'test@test.co', '98765432', '01-21-1999', 1, 1, @realizado OUTPUT, @mensaje OUTPUT;
EXEC CrearPaciente '313131', 'eqe', 'eq', 'a@dsa.co', '132131', '2024-01-17', 1, 1, @realizado OUTPUT, @mensaje OUTPUT;
PRINT @mensaje;


CREATE PROCEDURE ActualizarPaciente (
	@idPaciente INT,
	@numeroDocumento VARCHAR(15),
	@nombres VARCHAR(120),
	@apellidos VARCHAR(120),
	@email VARCHAR(150),
	@telefono VARCHAR(15),
	@fechaNacimiento DATETIME,
	@idTipoDocumento INT,
	@idTipoAfiliacion INT
)
AS
BEGIN 
	UPDATE Paciente
	SET 
		numeroDocumento = @numeroDocumento,
		nombres = @nombres,
		apellidos = @apellidos,
		email = @email,
		telefono = @telefono,
		fechaNacimiento = @fechaNacimiento,
		idTipoDocumento = @idTipoDocumento,
		idTipoAfiliacion = @idTipoAfiliacion
	WHERE
		idPaciente = @idPaciente;
END

DECLARE @realizadoActualizacion BIT;
DECLARE @mensajeActualizacion VARCHAR(100);

EXEC ActualizarPaciente 1, '123456', 'Harolds', 'Pedrazas', 'test@test.com', '98765432', '01-21-1999', 1, 1, @realizadoActualizacion OUTPUT, @mensajeActualizacion OUTPUT;
PRINT @mensajeActualizacion;


CREATE PROCEDURE EliminarPaciente (
	@idPaciente INT
)
AS
BEGIN
	DELETE FROM Paciente WHERE idPaciente = @idPaciente;
END

EXEC EliminarPaciente 4


CREATE PROCEDURE ObtenerTodosLosPacientes
AS
BEGIN
	SELECT
		idPaciente,
		CONCAT_WS(' ', sigla, nombre) AS tipoDocumento,
		numeroDocumento,
		nombres,
		apellidos,
		email,
		telefono,
		fechaNacimiento,
		descripcion AS Afiliacion
	FROM Paciente A
		INNER JOIN TipoDocumento B
			ON A.idTipoDocumento = B.idTipoDocumento
		INNER JOIN Afiliacion C
			ON A.idTipoAfiliacion = C.idTipoAfiliacion
END

EXEC ObtenerTodosLosPacientes



CREATE PROCEDURE ObtenerUnPaciente (
	@idPaciente INT
)
AS
BEGIN
	SELECT
		idPaciente,
		A.idTipoDocumento,
		numeroDocumento,
		nombres,
		apellidos,
		email,
		telefono,
		fechaNacimiento,
		C.idTipoAfiliacion
	FROM Paciente A
	INNER JOIN TipoDocumento B
		ON A.idTipoDocumento = B.idTipoDocumento
	INNER JOIN Afiliacion C
		ON A.idTipoAfiliacion = C.idTipoAfiliacion
	WHERE A.idPaciente = @idPaciente
END

EXEC ObtenerUnPaciente 1