namespace backend_tareas.Models
{
    public class Historico
    {
        public DateTime registerDate { get; set; }
        public string oficina { get; set; }
        public string descripcion { get; set; }
        public string debito { get; set; }
        public string abono { get; set; }
        public double balance { get; set; }
        public string transaccionId { get; set; }
    }
}
