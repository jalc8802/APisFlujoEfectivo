using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace backend_tareas
{
    public class Enumerator
    {
        public enum situacion
        {
            active = 1,
            hold = 2,
        }
        public enum moneda
        {
            BOB = 1,
            USD = 2,
        }
    }
}
