namespace BE
{
    public class Jugador : ElementoMovible
    {
        public Jugador(int x, int y) : base(x, y)
        {
        }
        public override void Aceptar(IVisitanteMovimiento visitante)
        {
            visitante.Visitar(this);
        }
    }
}
