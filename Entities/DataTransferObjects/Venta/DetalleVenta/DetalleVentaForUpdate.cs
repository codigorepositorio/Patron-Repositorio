﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.Venta.DetalleVenta
{
   public class DetalleVentaForUpdate
    {
        public int DetalleVentaId { get; set; }
        public int VentaId { get; set; }    
        public int ArticuloId { get; set; }
        public string Articulo { get; set; }
        public int Cantidad { get; set; }        
        public decimal PrecioVenta { get; set; }        
        public decimal Descuento { get; set; }        
    }
}
