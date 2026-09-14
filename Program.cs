using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConsumerAdviceApi
{
    public class AdviceSlipResponse
    {
        [JsonPropertyName("slip")]
        public Slip Slip { get; set; }
    }

    public class Slip
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("advice")]
        public string Advice { get; set; }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            string url = "https://api.adviceslip.com/advice";

            Console.WriteLine("Iniciando requisição para obter dados de um conselho:");
            Console.WriteLine(url);
            Console.WriteLine();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    string jsonResponse = await response.ContentReadAsStringAsync();

                    AdviceSlipResponse result = JsonSerializer.Deserialize<AdviceSlipResponse>(jsonResponse);

                    Console.WriteLine("Conselho de Hoje:");
                    Console.WriteLine(result?.Slip?.Advice);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao obter conselho: {ex.Message}");
                }
            }
        }
    }
}