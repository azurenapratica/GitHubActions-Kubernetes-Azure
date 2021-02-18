using System;

namespace SiteConsultaRepos.Models
{
    public class ResultadoConsultaGitHub
    {
        public string UrlPesquisa { get; set; }
        public Repositorio[] Repositorios { get; set; }
        public DateTime DataConsulta { get; set; }
    }
}