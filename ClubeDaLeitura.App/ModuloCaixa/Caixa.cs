using ClubeDaLeitura.App.Compartilhado;
using ClubeDaLeitura.App.ModuloRevista;

namespace ClubeDaLeitura.App.ModuloCaixa
{
    public class Caixa : EntidadeBase
    {
        public string etiqueta;
        public string cor;
        public int diasEmprestimo;
        public List<Revista> revistas = new List<Revista>();

        public Caixa(string etiqueta, string cor, int diasEmprestimo)
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
            else if (etiqueta.Length > 50)
                erros += "A etiqueta deve ter no máximo 50 caracteres!\n";

            if (string.IsNullOrWhiteSpace(cor))
                erros += "A cor é obrigatória!\n";

            if (diasEmprestimo >= 8)
                erros += "O nùmero de dias deve ser maior que 7!\n";

            return erros;
        }

        public override void AtualizarRegistro(EntidadeBase registroAtualizado)
        {
            Caixa caixaAtualizada = (Caixa)registroAtualizado;

            etiqueta = caixaAtualizada.etiqueta;
            cor = caixaAtualizada.cor;
            diasEmprestimo = caixaAtualizada.diasEmprestimo;
        }

        public bool ExistemEmprestimosParaCaixa(int idCaixa)
        {
            foreach (Revista revista in revistas)
            {
                if (revista.caixa != null && revista.caixa.id == idCaixa)
                {
                    return true;
                }
            }
            return false;
        }

        public override string ToString()
        {
            return $"ID: {id} | Etiqueta: {etiqueta} | Cor: {cor} | Dias Empréstimo: {diasEmprestimo}";
        }
    }
}