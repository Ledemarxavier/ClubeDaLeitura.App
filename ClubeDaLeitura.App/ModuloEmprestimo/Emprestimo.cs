using ClubeDaLeitura.App.Compartilhado;
using ClubeDaLeitura.App.ModuloAmigo;
using ClubeDaLeitura.App.ModuloRevista;

namespace ClubeDaLeitura.App.ModuloEmprestimo
{
    public class Emprestimo : EntidadeBase
    {
        public Amigo amigo;
        public Revista revista;
        public DateTime data;
        private string status;

        public Emprestimo(Amigo amigo, Revista revista, DateTime data, string status)
        {
            this.amigo = amigo;
            this.revista = revista;
            this.data = data;
            this.status = status;
        }

        public override void AtualizarRegistro(EntidadeBase registroAtualizado)
        {
            Emprestimo emprestimoAtualizado = (Emprestimo)registroAtualizado;

            amigo = emprestimoAtualizado.amigo;
            revista = emprestimoAtualizado.revista;
            data = emprestimoAtualizado.data;
        }

        public override string ToString()
        {
            return $"ID: {id} | Nome: {amigo} | Resvista: {revista} | Data: {data} | Status: {status}";
        }

        public override string Validar()
        {
            string erros = "";

            return erros;
        }
    }
}