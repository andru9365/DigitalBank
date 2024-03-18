USE [DigitalBankDB]
GO
/****** Object:  StoredProcedure [dbo].[SP_Adicionar_Log]    Script Date: 17/03/2024 9:47:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Andrés Felipe Muñoz>
-- Create date: <17/03/2024>
-- Description:	<Procedimiento para adicionar un log>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Adicionar_Log]
	@Metodo VARCHAR(100),
    @RequestMensaje NVARCHAR(MAX),
    @ResponseMensaje NVARCHAR(MAX),
    @IPCliente NVARCHAR(200),
    @EstadoError BIT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO DigitalBk_Log(Metodo, RequestMensaje, ResponseMensaje, IPCliente, EstadoError)
    VALUES (@Metodo, @RequestMensaje, @ResponseMensaje, @IPCliente, @EstadoError);

END


