namespace SistemaVenta.AplicacionWeb.Models.ViewModels
{
    public class VMSalida
    {
        public int IdSalida { get; set; }
        public string? NumeroSalida { get; set; }
        public int? IdTipoDocumentoOC { get; set; }
        public string? TipoDocumentoOC { get; set; }
        public int? IdUsuario { get; set; }
        public string? Usuario { get; set; }
        public string? DocumentoCliente { get; set; }
        public string? NombreCliente { get; set; }
        public string? SubTotal { get; set; }
        public string? ImpuestoTotal { get; set; }
        public string? Total { get; set; }
        public string? FechaRegistro { get; set; }
        public virtual ICollection<VMDetalleSalida> DetalleSalida { get; set; }
    }
}
