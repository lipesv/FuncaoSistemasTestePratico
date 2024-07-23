using FI.AtividadeEntrevista.CORE.Enums;
using FI.AtividadeEntrevista.CORE.Format;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FI.AtividadeEntrevista.CORE.Extensions
{
    public static class DictionaryExtensions
    {
        public static List<Dictionary<string, object>> AplicarFormatacao(this List<Dictionary<string, object>> lista)
        {
            foreach (var dic in lista)
            {
                foreach (var chave in dic.Keys.ToList())
                {
                    if (Enum.TryParse<TipoCampo>(chave, true, out var tipoEnum))
                    {
                        var tipoCampo = (TipoCampo)tipoEnum;
                        if (dic[chave] is string valor)
                        {
                            dic[chave] = Formatador.FormatarCampo(valor, tipoCampo);
                        }
                    }
                }
            }
            return lista;
        }
    }

}
