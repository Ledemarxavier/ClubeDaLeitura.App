using ClubeDaLeitura.App.Compartilhado;

namespace ClubeDaLeitura.App.ModuloAmigo
{
    public class Amigo : EntidadeBase
    {
        private string nome;
        private string responsavel;
        private string telefone;

        public Amigo(string nome, string responsavel, string telefone)
        {
            this.nome = nome;
            this.responsavel = responsavel;
            this.telefone = telefone;
        }

        public override string Validar()
        {
            string erros = "";

            if (string.IsNullOrWhiteSpace(nome))
                erros += "O nome é obrigatório!\n";
            else if (nome.Length < 2)
                erros += "O nome deve conter mais que 1 caractere!\n";

            if (string.IsNullOrWhiteSpace(responsavel))
                erros += "O nome do rensponsável é obrigatório!\n";
            else if (nome.Length < 2)
                erros += "O nome deve conter mais que 1 caractere!\n";

            if (string.IsNullOrWhiteSpace(telefone))
                erros += "O telefone é obrigatório!\n";
            else if (telefone.Length < 9)
                erros += "O telefone deve conter no mínimo 9 caracteres!\n";

            return erros;
        }

        private void ObterEmprestimos();
    }
}