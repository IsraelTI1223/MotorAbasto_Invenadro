using Newtonsoft.Json;
using PortalGRFP.Business.Login;
using PortalGRFP.Controllers;
using PortalGRFP.Entities.Models;
using PortalGRFP.Entities.Response;
using PortalGRFP.Models.Gmail;
using PortalGRFP.Properties;
using PortalGRFP.Utilities.Core.Responses;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Extensions
{
    public static class GmailOauth
    {
        /// <summary>
        /// Metodo Para solicitar Login Gmail
        /// </summary>
        /// <param name="context"></param>
        public static string Oauth2(this AccountController context, string correo)
        {
            string redirect = $"{context.Request.Url.Scheme}://{context.Request.Url.Authority}/Account/SaveGoogleUser";
            StringBuilder urlBulider = new StringBuilder(GoogleConstant.Oauth2Url);
            urlBulider.Append("client_id=" + GoogleConstant.ClientId);
            urlBulider.Append("&redirect_uri=" + redirect);
            urlBulider.Append("&response_type=" + "code");
            urlBulider.Append("&scope=" + GoogleConstant.Scopes);
            urlBulider.Append("&access_type=" + "offline");
            urlBulider.Append("&state=" + correo);
            urlBulider.Append("Aprompt=" + "select_account");
            urlBulider.Append("&login_hint=" + correo);

            return urlBulider.ToString();
        }
        /// <summary>
        /// Metodo para solicitar Token de Gmail
        /// </summary>
        /// <param name="context">Controller que invoca</param>
        /// <param name="code">Codigo de retorno Oauth2 Gmail</param>
        /// <returns></returns>
        public static async Task<ResponseSimple<UsuarioModel>> TokenAsync(this AccountController context, string code)
        {
            #region [HttpClient]
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://www.googleapis.com")
            };
            string redirect = $"{context.Request.Url.Scheme}://{context.Request.Url.Authority}/Account/SaveGoogleUser";
            var requestUrl = $"oauth2/v4/token?code={code}&client_id={GoogleConstant.ClientId}&client_secret={GoogleConstant.ClientSecret}&redirect_uri={redirect}&grant_type=authorization_code";

            var dict = new Dictionary<string, string>
            {
                { "Content-Type", "application/x-www-form-urlencoded" }
            };
            var req = new HttpRequestMessage(HttpMethod.Post, requestUrl) { Content = new FormUrlEncodedContent(dict) };
            var response = await httpClient.SendAsync(req);
            string jsonResponse = await response.Content.ReadAsStringAsync();
            #endregion

            var token = JsonConvert.DeserializeObject<GmailToken>(jsonResponse);
           
            var obj = await GetuserProfile(token.AccessToken);

            var res = new LoginBusines().Login(obj.Email);
            if (res.Result != null)
            {
                context.Session[Resources.SESSION_KEY_GMAIL_TOKEN] = token.AccessToken;
                context.Session[Resources.SESSION_KEY_USER] = res.Result;
                res.Result.FotoGmail = obj.Picture;
                res.Success = true;
            }
            else {
                res.Success = false;
            }

            return res;
        }
        /// <summary>
        /// Metodo scope Gmail para Obtener Datos del usuario
        /// </summary>
        /// <param name="accesstoken">Token de la sesion Gmail</param>
        /// <returns>Datos del usuario de Gmail</returns>
        public static async Task<UserProfile> GetuserProfile(string accesstoken)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://www.googleapis.com")
            };
            string url = $"https://www.googleapis.com/oauth2/v1/userinfo?alt=json&access_token={accesstoken}";
            var response = await httpClient.GetAsync(url);
            string jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UserProfile>(jsonResponse);
        }
    }
}