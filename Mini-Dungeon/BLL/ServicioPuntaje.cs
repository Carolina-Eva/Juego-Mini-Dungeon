
using BE;
using DAL;

namespace BLL
{
    public class ServicioPuntaje
    {
        private readonly Repository _repo = new Repository();

        public ServicioPuntaje()
        {

        }

        public async Task<int> ObtenerPuntajeUsuarioId(int usuarioId)
        {
            return await _repo.ObtenerPuntajeUsuarioId(usuarioId);
        }

        public async Task RegistrarVictoria(int usuarioId)
        {
            Puntaje p = new Puntaje
            {
                UsuarioId = usuarioId,
                Valor = 1,                    
                Fecha = DateTime.Now
            };

            await _repo.GuardarPuntaje(p);
        }
    }
}
