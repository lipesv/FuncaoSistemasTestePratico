SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE FI_SP_VerificaBeneficiarioCliente @CPF VARCHAR(14)
    , @IDCLIENTE BIGINT
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    SELECT COUNT(*)
    FROM BENEFICIARIOS
    WHERE CPF = @CPF
        AND IDCLIENTE = @IDCLIENTE
END
GO


