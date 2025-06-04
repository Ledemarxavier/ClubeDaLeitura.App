using ClubeDaLeitura.App.Compartilhado;
using ClubeDaLeitura.App.ModuloAmigo;
using ClubeDaLeitura.App.ModuloCaixa;
using ClubeDaLeitura.App.ModuloEmprestimo;
using ClubeDaLeitura.App.ModuloRevista;
using Microsoft.Win32;

namespace ClubeDaLeitura.App.ModuloTelas
{
    public class TelaRevista : TelaBase
    {
        private RevistaRepositorio revistaRepositorio;
        private CaixaRepositorio caixaRepositorio;

        public TelaRevista(RevistaRepositorio revistaRepositorio, CaixaRepositorio caixaRepositorio) : base("Revistas", revistaRepositorio)

        {
            this.revistaRepositorio = revistaRepositorio;
            this.caixaRepositorio = caixaRepositorio;
        }

        protected override EntidadeBase ObterDados()
        {
            Console.Write("Digite o título da revista: ");
            string titulo = Console.ReadLine();

            Console.Write("Digite o número da edição: ");
            int numeroEdicao = Convert.ToInt32(Console.ReadLine());

            Console.Write("Digite o ano de publicação: ");
            int anoPublicacao = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("\nSelecione uma caixa:");
            List<EntidadeBase> caixas = caixaRepositorio.SelecionarRegistros();

            if (caixas.Count == 0)
            {
                Console.WriteLine("Nenhuma caixa cadastrada!");
                Console.ReadLine();
                return null;
            }

            foreach (var caixa in caixas)
            {
                Console.WriteLine(caixa.ToString());
            }

            Console.Write("\nDigite o ID da caixa: ");
            int idCaixa = Convert.ToInt32(Console.ReadLine());

            Caixa caixaSelecionada = (Caixa)caixaRepositorio.SelecionarRegistroPorId(idCaixa);

            if (caixaSelecionada == null)
            {
                Console.WriteLine("Caixa não encontrada!");
                Console.ReadLine();
                return null;
            }

            if (VerificarRevistaExistente(titulo, numeroEdicao))
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Já existe uma revista com este título e número de edição cadastrado!");
                Console.ResetColor();
                Console.ReadLine();
                return null;
            }

            return new Revista(titulo, numeroEdicao, anoPublicacao, caixaSelecionada);
        }

        public bool VerificarRevistaExistente(string titulo, int numeroEdicao)
        {
            List<EntidadeBase> revistas = revistaRepositorio.SelecionarRegistros();
            foreach (Revista revista in revistas)
            {
                if (revista.titulo == titulo && revista.numeroEdicao == numeroEdicao)
                    return true;
            }
            return false;
        }

        public override bool ListarRegistros()
        {
            Console.Clear();
            Console.WriteLine("Lista de Revistas");
            Console.WriteLine("-----------------");

            List<EntidadeBase> revistas = revistaRepositorio.SelecionarRegistros();

            if (revistas.Count == 0)
            {
                Console.WriteLine("Nenhuma revista cadastrada!");
                Console.ReadLine();
                return false;
            }

            foreach (var revista in revistas)
            {
                Console.WriteLine(revista.ToString());
            }

            Console.ReadLine();
            return true;
        }

        public override bool ExcluirRegistro()
        {
            Console.Clear();
            Console.WriteLine($"Exclusão de revista");
            Console.WriteLine("-----------------------");

            if (!ListarRegistros())
                return false;

            Console.Write($"\nDigite o ID da revista a ser excluída: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            Revista revista = (Revista)revistaRepositorio.SelecionarRegistroPorId(idSelecionado);

            if (revista == null)
                return false;

            if (revista.status != StatusRevista.Disponivel)
            {
                Console.WriteLine("Não é possível excluir uma revista que está emprestada ou reservada!");
                Console.ReadLine();

                return false;
            }

            revistaRepositorio.ExcluirRegistro(idSelecionado);
            return true;
        }
    }
}