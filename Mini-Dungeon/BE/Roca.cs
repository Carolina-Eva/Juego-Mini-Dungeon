namespace BE
{
    public class Roca : ElementoMovible
    {
        public Roca(int x, int y) : base(x, y)
        {
        }
        public override void Aceptar(IVisitanteMovimiento visitante)
        {
            visitante.Visitar(this);
        }
    }
}
