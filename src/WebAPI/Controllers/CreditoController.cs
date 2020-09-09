using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.Extensions;
using Domain.DTO;
using Domain.Entities;
using WebAPI.Models;


namespace WebAPI.Controllers
{
    [Route("api/v1/credito")]
    [ApiController]
    public class CreditoController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public CreditoController(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }


        [HttpGet]
        [Route("linhas")]
        public async Task<IActionResult> ConsultarTodosTiposCredito()
        {
            try
            {
                IEnumerable<LinhaCredito> result = await _uow.LinhasCreditos.GetAllAsync();

                List<LinhaCreditoResponse> data = result.ToList()
                                                         .Select(c => new LinhaCreditoResponse 
                                                         { 
                                                             Descricao = c.Descricao, 
                                                             Taxa = c.PorcentoMes > 0 ? $"{c.PorcentoMes}% ao mês" : $"{c.PorcentoAno}% ao ano"
                                                         }).ToList();

                return Ok(new ApiOkResponse(data));
            }
            catch
            {
                return BadRequest(new ApiBadRequestResponse(new List<string> { "Erro ao tentar consultar as linhas de crédito" }));
            }
        }

        [HttpGet]
        [Route("linhas/info/{id}")]
        public async Task<IActionResult> ConsultarTipoCredito(int id)
        {
            try
            {
                LinhaCredito result = await _uow.LinhasCreditos.GetByIdAsync(id);

                LinhaCreditoResponse data = new LinhaCreditoResponse
                {
                    Descricao = result.Descricao,
                    Taxa = result.PorcentoMes > 0 ? $"{result.PorcentoMes}% ao mês" : $"{result.PorcentoAno}% ao ano"
                };

                return Ok(new ApiOkResponse(data));
            }
            catch
            {
                return BadRequest(new ApiBadRequestResponse(new List<string> { "Erro ao tentar consultar a linha de crédito" }));
            }
        }

        [HttpPost]
        [Route("solicitar")]
        public async Task<IActionResult> SolicitarCredito([FromBody]SolicitacaoCreditoDTO model)
        {
            try
            {
                SolicitacaoCredito credito = model.MapToEntity();

                LinhaCredito linhaCredito = await _uow.LinhasCreditos.GetByIdAsync(credito.TipoCredito);
                
                credito.PercentualTaxa = linhaCredito.PorcentoMes > 0 ? linhaCredito.PorcentoMes : linhaCredito.PorcentoAno;


                SolicitacaoCreditoStatus statusSolicitacao = null;

                if (credito.TipoCredito == 1)
                {
                    statusSolicitacao = credito.ProcessarSolicitacaoCreditoDireto();
                }
                else if (credito.TipoCredito == 2)
                {
                    statusSolicitacao = credito.ProcessarSolicitacaoCreditoConsignado();
                }
                else if (credito.TipoCredito == 3)
                {
                    statusSolicitacao = credito.ProcessarSolicitacaoCreditoPessoaJuridica();
                }
                else if (credito.TipoCredito == 4)
                {
                    statusSolicitacao = credito.ProcessarSolicitacaoCreditoPessoaFisica();
                }
                else if (credito.TipoCredito == 5)
                {
                    statusSolicitacao = credito.ProcessarSolicitacaoCreditoImobiliario();
                }

                return Ok(new ApiOkResponse(statusSolicitacao) { Message = statusSolicitacao.StatusCredito });
            }
            catch
            {
                return BadRequest(new ApiBadRequestResponse(new List<string> { "Erro ao tentar solicitar o crédito" }));
            }
        }

        [HttpPost]
        [Route("efetuar")]
        public async Task<IActionResult> EfetuarSolicitacao([FromBody]EfetuarSolicitacaoCreditoDTO model)
        {
            try
            {
                Credito credito = model.MapToEntity();                

                var result = await _uow.Credito.AdicionarCreditoCliente(credito);

                return Ok(new ApiOkResponse(result));
            }
            catch(System.Exception ex)
            {
                return BadRequest(new ApiBadRequestResponse(new List<string> { "Erro ao tentar efetuar a liberação do crédito" }));
            }
        }
    }
}