namespace Orizon.Rest.Chat.Domain.Interfaces.Repositories
{
    public interface IChatLeituraRepository
    {
        int NaoLidas(int idChat, int idLogin);
    }
}
