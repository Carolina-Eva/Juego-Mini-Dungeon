namespace BE
{
    public interface IVisitanteMovimiento
    {
        void VisitarJugador(Jugador jugador);
        void VisitarRoca(Roca roca);
        void VisitarCaja(Caja caja);
        void VisitarMeta(Meta meta);
    }
}
