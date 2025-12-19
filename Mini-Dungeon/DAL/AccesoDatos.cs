using Microsoft.Data.SqlClient;
using System.Data;

namespace DAL
{
    internal class AccesoDatos
    {
        private readonly string? _connectionString;

        internal AccesoDatos()
        {
            _connectionString = @"Data Source=DESKTOP-CLR13LB\SQLEXPRESS; Initial Catalog=Sokoban; Integrated Security=SSPI; TrustServerCertificate=True;";
        }

        private async Task<SqlConnection> CrearConexion()
        {
            var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }

        private SqlCommand CrearComando(string sql, SqlConnection connection, List<SqlParameter> parameters = null)
        {
            var cmd = new SqlCommand(sql, connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            if (parameters != null && parameters.Count > 0)
                cmd.Parameters.AddRange(parameters.ToArray());

            return cmd;
        }
        public async Task<DataTable> ObtenerData(string sql, List<SqlParameter> parameters = null)
        {
            try
            {
                var dataTable = new DataTable();

                using (var connection = await CrearConexion())
                using (var cmd = CrearComando(sql, connection, parameters))
                using (var reader = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                {
                    dataTable.Load(reader);
                }

                return dataTable;
            }
            catch (SqlException ex)
            {
                return null;
            }
        }

        public async Task<int> Escribir(string sql, List<SqlParameter> parameters = null)
        {
            using (var connection = await CrearConexion())
            using (var cmd = CrearComando(sql, connection, parameters))
            {
                try
                {
                    return await cmd.ExecuteNonQueryAsync();
                }
                catch (SqlException ex)
                {
                    return -1;
                }
            }
        }

        public async Task<(int Codigo, string Mensaje)> EscribirConMensajes(string sql, List<SqlParameter> parameters = null)
        {
            using (var connection = await CrearConexion())
            using (var cmd = CrearComando(sql, connection, parameters))
            {
                try
                {
                    int result = await cmd.ExecuteNonQueryAsync();
                    return (result, "Elemento eliminado correctamente");
                }
                catch (SqlException ex)
                {
                    int result = -1;
                    return (result, ex.Message);
                }
            }
        }



        public async Task<int> ObtenerEscalar(string sql, List<SqlParameter> parameters = null)
        {
            using (var connection = await CrearConexion())
            using (var cmd = CrearComando(sql, connection, parameters))
            {
                try
                {
                    var result = await cmd.ExecuteScalarAsync();
                    return result != null ? Convert.ToInt32(result) : -1;
                }
                catch (SqlException ex)
                {
                    return -1;
                }
            }
        }

        public SqlParameter CrearParametro<T>(string name, T value)
        {
            var parameter = new SqlParameter
            {
                ParameterName = name,
                Value = value ?? (object)DBNull.Value
            };
            return parameter;
        }

    }
}
