using System;
using System.Collections.Generic;

namespace CSPharma_DAL.Modelo
{
    public partial class TdcCatLineasDistribucion
    {
        public TdcCatLineasDistribucion()
        {
            TdcTchEstadoPedidos = new HashSet<TdcTchEstadoPedido>();
        }

        public string MdUuid { get; set; } = null!;
        public DateTime MdDate { get; set; }
        public long Id { get; set; }
        public string CodLinea { get; set; } = null!;
        public string CodProvincia { get; set; } = null!;
        public string CodMunicipio { get; set; } = null!;
        public string CodBarrio { get; set; } = null!;

        public virtual ICollection<TdcTchEstadoPedido> TdcTchEstadoPedidos { get; set; }
    }
}
