--DROP DATABASE IF EXISTS DigitalBankDB;

CREATE DATABASE DigitalBankDB;

USE DigitalBankDB;

CREATE TABLE DigitalBk_Estado (
    IdEstado INT PRIMARY KEY IDENTITY(1,1),
    NombreEstado VARCHAR(200) NOT NULL
);

CREATE TABLE DigitalBk_Genero(
    IdGenero INT PRIMARY KEY IDENTITY(1,1),
    NombreGenero VARCHAR(20) NOT NULL,
	EstadoGenero INT NOT NULL,
	FOREIGN KEY (EstadoGenero) REFERENCES DIGITALBK_Estado(IdEstado)
);

CREATE TABLE DigitalBk_Usuario (
	IdUsuario INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    FechaNacimiento DATE NOT NULL,
	Sexo INT NOT NULL,
	EstadoUsuario INT NOT NULL,
    FOREIGN KEY (Sexo) REFERENCES DigitalBk_Genero(IdGenero),
	FOREIGN KEY (EstadoUsuario) REFERENCES DIGITALBK_Estado(IdEstado)
);

CREATE TABLE DigitalBk_Log (
    IdLog INT PRIMARY KEY IDENTITY(1,1),
    FechaLog DATETIME DEFAULT GETDATE(),
    Metodo VARCHAR(100),
    RequestMensaje NVARCHAR(MAX),
    ResponseMensaje NVARCHAR(MAX),
    IPCliente NVARCHAR(200),
    EstadoError bit,
    
);


CREATE INDEX idx_NombreEstado ON DigitalBk_Estado(NombreEstado);
CREATE INDEX idx_NombreGenero ON DigitalBk_Genero(NombreGenero);
CREATE INDEX idx_Sexo ON DigitalBk_Usuario(Nombre);
CREATE INDEX idx_EstadoUsuario ON DigitalBk_Usuario(EstadoUsuario);
CREATE INDEX IDX_FechaLog ON DigitalBk_Log (FechaLog);
CREATE INDEX IDX_Metodo ON DigitalBk_Log (Metodo);


