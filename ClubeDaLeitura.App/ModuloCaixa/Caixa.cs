using ClubeDaLeitura.App.Compartilhado;
using ClubeDaLeitura.App.ModuloAmigo;

namespace ClubeDaLeitura.App.ModuloCaixa
{
    public class Caixa : EntidadeBase
    {
        private string etiqueta;
        private int cor;
        public int diasEmprestimo;

        public Caixa(string etiqueta, int cor, int diasEmprestimo)
        {
            this.etiqueta = etiqueta;
            this.cor = cor;
            this.diasEmprestimo = diasEmprestimo;
        }

        public override string Validar()
        {
            string erros = "";

            if (string.IsNullOrWhiteSpace(etiqueta))
                erros += "A etiqueta é obrigatória!\n";
            if (cor == default)
                erros += "O telefone é obrigatório!\n";
            else if (cor < 3)

                erros += "A cor deve conter no mínimo 3 caracteres!\n";

            return erros;
        }

        public override void AtualizarRegistro(EntidadeBase registroAtualizado)
        {
            Caixa caixaAtualizada = (Caixa)registroAtualizado;

            etiqueta = caixaAtualizada.etiqueta;
            cor = caixaAtualizada.cor;
            diasEmprestimo = caixaAtualizada.diasEmprestimo;
        }

        /* public void AdicionarRevista();

         public void RemoverRevista();*/
    }
}