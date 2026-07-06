using eAgenda.WebApp.Compartilhado.Infra.Orm;
using eAgenda.WebApp.Modulos.ModuloCategoria.Dominio;

namespace eAgenda.WebApp.Modulos.ModuloCategoria.Infra;

public sealed class RepositorioCategoriaEmOrm(EAgendaDbContext dbContext) :
    RepositorioBaseEmOrm<Categoria>(dbContext), IRepositorioCategoria
{
}
