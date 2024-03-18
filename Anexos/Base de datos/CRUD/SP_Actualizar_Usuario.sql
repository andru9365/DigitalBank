USE [DigitalBankDB]
GO
/****** Object:  StoredProcedure [dbo].[SP_Actualizar_Usuario]    Script Date: 17/03/2024 7:18:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Andrés Felipe Muñoz>
-- Create date: <17/03/2024>
-- Description:	<Procedimiento para adicionar un actualizar usuario>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Actualizar_Usuario]
    @IdUsuario INT,
    @NuevoNombre NVARCHAR(100),
    @NuevaFechaNacimiento DATE,
    @NuevoSexo INT,
	@NuevoEstado INT
AS
BEGIN
    SET NOCOUNT ON;
	
    IF EXISTS (SELECT 1 FROM DigitalBk_Usuario WHERE IdUsuario = @IdUsuario)
    BEGIN

        UPDATE DigitalBk_Usuario
        SET Nombre = @NuevoNombre,
            FechaNacimiento = @NuevaFechaNacimiento,
            Sexo = @NuevoSexo,
			EstadoUsuario = @NuevoEstado
        WHERE IdUsuario = @IdUsuario;
    END

END