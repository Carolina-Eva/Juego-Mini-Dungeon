namespace BE
{
    public interface IVisitanteMovimiento
    {
        void Visitar(Jugador jugador);
        void Visitar(Roca roca);
        void Visitar(Caja caja);
        void Visitar(Meta meta);
    }
}
