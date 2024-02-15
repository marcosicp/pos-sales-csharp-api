using DieteticaPuchiApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DieteticaPuchiApi.Interfaces
{
    public interface IMovimientosRepository
    {
        Task<IEnumerable<MovimientoModel>> GetAllMovimientos();
        Task<MovimientoModel> GetCierreCajaCalculo();
        bool AbrirCaja(MovimientoModel movimiento);
        bool CerrarCaja(MovimientoModel movimiento);
        bool AddMovimiento(MovimientoModel movimiento);
        double? IngresosCaja();
        double? EgresosCaja();
        double? AperturaInicialCaja();
        KeyValuePair<int, double> VentasDelDia();
        IEnumerable<bool> EstadoCaja();
    }
}
