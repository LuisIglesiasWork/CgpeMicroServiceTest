using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Cgpe.Du.CrossCuttings
{
    public static class HttpJsonResponse
    {
        /// <summary>
        /// Crea una respuesta HttpResponseMessage de tipo "application/json" con el estado Http indicado
        /// y serializando el contenido proporcionado 
        /// </summary>
        /// <param name="status">Estado que se quiera incluir en la respuesta</param>
        /// <param name="content">Contenido que se quiera serializar</param>
        /// <returns>Respuesta de tipo HttpResponseMessage</returns>
        public static HttpResponseMessage CreateResponse(HttpStatusCode status, object content)
        {
            //Por defecto, serializa en LowerCamelCase
            return HttpJsonResponse.CreateResponse(status, content.ToLowerCamelJson());
        }

        /// <summary>
        /// Crea una respuesta HttpResponseMessage de tipo "application/json" con el estado Http indicado
        /// y serializando el contenido proporcionado 
        /// </summary>
        /// <param name="status">Estado que se quiera incluir en la respuesta</param>
        /// <param name="content">Contenido que se quiera serializar</param>
        /// <param name="useLowerCamelCase">True si se quiere serializar usando LowerCamelCase. 
        /// Por defecto, es True.</param>
        /// <returns>Respuesta de tipo HttpResponseMessage</returns>
        public static HttpResponseMessage CreateResponse(HttpStatusCode status, object content, bool useLowerCamelCase)
        {
            string json;
            if (useLowerCamelCase)
                json = content.ToLowerCamelJson();
            else
                json = JsonConvert.SerializeObject(content);

            return HttpJsonResponse.CreateResponse(status, json);
        }

        /// <summary>
        /// Crea una respuesta HttpResponseMessage de tipo "application/json" con el estado Http indicado
        /// y el json proporcionado 
        /// </summary>
        /// <param name="status">Estado que se quiera incluir en la respuesta</param>
        /// <param name="json">Contenido serializado en Json</param>
        /// <returns>Respuesta de tipo HttpResponseMessage</returns>
        public static HttpResponseMessage CreateResponse(HttpStatusCode status, string json)
        {
            HttpResponseMessage resHttp = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
            };

            return resHttp;
        }
    }
}
