//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AplicaciónTrimestre.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public usuario()
        {
            this.compra = new HashSet<compra>();
            this.usuariorol = new HashSet<usuariorol>();
        }
    
        public int id { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [StringLength(10, ErrorMessage = "Maximo 10 caracteres")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        public string apellido { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [DataType(DataType.Date)]
        public System.DateTime fecha_nacimiento { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        public string password { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<compra> compra { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<usuariorol> usuariorol { get; set; }
    }
}
