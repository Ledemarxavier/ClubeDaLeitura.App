using ClubeDaLeitura.App.Compartilhado;
using ClubeDaLeitura.App.ModuloAmigo;
using ClubeDaLeitura.App.ModuloEmprestimo;
using Microsoft.Win32;

namespace ClubeDaLeitura.App.ModuloTelas
{
    public class TelaAmigo : TelaBase
    {
        private AmigoRepositorio amigoRepositorio;
        private EmprestimoRepositorio emprestimoRepositorio;

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

            if (VerificarAmigoExistente(nome, telefone))
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Já existe um amigo com este nome e telefone cadastrado!");
                Console.ResetColor();
                Console.ReadLine();
                return null;
            }

            return new Amigo(nome, responsavel, telefone);
        }

        public bool VerificarAmigoExistente(string nome, string telefone)
        {
            List<EntidadeBase> amigos = amigoRepositorio.SelecionarRegistros();
            foreach (Amigo amigo in amigos)
            {
                if (amigo.nome == nome && amigo.telefone == telefone)
                    return true;
            }
            return false;
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

        public bool VisualizarEmprestimosAmigo()
        {
            Console.Clear();
            Console.WriteLine("Visualização de Empréstimos por Amigo");
            Console.WriteLine("-------------------------------------");

            List<EntidadeBase> amigos = amigoRepositorio.SelecionarRegistros();

            if (amigos.Count == 0)
            {
                Console.WriteLine("Nenhum amigo cadastrado!");
                Console.ReadLine();
                return false;
            }

            foreach (var amigo in amigos)
            {
                Console.WriteLine(amigo.ToString());
            }

            Console.Write("\nDigite o ID do amigo para ver os empréstimos: ");
            int idAmigo = Convert.ToInt32(Console.ReadLine());

            Amigo amigoSelecionado = (Amigo)amigoRepositorio.SelecionarRegistroPorId(idAmigo);

            if (amigoSelecionado == null)
            {
                Console.WriteLine("Amigo não encontrado!");
                Console.ReadLine();
                return false;
            }

            Console.WriteLine($"\nEmpréstimos do amigo: {amigoSelecionado.nome}");

            if (amigoSelecionado.emprestimos.Count == 0)
                Console.WriteLine("Nenhum empréstimo registrado.");
            else
                foreach (var emprestimo in amigoSelecionado.emprestimos)
                    Console.WriteLine(emprestimo.ToString());

            Console.ReadLine();
            return true;
        }

        public override bool ExcluirRegistro()
        {
            Console.Clear();
            Console.WriteLine($"Exclusão de amigo");
            Console.WriteLine("-----------------------");

            if (!ListarRegistros())
                return false;

            Console.Write($"\nDigite o ID do amigo a ser excluído: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            Amigo amigo = (Amigo)amigoRepositorio.SelecionarRegistroPorId(idSelecionado);

            if (amigo == null)
            {
                Console.WriteLine("Amigo não encontrado!");
                Console.ReadLine();
                return false;
            }

            if (amigo.ExistemEmprestimosParaAmigo(idSelecionado))
            {
                Console.WriteLine("Não é possível excluir um amigo com empréstimos vinculados!");
                Console.ReadLine();
                return false;
            }

            amigoRepositorio.ExcluirRegistro(idSelecionado);
            return true;
        }

        public override void Menu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Gerenciamento de amigos");
                Console.WriteLine("-----------------------------");
                Console.WriteLine($"1. Cadastrar amigo");
                Console.WriteLine($"2. Listar amigos");
                Console.WriteLine($"3. Editar amigo");
                Console.WriteLine($"4. Excluir amigo");
                Console.WriteLine($"5. Visualizar empréstimos do amigo");
                Console.WriteLine("0. Voltar");
                Console.Write("Opção: ");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        CadastrarRegistro();
                        break;

                    case "2":
                        ListarRegistros();
                        break;

                    case "3":
                        AtualizarRegistro();
                        break;

                    case "4":
                        ExcluirRegistro();
                        break;

                    case "5":
                        VisualizarEmprestimosAmigo();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Opção inválida.");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
}