using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Orizon.Rest.Chat.API.Token
{
    public class AuthorizationToken
    {
        public enum TIPO
        {
            PRESTADOR = 1,
            OPERADORA = 0,
            OUTRO = 2
        }

        public string jti { get; set; }
        public int exp { get; set; }
        public int nbf { get; set; }
        public int iat { get; set; }
        public string iss { get; set; }
        public dynamic aud { get; set; }
        public string sub { get; set; }
        public string typ { get; set; }
        public string azp { get; set; }
        public string session_state { get; set; }
        public string client_session { get; set; }
        public RealmAccess realm_access { get; set; }
        public string nomeUsuario { get; set; }
        public int PrestadorId { get; set; }
        public string idPrestador { get; set; }
        public string name { get; set; }
        public string nomePrestador { get; set; }
        public string preferred_username { get; set; }
        public string idComprador { get; set; }
        public int CompradorId { get; set; }
        public TIPO Tipo { get; set; }
        public string grupoAcesso { get; set; }
        public string flex { get; set; }
        public string idLogin { get; set; }
        public string permiteDigitar { get; set; }
        public string tipoPrestador { get; set; }
        public string moduloImagem { get; set; }
        public bool impersonation { get; set; }
        public string faBandejaAuditor { get; set; }
        public void Bind(HttpRequestMessage Request)
        {
            try
            {
                CompradorId = Convert.ToInt32(idComprador);
            }
            catch (Exception)
            {
                CompradorId = 0;
            }

            try
            {
                PrestadorId = Convert.ToInt32(idPrestador);
            }
            catch (Exception)
            {
                PrestadorId = 0;
            }

            if (impersonation)
            {
                string sQueryString = Request.RequestUri.Query.Replace("?", "");
                foreach (var item in sQueryString.Split('&'))
                {
                    string[] chave_valor = item.Split('=');
                    if (
                            (chave_valor[0].ToUpper() == "Prestador_Id".ToUpper()) ||
                            (chave_valor[0].ToUpper() == "PrestadorId".ToUpper()) ||
                            (chave_valor[0].ToUpper() == "Id_Prestador".ToUpper()) ||
                            (chave_valor[0].ToUpper() == "IdPrestador".ToUpper())
                        )
                    {
                        if (chave_valor[1] != "")
                        {
                            PrestadorId = Convert.ToInt32(chave_valor[1]);
                        }
                    }
                }
            }
        }
    }

    public class RealmAccess
    {
        public List<string> roles { get; set; }
    }

    public class RealmManagement
    {
        public List<string> roles { get; set; }
    }

    public class Account
    {
        public List<string> roles { get; set; }
    }
}
