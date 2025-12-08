using BE;

namespace BLL
{
    public class ServicioTablero
    {
        private readonly Tablero _tablero;
        public event Action NivelCompletado;

        public ServicioTablero(Tablero tablero)
        {
            _tablero = tablero;
        }

        public bool MoverJugador(Direccion direccion)
        {
            Jugador jugador = BuscarJugador();

            if (jugador == null)
                return false;

            var visitante = new VisitanteMovimiento(_tablero, direccion);

            visitante.NivelCompletado += () => NivelCompletado?.Invoke();
            jugador.Aceptar(visitante);
            return visitante.MovimientoRealizado;
        }


        private Jugador BuscarJugador()
        {
            for (int x = 0; x < _tablero.Filas; x++)
            {
                for (int y = 0; y < _tablero.Columnas; y++)
                {
                    if (_tablero.Grilla[x, y] is Jugador jugador)
                        return jugador;
                }
            }

            return null;
        }

    }
}
