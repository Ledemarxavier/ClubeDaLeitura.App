using ClubeDaLeitura.App.Compartilhado;
using ClubeDaLeitura.App.ModuloAmigo;
using ClubeDaLeitura.App.ModuloEmprestimo;
using ClubeDaLeitura.App.ModuloRevista;
using Microsoft.Win32;

namespace ClubeDaLeitura.App.ModuloTelas
{
    public class TelaAmigo : TelaBase
    {
        private AmigoRepositorio amigoRepositorio;
        private EmprestimoRepositorio emprestimoRepositorio;
        private List<EntidadeBase> amigos;

        public TelaAmigo(AmigoRepositorio amigoRepositorio, EmprestimoRepositorio emprestimoRepositorio) : base("Amigos", amigoRepositorio)

        {
            this.amigoRepositorio = amigoRepositorio;
            this.emprestimoRepositorio = emprestimoRepositorio;
        }

        protected override Amigo ObterDados()
        {
            Console.Write("Digite o nome do amigo: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o nome do responsável: ");
            string responsavel = Console.ReadLine();

            Console.Write("Digite o telefone no formato (XX) XXXX-XXXX ou (XX) XXXXX-XXXX: ");
            string telefone = Console.ReadLine();

            return new Amigo(nome, responsavel, telefone);
        }

        public override void CadastrarRegistro()
        {
            Console.Clear();
            Console.WriteLine($"Cadastro de {nomeEntidade}");
            Console.WriteLine();

            Amigo novoRegistro = (Amigo)ObterDados();

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

            List<EntidadeBase> amigos = amigoRepositorio.SelecionarRegistros();

            foreach (EntidadeBase entidade in amigos)
            {
                Amigo amigo = (Amigo)entidade;

                if (amigo == null)
                    continue;

                if (amigo.nome == novoRegistro.nome || amigo.telefone == novoRegistro.telefone)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Um amigo com este nome ou telefone já foi cadastrado!");
                    Console.ResetColor();

                    Console.Write("\nDigite ENTER para continuar...");
                    Console.ReadLine();

                    CadastrarRegistro();
                    return;
                }
            }

            amigoRepositorio.CadastrarRegistro(novoRegistro);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n{nomeEntidade} cadastrado com sucesso!");
            Console.ResetColor();

            Console.Write("\nDigite ENTER para continuar...");
            Console.ReadLine();
        }

        public override bool ListarRegistros()
        {
            List<EntidadeBase> amigos = amigoRepositorio.SelecionarRegistros();

            if (amigos.Count == 0)
            {
                Console.WriteLine("Nenhum amigo cadastrado!");
                Console.ReadLine();
                return false;
            }

            Console.WriteLine("\nLista de Amigos");
            Console.WriteLine("----------------");

            foreach (Amigo amigo in amigos)
            {
                Console.WriteLine(amigo.ToString());
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

            List<EntidadeBase> emprestimos = emprestimoRepositorio.SelecionarRegistros();

            foreach (var entidade in emprestimos)
            {
                Emprestimo emprestimo = (Emprestimo)entidade;

                if (emprestimo == null)
                    continue;

                if (emprestimo.amigo != null && emprestimo.amigo.id == idSelecionado)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("O amigo não pode ser excluído enquanto houver pendências!");
                    Console.ResetColor();

                    Console.Write("\nDigite ENTER para continuar...");
                    Console.ReadLine();

                    return false;
                }
            }

            amigoRepositorio.ExcluirRegistro(idSelecionado);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n{nomeEntidade} excluído com sucesso!");
            Console.ResetColor();

            Console.Write("\nDigite ENTER para continuar...");
            Console.ReadLine();

            return true;
        }
    }
}