using System.Collections.Generic;
using System.Linq;
using Domain.DTO;
using Domain.Entities;

namespace Application.Extensions
{
    public static class MapperDtoToEntity
    {

        public static SolicitacaoCredito MapToEntity(this SolicitacaoCreditoDTO dto)
        {
            SolicitacaoCredito entity = new SolicitacaoCredito
            {
                DataPrimeiroVencimento = dto.DataPrimeiroVencimento,
                QtdeParcelas = dto.QtdeParcelas,
                TipoCredito = dto.TipoCredito,
                ValorCredito = dto.ValorCredito
            };

            return entity;
        }

        public static Credito MapToEntity(this EfetuarSolicitacaoCreditoDTO dto)
        {
            System.DateTime dataCadastro = System.DateTime.Now;

            Credito credito = new Credito();

            // mapeia os dados do cliente
            credito.DadosSolicitacaoCliente = new Cliente
            {
                Nome = dto.Nome,
                Celular = dto.Celular,
                Email = dto.Email,
                Cpf = dto.Cpf,
                Cnpj = dto.Cnpj,
                Uf = dto.Uf,
                DataCadastro = dataCadastro
            };

            
            // mapeia os dados do financiamento
            credito.DadosSolicitacaoCliente.Financiamentos = new List<Financiamento>();
            credito.DadosSolicitacaoCliente.Financiamentos.Add(new Financiamento
            {
                IdLinhaCredito = dto.TipoCredito,                
                ValorSolicitado = dto.ValorCredito,
                ValorTotal = dto.ValorTotal,
                ValorJuros = dto.ValorJuros,
                QtdeParcelas = dto.QtdeParcelas,
                ValorParcelas = dto.ValorParcela,
                DataPrimeiroVencimento = dto.DataPrimeiroVencimento,
                DiaVencimento = dto.DataPrimeiroVencimento.Day,
                DataCadastro = dataCadastro
            });

            // mapeia os dados da parcela
            List<Parcela> parcelas = new List<Parcela>();
            for (int i = 1; i <= dto.QtdeParcelas; i++)
            {
                Parcela parcela = new Parcela();
                parcela.NroParcela = i;
                parcela.ValorParcela = dto.ValorParcela;
                                
                if(i == 1)
                {
                    parcela.DataVencimento = dto.DataPrimeiroVencimento;
                }
                else
                {
                    var ultimaDataVenc = parcelas.LastOrDefault().DataVencimento;
                    parcela.DataVencimento = ultimaDataVenc.AddMonths(1);
                }

                parcela.DiasEmAtraso = 0;
                parcelas.Add(parcela);
            }
            credito.DadosSolicitacaoCliente.Financiamentos.FirstOrDefault().Parcelas = parcelas;

            return credito;
        }
    }
}

