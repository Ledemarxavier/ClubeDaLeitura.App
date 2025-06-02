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

            if (amigos.Count == 0)
            {
                Console.WriteLine("Nenhum amigo cadastrado!");
                Console.ReadLine();
                return null;
            }

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

            if (amigoSelecionado.ExistemEmprestimosParaAmigo(idAmigo))
            {
                Console.WriteLine("Este amigo já possui um empréstimo ativo!");
                Console.ReadLine();
                return null;
            }

            Console.WriteLine("\nSelecione uma revista disponível:");
            List<Revista> revistasDisponiveis = revistaRepositorio.SelecionarRevistasDisponiveis();

            if (revistasDisponiveis.Count == 0)
            {
                Console.WriteLine("Nenhuma revista disponível!");
                Console.ReadLine();
                return null;
            }

            foreach (var revista in revistasDisponiveis)
            {
                Console.WriteLine(revista.ToString());
            }

            Console.Write("\nDigite o ID da revista: ");
            int idRevista = Convert.ToInt32(Console.ReadLine());

            Revista revistaSelecionada = (Revista)revistaRepositorio.SelecionarRegistroPorId(idRevista);

            if (revistaSelecionada == null || revistaSelecionada.status != StatusRevista.Disponivel)
            {
                Console.WriteLine("Revista não disponível para empréstimo!");
                Console.ReadLine();
                return null;
            }

            int diasEmprestimo = revistaSelecionada.caixa.diasEmprestimo;
            DateTime dataEmprestimo = DateTime.Now;

            return new Emprestimo(amigoSelecionado, revistaSelecionada, dataEmprestimo, diasEmprestimo);
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
    }
}