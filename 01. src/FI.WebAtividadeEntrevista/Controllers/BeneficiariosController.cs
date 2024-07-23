using AutoMapper;
using FI.AtividadeEntrevista.CONTRACTS.Beneficiario;
using FI.AtividadeEntrevista.DAL.Padrao;
using FI.AtividadeEntrevista.DML;
using FI.WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace FI.WebAtividadeEntrevista.Controllers
{
    public class BeneficiariosController : BaseController
    {
        private readonly IBoBeneficiario _boBeneficiario;
        private readonly IMapper _mapper;

        public BeneficiariosController(IBoBeneficiario boBeneficiario, IMapper mapper)
        {
            _boBeneficiario = boBeneficiario;
            _mapper = mapper;
        }

        // GET: Beneficiarios/Create
        [HttpGet]
        public ActionResult Cadastro(long IdCliente)
        {
            var beneficiario = new BeneficiarioModel { IdCliente = IdCliente };
            return PartialView("_BeneficiariosCadastroPartial", beneficiario);
        }

        // POST: Beneficiarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastro(BeneficiarioModel beneficiario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _boBeneficiario.Incluir(_mapper.Map<Beneficiario>(beneficiario));
                    return JsonResponse(HttpStatusCode.Created, mensagens: new List<string> { "Cadastro realizado com sucesso!" });
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return JsonErrorResponse("_BeneficiariosCadastroPartial");
        }

        [HttpPost]
        public JsonResult Lista(QueryParameters queryParameters)
        {
            try
            {
                var result = _boBeneficiario.Listar(queryParameters);

                //Return result to jTable
                return Json(new { Result = "OK", Records = result.Records, TotalRecordCount = result.TotalRecordCount });
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

        // GET: Beneficiarios/Edit/5
        [HttpGet]
        public ActionResult Atualiza(long id)
        {
            var beneficiario = _mapper.Map<List<BeneficiarioModel>>(_boBeneficiario.Consultar(id));

            if (beneficiario == null)
            {
                return JsonResponse(HttpStatusCode.BadRequest, mensagens: new List<string> { "Beneficiário não localizado." });
            }

            return PartialView("_BeneficiariosAtualizaPartial", beneficiario.FirstOrDefault());
        }

        // POST: Beneficiarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Atualiza(long id, Beneficiario beneficiario)
        {
            if (id != beneficiario.Id)
            {
                return JsonResponse(HttpStatusCode.BadRequest, mensagens: new List<string> { "Beneficiário não localizado." });
            }

            if (ModelState.IsValid)
            {
                _boBeneficiario.Alterar(_mapper.Map<Beneficiario>(beneficiario));
                return JsonResponse(HttpStatusCode.OK, mensagens: new List<string> { "Cadastro atualizado com sucesso!" });
            }

            return JsonErrorResponse("_BeneficiariosAtualizaPartial");
        }

        //// GET: Beneficiarios/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var beneficiario = await GetBeneficiarioById(id.Value);
        //    if (beneficiario == null)
        //    {
        //        return NotFound();
        //    }

        //    return PartialView(beneficiario);
        //}

        //// POST: Beneficiarios/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    var beneficiario = await GetBeneficiarioById(id);
        //    if (beneficiario != null)
        //    {
        //        await DeleteBeneficiario(id);
        //    }
        //    return RedirectToAction(nameof(Details), "Clientes", new { id = beneficiario.ClienteId });
        //}
    }
}