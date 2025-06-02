using System;
using ClubeDaLeitura.App.Compartilhado;
using ClubeDaLeitura.App.ModuloAmigo;

namespace ClubeDaLeitura.App.ModuloCaixa
{
    public class TelaCaixa : TelaBase
    {
        private CaixaRepositorio caixaRepositorio;

        public TelaCaixa(CaixaRepositorio caixaRepositorio) : base("Caixas", caixaRepositorio)
        {
            this.caixaRepositorio = caixaRepositorio;
        }

        protected override Caixa ObterDados()
        {
            Console.Write("Digite a etiqueta da caixa: ");
            string etiqueta = Console.ReadLine();

            Console.Write("Digite a cor da caixa: ");
            string cor = Console.ReadLine();

            Console.Write("Digite os dias de empréstimo (Max. 7): ");
            int diasEmprestimo = Convert.ToInt32(Console.ReadLine());

            if (diasEmprestimo <= 0)
                diasEmprestimo = 7;

            return new Caixa(etiqueta, cor, diasEmprestimo);
        }

        public override bool ListarRegistros()
        {
            List<EntidadeBase> caixas = caixaRepositorio.SelecionarRegistros();

            if (caixas.Count == 0)
            {
                Console.WriteLine("Nenhuma caixa cadastrada!");
                Console.ReadLine();
                return false;
            }

            Console.WriteLine("\nLista de Caixas");
            Console.WriteLine("----------------");

            foreach (Caixa caixa in caixas)
            {
                Console.WriteLine(caixa.ToString());
            }

            Console.WriteLine("\nDigite ENTER para continuar...");
            Console.ReadLine();

            return true;
        }

        public override void ExcluirRegistro()
        {
            Console.Clear();
            Console.WriteLine($"Exclusão de caixa");
            Console.WriteLine("-----------------------");

            if (!ListarRegistros())
                return;

            Console.Write($"\nDigite o ID da caixa a ser excluída: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            Caixa caixa = (Caixa)caixaRepositorio.SelecionarRegistroPorId(idSelecionado);

            if (caixa == null)
            {
                Console.WriteLine("Caixa não encontrada!");
                Console.ReadLine();
                return;
            }

            if (caixa.ExistemEmprestimosParaCaixa(idSelecionado))
            {
                Console.WriteLine("Não é possível excluir uma caixa com empréstimos vinculados!");
                Console.ReadLine();
                return;
            }

            base.ExcluirRegistro();
        }
    }
}