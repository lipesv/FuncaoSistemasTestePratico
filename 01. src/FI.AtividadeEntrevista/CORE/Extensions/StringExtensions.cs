using System;
using System.Text.RegularExpressions;

namespace FI.AtividadeEntrevista.DML.Extensions
{
    public static class StringExtensions
    {
        #region Métodos Extensivos para CPF

            public static bool EhValido(this string cpf)
            {
                if (string.IsNullOrWhiteSpace(cpf))
                    return false;

                // Verifica o formato
                if (!Regex.IsMatch(cpf, @"^\d{3}\.\d{3}\.\d{3}-\d{2}$"))
                    return false;

                // Remove caracteres de pontuação
                cpf = cpf.RemoveMascara();

                // Verifica se todos os dígitos são iguais
                if (new string(cpf[0], cpf.Length) == cpf)
                    return false;

                // Calcula o primeiro dígito verificador
                int soma = 0;
                for (int i = 0; i < 9; i++)
                    soma += (cpf[i] - '0') * (10 - i);
                int primeiroVerificador = soma % 11;
                if (primeiroVerificador < 2)
                    primeiroVerificador = 0;
                else
                    primeiroVerificador = 11 - primeiroVerificador;

                // Calcula o segundo dígito verificador
                soma = 0;
                for (int i = 0; i < 10; i++)
                    soma += (cpf[i] - '0') * (11 - i);
                int segundoVerificador = soma % 11;
                if (segundoVerificador < 2)
                    segundoVerificador = 0;
                else
                    segundoVerificador = 11 - segundoVerificador;

                // Verifica os dígitos verificadores
                return cpf.EndsWith($"{primeiroVerificador}{segundoVerificador}");
            }

            public static string RemoveMascara(this string value)
            {
                return value.Replace(".", "")
                            .Replace("-", "")
                            .Replace("(", "")
                            .Replace(")", "");
            }

            public static string AplicaMascaraCpf(this string cpf)
            {
                if (string.IsNullOrEmpty(cpf) || cpf.Length != 11)
                    return cpf;

                return Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
            }

        #endregion


    }
}
