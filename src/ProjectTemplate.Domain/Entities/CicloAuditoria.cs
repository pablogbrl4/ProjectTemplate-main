namespace Orizon.Rest.Chat.Domain.Entities
{
    public class CicloAuditoria
    {
        public int ciclo { get; set; }
        public int protocolo { get; set; }
        public decimal? quantidade { get; set; }
        public decimal? valorUnidade { get; set; }
        public string descricaoItem { get; set; }
        public string observacao { get; set; }
    }
}
