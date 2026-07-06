using eAgenda.WebApp.Compartilhado.Infra.Orm;
using eAgenda.WebApp.Modulos.ModuloDespesa.Dominio;
using Microsoft.EntityFrameworkCore;

namespace eAgenda.WebApp.Modulos.ModuloDespesa.Infra;

public sealed class RepositorioDespesaEmOrm(EAgendaDbContext dbContext) :
    RepositorioBaseEmOrm<Despesa>(dbContext), IRepositorioDespesa
{
    public override List<Despesa> SelecionarTodos()
    {
        return registros
            .Include(d => d.Categorias)
            .ThenInclude(c => c.Despesas)
            .OrderByDescending(d => d.DataOcorrencia)
            .ThenBy(d => d.Descricao)
            .ToList();
    }
}
