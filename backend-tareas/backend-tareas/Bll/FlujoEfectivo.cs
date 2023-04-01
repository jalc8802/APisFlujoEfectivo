using backend_tareas.Context;
using backend_tareas.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend_tareas.Bll
{
    public class FlujoEfectivo
    {
        private readonly AplicationDbContext _context;
        public FlujoEfectivo(AplicationDbContext context)
        {
            _context = context;
        }
        public Dto.FlujoEfectivo.CrearCuenta.Response CrearCuenta(Dto.FlujoEfectivo.CrearCuenta.Request objDatos)
        {
            Dto.FlujoEfectivo.CrearCuenta.Response response = new Dto.FlujoEfectivo.CrearCuenta.Response();
            Models.Cuenta objCuenta = new Models.Cuenta();
            Models.Transaccion objTransaccion = new Models.Transaccion();
            string msgValidation = string.Empty;
            try
            {
                //Validar los datos a ingresar
                if (Validations.DataValidation(objDatos, null, ref msgValidation))
                {
                    response.mensaje = msgValidation;
                    return response;
                }
                //Validar la cuenta
                var cuenta = _context.cuentas.Where(x => x.numeroCuenta == objDatos.numeroCuenta).FirstOrDefault();
                if (cuenta != null)
                {
                    response.correcto = false;
                    response.mensaje = "Número de cuenta ya existe.";
                    return response;
                }
                //Cargar Cuenta
                objCuenta.numeroCuenta = objDatos.numeroCuenta;
                objCuenta.moneda = objDatos.moneda;
                objCuenta.tipoCuenta = objDatos.tipoCuenta;
                objCuenta.situacion = Enumerator.situacion.active.ToString().ToUpper();
                objCuenta.operacion = false;
                objCuenta.registerDate = DateTime.Now;
                //Registrar cuenta
                _context.cuentas.Add(objCuenta);
                _context.SaveChanges();
                //Cargar Transaccion
                objTransaccion.cuentaId = objCuenta.id;
                objTransaccion.registerDate = DateTime.Now;
                objTransaccion.oficina = "La Paz";
                objTransaccion.descripcion = "Abono Cta inicial:" + objDatos.numeroCuenta;
                objTransaccion.debito = string.Empty;
                objTransaccion.abono = objDatos.balanceInicial.ToString();
                objTransaccion.balance = objDatos.balanceInicial;
                objTransaccion.transaccionId = Guid.NewGuid().ToString().Substring(0, 10).ToUpper();
                //Registrar transaccion 
                _context.transacciones.Add(objTransaccion);
                _context.SaveChanges();
                response.correcto = true;
                response.mensaje = "OK";
            }
            catch (Exception ex)
            {
                response.correcto = false;
                response.mensaje = ex.Message;
            }
            return response;
        }
        public Dto.FlujoEfectivo.MovimientoDinero.Response DepositarDinero(Dto.FlujoEfectivo.MovimientoDinero.Request objDatos)
        {
            Dto.FlujoEfectivo.MovimientoDinero.Response response = new Dto.FlujoEfectivo.MovimientoDinero.Response();
            Models.Transaccion objTransaccion = new Models.Transaccion();
            string msgValidation = string.Empty;
            try
            {
                //Validar los datos a ingresar
                if (Validations.DataValidation(objDatos, null, ref msgValidation))
                {
                    response.mensaje = msgValidation;
                    return response;
                }
                //Validar la cuenta
                var objCuenta = _context.cuentas.Where(x => x.numeroCuenta == objDatos.numeroCuenta).FirstOrDefault();
                if (objCuenta == null)
                {
                    response.correcto = false;
                    response.mensaje = "Cuenta inexistente, colocar la cuenta correcta";
                    return response;
                }
                //Validar la moneda 
                if (objCuenta.moneda != objDatos.moneda)
                {
                    response.correcto = false;
                    response.mensaje = "La moneda de la cuenta es diferente, colocar la misma.";
                    return response;
                }
                //Obtener la ultima transaccion
                var listaTransacciones = _context.transacciones.Where(x => x.cuentaId == objCuenta.id).ToList();
                var transaccion = listaTransacciones.OrderByDescending(x => x.registerDate).FirstOrDefault();
                if (transaccion == null)
                {
                    response.correcto = false;
                    response.mensaje = "Transacción inexistente.";
                    return response;
                }
                //Verificar la  cuenta si la situacion = Hold
                if (objCuenta.situacion == Enumerator.situacion.hold.ToString().ToUpper() && objDatos.monto >= double.Parse(transaccion.balance.ToString().Replace("-", "")))
                {
                    //Actualizar la situacion de la cuenta
                    objCuenta.situacion = Enumerator.situacion.active.ToString().ToUpper();
                    //Registrar cuenta
                    _context.cuentas.Update(objCuenta);
                    _context.SaveChanges();
                }

                //Cargar Transaccion
                objTransaccion.cuentaId = objCuenta.id;
                objTransaccion.registerDate = DateTime.Now;
                objTransaccion.oficina = "La Paz";
                objTransaccion.descripcion = "Abono Cuenta: " + objDatos.numeroCuenta;
                objTransaccion.debito = string.Empty;
                objTransaccion.abono = objDatos.monto.ToString();
                objTransaccion.balance = Math.Round(transaccion.balance + objDatos.monto);
                objTransaccion.transaccionId = Guid.NewGuid().ToString().Substring(0, 10).ToUpper();
                //Registrar transaccion 
                _context.transacciones.Add(objTransaccion);
                _context.SaveChanges();
                response.correcto = true;
                response.mensaje = "OK";
            }
            catch (Exception ex)
            {
                response.correcto = false;
                response.mensaje = ex.Message;
            }
            return response;
        }
        public Dto.FlujoEfectivo.MovimientoDinero.Response RetirarDinero(Dto.FlujoEfectivo.MovimientoDinero.Request objDatos)
        {
            Dto.FlujoEfectivo.MovimientoDinero.Response response = new Dto.FlujoEfectivo.MovimientoDinero.Response();
            Models.Transaccion objTransaccion = new Models.Transaccion();
            string msgValidation = string.Empty;
            try
            {
                //Validar los datos a ingresar
                if (Validations.DataValidation(objDatos, null, ref msgValidation))
                {
                    response.mensaje = msgValidation;
                    return response;
                }
                //Validar la cuenta
                var objCuenta = _context.cuentas.Where(x => x.numeroCuenta == objDatos.numeroCuenta).FirstOrDefault();
                if (objCuenta == null)
                {
                    response.correcto = false;
                    response.mensaje = "Cuenta inexistente, colocar la cuenta correcta";
                    return response;
                }
                //Validar la moneda 
                if (objCuenta.moneda != objDatos.moneda)
                {
                    response.correcto = false;
                    response.mensaje = "La moneda de la cuenta es diferente, colocar la misma.";
                    return response;
                }
                //Obtener la ultima transaccion
                var listaTransacciones = _context.transacciones.Where(x => x.cuentaId == objCuenta.id).ToList();
                var transaccion = listaTransacciones.OrderByDescending(x => x.registerDate).FirstOrDefault();
                if (transaccion == null)
                {
                    response.correcto = false;
                    response.mensaje = "Transacción inexistente.";
                    return response;
                }
                //Verificar la  cuenta si la situacion = Hold
                if (objCuenta.situacion == Enumerator.situacion.hold.ToString().ToUpper())
                {
                    response.correcto = false;
                    response.mensaje = "la situación de la cuenta " + objCuenta.numeroCuenta + " es HOLD.";
                    return response;
                }
                //Verificar que el balance no sea negativo
                if (transaccion.balance < 0)
                {
                    response.correcto = false;
                    response.mensaje = "El saldo se encuentra en negativo.";
                    return response;
                }
                //Verificar si el monto a retirar es mayor al monto de la cuenta 
                if (objDatos.monto > transaccion.balance && objCuenta.operacion == false)
                {
                    //Actualizar la situacion de la cuenta
                    objCuenta.situacion = Enumerator.situacion.hold.ToString().ToUpper();
                    objCuenta.operacion = true;
                    //Registrar cuenta
                    _context.cuentas.Update(objCuenta);
                    _context.SaveChanges();
                }

                //Cargar Transaccion
                objTransaccion.cuentaId = objCuenta.id;
                objTransaccion.registerDate = DateTime.Now;
                objTransaccion.oficina = "La Paz";
                objTransaccion.descripcion = "Debito Cuenta: " + objDatos.numeroCuenta;
                objTransaccion.debito = objDatos.monto.ToString();
                objTransaccion.abono = string.Empty;
                objTransaccion.balance = transaccion.balance - objDatos.monto;
                objTransaccion.transaccionId = Guid.NewGuid().ToString().Substring(0, 10).ToUpper();
                //Registrar transaccion 
                _context.transacciones.Add(objTransaccion);
                _context.SaveChanges();
                response.correcto = true;
                response.mensaje = "OK";
            }
            catch (Exception ex)
            {
                response.correcto = false;
                response.mensaje = ex.Message;
            }
            return response;
        }
        public Dto.FlujoEfectivo.HistoricoTransacciones.Response HistoricoTransacciones(Dto.FlujoEfectivo.HistoricoTransacciones.Request objDatos)
        {
            Dto.FlujoEfectivo.HistoricoTransacciones.Response response = new Dto.FlujoEfectivo.HistoricoTransacciones.Response();
            string msgValidation = string.Empty;
            try
            {
                //Validar los datos a ingresar
                if (Validations.DataValidation(objDatos, null, ref msgValidation))
                {
                    response.mensaje = msgValidation;
                    return response;
                }
                //Validar la cuenta
                var objCuenta = _context.cuentas.Where(x => x.numeroCuenta == objDatos.numeroCuenta).FirstOrDefault();
                if (objCuenta == null)
                {
                    response.correcto = false;
                    response.mensaje = "Cuenta inexistente, colocar la cuenta correcta";
                    return response;
                }

                //Obtener la lista de transacciones
                var listaTransacciones = _context.transacciones.Where(x => x.cuentaId == objCuenta.id).ToList().OrderBy(x => x.registerDate);
                if (listaTransacciones == null)
                {
                    response.correcto = false;
                    response.mensaje = "La lista de transacciones se encuentra vacia.";
                    return response;
                }
                //Cargar la lista 
                foreach (var item in listaTransacciones)
                {
                    Models.Historico objHistorico = new Models.Historico();
                    objHistorico.registerDate = item.registerDate;
                    objHistorico.oficina = item.oficina;
                    objHistorico.descripcion = item.descripcion;
                    objHistorico.debito = item.debito;
                    objHistorico.abono = item.abono;
                    objHistorico.balance = item.balance;
                    objHistorico.transaccionId = item.transaccionId;
                    response.listaTransacciones.Add(objHistorico);
                }
                response.correcto = true;
                response.mensaje = "OK";
            }
            catch (Exception ex)
            {
                response.correcto = false;
                response.mensaje = ex.Message;
            }
            return response;
        }
        public Dto.FlujoEfectivo.ConsultarSaldo.Response ConsultarSaldo(Dto.FlujoEfectivo.ConsultarSaldo.Request objDatos)
        {
            Dto.FlujoEfectivo.ConsultarSaldo.Response response = new Dto.FlujoEfectivo.ConsultarSaldo.Response();
            string msgValidation = string.Empty;
            try
            {
                //Validar los datos a ingresar
                if (Validations.DataValidation(objDatos, null, ref msgValidation))
                {
                    response.mensaje = msgValidation;
                    return response;
                }
                //Validar la cuenta
                var objCuenta = _context.cuentas.Where(x => x.numeroCuenta == objDatos.numeroCuenta).FirstOrDefault();
                if (objCuenta == null)
                {
                    response.correcto = false;
                    response.mensaje = "Cuenta inexistente, colocar la cuenta correcta";
                    return response;
                }

                //Obtener la ultima transaccion
                var transaccion = _context.transacciones.Where(x => x.cuentaId == objCuenta.id).ToList().OrderByDescending(x => x.registerDate).FirstOrDefault();
                if (transaccion == null)
                {
                    response.correcto = false;
                    response.mensaje = "Transacción inexistente.";
                    return response;
                }
                //Cargar el objeto de cuenta
                response.cuenta = objCuenta.numeroCuenta;
                response.moneda = objCuenta.moneda;
                response.balance = transaccion.balance;
                response.correcto = true;
                response.mensaje = "OK";
            }
            catch (Exception ex)
            {
                response.correcto = false;
                response.mensaje = ex.Message;
            }
            return response;
        }
    }
}
