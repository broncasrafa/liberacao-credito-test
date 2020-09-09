using System;
using System.Transactions;

namespace Domain.Common
{
    public static class Utilitarios
    {
        public static double ObterCalculoValorTotalJuros(double valorPrincipal, double taxaJuros, int periodoTotal)
        {
            // Valor financiado
            double C = valorPrincipal;
            // Parcelas
            int n = periodoTotal;
            // Taxa mensal
            double i = (Convert.ToDouble(taxaJuros) / 100);
            
            // Valor Total
            //double M = (C * Math.Pow((1 + i), n));
            //return Math.Round((M), 2);

            // Prestação
            double PMT = (C * Math.Pow((1 + i), n) * i) / (Math.Pow((1 + i), n) - 1);
            // Valor Total            
            return Math.Round((PMT * n), 2);
        }

        public static double ConverterTaxaAnualEmMensal(double taxaAnual)
        {
            double i = (Convert.ToDouble(taxaAnual) / 100);
            double a = (1 + i);
            double b = 1 / 12D;
            double c = Math.Pow(a, b);
            double d = c - 1D;
            double e = d * 100D;
            return Convert.ToDouble(Math.Round(Convert.ToDecimal(e), 2));
        }

        public static double ObterValorParcelas(double valorPrincipal, double taxaJuros, int periodoTotal)
        {
            // Valor financiado
            double C = valorPrincipal;
            // Parcelas
            int n = periodoTotal;
            // Taxa mensal
            double i = (Convert.ToDouble(taxaJuros) / 100);            
            // Prestação
            double PMT = (C * Math.Pow((1 + i), n) * i) / (Math.Pow((1 + i), n) - 1);

            return Math.Round(PMT, 2);
        }

        public static double ObterValorJurosTotal(double valorPrincipal, double valorTotal)
        {
            return Math.Round(Math.Round(valorTotal, 2) - Math.Round(valorPrincipal, 2), 2);
        }
    }
}
