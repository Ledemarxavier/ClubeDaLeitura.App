using ClubeDaLeitura.App.Compartilhado;

namespace ClubeDaLeitura.App.ModuloRevista
{
    public class RevistaRepositorio : BaseRepositorio
    {
        public List<Revista> SelecionarRevistasDisponiveis()
        {
            List<Revista> revistasDisponiveis = new List<Revista>();

            foreach (Revista revista in revistasDisponiveis)
            {
                if (revista.status == StatusRevista.Disponivel)
                    revistasDisponiveis.Add(revista);
            }

            return revistasDisponiveis;
        }
    }
}