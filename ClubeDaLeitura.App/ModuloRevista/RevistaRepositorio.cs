using ClubeDaLeitura.App.Compartilhado;
using ClubeDaLeitura.App.ModuloEmprestimo;
using Microsoft.Win32;

namespace ClubeDaLeitura.App.ModuloRevista
{
    public class RevistaRepositorio : BaseRepositorio
    {
        public List<EntidadeBase> SelecionarRevistasDisponiveis()
        {
            List<EntidadeBase> revistasDisponiveis = new List<EntidadeBase>();

            foreach (var revista in registros)
            {
                Revista revistaAtual = (Revista)revista;
                if (revistaAtual.status == StatusRevista.Disponivel)
                    revistasDisponiveis.Add(revista);
            }

            return revistasDisponiveis;
        }
    }
}