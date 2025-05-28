using ClubeDaLeitura.App.Compartilhado;
using ClubeDaLeitura.App.ModuloAmigo;
using ClubeDaLeitura.App.ModuloRevista;

namespace ClubeDaLeitura.App.ModuloEmprestimo
{
    public class Emprestimo : EntidadeBase
    {
        private Amigo amigo;
        private Revista revista;
        private DateTime data;
        private string status;

        public Emprestimo(Amigo amigo, Revista revista, DateTime data, string status)
        {
            this.amigo = amigo;
            this.revista = revista;
            this.data = data;
            this.status = status;
        }

        public override string Validar()
        {
            string erros = "";

            return erros;
        }
    }
}