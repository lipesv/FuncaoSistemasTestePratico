using AutoMapper;
using FI.AtividadeEntrevista.CONTRACTS.Beneficiario;
using FI.AtividadeEntrevista.CONTRACTS.Cliente;
using FI.AtividadeEntrevista.DML;
using FI.AtividadeEntrevista.DML.Extensions;
using FI.WebAtividadeEntrevista.Controllers;
using FI.WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace WebAtividadeEntrevista.Controllers
{
    public class ClienteController : BaseController
    {
        private readonly IBoCliente _boCliente;
        private readonly IMapper _mapper;

        public ClienteController(IBoCliente boCliente, IMapper mapper)
        {
            _boCliente = boCliente;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Incluir()
        {
            var cliente = new ClienteModel { Estados = ObterEstados() };
            return View(cliente);
        }

        [HttpPost]
        public JsonResult Incluir(ClienteModel model)
        {
            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                return JsonResponse(HttpStatusCode.BadRequest, mensagens: erros);

            }
            else
            {
                var cliente = _mapper.Map<Cliente>(model);
                _boCliente.Incluir(cliente);

                return JsonResponse(HttpStatusCode.Created, mensagens: new List<string> { "Cadastro efetuado com sucesso." });
            }
        }

        [HttpGet]
        public ActionResult Alterar(long id)
        {
            var clienteModel = _mapper.Map<ClienteModel>(_boCliente.Consultar(id));
            clienteModel.Estados = ObterEstados();

            return View(clienteModel);
        }

        [HttpPost]
        public JsonResult Alterar(ClienteModel model)
        {

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                return JsonResponse(HttpStatusCode.BadRequest, mensagens: erros);
            }
            else
            {
                var cliente = _mapper.Map<Cliente>(model);
                _boCliente.Alterar(cliente);

                return JsonResponse(HttpStatusCode.OK, mensagens: new List<string> { "Cadastro alterado com sucesso." });
            }
        }

        [HttpPost]
        public JsonResult ClienteList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                int qtd = 0;
                string campo = string.Empty;
                string crescente = string.Empty;
                string[] array = jtSorting.Split(' ');

                if (array.Length > 0)
                    campo = array[0];

                if (array.Length > 1)
                    crescente = array[1];

                List<Cliente> clientes = _boCliente.Pesquisa(jtStartIndex,
                                                             jtPageSize,
                                                             campo,
                                                             crescente.Equals("ASC", StringComparison.InvariantCultureIgnoreCase),
                                                             out qtd);

                //Return result to jTable
                return Json(new
                {
                    Result = "OK",
                    Records = clientes,
                    TotalRecordCount = qtd
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Result = "ERROR",
                    Message = ex.Message
                });
            }
        }

        private List<SelectListItem> ObterEstados()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Value = "AC", Text = "Acre" },
                new SelectListItem { Value = "AL", Text = "Alagoas" },
                new SelectListItem { Value = "AP", Text = "Amapá" },
                new SelectListItem { Value = "AM", Text = "Amazonas" },
                new SelectListItem { Value = "BA", Text = "Bahia" },
                new SelectListItem { Value = "CE", Text = "Ceará" },
                new SelectListItem { Value = "DF", Text = "Distrito Federal" },
                new SelectListItem { Value = "ES", Text = "Espírito Santo" },
                new SelectListItem { Value = "GO", Text = "Goiás" },
                new SelectListItem { Value = "MA", Text = "Maranhão" },
                new SelectListItem { Value = "MT", Text = "Mato Grosso" },
                new SelectListItem { Value = "MS", Text = "Mato Grosso do Sul" },
                new SelectListItem { Value = "MG", Text = "Minas Gerais" },
                new SelectListItem { Value = "PA", Text = "Pará" },
                new SelectListItem { Value = "PB", Text = "Paraíba" },
                new SelectListItem { Value = "PR", Text = "Paraná" },
                new SelectListItem { Value = "PE", Text = "Pernambuco" },
                new SelectListItem { Value = "PI", Text = "Piauí" },
                new SelectListItem { Value = "RJ", Text = "Rio de Janeiro" },
                new SelectListItem { Value = "RN", Text = "Rio Grande do Norte" },
                new SelectListItem { Value = "RS", Text = "Rio Grande do Sul" },
                new SelectListItem { Value = "RO", Text = "Rondônia" },
                new SelectListItem { Value = "RR", Text = "Roraima" },
                new SelectListItem { Value = "SC", Text = "Santa Catarina" },
                new SelectListItem { Value = "SP", Text = "São Paulo" },
                new SelectListItem { Value = "SE", Text = "Sergipe" },
                new SelectListItem { Value = "TO", Text = "Tocantins" }
            };
        }

        //[HttpGet]
        //// GET: Clientes/Beneficiarios/5
        //public ActionResult Beneficiarios(long id)
        //{
        //    return PartialView("_BeneficiariosList");
        //}

        //private List<Beneficiario> GetBeneficiariosByClienteId(int clienteId)
        //{
        //    List<Beneficiario> beneficiarios = new List<Beneficiario>();

        //    using (SqlConnection conn = new SqlConnection(_connectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("spGetBeneficiariosByClienteId", conn);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@ClienteId", clienteId);

        //        conn.Open();
        //        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
        //        {
        //            while (reader.Read())
        //            {
        //                beneficiarios.Add(new Beneficiario
        //                {
        //                    BeneficiarioId = Convert.ToInt32(reader["BeneficiarioId"]),
        //                    ClienteId = Convert.ToInt32(reader["ClienteId"]),
        //                    Nome = reader["Nome"].ToString(),
        //                    Documento = reader["Documento"].ToString()
        //                });
        //            }
        //        }
        //    }
        //    return beneficiarios;
        //}
    }
}