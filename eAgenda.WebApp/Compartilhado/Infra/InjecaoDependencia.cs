using eAgenda.WebApp.Compartilhado.Infra.Sql;
using eAgenda.WebApp.Modulos.ModuloContato.Dominio;
using eAgenda.WebApp.Modulos.ModuloContato.Infra;

namespace eAgenda.WebApp.Compartilhado.Infra;

public static class InjecaoDependencia
{
    public static void AddInfraRepositories(this IServiceCollection services)
    {
        services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();

        services.AddScoped<IRepositorioContato, RepositorioContatoEmSql>();
    }
}
