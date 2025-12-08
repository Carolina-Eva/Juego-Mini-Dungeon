using BE;

namespace BLL
{
    public class VisitanteMovimiento : IVisitanteMovimiento
    {
        private readonly Tablero _tablero;
        private readonly Direccion _direccion;
        private bool _movimientoRealizado;
        public event Action NivelCompletado;

        public bool MovimientoRealizado => _movimientoRealizado;

        public VisitanteMovimiento(Tablero tablero, Direccion direccion)
        {
            _tablero = tablero;
            _direccion = direccion;
            _movimientoRealizado = false;
        }

        // ============================================
        // VISITAR JUGADOR
        // ============================================

        public void VisitarJugador(Jugador jugador)
        {
            var (nuevoX, nuevoY) = CalcularNuevaPosicion(jugador.PosX, jugador.PosY);

            // Validación de límites
            if (!EsPosicionValida(nuevoX, nuevoY))
                return; // Movimiento inválido

            var elementoDestino = _tablero.ObtenerElementoEn(nuevoX, nuevoY);

            if (elementoDestino == null)
            {
                Mover(jugador, nuevoX, nuevoY);
                return;
            }

            // Se aplica doble despacho
            elementoDestino.Aceptar(this);
        }

        // ============================================
        // VISITAR ROCA
        // ============================================

        public void VisitarRoca(Roca roca)
        {
            // La roca impide el movimiento
            _movimientoRealizado = false;
        }

        // ============================================
        // VISITAR CAJA
        // ============================================

        public void VisitarCaja(Caja caja)
        {
            // Calcular la posición detrás de la caja
            var (nuevoX, nuevoY) = CalcularNuevaPosicion(caja.PosX, caja.PosY);

            // Chequear límites: la caja NO puede salir del tablero
            if (!EsPosicionValida(nuevoX, nuevoY))
                return;

            var elementoDetras = _tablero.ObtenerElementoEn(nuevoX, nuevoY);

            // Se puede empujar si la celda detrás está vacía
            if (elementoDetras == null)
            {
                // Guardar antigua posición de la caja (donde quedará el jugador)
                int posJugadorX = caja.PosX;
                int posJugadorY = caja.PosY;

                // Mover la caja
                Mover(caja, nuevoX, nuevoY);

                // Ahora mover el jugador a la antigua posición de la caja
                var (jugadorX, jugadorY) = CalcularNuevaPosicion(posJugadorX, posJugadorY);
                jugadorX = posJugadorX;
                jugadorY = posJugadorY;

                var jugador = _tablero.ObtenerElementoEn(jugadorX, jugadorY) as Jugador;

                if (jugador != null)
                    Mover(jugador, posJugadorX, posJugadorY);
            }

        }
        public void VisitarMeta(Meta meta)
        {
            // El jugador alcanzó la meta
            NivelCompletado?.Invoke();
        }


        // ============================================
        // FUNCIONES AUXILIARES
        // ============================================

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
