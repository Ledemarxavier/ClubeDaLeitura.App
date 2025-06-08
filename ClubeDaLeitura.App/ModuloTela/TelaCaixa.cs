using System;
using ClubeDaLeitura.App.Compartilhado;
using ClubeDaLeitura.App.ModuloAmigo;
using ClubeDaLeitura.App.ModuloEmprestimo;
using ClubeDaLeitura.App.ModuloRevista;

namespace ClubeDaLeitura.App.ModuloCaixa
{
    public class TelaCaixa : TelaBase
    {
        private CaixaRepositorio caixaRepositorio;
        private RevistaRepositorio revistaRepositorio;

        public TelaCaixa(CaixaRepositorio caixaRepositorio, RevistaRepositorio revistaRepositorio) : base("Caixas", caixaRepositorio)
        {
            this.caixaRepositorio = caixaRepositorio;
            this.revistaRepositorio = revistaRepositorio;
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

            if (caixas.Count < 0)
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

        public override bool ExcluirRegistro()
        {
            Console.Clear();
            Console.WriteLine($"Exclusão de {nomeEntidade}");
            Console.WriteLine("-----------------------");

            if (!ListarRegistros())
                return false;

            Console.Write($"\nDigite o ID do {nomeEntidade} a ser excluído: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            List<EntidadeBase> revistas = revistaRepositorio.SelecionarRegistros();

            foreach (var entidade in revistas)
            {
                Revista revista = (Revista)entidade;

                if (revista == null)
                    continue;

                if (revista.caixa != null && revista.caixa.id == idSelecionado)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("A Caixa não pode ser excluída enquanto houver pendências!");
                    Console.ResetColor();

                    Console.Write("\nDigite ENTER para continuar...");
                    Console.ReadLine();

                    return false;
                }
            }

            caixaRepositorio.ExcluirRegistro(idSelecionado);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n{nomeEntidade} excluída com sucesso!");
            Console.ResetColor();

            Console.Write("\nDigite ENTER para continuar...");
            Console.ReadLine();

            return true;
        }
    }
}