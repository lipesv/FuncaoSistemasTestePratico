using FI.AtividadeEntrevista.BLL.Exceptions;
using FI.AtividadeEntrevista.CONTRACTS.Cliente;
using FI.AtividadeEntrevista.CONTRACTS.Validation;
using FI.AtividadeEntrevista.DML.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace FI.AtividadeEntrevista.BLL.Cliente
{
    public class BoCliente : IBoCliente
    {
        private readonly IValidationService _validationService;

        public BoCliente(IValidationService validationService)
        {
            _validationService = validationService;
        }

        public List<DML.Cliente> Listar()
        {
            try
            {
                DAL.DaoCliente cli = new DAL.DaoCliente();
                return cli.Listar();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DML.Cliente Consultar(long id)
        {
            try
            {
                DAL.DaoCliente cli = new DAL.DaoCliente();
                return cli.Consultar(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<DML.Cliente> Pesquisa(int iniciarEm,
                                          int quantidade,
                                          string campoOrdenacao,
                                          bool crescente,
                                          out int qtd)
        {
            try
            {
                DAL.DaoCliente cli = new DAL.DaoCliente();
                return cli.Pesquisa(iniciarEm, quantidade, campoOrdenacao, crescente, out qtd);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public long Incluir(DML.Cliente cliente)
        {
            try
            {
                _validationService.ValidateCpf(cliente.Cpf);

                if (VerificarExistencia(cliente.Cpf))
                {
                    throw new ValidationException("CPF existente na base de dados.", HttpStatusCode.BadRequest);
                }

                DAL.DaoCliente cli = new DAL.DaoCliente();
                return cli.Incluir(cliente);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Alterar(DML.Cliente cliente)
        {
            try
            {
                _validationService.ValidateCpf(cliente.Cpf);

                var clientes = Listar();
                
                var cpfExiste = clientes.Count() > 1
                                && clientes.Any(c => c.Cpf.RemoveMascara() == cliente.Cpf.RemoveMascara());

                if (cpfExiste)
                {
                    throw new ValidationException("Já existe um registro de cliente com o CPF informado na base de dados.", HttpStatusCode.BadRequest);
                }

                DAL.DaoCliente cli = new DAL.DaoCliente();
                cli.Alterar(cliente);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Excluir(long id)
        {
            try
            {
                DAL.DaoCliente cli = new DAL.DaoCliente();
                cli.Excluir(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool VerificarExistencia(string CPF)
        {
            try
            {
                DAL.DaoCliente cli = new DAL.DaoCliente();
                return cli.VerificarExistencia(CPF.RemoveMascara());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
