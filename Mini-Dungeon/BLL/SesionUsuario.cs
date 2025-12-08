using System.ComponentModel;
using BE;

namespace BLL
{
    public class SesionUsuario
    {
        private static readonly object _lock = new object();
        private static SesionUsuario _instance;
        public static SesionUsuario GetInstance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null) _instance = new SesionUsuario();
                    return _instance;
                }
            }
        }
        public Usuario Usuario { get; private set; }

        private SesionUsuario() { }
        public void Login(Usuario usuario)
        {
            _instance.Usuario = usuario;
        }
        public void Logout()
        {
            _instance = null;
        }

    }
}
