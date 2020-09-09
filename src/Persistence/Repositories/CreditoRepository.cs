using Application.Extensions;
using Application.Interfaces;
using Dapper;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class CreditoRepository : ApplicationDbContext, ICreditoRepository
    {
        private readonly IClienteRepository clienteRepository;
        private readonly IFinanciamentoRepository financiamentoRepository;
        private readonly IParcelaRepository parcelaRepository;

        public CreditoRepository(IConfiguration configuration) 
            : base(configuration)
        {
        }

        public async Task<int> AdicionarCreditoCliente(Credito credito)
        {
            using (IDbConnection connection = CreateConnection())
            using (var transaction = connection.BeginTransaction())
            {
                var cliente = new
                {
                    Nome = credito.DadosSolicitacaoCliente.Nome,
                    Celular = credito.DadosSolicitacaoCliente.Celular,
                    Email = credito.DadosSolicitacaoCliente.Email,
                    Cpf = credito.DadosSolicitacaoCliente.Cpf,
                    Cnpj = credito.DadosSolicitacaoCliente.Cnpj,
                    Uf = credito.DadosSolicitacaoCliente.Uf
                };

                var idCliente = await connection.ExecuteScalarAsync(@"PRC_INSERT_CLIENTE", cliente, transaction, 0, CommandType.StoredProcedure);


                var objFinanciamento = credito.DadosSolicitacaoCliente.Financiamentos.FirstOrDefault();
                objFinanciamento.IdCliente = Convert.ToInt32(idCliente);

                var financiamento = new
                {
                    IdCliente = objFinanciamento.IdCliente,
                    IdLinhaCredito = objFinanciamento.IdLinhaCredito,
                    ValorSolicitado = objFinanciamento.ValorSolicitado,
                    ValorTotal = objFinanciamento.ValorTotal,
                    ValorJuros = objFinanciamento.ValorJuros,
                    QtdeParcelas = objFinanciamento.QtdeParcelas,
                    ValorParcelas = objFinanciamento.ValorParcelas,
                    DataPrimeiroVencimento = objFinanciamento.DataPrimeiroVencimento,
                    DiaVencimento = objFinanciamento.DiaVencimento
                };

                var idFinanciamento = await connection.ExecuteScalarAsync(@"PRC_INSERT_FINANCIAMENTO", financiamento, transaction, 0, CommandType.StoredProcedure);

                List<Parcela> parcelas = objFinanciamento.Parcelas;
                parcelas.ForEach(c => c.IdFinanciamento = Convert.ToInt32(idFinanciamento));


                SqlMapper.ICustomQueryParameter mapperParcela = parcelas.ConvertToDataTable().AsTableValuedParameter("TP_PARCELAS");

                await connection.QueryAsync(@"PRC_INSERT_PARCELA", new { Parcelas = mapperParcela }, transaction, 0, CommandType.StoredProcedure);

                transaction.Commit();

                return Convert.ToInt32(idCliente);
            }
        }
    }
}
