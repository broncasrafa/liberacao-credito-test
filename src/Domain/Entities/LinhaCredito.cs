using Domain.Common;

namespace Domain.Entities
{
    public class LinhaCredito : BaseEntity
    {
        public string Descricao { get; set; }
        public int PorcentoMes { get; set; }
        public int PorcentoAno { get; set; }
    }
}
