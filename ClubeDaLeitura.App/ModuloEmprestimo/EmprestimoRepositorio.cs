using ClubeDaLeitura.App.Compartilhado;
using Microsoft.Win32;

namespace ClubeDaLeitura.App.ModuloEmprestimo;

public class EmprestimoRepositorio : BaseRepositorio
{
    public List<EntidadeBase> SelecionarEmprestimosAtivos()
    {
        List<EntidadeBase> emprestimosAtivos = new List<EntidadeBase>();

        foreach (var registro in registros)
        {
            Emprestimo emprestimoAtual = (Emprestimo)registro;

            if (emprestimoAtual == null)
                continue;

            if (emprestimoAtual.status == StatusEmprestimo.Aberto || emprestimoAtual.status == StatusEmprestimo.Atrasado)
                emprestimosAtivos.Add(emprestimoAtual);
        }

        return emprestimosAtivos;
    }

    public List<EntidadeBase> SelecionarEmprestimosConcluidos()
    {
        List<EntidadeBase> emprestimosConcluidos = new List<EntidadeBase>();

        foreach (var registro in registros)
        {
            Emprestimo emprestimoAtual = (Emprestimo)registro;

            if (emprestimoAtual == null)
                continue;

            if (emprestimoAtual.status == StatusEmprestimo.Concluido)
                emprestimosConcluidos.Add(emprestimoAtual);
        }

        return emprestimosConcluidos;
    }
}