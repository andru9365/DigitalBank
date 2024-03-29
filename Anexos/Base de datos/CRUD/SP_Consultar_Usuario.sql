USE [DigitalBankDB]
GO
/****** Object:  StoredProcedure [dbo].[SP_Consultar_Usuario]    Script Date: 17/03/2024 7:20:37 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Andrés Felipe Muñoz>
-- Create date: <17/03/2024>
-- Description:	<Procedimiento para consultar usuarios>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Consultar_Usuario]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT IdUsuario, Nombre, FechaNacimiento, Sexo, EstadoUsuario
    FROM DigitalBk_Usuario;
END