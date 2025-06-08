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
            string titulo = Console.ReadLine().ToUpper();

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

            return new Revista(titulo, numeroEdicao, anoPublicacao, caixaSelecionada);
        }

        public override void CadastrarRegistro()
        {
            Console.Clear();
            Console.WriteLine($"Cadastro de {nomeEntidade}");
            Console.WriteLine();

            Revista novoRegistro = (Revista)ObterDados();

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

            List<EntidadeBase> revistas = revistaRepositorio.SelecionarRegistros();

            foreach (EntidadeBase entidade in revistas)
            {
                Revista revista = (Revista)entidade;

                if (revista == null)
                    continue;

                if (revista.titulo == novoRegistro.titulo && revista.numeroEdicao == novoRegistro.numeroEdicao)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Uma revista com este título ou edição já foi cadastrada!");
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

            foreach (Revista revista in revistas)
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

            return true;
        }
    }
}