SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROC [dbo].[FI_SP_IncClienteV2] @NOME VARCHAR(50)
    , @SOBRENOME VARCHAR(255)
    , @NACIONALIDADE VARCHAR(50)
    , @CPF VARCHAR(14)
    , @EMAIL VARCHAR(2079)
    , @TELEFONE VARCHAR(15)
    , @CEP VARCHAR(9)
    , @CIDADE VARCHAR(50)
    , @ESTADO VARCHAR(2)
    , @LOGRADOURO VARCHAR(500)
AS
BEGIN
    INSERT INTO CLIENTES (
        NOME
        , SOBRENOME
        , NACIONALIDADE
        , CPF
        , EMAIL
        , TELEFONE
        , CEP
        , CIDADE
        , ESTADO
        , LOGRADOURO
        )
    VALUES (
        @NOME
        , @SOBRENOME
        , @NACIONALIDADE
        , @CPF
        , @EMAIL
        , @TELEFONE
        , @CEP
        , @CIDADE
        , @ESTADO
        , @LOGRADOURO
        )

    SELECT SCOPE_IDENTITY()
END
GO


