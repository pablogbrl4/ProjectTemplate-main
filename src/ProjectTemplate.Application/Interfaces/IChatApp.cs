﻿using Orizon.Rest.Chat.Domain.Entities;
using System.Collections.Generic;

namespace Orizon.Rest.Chat.Application.Interfaces
{
    public interface IChatApp
    {
        int Insert(int idLogin);

        void Lido(int idChat, int idLogin);

        IEnumerable<ChatE> Listar(int? idChat, int idLogin);

        IEnumerable<ChatE> Listar(int? idChat, int idLogin, string origem);
    }
}