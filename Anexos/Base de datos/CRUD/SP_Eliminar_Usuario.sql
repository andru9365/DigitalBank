USE [DigitalBankDB]
GO
/****** Object:  StoredProcedure [dbo].[SP_Eliminar_Usuario]    Script Date: 17/03/2024 7:21:29 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Andrés Felipe Muñoz>
-- Create date: <17/03/2024>
-- Description:	<Procedimiento para eliminar un usuario>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Eliminar_Usuario]
    @IdUsuario INT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM DigitalBk_Usuario WHERE IdUsuario = @IdUsuario)
    BEGIN

        DELETE FROM DigitalBk_Usuario WHERE IdUsuario = @IdUsuario;

    END
    ELSE
    BEGIN
        PRINT 'No se encontró ningún usuario con el ID';
    END
END
