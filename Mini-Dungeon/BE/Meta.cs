namespace BE
{
    public class Meta : ElementoMovible
    {
        public Meta(int x, int y) : base(x, y)
        {
        }
        public override void Aceptar(IVisitanteMovimiento visitante)
        {
            visitante.VisitarMeta(this);
        }
    }
}
