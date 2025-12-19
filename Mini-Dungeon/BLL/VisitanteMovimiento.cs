using BE;

namespace BLL
{
    public class VisitanteMovimiento : IVisitanteMovimiento
    {
        private readonly Tablero _tablero;
        private readonly Direccion _direccion;
        private bool _movimientoRealizado;
        public event Action NivelCompletado;
        private Jugador _jugadorActual;

        public bool MovimientoRealizado => _movimientoRealizado;

        public VisitanteMovimiento(Tablero tablero, Direccion direccion)
        {
            _tablero = tablero;
            _direccion = direccion;
            _movimientoRealizado = false;
        }

        public void VisitarJugador(Jugador jugador)
        {
            _jugadorActual = jugador;
            var (nuevoX, nuevoY) = CalcularNuevaPosicion(jugador.PosX, jugador.PosY);

            if (!EsPosicionValida(nuevoX, nuevoY))
                return;

            var elementoDestino = _tablero.ObtenerElementoEn(nuevoX, nuevoY);

            if (elementoDestino == null)
            {
                Mover(jugador, nuevoX, nuevoY);
                return;
            }

            elementoDestino.Aceptar(this);
        }

        public void VisitarRoca(Roca roca)
        {
            _movimientoRealizado = false;
        }

        public void VisitarCaja(Caja caja)
        {
            var (nuevoX, nuevoY) = CalcularNuevaPosicion(caja.PosX, caja.PosY);

            if (!EsPosicionValida(nuevoX, nuevoY))
                return;

            var detras = _tablero.ObtenerElementoEn(nuevoX, nuevoY);

            if (detras == null)
            {
                int posCajaX = caja.PosX;
                int posCajaY = caja.PosY;

                Mover(caja, nuevoX, nuevoY);

                if (_jugadorActual != null)
                    Mover(_jugadorActual, posCajaX, posCajaY);
            }
        }

        public void VisitarMeta(Meta meta)
        {
            NivelCompletado?.Invoke();
        }

        private (int x, int y) CalcularNuevaPosicion(int x, int y)
        {
            return _direccion switch
            {
                Direccion.Arriba => (x - 1, y),
                Direccion.Abajo => (x + 1, y),
                Direccion.Izquierda => (x, y - 1),
                Direccion.Derecha => (x, y + 1),
                _ => (x, y)
            };
        }

        private bool EsPosicionValida(int x, int y)
        {
            return x >= 0 && x < _tablero.Filas &&
                   y >= 0 && y < _tablero.Columnas;
        }

        private void Mover(ElementoMovible elemento, int nuevoX, int nuevoY)
        {
            _tablero.ColocarElementoEn(elemento.PosX, elemento.PosY, null);
            _tablero.ColocarElementoEn(nuevoX, nuevoY, elemento);
            _movimientoRealizado = true;
        }

    }
}
