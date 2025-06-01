using ClubeDaLeitura.App.Compartilhado;
using ClubeDaLeitura.App.ModuloAmigo;
using ClubeDaLeitura.App.ModuloEmprestimo;

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

            return new Amigo(nome, responsavel, telefone);
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

            if (!ListarRegistros())
                return false;

            Console.Write("\nDigite o ID do amigo para ver os empréstimos: ");
            int idAmigo = Convert.ToInt32(Console.ReadLine());

            Amigo amigoSelecionado = (Amigo)emprestimoRepositorio.SelecionarRegistroPorId(idAmigo);

            Console.WriteLine($"\nEmpréstimos do amigo: {amigoSelecionado.nome}");

            Console.WriteLine(amigoSelecionado.ObterEmprestimo());
            return true;

            Console.ReadLine();
        }

        public override void ExcluirRegistro()
        {
            Console.Clear();
            Console.WriteLine($"Exclusão de amigo");
            Console.WriteLine("-----------------------");

            if (!ListarRegistros())
                return;

            Console.Write($"\nDigite o ID do amigo a ser excluído: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            Amigo amigo = (Amigo)amigoRepositorio.SelecionarRegistroPorId(idSelecionado);

            if (amigo == null)
            {
                Console.WriteLine("Amigo não encontrado!");
                Console.ReadLine();
                return;
            }

            if (amigo.ExistemEmprestimosParaAmigo(idSelecionado))
            {
                Console.WriteLine("Não é possível excluir um amigo com empréstimos vinculados!");
                Console.ReadLine();
                return;
            }

            base.ExcluirRegistro();
        }
    }
}