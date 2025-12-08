using BE;
using BLL;

namespace UI
{
    public partial class LogIn : Form
    {
        LoginManager _loginManager = new LoginManager();
        ServicioHash _servicioHash = new ServicioHash();

        public LogIn()
        {
            InitializeComponent();
        }

        private async void btnLogIn_Click(object sender, EventArgs e)
        {
            string usuario = tbUsuario.Text.Trim();
            string password = tbPassword.Text.Trim();

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show(
                    "Ingrese Usuario y Contraseña para ingresar",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            try
            {
                var hashedPass = _servicioHash.CalculateHash(password);
                var result = await _loginManager.Login(usuario, hashedPass);
                if (result)
                {
                    var userId = await _loginManager.ObtenerUsuarioId(usuario);

                    Usuario user = new Usuario(userId, usuario);
                    SesionUsuario.GetInstance.Login(user);

                    MessageBox.Show(
                        "¡Inicio de sesión exitoso!",
                        "Éxito",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    Juego juegoForm = new Juego();
                    juegoForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show(
                        $"Error al intentar iniciar sesión, Usuario y contraseña incorrectos",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al intentar iniciar sesión: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

        }
    }
}
