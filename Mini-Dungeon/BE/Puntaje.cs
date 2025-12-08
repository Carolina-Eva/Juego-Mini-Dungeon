
using System.Data;

namespace BE
{
    public class Puntaje
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int Valor { get; set; }
        public DateTime Fecha { get; set; }

        public Puntaje MapeoPuntaje(DataRow row)
        {
            return new Puntaje
            {
                Id = row.Table.Columns.Contains("Id") ? Convert.ToInt32(row["Id"]) : 0,
                UsuarioId = row.Table.Columns.Contains("UsuarioId") ? Convert.ToInt32(row["UsuarioId"]) : 0,
                Valor = row.Table.Columns.Contains("Valor") ? Convert.ToInt32(row["Valor"]) : 0,
                Fecha = row.Table.Columns.Contains("Fecha") ? Convert.ToDateTime(row["Fecha"]) : DateTime.MinValue
            };
        }
    }
}
