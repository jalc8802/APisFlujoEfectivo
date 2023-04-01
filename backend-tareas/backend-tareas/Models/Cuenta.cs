using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend_tareas.Models
{
    public class Cuenta
    {
        public int id { get; set; }
        [Required]
        public long numeroCuenta { get; set; }
        [Required]
        public string moneda { get; set; }
        [Required]
        public string tipoCuenta { get; set; }
        [Required]
        public string situacion { get; set; }
        [Required]
        public DateTime registerDate { get; set; }
        public bool operacion { get; set; }

    }
}
