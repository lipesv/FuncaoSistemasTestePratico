SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROC [dbo].[FI_SP_IncClienteV2] @NOME VARCHAR(50)
    , @SOBRENOME VARCHAR(255)
    , @CPF VARCHAR(11)
    , @EMAIL VARCHAR(2079)
    , @NACIONALIDADE VARCHAR(50)
    , @CEP VARCHAR(9)
    , @CIDADE VARCHAR(50)
    , @ESTADO VARCHAR(2)
    , @LOGRADOURO VARCHAR(500)
    , @TELRES VARCHAR(15)
    , @TELCEL VARCHAR(15)
AS
BEGIN
    INSERT INTO CLIENTES (
        NOME
        , SOBRENOME
        , CPF
        , EMAIL
        , NACIONALIDADE
        , CEP
        , CIDADE
        , ESTADO
        , LOGRADOURO
        , TELRES
        , TELCEL
        )
    VALUES (
        @NOME
        , @SOBRENOME
        , @CPF
        , @EMAIL
        , @NACIONALIDADE
        , @CEP
        , @CIDADE
        , @ESTADO
        , @LOGRADOURO
        , @TELRES
        , @TELCEL
        )

    SELECT SCOPE_IDENTITY()
END
GO


