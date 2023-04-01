using backend_tareas.Context;
using backend_tareas.Models;
using backend_tareas.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_tareas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlujoEfectivoController : ControllerBase
    {
        private readonly Bll.FlujoEfectivo _flujoEfectivo;
        public FlujoEfectivoController(Bll.FlujoEfectivo flujoEfectivo)
        {
            _flujoEfectivo = flujoEfectivo;
        }
        [HttpPost("CrearCuenta")]
        public async Task<Dto.FlujoEfectivo.CrearCuenta.Response> CrearCuenta([FromBody] Dto.FlujoEfectivo.CrearCuenta.Request objDatos)
        {
            Dto.FlujoEfectivo.CrearCuenta.Response response = new Dto.FlujoEfectivo.CrearCuenta.Response();
            response = _flujoEfectivo.CrearCuenta(objDatos);
            return response;
        }
        [HttpPost("DepositarDinero")]
        public async Task<Dto.FlujoEfectivo.MovimientoDinero.Response> DepositarDinero([FromBody] Dto.FlujoEfectivo.MovimientoDinero.Request objDatos)
        {
            Dto.FlujoEfectivo.MovimientoDinero.Response response = new Dto.FlujoEfectivo.MovimientoDinero.Response();
            response = _flujoEfectivo.DepositarDinero(objDatos);
            return response;
        }
        [HttpPost("RetirarDinero")]
        public async Task<Dto.FlujoEfectivo.MovimientoDinero.Response> RetirarDinero([FromBody] Dto.FlujoEfectivo.MovimientoDinero.Request objDatos)
        {
            Dto.FlujoEfectivo.MovimientoDinero.Response response = new Dto.FlujoEfectivo.MovimientoDinero.Response();
            response = _flujoEfectivo.RetirarDinero(objDatos);
            return response;
        }
        [HttpPost("HistoricoTransacciones")]
        public async Task<Dto.FlujoEfectivo.HistoricoTransacciones.Response> HistoricoTransacciones([FromBody] Dto.FlujoEfectivo.HistoricoTransacciones.Request objDatos)
        {
            Dto.FlujoEfectivo.HistoricoTransacciones.Response response = new Dto.FlujoEfectivo.HistoricoTransacciones.Response();
            response = _flujoEfectivo.HistoricoTransacciones(objDatos);
            return response;
        }
        [HttpPost("ConsultarSaldo")]
        public async Task<Dto.FlujoEfectivo.ConsultarSaldo.Response> ConsultarSaldo([FromBody] Dto.FlujoEfectivo.ConsultarSaldo.Request objDatos)
        {
            Dto.FlujoEfectivo.ConsultarSaldo.Response response = new Dto.FlujoEfectivo.ConsultarSaldo.Response();
            response = _flujoEfectivo.ConsultarSaldo(objDatos);
            return response;
        }
    }
}
