using BE;
using Microsoft.Data.SqlClient;


namespace DAL
{
    public class Repository
    {
        AccesoDatos _acceso = new AccesoDatos();
        public async Task<bool> ValidarCrendeciales(string usuario, string hashedpass)
        {
            string sql = $"VALIDAR_USUARIO";
            try
            {
                var parametros = new List<SqlParameter>
                {
                    _acceso.CrearParametro("@Username", usuario),
                    _acceso.CrearParametro("@PasswordHash", hashedpass)
                };
                var result = await _acceso.ObtenerEscalar(sql, parametros);

                return result != null && Convert.ToInt32(result) > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<int> ObtenerUsuarioId(string usuario)
        {
            string sql = "OBTENER_USUARIO_ID";
            var parametros = new List<SqlParameter>
            {
                _acceso.CrearParametro("@Nombre", usuario)
            };
            var result = await _acceso.ObtenerEscalar(sql, parametros);

            return result != null ? Convert.ToInt32(result) : 0;
        }

        public async Task<int> ObtenerPuntajeUsuarioId(int Id)
        {
            string sql = "OBTENER_PUNTAJE";
            var parametros = new List<SqlParameter>
            {
                _acceso.CrearParametro("@UsuarioId", Id)
            };
            var result = await _acceso.ObtenerEscalar(sql, parametros);

            return result != null ? Convert.ToInt32(result) : 0;
        }

        public async Task<Puntaje> GuardarPuntaje(Puntaje puntaje)
        {
            string sql = "GUARDAR_PUNTAJE";
            var parametros = new List<SqlParameter>
            {
                _acceso.CrearParametro("@UsuarioId", puntaje.UsuarioId),
                _acceso.CrearParametro("@Puntaje", puntaje.Valor),
                _acceso.CrearParametro("@Fecha", puntaje.Fecha)
            };
            var table = await _acceso.ObtenerData(sql, parametros);
            Puntaje p = new Puntaje();
            if (table.Rows.Count == 0)
                return null;
            var row = table.Rows[0];
            return p.MapeoPuntaje(row);
        }
    }
}
