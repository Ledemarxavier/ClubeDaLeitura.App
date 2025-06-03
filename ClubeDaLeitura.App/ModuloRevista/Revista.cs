using ClubeDaLeitura.App.Compartilhado;
using ClubeDaLeitura.App.ModuloAmigo;
using ClubeDaLeitura.App.ModuloCaixa;
using ClubeDaLeitura.App.ModuloEmprestimo;
using Microsoft.Win32;

namespace ClubeDaLeitura.App.ModuloRevista
{
    public enum StatusRevista
    {
        Disponivel,
        Emprestada,
        Reservada
    }

    public class Revista : EntidadeBase
    {
        public string titulo;
        public int numeroEdicao;
        public int anoPublicacao;
        public StatusRevista status;
        public Caixa caixa;

        public Revista(string titulo, int numeroEdicao, int anoPublicacao, Caixa caixa)
        {
            this.titulo = titulo;
            this.numeroEdicao = numeroEdicao;
            this.anoPublicacao = anoPublicacao;
            this.caixa = caixa;
            this.status = StatusRevista.Disponivel;
        }

        public override string Validar()
        {
            string erros = "";

            if (string.IsNullOrWhiteSpace(titulo))
                erros += "O título é obrigatório!\n";
            else if (titulo.Length < 2 || titulo.Length > 100)
                erros += "O título deve conter entre 2 a 100 caracteres!\n";

            if (numeroEdicao <= 0)
                erros += "O número da edição deve ser positivo!\n";

            if (anoPublicacao <= 0 || anoPublicacao > DateTime.Now.Year)
                erros += "O ano de publicação deve ser válido!\n";

            if (caixa == null)
                erros += "A caixa é obrigatória!\n";

            if (VerificarRevistaExistente(titulo, numeroEdicao))
                erros += "Já existe uma revista com este título e edição!\n";

            return erros;
        }

        public override void AtualizarRegistro(EntidadeBase registroAtualizado)
        {
            Revista revistaAtualizada = (Revista)registroAtualizado;

            titulo = revistaAtualizada.titulo;
            numeroEdicao = revistaAtualizada.numeroEdicao;
            anoPublicacao = revistaAtualizada.anoPublicacao;
            caixa = revistaAtualizada.caixa;
        }

        public bool VerificarRevistaExistente(string titulo, int numeroEdicao)
        {
            List<EntidadeBase> revistas = new List<EntidadeBase>();
            foreach (Revista revista in revistas)
            {
                if (revista.titulo == titulo && revista.numeroEdicao == numeroEdicao)
                    return true;
            }
            return false;
        }

        public void Emprestar()
        {
            status = StatusRevista.Emprestada;
        }

        public void Devolver()
        {
            status = StatusRevista.Disponivel;
        }

        public void Reservar()
        {
            status = StatusRevista.Reservada;
        }

        public override string ToString()
        {
            return $"ID: {id} | Título: {titulo} | Edição: {numeroEdicao} | Ano: {anoPublicacao} | Status: {status} | Caixa: {caixa.etiqueta}";
        }
    }
}