using ClubeDaLeitura.App.Compartilhado;
using ClubeDaLeitura.App.ModuloAmigo;
using ClubeDaLeitura.App.ModuloRevista;

namespace ClubeDaLeitura.App.ModuloEmprestimo
{
    public enum StatusEmprestimo
    {
        Aberto,
        Concluido,
        Atrasado
    }

    public class Emprestimo : EntidadeBase
    {
        public Amigo amigo;
        public Revista revista;
        public DateTime dataEmprestimo;
        public DateTime dataDevolucao;
        public StatusEmprestimo status;

        public Emprestimo(Amigo amigo, Revista revista, DateTime dataEmprestimo, int diasEmprestimo)
        {
            this.amigo = amigo;
            this.revista = revista;
            this.dataEmprestimo = dataEmprestimo;
            this.dataDevolucao = dataEmprestimo.AddDays(diasEmprestimo);
            this.status = StatusEmprestimo.Aberto;
        }

        public override string Validar()
        {
            string erros = "";

            if (amigo == null)
                erros += "O amigo é obrigatório!\n";

            if (revista == null)
                erros += "A revista é obrigatória!\n";
            else if (revista.status != StatusRevista.Disponivel)
                erros += "A revista não está disponível para empréstimo!\n";

            return erros;
        }

        public override void AtualizarRegistro(EntidadeBase registroAtualizado)
        {
            Emprestimo emprestimoAtualizado = (Emprestimo)registroAtualizado;

            amigo = emprestimoAtualizado.amigo;
            revista = emprestimoAtualizado.revista;
            dataEmprestimo = emprestimoAtualizado.dataEmprestimo;
            dataDevolucao = emprestimoAtualizado.dataDevolucao;
        }

        public override string ToString()
        {
            return $"ID: {id} | Nome: {amigo} | Resvista: {revista} | Data Emprestimo: {dataEmprestimo} | Data Devolução: {dataDevolucao} | Status: {status}";
        }
    }
}