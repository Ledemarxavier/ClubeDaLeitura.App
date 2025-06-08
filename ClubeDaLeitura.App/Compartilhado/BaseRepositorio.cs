namespace ClubeDaLeitura.App.Compartilhado
{
    public class BaseRepositorio
    {
        public List<EntidadeBase> registros = new List<EntidadeBase>();
        private int contadorRegistros = 0;

        public void CadastrarRegistro(EntidadeBase novoRegistro)
        {
            contadorRegistros++;
            novoRegistro.id = contadorRegistros;

            registros.Add(novoRegistro);
        }

        public bool EditarRegistro(int idSelecionado, EntidadeBase registroAtualizado)
        {
            EntidadeBase registroSelecionado = SelecionarRegistroPorId(idSelecionado);

            if (registroSelecionado == null)
                return false;

            registroSelecionado.AtualizarRegistro(registroAtualizado);

            return true;
        }

        public bool ExcluirRegistro(int id)
        {
            var registro = SelecionarRegistroPorId(id);
            if (registro == null)
                return false;

            return registros.Remove(registro);
        }

        public List<EntidadeBase> SelecionarRegistros()
        {
            return registros;
        }

        public EntidadeBase SelecionarRegistroPorId(int idSelecionado)
        {
            foreach (var r in registros)
            {
                if (r.id == idSelecionado)
                    return r;
            }
            return null;
        }
    }
}