﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class ArticuloForCreationDto
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Stock { get; set; }
        public string Descripcion { get; set; }
        public bool Condicion { get; set; }  
        
    }
}