using FI.AtividadeEntrevista.BLL.Exceptions;
using FI.AtividadeEntrevista.CONTRACTS.Beneficiario;
using FI.AtividadeEntrevista.CONTRACTS.Validation;
using FI.AtividadeEntrevista.CORE.Extensions;
using FI.AtividadeEntrevista.DAL.Beneficiario;
using FI.AtividadeEntrevista.DAL.Padrao;
using FI.AtividadeEntrevista.DML;
using FI.AtividadeEntrevista.DML.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace FI.AtividadeEntrevista.BLL.Beneficiario
{
    public class BoBeneficiario : IBoBeneficiario
    {
        private readonly IValidationService _validationService;

        public BoBeneficiario(IValidationService validationService)
        {
            _validationService = validationService;
        }

        public List<DML.Beneficiario> Consultar(long? id, long? IdCliente = null)
        {
            try
            {
                DaoBeneficiario daoBeneficiario = new DaoBeneficiario();
                var dados = daoBeneficiario.Consultar(id, IdCliente);

                return dados;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<DML.Beneficiario> Listar(int iniciarEm,
                                             int quantidade,
                                             string campoOrdenacao,
                                             bool crescente,
                                             out int qtd,
                                             long clienteId = 0)
        {
            throw new NotImplementedException();
        }

        public QueryResult Listar(QueryParameters queryParameters)
        {
            try
            {
                DaoBeneficiario beneficiario = new DaoBeneficiario();
                var result = beneficiario.Listar(queryParameters);

                result.Records = result.Records.AplicarFormatacao();

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public long Incluir(DML.Beneficiario beneficiario)
        {
            try
            {
                _validationService.ValidateCpf(beneficiario.Cpf);

                if (VerificarExistencia(beneficiario.Cpf, beneficiario.IdCliente))
                {
                    throw new ValidationException("Já existe um beneficiário associado ao cliente com o CPF informado.", HttpStatusCode.BadRequest);
                }

                DaoBeneficiario daoBeneficiario = new DaoBeneficiario();
                return daoBeneficiario.Incluir(beneficiario);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool VerificarExistencia(string CPF, long clienteId)
        {
            try
            {
                DaoBeneficiario beneficiario = new DaoBeneficiario();
                return beneficiario.VerificarExistencia(CPF.RemoveMascara(), clienteId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Alterar(DML.Beneficiario beneficiario)
        {
            try
            {
                _validationService.ValidateCpf(beneficiario.Cpf);

                //if (VerificarExistencia(beneficiario.Cpf, beneficiario.IdCliente))
                //{
                //    throw new ValidationException("Já existe um beneficiário associado ao cliente com o CPF informado.", HttpStatusCode.BadRequest);
                //}

                DaoBeneficiario daoBeneficiario = new DaoBeneficiario();
                daoBeneficiario.Alterar(beneficiario);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Excluir(long id)
        {
            throw new NotImplementedException();
        }


    }
}
