using backend_tareas.Models;
using System.ComponentModel.DataAnnotations;

namespace backend_tareas.Dto
{
    public class FlujoEfectivo
    {
        public class CrearCuenta
        {
            public class Request
            {
                public long numeroCuenta { get; set; }
                public string moneda { get; set; }
                public string tipoCuenta { get; set; }
                public double balanceInicial { get; set; }

                public Request()
                { }
                public Request(long numeroCuenta, string moneda, string tipoCuenta, double balanceInicial)
                {
                    this.numeroCuenta = numeroCuenta;
                    this.moneda = moneda;
                    this.tipoCuenta = tipoCuenta;
                    this.balanceInicial = balanceInicial;
                }
            }
            public class Response : BaseResponse
            {
            }
        }
        public class MovimientoDinero
        {
            public class Request
            {
                public long numeroCuenta { get; set; }
                public string moneda { get; set; }
                public double monto { get; set; }
                public Request()
                { }
                public Request(long numeroCuenta, string moneda, double monto)
                {
                    this.numeroCuenta = numeroCuenta;
                    this.moneda = moneda;
                    this.monto = monto;
                }
            }
            public class Response : BaseResponse
            {
            }
        }
        public class ConsultarSaldo
        {
            public class Request
            {
                public long numeroCuenta { get; set; }
                public Request()
                { }
                public Request(long numeroCuenta)
                {
                    this.numeroCuenta = numeroCuenta;
                }
            }
            public class Response : BaseResponse
            {
                public long cuenta { get; set; }
                public string moneda { get; set; }
                public double balance { get; set; }
            }
        }
        public class HistoricoTransacciones
        {
            public class Request
            {
                public long numeroCuenta { get; set; }
                public Request()
                { }
                public Request(long numeroCuenta)
                {
                    this.numeroCuenta = numeroCuenta;
                }
            }
            public class Response : BaseResponse
            {
               public List<Historico> listaTransacciones { get; set; } = new List<Historico>();
            }
        }
    }
}
