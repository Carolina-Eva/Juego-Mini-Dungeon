namespace BE
{
    //ObjectStructure
    public class Tablero
    {
        public ElementoMovible[,] Grilla { get; private set; }

        public int Filas => Grilla.GetLength(0);
        public int Columnas => Grilla.GetLength(1);

        public Tablero(int filas, int columnas)
        {
            Grilla = new ElementoMovible[filas, columnas];
        }

        public ElementoMovible ObtenerElementoEn(int x, int y)
        {
            return Grilla[x, y];
        }

        public void ColocarElementoEn(int x, int y, ElementoMovible elemento)
        {
            Grilla[x, y] = elemento;
            if (elemento != null)
            {
                elemento.PosX = x;
                elemento.PosY = y;
            }
        }
    }
}
