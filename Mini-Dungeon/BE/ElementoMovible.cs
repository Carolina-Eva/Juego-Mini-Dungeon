namespace BE
{
    public abstract class ElementoMovible
    {
        public int PosX { get; set; }
        public int PosY { get; set; }

        protected ElementoMovible(int x, int y)
        {
            PosX = x;
            PosY = y;
        }
        public abstract void Aceptar(IVisitanteMovimiento visitante);
    }
}
