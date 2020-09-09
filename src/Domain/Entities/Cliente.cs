using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities
{
    [Table("tbCliente")]
    public class Cliente : BaseEntity
    {
        public string Nome { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Cnpj { get; set; }
        public string Uf { get; set; }
        public List<Financiamento> Financiamentos { get; set; }

        public Cliente()
        {
            Financiamentos = new List<Financiamento>();
        }
    }
}