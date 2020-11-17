
using GitHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class GitApiClient
    {
        private readonly HttpClient _httpClient;

        public GitApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Repositorio>> GetRepositoriosAsync()
        {
            _httpClient.DefaultRequestHeaders.Add("user-agent", "rudynik");
            _httpClient.DefaultRequestHeaders.Add("accept", "application/vnd.github.v3+json");           
            HttpResponseMessage response = await _httpClient.GetAsync("/users/rudynik/repos");            
            response.EnsureSuccessStatusCode();

            IEnumerable<Repositorio> repositorios;

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                repositorios = Enumerable.Empty<Repositorio>();
            }
            else
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();

                repositorios = await JsonSerializer.DeserializeAsync<IEnumerable<Repositorio>>(responseStream);
            }

            return repositorios;
        }

        public async Task<Repositorio> GetRepoAsync(string nome)
        {
            _httpClient.DefaultRequestHeaders.Add("user-agent", "rudynik");
            _httpClient.DefaultRequestHeaders.Add("accept", "application/vnd.github.v3+json");
            HttpResponseMessage response = await _httpClient.GetAsync($"/repos/rudynik/{nome}");
            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<Repositorio>(responseStream);
        }

        public async Task<IEnumerable<Contribuidores>> GetContribuidoresAsync(string nome)
        {
            _httpClient.DefaultRequestHeaders.Add("user-agent", "rudynik");
            _httpClient.DefaultRequestHeaders.Add("accept", "application/vnd.github.v3+json");
            HttpResponseMessage response = await _httpClient.GetAsync($"/repos/rudynik/{nome}/contributors");
            response.EnsureSuccessStatusCode();

            IEnumerable<Contribuidores> contribuidores;


            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
			{
                contribuidores = Enumerable.Empty<Contribuidores>();
			}
			else
			{
                using var responseStream = await response.Content.ReadAsStreamAsync();

                contribuidores = await JsonSerializer.DeserializeAsync<IEnumerable<Contribuidores>>(responseStream);
            }

            return contribuidores;
        }
    }
}
