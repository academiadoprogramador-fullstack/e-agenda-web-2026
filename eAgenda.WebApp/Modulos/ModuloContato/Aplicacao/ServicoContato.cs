using FluentResults;
using eAgenda.WebApp.Modulos.ModuloContato.Dominio;

namespace eAgenda.WebApp.Modulos.ModuloContato.Aplicacao;

public class ServicoContato
{
    private readonly IRepositorioContato repositorioContato;

    public ServicoContato(
        IRepositorioContato repositorioContato
    )
    {
        this.repositorioContato = repositorioContato;
    }

    public Result Cadastrar(CadastrarContatoDto dto)
    {
        if (ExisteContatoComMesmoEmail(dto.Email))
            return Falha(nameof(dto.Email), "Ja existe um contato com este email.");

        if (ExisteContatoComMesmoTelefone(dto.Telefone))
            return Falha(nameof(dto.Telefone), "Ja existe um contato com este telefone.");

        Contato novoContato = new Contato(
            dto.Nome,
            dto.Email,
            dto.Telefone,
            dto.Cargo,
            dto.Empresa
        );

        Result resultadoValidacao = ValidarEntidade(novoContato);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioContato.Cadastrar(novoContato);

        return Result.Ok();
    }

    public Result Editar(EditarContatoDto dto)
    {
        if (ExisteContatoComMesmoEmail(dto.Email, dto.Id))
            return Falha(nameof(dto.Email), "Ja existe um contato com este email.");

        if (ExisteContatoComMesmoTelefone(dto.Telefone, dto.Id))
            return Falha(nameof(dto.Telefone), "Ja existe um contato com este telefone.");

        Contato contatoAtualizado = new Contato(
            dto.Nome,
            dto.Email,
            dto.Telefone,
            dto.Cargo,
            dto.Empresa
        );

        Result resultadoValidacao = ValidarEntidade(contatoAtualizado);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        bool conseguiuEditar = repositorioContato.Editar(dto.Id, contatoAtualizado);

        if (!conseguiuEditar)
            return Falha(string.Empty, "Contato nao encontrado.");

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        Contato? contato = repositorioContato.SelecionarPorId(id);

        if (contato == null)
            return Falha(string.Empty, "Contato nao encontrado.");

        repositorioContato.Excluir(id);

        return Result.Ok();
    }

    public List<ListarContatosDto> SelecionarTodos()
    {
        return repositorioContato
            .SelecionarTodos()
            .Select(c => new ListarContatosDto(c.Id, c.Nome, c.Email, c.Telefone, c.Cargo, c.Empresa))
            .ToList();
    }

    public Result<DetalhesContatoDto> SelecionarPorId(Guid id)
    {
        Contato? contato = repositorioContato.SelecionarPorId(id);

        if (contato == null)
            return Result.Fail("Contato nao encontrado.");

        return Result.Ok(new DetalhesContatoDto(
            contato.Id,
            contato.Nome,
            contato.Email,
            contato.Telefone,
            contato.Cargo,
            contato.Empresa
        ));
    }

    private bool ExisteContatoComMesmoEmail(string email, Guid? idIgnorado = null)
    {
        string emailNormalizado = NormalizarEmail(email);

        return repositorioContato
            .SelecionarTodos()
            .Any(c =>
                c.Id != idIgnorado &&
                NormalizarEmail(c.Email) == emailNormalizado
            );
    }

    private bool ExisteContatoComMesmoTelefone(string telefone, Guid? idIgnorado = null)
    {
        string telefoneNormalizado = NormalizarTelefone(telefone);

        return repositorioContato
            .SelecionarTodos()
            .Any(c =>
                c.Id != idIgnorado &&
                NormalizarTelefone(c.Telefone) == telefoneNormalizado
            );
    }

    private static string NormalizarEmail(string email)
    {
        return email.Trim().ToLowerInvariant();
    }

    private static string NormalizarTelefone(string telefone)
    {
        return new string(telefone.Where(char.IsDigit).ToArray());
    }

    private static Result ValidarEntidade(Contato contato)
    {
        List<string> erros = contato.Validar();

        if (erros.Count == 0)
            return Result.Ok();

        return Falha(string.Empty, erros.First());
    }

    private static Result Falha(string campo, string mensagem)
    {
        return Result.Fail(new Error(mensagem).WithMetadata("Campo", campo));
    }
}
