USE [DigitalBankDB]
GO
/****** Object:  StoredProcedure [dbo].[SP_Adicionar_Usuario]    Script Date: 17/03/2024 7:18:29 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Andrés Felipe Muñoz>
-- Create date: <17/03/2024>
-- Description:	<Procedimiento para adicionar un usuario>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Adicionar_Usuario]
	@Nombre VARCHAR(100),
    @FechaNacimiento DATE,
    @Sexo INT,
    @EstadoUsuario INT
AS
BEGIN

SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM DigitalBk_Usuario WHERE Nombre = @Nombre)
    BEGIN
        
        RETURN 'El usuario ya existe';
    END

    INSERT INTO DigitalBk_Usuario (
		Nombre, 
		FechaNacimiento, 
		Sexo, 
		EstadoUsuario
	)
    VALUES (
		@Nombre,
		@FechaNacimiento,
		@Sexo,
		@EstadoUsuario
	);

    DECLARE @IDUsuario INT;
    SET @IDUsuario = SCOPE_IDENTITY();

    SELECT @IDUsuario AS IDUsuario;

END
