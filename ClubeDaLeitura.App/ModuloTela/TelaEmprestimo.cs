using ClubeDaLeitura.App.Compartilhado;
using ClubeDaLeitura.App.ModuloAmigo;

using ClubeDaLeitura.App.ModuloEmprestimo;
using ClubeDaLeitura.App.ModuloRevista;

namespace ClubeDaLeitura.App.ModuloTela
{
    public class TelaEmprestimo : TelaBase
    {
        private EmprestimoRepositorio emprestimoRepositorio;
        private RevistaRepositorio revistaRepositorio;
        private AmigoRepositorio amigoRepositorio;

        public TelaEmprestimo(EmprestimoRepositorio emprestimoRepositorio, RevistaRepositorio revistaRepositorio, AmigoRepositorio amigoRepositorio) : base("Emprestimos", emprestimoRepositorio)

        {
            this.emprestimoRepositorio = emprestimoRepositorio;
            this.revistaRepositorio = revistaRepositorio;
            this.amigoRepositorio = amigoRepositorio;
        }

        protected override Emprestimo ObterDados()
        {
            Console.WriteLine("\nSelecione um amigo:");
            List<EntidadeBase> amigos = amigoRepositorio.SelecionarRegistros();

            foreach (var amigo in amigos)
            {
                Console.WriteLine(amigo.ToString());
            }

            Console.Write("\nDigite o ID do amigo: ");
            int idAmigo = Convert.ToInt32(Console.ReadLine());

            Amigo amigoSelecionado = (Amigo)amigoRepositorio.SelecionarRegistroPorId(idAmigo);

            if (amigoSelecionado == null)
            {
                Console.WriteLine("Amigo não encontrado!");
                Console.ReadLine();
                return null;
            }

            Console.WriteLine("\nSelecione uma revista: ");
            List<EntidadeBase> revistas = revistaRepositorio.SelecionarRevistasDisponiveis();

            foreach (var revista in revistas)
            {
                Console.WriteLine(revista.ToString());
            }
            Console.Write("\nDigite o ID da revista: ");
            int idRevista = Convert.ToInt32(Console.ReadLine());

            Revista revistaSelecionada = (Revista)revistaRepositorio.SelecionarRegistroPorId(idRevista);

            int diasEmprestimo = revistaSelecionada.caixa.diasEmprestimo;
            DateTime dataEmprestimo = DateTime.Now;

            return new Emprestimo(amigoSelecionado, revistaSelecionada, dataEmprestimo, diasEmprestimo);
        }

        public override void CadastrarRegistro()
        {
            Console.Clear();
            Console.WriteLine($"Cadastro de {nomeEntidade}");
            Console.WriteLine();

            Emprestimo novoRegistro = (Emprestimo)ObterDados();

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

            var emprestimos = emprestimoRepositorio.SelecionarEmprestimosAtivos();

            foreach (EntidadeBase entidade in emprestimos)
            {
                Emprestimo emprestimo = (Emprestimo)entidade;

                if (emprestimo == null)
                    continue;

                if (novoRegistro.amigo.id == emprestimo.amigo.id)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("O amigo já esta com um empréstimo ativo!");
                    Console.ResetColor();

                    Console.Write("\nDigite ENTER para continuar...");
                    Console.ReadLine();

                    CadastrarRegistro();
                    return;
                }
            }
            Revista revista = novoRegistro.revista;
            revista.Emprestar();
            revistaRepositorio.EditarRegistro(revista.id, revista);

            emprestimoRepositorio.CadastrarRegistro(novoRegistro);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n{nomeEntidade} cadastrado com sucesso!");
            Console.ResetColor();

            Console.Write("\nDigite ENTER para continuar...");
            Console.ReadLine();
        }

        public bool VisualizarEmprestimosAtivos()
        {
            Console.Clear();
            Console.WriteLine("Visualização de Empréstimos Ativos");
            Console.WriteLine("-------------------------------------");

            List<EntidadeBase> emprestimos = emprestimoRepositorio.SelecionarEmprestimosAtivos();

            if (emprestimos.Count < 0)
                Console.WriteLine("Nenhum empréstimo registrado.");
            else
                foreach (EntidadeBase entidade in emprestimos)
                {
                    Emprestimo emprestimo = (Emprestimo)entidade;
                    if (emprestimo.status == StatusEmprestimo.Atrasado)
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine(emprestimo.ToString());
                    Console.ResetColor();
                }

            Console.ReadLine();

            return true;
        }

        public override bool ListarRegistros()
        {
            List<EntidadeBase> emprestimos = emprestimoRepositorio.SelecionarRegistros();

            if (emprestimos.Count == 0)
            {
                Console.WriteLine("Nenhum emprestimo cadastrado!");
                Console.ReadLine();
                return false;
            }

            Console.WriteLine("\nLista de Emprestimos");
            Console.WriteLine("----------------");

            foreach (Emprestimo emprestimo in emprestimos)
            {
                Console.WriteLine(emprestimo.ToString());
            }

            Console.WriteLine("\nDigite ENTER para continuar...");
            Console.ReadLine();

            return true;
        }

        public override bool ExcluirRegistro()
        {
            Console.Clear();
            Console.WriteLine($"Exclusão de caixa");
            Console.WriteLine("-----------------------");

            if (!ListarRegistros())
                return false;

            Console.Write($"\nDigite o ID do empréstimo a ser excluído: ");

            int idSelecionado = Convert.ToInt32(Console.ReadLine());
            Emprestimo emprestimo = (Emprestimo)emprestimoRepositorio.SelecionarRegistroPorId(idSelecionado);

            if (emprestimo == null)
            {
                Console.WriteLine("empréstimo não encontrado!");
                Console.ReadLine();
                return false;
            }

            return true;
        }

        public override void Menu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Gerenciamento de {nomeEntidade}");
                Console.WriteLine("-----------------------------");
                Console.WriteLine($"1. Cadastrar {nomeEntidade}");
                Console.WriteLine($"2. Listar {nomeEntidade}");
                Console.WriteLine($"3. Editar {nomeEntidade}");
                Console.WriteLine($"4. Vizualizar empréstimos ativos");
                Console.WriteLine($"5. Excluir {nomeEntidade}");
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
                        VisualizarEmprestimosAtivos();
                        break;

                    case "5":
                        ExcluirRegistro();
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