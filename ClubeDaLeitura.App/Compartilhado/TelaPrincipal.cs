using ClubeDaLeitura.App.ModuloAmigo;
using ClubeDaLeitura.App.ModuloCaixa;
using ClubeDaLeitura.App.ModuloEmprestimo;
using ClubeDaLeitura.App.ModuloRevista;
using ClubeDaLeitura.App.ModuloTela;
using ClubeDaLeitura.App.ModuloTelas;

namespace ClubeDaLeitura.App.Compartilhado
{
    public class TelaPrincipal
    {
        private AmigoRepositorio amigoRepositorio = new();
        private EmprestimoRepositorio emprestimoRepositorio = new();
        private RevistaRepositorio revistaRepositorio = new();
        private CaixaRepositorio caixaRepositorio = new();

        public void menuPrincipal()
        {
            TelaAmigo telaAmigo = new TelaAmigo(amigoRepositorio, emprestimoRepositorio);
            TelaEmprestimo telaEmprestimo = new TelaEmprestimo(emprestimoRepositorio, revistaRepositorio, amigoRepositorio);
            TelaCaixa telaCaixa = new TelaCaixa(caixaRepositorio);
            TelaRevista telaRevista = new TelaRevista(revistaRepositorio, caixaRepositorio);
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Sistema de Gestão do Clube da Leitura");
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine("1. Gerenciar Amigos");
                Console.WriteLine("2. Gerenciar Caixas");
                Console.WriteLine("3. Gerenciar Emprestimos");
                Console.WriteLine("4. Gerenciar Revistas");
                Console.WriteLine("0. Sair");
                Console.Write("Opção: ");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        telaAmigo.Menu();
                        break;

                    case "2":
                        telaCaixa.Menu();
                        break;

                    case "3":
                        telaEmprestimo.Menu();
                        break;

                    case "4":
                        telaRevista.Menu();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Opção inválida!");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
}