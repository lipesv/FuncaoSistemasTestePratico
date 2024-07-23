SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROC [dbo].[FI_SP_AltBenef] @NOME VARCHAR(255)
    , @CPF VARCHAR(11)
    , @ID BIGINT
AS
BEGIN
    UPDATE BENEFICIARIOS
    SET CPF = @CPF
        , NOME = @NOME
    WHERE ID = @ID
END
GO

