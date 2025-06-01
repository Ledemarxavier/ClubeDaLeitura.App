using ClubeDaLeitura.App.Compartilhado;
using ClubeDaLeitura.App.ModuloAmigo;
using ClubeDaLeitura.App.ModuloCaixa;
using ClubeDaLeitura.App.ModuloEmprestimo;

namespace ClubeDaLeitura.App.ModuloRevista
{
    public class Revista : EntidadeBase
    {
        public string titulo;
        private int numeroEdicao;
        private int anoPublicacao;
        private string statusEmprestimo;
        private Caixa caixa;

        public Revista(string titulo, int numeroEdicao, int anoPublicacao, string statusEmprestimo, Caixa caixa)
        {
            this.titulo = titulo;
            this.numeroEdicao = numeroEdicao;
            this.anoPublicacao = anoPublicacao;
            this.statusEmprestimo = statusEmprestimo;
            this.caixa = caixa;
        }

        public override string Validar()
        {
            return "";
        }

        public override void AtualizarRegistro(EntidadeBase registroAtualizado)
        {
            Revista revistaAtualizada = (Revista)registroAtualizado;

            titulo = revistaAtualizada.titulo;
            numeroEdicao = revistaAtualizada.numeroEdicao;
            anoPublicacao = revistaAtualizada.anoPublicacao;
            statusEmprestimo = revistaAtualizada.statusEmprestimo;
            caixa = revistaAtualizada.caixa;
        }
    }
}