using FI.AtividadeEntrevista.CORE.Enums;
using System;

namespace FI.AtividadeEntrevista.CORE.Format
{
    public static class Formatador
    {
        public static string FormatarCampo(string valor, TipoCampo tipo)
        {
            switch (tipo)
            {
                case TipoCampo.CPF:
                    return FormatarCpf(valor);
                case TipoCampo.Data:
                    return FormatarData(valor);
                case TipoCampo.Telefone:
                    return FormatarTelefone(valor);
                default:
                    return valor;
            }
        }

        private static string FormatarCpf(string cpf)
        {
            if (cpf.Length == 11)
            {
                return Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
            }
            return cpf;
        }

        private static string FormatarData(string data)
        {
            if (DateTime.TryParse(data, out DateTime dt))
            {
                return dt.ToString("dd/MM/yyyy");
            }
            return data;
        }

        private static string FormatarTelefone(string telefone)
        {
            if (telefone.Length == 10)
            {
                return Convert.ToUInt64(telefone).ToString(@"\(00\) 0000\-0000");
            }
            if (telefone.Length == 11)
            {
                return Convert.ToUInt64(telefone).ToString(@"\(00\) 00000\-0000");
            }
            return telefone;
        }
    }

}
