using eAgenda.WebApp.Compartilhado.Infra.Orm;
using eAgenda.WebApp.Modulos.ModuloContato.Dominio;
using Microsoft.EntityFrameworkCore;

namespace eAgenda.WebApp.Modulos.ModuloContato.Infra;

public sealed class RepositorioContatoEmOrm(EAgendaDbContext dbContext) : IRepositorioContato
{
    public void Cadastrar(Contato entidade)
    {
        dbContext.Contatos.Add(entidade);

        dbContext.SaveChanges(); // commit
    }

    public bool Editar(Guid idSelecionado, Contato entidadeAtualizada)
    {
        Contato? contatoSelecionado = SelecionarPorId(idSelecionado);

        if (contatoSelecionado == null)
            return false;

        contatoSelecionado.Atualizar(entidadeAtualizada);

        dbContext.SaveChanges();

        return true;
    }

    public bool Excluir(Guid idSelecionado)
    {
        Contato? contatoSelecionado = SelecionarPorId(idSelecionado);

        if (contatoSelecionado == null)
            return false;

        dbContext.Contatos.Remove(contatoSelecionado);

        dbContext.SaveChanges();

        return true;
    }

    public Contato? SelecionarPorId(Guid idSelecionado)
    {
        return dbContext.Contatos.SingleOrDefault(c => c.Id == idSelecionado);
    }

    public List<Contato> SelecionarTodos()
    {
        return dbContext.Contatos.OrderBy(c => c.Nome).ToList();
    }

    public List<Contato> Filtrar(Func<Contato, bool> filtro)
    {
        return dbContext.Contatos.Where(filtro).ToList();
    }
}
