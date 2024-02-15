using DieteticaPuchiApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DieteticaPuchiApi.Interfaces
{
    public interface IVentasRepository
    {
        Task<IEnumerable<VentaModel>> GetAllVentas();
        Task<double?> GetTotalVentasHoy();
        Task<double?> GetTotalVentasMes(); 
        Task<double?> GetTotalVentasTransferenciaMes();
        Task<double?> GetTotalVentasMercadoPagoMes();
        Task<double?> GetTotalVentasTresCuotasMes();
        Task<double?> GetTotalVentasDebitoMes();
        Task<double?> GetTotalVentasEfectivoMes();
        Task<double?> GetTotalVentasCuentaCorrienteMes();
        Task<double?> GetTotalVentasUnaCuotaMes();
        Task<double?> GetTotalVentasPedidosYaMes();

        Task<double?> GetTotalVentasTransferenciaHoy();
        Task<double?> GetTotalVentasMercadoPagoHoy();
        Task<double?> GetTotalVentasTresCuotasHoy();
        Task<double?> GetTotalVentasDebitoHoy();
        Task<double?> GetTotalVentasEfectivoHoy();
        Task<double?> GetTotalVentasPedidosYaHoy();
        Task<double?> GetTotalVentasCuentaCorrienteHoy();
        Task<double?> GetTotalVentasUnaCuotaHoy();
        Task<VentaModel> AddVenta(VentaModel venta);
        Task<string> NotificarPago(VentaModel venta);
        Task<VentaModel> UpdateVenta(VentaModel venta);

    }
}
