IF db_id('VINICIUSMAIAITIXDB') IS NULL
	CREATE DATABASE VINICIUSMAIAITIXDB 
GO

USE VINICIUSMAIAITIXDB
GO

CREATE TABLE Paciente
(
Id INTEGER IDENTITY(1,1),
Nome VARCHAR(100) NOT NULL,
DataNascimento DATE NOT NULL,

CONSTRAINT PK_PACIENTE PRIMARY KEY(Id) 
);

CREATE TABLE Consulta
(
Id INTEGER IDENTITY(1,1),
IdPaciente INTEGER NOT NULL,
DataHoraInicio DATETIME NOT NULL,
DataHoraFim DATETIME NOT NULL,
Observacoes VARCHAR(200),

CONSTRAINT PK_CONSULTA PRIMARY KEY (Id),
CONSTRAINT FK_CONSULTA_PACIENTE FOREIGN KEY (IdPaciente) REFERENCES PACIENTE(Id) ON DELETE CASCADE
);

SET DATEFORMAT 'ymd';

INSERT INTO PACIENTE (Nome, DataNascimento) VALUES ('Vinícius da Cruz Maia', '1991-06-13');