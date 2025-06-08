using ClubeDaLeitura.App.Compartilhado;
using ClubeDaLeitura.App.ModuloEmprestimo;
using Microsoft.Win32;

namespace ClubeDaLeitura.App.ModuloAmigo
{
    public class Amigo : EntidadeBase
    {
        public string nome;
        public string responsavel;
        public string telefone;
        public List<Emprestimo> emprestimos = new List<Emprestimo>();

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
            else if (nome.Length < 3 || nome.Length > 100)
                erros += "O nome deve conter entre 3 a 100 caracteres!\n";

            if (string.IsNullOrWhiteSpace(responsavel))
                erros += "O nome do rensponsável é obrigatório!\n";
            else if (responsavel.Length < 3 || responsavel.Length > 100)
                erros += "O nome deve conter entre 3 a 100 caracteres!\n";

            if (string.IsNullOrWhiteSpace(telefone))
                erros += "O telefone é obrigatório!\n";
            else if (!ValidarTelefone(telefone))
                erros += "O telefone deve estar no formato (XX) XXXX-XXXX ou (XX) XXXXX-XXXX!\n";

            return erros;
        }

        public override void AtualizarRegistro(EntidadeBase registroAtualizado)
        {
            Amigo amigoAtualizado = (Amigo)registroAtualizado;

            nome = amigoAtualizado.nome;
            responsavel = amigoAtualizado.responsavel;
            telefone = amigoAtualizado.telefone;
        }

        public bool ValidarTelefone(string telefone)
        {
            var regex = new System.Text.RegularExpressions.Regex(@"^\(\d{2}\) \d{4,5}-\d{4}$");
            return regex.IsMatch(telefone);
        }

        public override string ToString()
        {
            return $"ID: {id} | Nome: {nome} | Responsável: {responsavel} | Telefone: {telefone}";
        }
    }
}