using BE;
using BLL;
using DAL;
using Microsoft.VisualBasic.Logging;

namespace UI
{
    public partial class Juego : Form
    {
        private readonly ServicioPuntaje _servicioPuntaje;
        private readonly Usuario _usuarioActual;

        private PictureBox[,] _celdas;
        private const int TileSize = 50;

        private Tablero _tablero;
        private ServicioTablero _servicioMovimiento;

        // Sprites
        private Image spriteJugador = Properties.Resources.spriteJugador;
        private Image spriteCaja = Properties.Resources.spriteCaja;
        private Image spriteRoca = Properties.Resources.spriteRoca;
        private Image spriteVacio = Properties.Resources.spriteVacio;
        private Image spriteMeta = Properties.Resources.spritePremio;

        public Juego()
        {
            InitializeComponent();

            _usuarioActual = SesionUsuario.GetInstance.Usuario;
            lblUsuario.Text = $"Bienvenido: {_usuarioActual.Nombre}";
            _servicioPuntaje = new ServicioPuntaje();

            InicializarJuego();
        }

        private async void OnNivelCompletado()
        {
            MessageBox.Show("¡Felicitaciones! Completaste el nivel 🎉",
                            "Nivel Completado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
            await _servicioPuntaje.RegistrarVictoria(_usuarioActual.Id);
            
            InicializarJuego();
        }

        private async Task ObtenerPuntos()
        {
            int puntos = await _servicioPuntaje.ObtenerPuntajeUsuarioId(_usuarioActual.Id);
            lblPuntos.Text = $"Puntos: {puntos}";
        }

        private async Task InicializarJuego()
        {
            await ObtenerPuntos();
            // 1. Desuscribirse del servicio antiguo si existe
            if (_servicioMovimiento != null)
                _servicioMovimiento.NivelCompletado -= OnNivelCompletado;

            // 2. Limpiar grilla visual
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.ColumnStyles.Clear();

            // Crear tablero 8x12
            _tablero = new Tablero(8, 12);
            _servicioMovimiento = new ServicioTablero(_tablero);

            _servicioMovimiento.NivelCompletado += OnNivelCompletado;

            CrearGrillaVisual();
            CargarNivelDemo();
            RenderizarTablero();

            this.Focus();
        }

        // ================================================================
        // ===============   CREACIÓN DE GRILLA VISUAL    =================
        // ================================================================

        private void CrearGrillaVisual()
        {
            int filas = _tablero.Filas;
            int columnas = _tablero.Columnas;

            tableLayoutPanel1.RowCount = filas;
            tableLayoutPanel1.ColumnCount = columnas;

            tableLayoutPanel1.Width = columnas * TileSize;
            tableLayoutPanel1.Height = filas * TileSize;

            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.ColumnStyles.Clear();

            _celdas = new PictureBox[filas, columnas];

            for (int x = 0; x < filas; x++)
            {
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, TileSize));

                for (int y = 0; y < columnas; y++)
                {
                    tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, TileSize));

                    PictureBox pb = new PictureBox();
                    pb.Width = TileSize;
                    pb.Height = TileSize;
                    pb.BackColor = Color.Black;
                    pb.Margin = new Padding(0);
                    pb.Padding = new Padding(0);
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;

                    _celdas[x, y] = pb;
                    tableLayoutPanel1.Controls.Add(pb, y, x);
                }
            }
        }

        // ================================================================
        // =============   NIVEL DE DEMOSTRACIÓN    =======================
        // ================================================================

        private void CargarNivelDemo()
        {
            // LIMPIAR TABLERO
            for (int x = 0; x < _tablero.Filas; x++)
                for (int y = 0; y < _tablero.Columnas; y++)
                    _tablero.ColocarElementoEn(x, y, null);

            // ======================================================
            // PAREDES EXTERNAS
            // ======================================================

            for (int y = 0; y < 12; y++)
            {
                _tablero.ColocarElementoEn(0, y, new Roca(0, y));
                _tablero.ColocarElementoEn(7, y, new Roca(7, y));
            }

            for (int x = 0; x < 8; x++)
            {
                _tablero.ColocarElementoEn(x, 0, new Roca(x, 0));
                _tablero.ColocarElementoEn(x, 11, new Roca(x, 11));
            }

            // ======================================================
            // LABERINTO INTERNO
            // ======================================================

            // Línea de paredes centro superior
            for (int y = 3; y <= 7; y++)
                _tablero.ColocarElementoEn(2, y, new Roca(2, y));

            // Bloque central
            _tablero.ColocarElementoEn(3, 3, new Roca(3, 3));
            _tablero.ColocarElementoEn(3, 4, new Roca(3, 4));
            _tablero.ColocarElementoEn(4, 4, new Roca(4, 4));

            // Pared lateral derecha
            _tablero.ColocarElementoEn(1, 8, new Roca(1, 8));
            _tablero.ColocarElementoEn(2, 8, new Roca(2, 8));
            _tablero.ColocarElementoEn(3, 8, new Roca(3, 8));

            // ======================================================
            // CAJAS (obstáculos reales)
            // ======================================================

            _tablero.ColocarElementoEn(4, 2, new Caja(4, 2));
            _tablero.ColocarElementoEn(5, 5, new Caja(5, 5));
            _tablero.ColocarElementoEn(3, 7, new Caja(3, 7));

            // Una caja tapa parte del camino
            _tablero.ColocarElementoEn(6, 8, new Caja(6, 6));
            _tablero.ColocarElementoEn(5, 8, new Caja(5, 8));
            _tablero.ColocarElementoEn(4, 8, new Caja(4, 8));

            // ======================================================
            // JUGADOR
            // ======================================================

            _tablero.ColocarElementoEn(1, 1, new Jugador(1, 1));

            // ======================================================
            // META (salida)
            // ======================================================

            _tablero.ColocarElementoEn(6, 10, new Meta(6, 10));
        }


        // ================================================================
        // ===============   RENDERIZAR TABLERO    ========================
        // ================================================================

        private void RenderizarTablero()
        {
            for (int x = 0; x < _tablero.Filas; x++)
            {
                for (int y = 0; y < _tablero.Columnas; y++)
                {
                    var elemento = _tablero.Grilla[x, y];

                    if (elemento is Jugador)
                        _celdas[x, y].Image = spriteJugador;
                    else if (elemento is Caja)
                        _celdas[x, y].Image = spriteCaja;
                    else if (elemento is Roca)
                        _celdas[x, y].Image = spriteRoca;
                    else if (elemento is Meta)
                        _celdas[x, y].Image = spriteMeta;
                    else
                        _celdas[x, y].Image = spriteVacio;
                }
            }
        }

        // ================================================================
        // ===============   CAPTURA DE TECLADO    ========================
        // ================================================================

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool movio = false;

            switch (keyData)
            {
                case Keys.Up:
                    movio = _servicioMovimiento.MoverJugador(Direccion.Arriba);
                    break;

                case Keys.Down:
                    movio = _servicioMovimiento.MoverJugador(Direccion.Abajo);
                    break;

                case Keys.Left:
                    movio = _servicioMovimiento.MoverJugador(Direccion.Izquierda);
                    break;

                case Keys.Right:
                    movio = _servicioMovimiento.MoverJugador(Direccion.Derecha);
                    break;
            }

            if (movio)
                RenderizarTablero();

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            InicializarJuego();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            SesionUsuario.GetInstance.Logout();

            this.Hide();
            var loginForm = new LogIn();
            loginForm.Show();

            this.Close();
        }
    }
}
