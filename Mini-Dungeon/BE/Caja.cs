namespace BE
{
    public class Caja : ElementoMovible
    {
        public Caja(int x, int y) : base(x, y)
        {
        }
        public override void Aceptar(IVisitanteMovimiento visitante)
        {
            visitante.VisitarCaja(this);
        }
    }
}
