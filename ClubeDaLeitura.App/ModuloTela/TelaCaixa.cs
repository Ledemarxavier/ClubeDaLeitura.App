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
            string etiqueta = Console.ReadLine().ToUpper();

            Console.Write("Digite a cor da caixa: ");
            string cor = Console.ReadLine();

            Console.Write("Digite os dias de empréstimo (Max. 7): ");
            int diasEmprestimo = Convert.ToInt32(Console.ReadLine());

            if (diasEmprestimo <= 0)
                diasEmprestimo = 7;

            return new Caixa(etiqueta, cor, diasEmprestimo);
        }

        public override void CadastrarRegistro()
        {
            Console.Clear();
            Console.WriteLine($"Cadastro de {nomeEntidade}");
            Console.WriteLine();

            Caixa novoRegistro = (Caixa)ObterDados();

            string erros = novoRegistro.Validar();

            if (erros.Length > 0)
            {
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(erros);
                Console.ResetColor();

                Console.Write("\nDigite ENTER para continuar...");
                Console.ReadLine();

                CadastrarRegistro();
                return;
            }

            List<EntidadeBase> caixas = revistaRepositorio.SelecionarRegistros();

            foreach (EntidadeBase entidade in caixas)
            {
                Caixa caixa = (Caixa)entidade;

                if (caixa == null)
                    continue;

                if (caixa.etiqueta == novoRegistro.etiqueta)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Uma Caixa com esta etiqueta já foi cadastrada!");
                    Console.ResetColor();

                    Console.Write("\nDigite ENTER para continuar...");
                    Console.ReadLine();

                    CadastrarRegistro();
                    return;
                }
            }

            revistaRepositorio.CadastrarRegistro(novoRegistro);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n{nomeEntidade} cadastrado com sucesso!");
            Console.ResetColor();

            Console.Write("\nDigite ENTER para continuar...");
            Console.ReadLine();
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

            Console.Write($"\nDigite o ID do {nomeEntidade} a ser excluída: ");
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