using BE;
using BLL;

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

        private Image spriteVacio = Properties.Resources.spriteVacio;
        private Dictionary<Type, Image> _sprites = new Dictionary<Type, Image>
        {
            { typeof(Jugador), Properties.Resources.spriteJugador },
            { typeof(Caja), Properties.Resources.spriteCaja },
            { typeof(Roca), Properties.Resources.spriteRoca },
            { typeof(Meta), Properties.Resources.spritePremio }
        };

        private int nivelActual = 1;
        private int totalNiveles = 3;

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
            MessageBox.Show("¡Nivel completado!");

            nivelActual++;

            if (nivelActual > totalNiveles)
            {
                MessageBox.Show("¡Ganaste todos los niveles!");
                nivelActual = 1;
            }

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

            CargarNivelActual();

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
        // =============   CARGA DE NIVELES    =======================
        // ================================================================

        private void CargarNivelActual()
        {
            string ruta = Path.Combine("Niveles", $"Nivel{nivelActual}.txt");

            if (!File.Exists(ruta))
            {
                MessageBox.Show($"No existe el archivo {ruta}. Reiniciando a nivel 1.");
                nivelActual = 1;
                ruta = Path.Combine("Niveles", "Nivel1.txt");
            }

            CargarNivelDesdeArchivo(ruta);
        }

        private void LimpiarTablero()
        {
            for (int x = 0; x < _tablero.Filas; x++)
                for (int y = 0; y < _tablero.Columnas; y++)
                    _tablero.ColocarElementoEn(x, y, null);
        }

        private void CargarNivelDesdeArchivo(string ruta)
        {
            LimpiarTablero();

            var lineas = File.ReadAllLines(ruta);

            for (int x = 0; x < lineas.Length; x++)
            {
                for (int y = 0; y < lineas[x].Length; y++)
                {
                    char c = lineas[x][y];

                    switch (c)
                    {
                        case '#':
                            _tablero.ColocarElementoEn(x, y, new Roca(x, y));
                            break;

                        case '@':
                            _tablero.ColocarElementoEn(x, y, new Jugador(x, y));
                            break;

                        case '$':
                            _tablero.ColocarElementoEn(x, y, new Caja(x, y));
                            break;

                        case '.':
                            _tablero.ColocarElementoEn(x, y, new Meta(x, y));
                            break;

                        case '*': // TODO Caja sobre meta
                            _tablero.ColocarElementoEn(x, y, new Caja(x, y));
                            break;

                        case '+': // TODO Jugador sobre meta
                            _tablero.ColocarElementoEn(x, y, new Jugador(x, y));
                            break;

                        case ' ':
                        default:
                            // vacío
                            break;
                    }
                }
            }
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

                    if (elemento != null && _sprites.TryGetValue(elemento.GetType(), out var img))
                        _celdas[x, y].Image = img;
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
