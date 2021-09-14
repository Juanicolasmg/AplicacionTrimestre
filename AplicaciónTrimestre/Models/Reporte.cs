using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicaciónTrimestre.Models
{
    public class Reporte
    {
        public String nombreProveedor { get; set; }
        public String direccionProveedor { get; set; }
        public String telefonoProveedor { get; set; }
        public String nombreProducto { get; set; }
        public int precioProducto { get; set; }
    }
}