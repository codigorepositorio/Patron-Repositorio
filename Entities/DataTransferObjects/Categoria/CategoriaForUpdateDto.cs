﻿
namespace Entities.DataTransferObjects
{
   public class CategoriaForUpdateDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Condicion { get; set; }
        //public IEnumerable<ArticuloForUpdateDto> Articulos { get; set; }        
    }
}
