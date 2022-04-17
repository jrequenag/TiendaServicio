using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using TiendaServicio.Api.CarritoCompra.RemoteInterface;
using TiendaServicio.Api.CarritoCompra.RemoteModel;

namespace TiendaServicio.Api.CarritoCompra.RemoteService
{
    public class LibroService : ILibroService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly ILogger<LibroService> _logger;

        public LibroService(IHttpClientFactory httpClient
            , ILogger<LibroService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        public async Task<(bool resultado, LibroRemote Libro, string ErrorMessage)> GetLibro(Guid LibroId)
        {
            try
            {
                var client = _httpClient.CreateClient("Libros");
                var response = await client.GetAsync($"api/Libro/{LibroId}");
                if (response.IsSuccessStatusCode)
                {
                    var contenido = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var result = JsonSerializer.Deserialize<LibroRemote>(contenido, options);
                    return (true, result, "");
                }
                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                _logger?.LogError($"ocurrio un error al procesar el request error => {ex.Message}, en => {ex.StackTrace}");
                return (false, null, ex.Message);
            }
        }
    }
}
