﻿using System;
using System.Collections.Generic;
using SistemaVenta.Entity.Models;

namespace SistemaVenta.Entity.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Venta = new HashSet<Venta>();
        }

        public int IdUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public int? IdRol { get; set; }
        public string? UrlFoto { get; set; }
        public string? NombreFoto { get; set; }
        public string? Clave { get; set; }
        public bool? EsActivo { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public virtual Rol? IdRolNavigation { get; set; }
        public virtual ICollection<Venta> Venta { get; set; }
        public virtual ICollection<Salida> Salida { get; set; }
    }
}
