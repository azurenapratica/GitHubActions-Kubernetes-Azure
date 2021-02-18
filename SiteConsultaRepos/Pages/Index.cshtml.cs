using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SiteConsultaRepos.HttpClients;
using SiteConsultaRepos.Models;

namespace SiteConsultaRepos.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly APIGitHubClient _apiGitHubClient;
        private readonly IMemoryCache _memoryCache;
        public ResultadoConsultaGitHub DadosConsultaGitHub;

        public IndexModel(ILogger<IndexModel> logger,
            IConfiguration configuration,
            APIGitHubClient apiGitHubClient,
            IMemoryCache memoryCache)
        {
            _logger = logger;
            _apiGitHubClient = apiGitHubClient;
            _memoryCache = memoryCache;
        }

        public void OnGet()
        {
            bool utilizouCache = true;

            DadosConsultaGitHub = _memoryCache.GetOrCreate<ResultadoConsultaGitHub>(
                "ResultadoConsultaGitHub", context =>
            {
                utilizouCache = false;
                context.SetAbsoluteExpiration(TimeSpan.FromMinutes(20));
                context.SetPriority(CacheItemPriority.High);
                return _apiGitHubClient.ObterRepositorios();
            });

            if (utilizouCache)
                _logger.LogInformation("Utilizado cache da consulta ao GitHub");
        }
    }
}