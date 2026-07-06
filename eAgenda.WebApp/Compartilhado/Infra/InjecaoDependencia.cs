using eAgenda.WebApp.Compartilhado.Infra.Orm;
using eAgenda.WebApp.Compartilhado.Infra.Sql;
using eAgenda.WebApp.Modulos.ModuloCategoria.Dominio;
using eAgenda.WebApp.Modulos.ModuloCategoria.Infra;
using eAgenda.WebApp.Modulos.ModuloCompromisso.Dominio;
using eAgenda.WebApp.Modulos.ModuloCompromisso.Infra;
using eAgenda.WebApp.Modulos.ModuloContato.Dominio;
using eAgenda.WebApp.Modulos.ModuloContato.Infra;
using eAgenda.WebApp.Modulos.ModuloDespesa.Dominio;
using eAgenda.WebApp.Modulos.ModuloDespesa.Infra;
using eAgenda.WebApp.Modulos.ModuloTarefa.Dominio;
using eAgenda.WebApp.Modulos.ModuloTarefa.Infra;
using Microsoft.EntityFrameworkCore;

namespace eAgenda.WebApp.Compartilhado.Infra;

public static class InjecaoDependencia
{
    public static void AddInfraRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EAgendaDbContext>(options =>
        {
            string? connectionString = configuration.GetConnectionString("SqlServer");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    $"A connection string \"SqlServer\" não foi encontrada."
                );
            }

            options.UseSqlServer(connectionString);
        });

        services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();

        services.AddScoped<IRepositorioContato, RepositorioContatoEmOrm>();
        services.AddScoped<IRepositorioCompromisso, RepositorioCompromissoEmOrm>();
        services.AddScoped<IRepositorioCategoria, RepositorioCategoriaEmOrm>();
        services.AddScoped<IRepositorioDespesa, RepositorioDespesaEmOrm>();
        services.AddScoped<IRepositorioTarefa, RepositorioTarefaEmSql>();
    }
}
