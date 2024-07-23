SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROC [dbo].[FI_SP_ConsCliente] @ID BIGINT = NULL
AS
BEGIN
    SELECT ID
        , NOME
        , SOBRENOME
        , NACIONALIDADE
        , CPF
        , EMAIL
        , TELEFONE
        , CEP
        , CIDADE
        , ESTADO
        , LOGRADOURO
    FROM CLIENTES C WITH (NOLOCK)
    WHERE ISNULL(@ID, 0) = 0
        OR ID = @ID

    IF EXISTS (
            SELECT 1
            FROM BENEFICIARIOS
            WHERE IDCLIENTE = @ID
            )
    BEGIN
        SELECT *
        FROM BENEFICIARIOS
        WHERE IDCLIENTE = @ID
    END
END
GO