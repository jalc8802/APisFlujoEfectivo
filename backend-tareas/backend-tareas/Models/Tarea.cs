using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend_tareas.Models
{
  public class Tarea
  {
    public int id { get; set; }
    [Required]
    [Column(TypeName ="varchar(100)")]
    public string nombre { get; set; }
    [Required]
    public bool estado { get; set; }
  }
}
