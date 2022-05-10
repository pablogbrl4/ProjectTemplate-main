namespace Orizon.Rest.Chat.Domain.Entities
{
    public class Mensagem
    {
        /// <summary>
        /// Identificador do Chat
        /// </summary>
        public int FkChat { get; set; }
        /// <summary>
        /// Conteudo da conversa
        /// </summary>
        public string Conversa { get; set; }
        /// <summary>
        /// Descrição do usuario logado
        /// </summary>
        public string DsLoginRemetente { get; set; }
        /// <summary>
        /// Id do usuario logado
        /// </summary>
        public int IdLoginRemetente { get; set; }
        /// <summary>
        /// Id do Item
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Tipo de apontamento Item / Guia
        /// </summary>
        public string Tipo { get; set; }
        /// <summary>
        /// Origem da chamada Prestador / Auditor
        /// </summary>
        public string Origem { get; set; }
        /// <summary>
        /// Acao disparada pelo prestador quando Aceita(1) ou quando justifica(2)
        /// </summary>
        public int Acao { get; set; }
        /// <summary>
        /// Id da Guia
        /// </summary>
        public int GuiaId { get; set; }
        /// <summary>
        /// Chave do chat conversas
        /// </summary>
        public int IdChatConversas { get; set; }
    }

    public static class MensagemChat
    {
        public const string TipoApontamentoItem = "I";
        public const string TipoApontamentoGuia = "G";
        public const string OrigemPrestador = "PRE";
        public const string OrigemAuditor = "AUD";
    }

    public enum StatusItem
    {
        Aprovado = 1,
        Questionado = 2,
        Justificado = 3,
        Criticado = 4,
        ItensAdicionados = 5,
        Aceito = 6
    }

    public enum StatusContaNegociacao
    {
        Questionada = 1,
        Justificada = 2,
        Aceita = 3
    }

    public enum Acao
    {
        SemAcao = 0,
        Aceitar = 1,
        Justificar = 2
    }
}
