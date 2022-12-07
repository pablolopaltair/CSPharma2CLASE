using System;
using System.Collections.Generic;

namespace CSPharma_DAL.Modelo
{
    public partial class TdcCatEstadosPagoPedido
    {
        public TdcCatEstadosPagoPedido()
        {
            TdcTchEstadoPedidos = new HashSet<TdcTchEstadoPedido>();
        }

        public string MdUuid { get; set; } = null!;
        public DateTime MdDate { get; set; }
        public long Id { get; set; }
        public string CodEstadoPago { get; set; } = null!;
        public string? DesEstadoPago { get; set; }

        public virtual ICollection<TdcTchEstadoPedido> TdcTchEstadoPedidos { get; set; }
    }
}
