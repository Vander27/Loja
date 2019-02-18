using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Projeto.Entities;
using Projeto.Repository.Persistence;
using Projeto.Services.Models;

namespace Projeto.Services.Controllers
{
    [RoutePrefix("api")]
    public class OperacaoController : ApiController
    {
        [HttpGet] //url api/mdr
        [Route("mdr")]
        public HttpResponseMessage Consultar()
        {

            try
            {
                List<ClienteAdquirenteConsultaViewModel> lista = new List<ClienteAdquirenteConsultaViewModel>();
                ClienteAdquirenteRepository repAdquirente = new ClienteAdquirenteRepository();
                TaxaRepository repTaxa = new TaxaRepository();

                //


               
                foreach (ClienteAdquirente a in repAdquirente.FindAll())
                {
                    ClienteAdquirenteConsultaViewModel model = new ClienteAdquirenteConsultaViewModel();
                    model.Adquirente = a.Adquirente;

                    model.Taxas = new List<TaxaConsultaViewModel>();

                    foreach (Taxa t in repTaxa.ClienteAdquirente(a.IdClienteAdquirente))
                    {
                        TaxaConsultaViewModel modelTaxa = new TaxaConsultaViewModel();
                        modelTaxa.Bandeira = t.Bandeira;
                        modelTaxa.Credito = t.Credito;
                        modelTaxa.Debito = t.Debito;

                        model.Taxas.Add(modelTaxa);
                    }


                    lista.Add(model);
                }

                return Request.CreateResponse(HttpStatusCode.OK, lista);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }


        }


        [HttpPost] //url api/operacao
        [Route("operacao")]
        public HttpResponseMessage Calculo(OperacaoViewModel model)
        {


            try
            {


                ClienteAdquirente Adquirente = new ClienteAdquirente();
                ClienteAdquirenteRepository repAdquirente = new ClienteAdquirenteRepository();
                ClienteAdquirente adquirente = repAdquirente.Adquirente(model.Adquirente);
               


                decimal taxa = 10 ;
                if (model.Tipo.Equals("credito"))
                {
                    taxa = repAdquirente.Credito(model.Bandeira, adquirente.IdClienteAdquirente);
                }
                else if (model.Tipo.Equals("debito"))
                {
                    taxa = repAdquirente.Credito(model.Bandeira, adquirente.IdClienteAdquirente);
                }


                OperacaoSaidaViewModel saida = new OperacaoSaidaViewModel();
                saida.ValorLiquido = (model.Valor ) - (model.Valor * taxa / 100);

                return Request.CreateResponse(HttpStatusCode.OK, saida );
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Erro servidor: " + e.Message);
            }


        }

    }
}
