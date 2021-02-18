using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SiteConsultaRepos.Models;

namespace SiteConsultaRepos.HttpClients
{
    public class APIGitHubClient
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly ILogger<APIGitHubClient> _logger;

        public APIGitHubClient(
            HttpClient client, IConfiguration configuration,
            ILogger<APIGitHubClient> logger)
        {
            _client = client;
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");

            _configuration = configuration;
            _logger = logger;
        }

        public ResultadoConsultaGitHub ObterRepositorios()
        {
            var dataConsulta = DateTime.Now;
            var repositorios = _client.GetFromJsonAsync<Repositorio[]>(
                _configuration["UrlConsultaGitHub"]).Result;
            _logger.LogInformation(
                "Consulta ao GitHub | " +
                $"No. de repositorios encontrados em {dataConsulta:dd/MM/yyyy HH:mm:ss}: {repositorios.Length}");

            return new ()
            {
                UrlPesquisa = _configuration["UrlConsultaGitHub"],
                Repositorios = repositorios,
                DataConsulta = dataConsulta
            };
        }
    }
}