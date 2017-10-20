using Newtonsoft.Json;
using SC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SC.Services
{
    public class EmotionService
    {
        private readonly string _key;
        private readonly string _reconhecimentoEmocaoUri = "https://api.projectoxford.ai/emotion/v1.0/recognize";

        public EmotionService(string key) =>  _key = key;

        public async Task<List<ResultadoEmocao>> ReconhecimentoEmocaoPorImagemUrlAsync(string imageUrl)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _key);

            var stringContent = new StringContent(@"{""url"":""" + imageUrl + @"""}");

            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            try
            {
                var resposta = await httpClient.PostAsync(_reconhecimentoEmocaoUri, stringContent); // junta a url da api com o cabeçalho + url da imagem
                var json = await resposta.Content.ReadAsStringAsync();
                if(resposta.IsSuccessStatusCode)
                {
                    var imagemResultadoEmocao = JsonConvert.DeserializeObject<List<ResultadoEmocao>>(json); // pega a resposta do servidor e desserilializa na model
                    return imagemResultadoEmocao;
                }
               throw new Exception(json);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<ResultadoEmocao>> ReconhecimentoEmocaoPorStreamAsync(Stream stream)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _key);

            var streamContent = new StreamContent(stream);

            streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            try
            {
                var resposta = await httpClient.PostAsync(_reconhecimentoEmocaoUri, streamContent);
                var json = await resposta.Content.ReadAsStringAsync();

                if(resposta.IsSuccessStatusCode)
                {
                    var imagemResultadoEmocao = JsonConvert.DeserializeObject<List<ResultadoEmocao>>(json);

                    return imagemResultadoEmocao;

                }

                throw new Exception(json);
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        
    }
}
