using System;
using System.Collections.Generic;

namespace Domain.DTO
{
    public class SolicitacaoCreditoDTO
    {
        public double ValorCredito { get; set; }
        public int TipoCredito { get; set; }
        public int QtdeParcelas { get; set; }
        public DateTime DataPrimeiroVencimento { get; set; }


        public IEnumerable<string> Validate()
        {
            var results = new List<string>();

            if (ValorCredito == 0)
                results.Add("Valor do crédito é obrigatório");
            else if (ValorCredito > 1000000D)
                results.Add("O valor máximo a ser liberado para qualquer tipo de empréstimo é de R$ 1.000.000,00");


            if (TipoCredito == 0)
                results.Add("Tipo de crédito é obrigatório");
            else if (TipoCredito > 5)
                results.Add("Tipo de crédito inválido");


            if (QtdeParcelas == 0)
                results.Add("Quantidade de parcelas é obrigatório");
            else if (QtdeParcelas < 5 && QtdeParcelas > 72)
                results.Add("Quantidade de parcelas deve ser entre 5 e 72");


            if (QtdeParcelas == 0)
                results.Add("Quantidade de parcelas é obrigatório");
            else if (QtdeParcelas < 5)
                results.Add("Quantidade de parcelas deve ser entre 5 e 72");
            else if (QtdeParcelas > 72)
                results.Add("Quantidade de parcelas deve ser entre 5 e 72");


            var dataMinima = DateTime.Now.Date.AddDays(15);
            var dataMaxima = DateTime.Now.Date.AddDays(40);
            DateTime dataSolicitada = DataPrimeiroVencimento;

            if(dataSolicitada == DateTime.MinValue)
            {
                results.Add("Data do primeiro vencimento é obrigatório");
            }
            else
            {
                if (!(dataSolicitada.Date >= dataMinima))
                {
                    results.Add("A data de vencimento solicitada é menor do que 15 dias a contar da data de hoje");                    
                }

                else if (!(dataSolicitada.Date <= dataMaxima))
                {
                    results.Add("A data de vencimento solicitada é maior do que 40 dias a contar da data de hoje");                    
                }
            }

            return results;
        }
    }
}
